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
    public class InvoiceController : ApiBaseController
    {
        private readonly IGenericRepository<Invoice> _repository;
        private readonly IMapper _mapper;

        public InvoiceController(IGenericRepository<Invoice> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InvoiceDto>>> GetInvoices()
        {
            var invoice = await _repository.GetAllAsync();
            if (invoice is null) return NotFound(new ApiResponse(404));
            var MappedInvocies = _mapper.Map<IReadOnlyList<Invoice>, IReadOnlyList<InvoiceDto>>(invoice);
            return Ok(MappedInvocies);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("id")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            if (invoice is null) return NotFound(new ApiResponse(404));
            var mapedinvoice = _mapper.Map<Invoice, InvoiceDto>(invoice);
            return Ok(mapedinvoice);
        }
    }
}
