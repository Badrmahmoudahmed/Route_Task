using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrederManagement.Core.Specification
{
    public class OrderWithOrderItemSpec : BaseSpecification<Order>
    {
        public OrderWithOrderItemSpec(int id):base(O=>O.Id == id)
        {
            Includes.Add(o => o.OrderItems);
        }
        public OrderWithOrderItemSpec()
        {
            Includes.Add(o => o.OrderItems);
        }
    }
}
