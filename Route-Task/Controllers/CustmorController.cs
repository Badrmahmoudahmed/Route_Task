using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Repository.Contract;
using OrederManagement.Core.Specification;
using Route_Task.ErrorHandler;
using Route_Task.Helpers;

namespace Route_Task.Controllers
{
    public class CustmorController : ApiBaseController
    {
        private readonly IGenericRepository<Custmor> _repository;
        private readonly IMapper _mapper;

        public CustmorController(IGenericRepository<Custmor> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Custmor>> CreateCustmor(CustmorToReturnDto AddedCustmor)
        {
            var MappedCustmor = _mapper.Map<CustmorToReturnDto, Custmor>(AddedCustmor);
            var result = await _repository.AddAsync(MappedCustmor);

            if (!(result > 0)) return BadRequest(new ApiResponse(400));

            return Ok(MappedCustmor);
        }

        [Authorize]
        [HttpGet("{id}/Orders")]
        public async Task<ActionResult<OrderToReturnDto>> GetCustmorWithAllOrders(int id)
        {
            var spec = new CustmorWithOrderSpec(id);
            var custmor = await _repository.GetByIdAsyncWithSpec(spec);

            if(custmor is null) return NotFound(new ApiResponse(404));

            var order = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(custmor.Orders);
            return Ok(order);
        }
    }
}
