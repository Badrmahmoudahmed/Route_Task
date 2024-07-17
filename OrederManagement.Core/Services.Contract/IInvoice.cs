using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Services.Contract
{
    public interface IInvoice
    {
        Task<Invoice> CreateInvoice(int orderId,decimal TotalAmount);
    }
}
