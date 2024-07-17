using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Repository.Contract;
using Route_Task.ErrorHandler;
using Route_Task.Helpers;

namespace Route_Task.Controllers
{

    public class ProductController : ApiBaseController
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReurnDto>>> GetProducts()
        {
            var products = await _repository.GetAllAsync();
            if (products is null) return NotFound(new ApiResponse(404));

            var mappedproduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReurnDto>>(products);
            return Ok(mappedproduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReurnDto>> GetProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null) return NotFound(new ApiResponse(404));

            var MappedProduct = _mapper.Map<Product, ProductToReurnDto>(product);
            return Ok(MappedProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductToReurnDto Addedproduct)
        {
            var product = new Product()
            {
                Name = Addedproduct.Name,
                Price = Addedproduct.Price,
                Stock = Addedproduct.Stock,
            };
            var result = await _repository.AddAsync(product);
            if (!(result > 0)) return BadRequest(new ApiResponse(400));
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(int id,ProductToReurnDto UpdatedProduct)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product is null) return BadRequest(new ApiResponse(400));

            product.Name = UpdatedProduct.Name;
            product.Price = UpdatedProduct.Price;
            product.Stock = UpdatedProduct.Stock;

            var result = await _repository.UpdateAsync(product);

            if (!(result > 0)) return BadRequest(new ApiResponse(400));

            return Ok(product);
        }
    }
}
