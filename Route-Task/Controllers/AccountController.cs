using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Repository.Contract;
using OrederManagement.Core.Services.Contract;
using OrederManagement.Core.Specification;
using Route_Task.ErrorHandler;
using Route_Task.Helpers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Route_Task.Controllers
{
 
    public class AccountController : ApiBaseController
    {
        private readonly ITokenService _tokenService;
        private readonly IGenericRepository<User> _repository;

        public AccountController(ITokenService tokenService,IGenericRepository<User> repository)
        {
            _tokenService = tokenService;
            _repository = repository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var checkuser = await CheckUserExist(model.Username);

            if (checkuser.Value == true)
                return BadRequest(new ApiResponse(401, "User Already Exist"));

            var hmac = new HMACSHA512();
            var CreatedUser = new User()
            {
                Username = model.Username,
                Role = model.Role,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.PasswordHash)),
                PasswordSalt = hmac.Key
            };

            var result = await _repository.AddAsync(CreatedUser);
            if (!(result > 0)) return BadRequest(new ApiResponse(400));

            var usertoreturn = new UserDto()
            {
                Username = model.Username,
                Role = model.Role,
                Token = await _tokenService.CreateTokenAsync(CreatedUser)
            };

            return Ok(usertoreturn);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var spec = new LoginSpecification(model.Username);
            var user = await _repository.GetByIdAsyncWithSpec(spec);

            if (user is null) return Unauthorized(new ApiResponse(401));


            var passhashed = new HMACSHA512(user.PasswordSalt).ComputeHash(Encoding.UTF8.GetBytes(model.PasswordHash));

            for (int i = 0; i < passhashed.Length ; i++)
            {
                if (passhashed[i] != user.PasswordHash[i])
                    return Unauthorized(new ApiResponse(401));
            }

            var usertoreturn = new UserDto()
            {
                Username = model.Username,
                Token = await _tokenService.CreateTokenAsync(user)
            };

            return Ok(usertoreturn);
        }

        [HttpGet]
        public async Task<ActionResult<bool>> CheckUserExist(string username)
            => await _repository.GetByIdAsyncWithSpec(new LoginSpecification(username)) is not null ? true : false; 

         

    }
}
