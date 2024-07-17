using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrederManagement.Core.Services.Contract
{
    public interface IOrderServices
    {

        Task<Order> CreateOrderAsync(string paymentMethod, int custmorid, List<OrderItem> orderItems);
        
    }
}
