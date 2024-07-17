using OrderManagement.Core.Services.Contract;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangement.Services
{
    public class InvoiceService : IInvoice
    {
        private readonly IGenericRepository<Invoice> _repository;

        public InvoiceService(IGenericRepository<Invoice> repository)
        {
            _repository = repository;
        }
        public async Task<Invoice> CreateInvoice(int orderId,decimal TotalAmount)
        {
            var invoice = new Invoice()
            {
                OrderId = orderId,
                TotalAmount = TotalAmount
            };
            await _repository.AddAsync(invoice);

            return invoice;
        }
    }
}
