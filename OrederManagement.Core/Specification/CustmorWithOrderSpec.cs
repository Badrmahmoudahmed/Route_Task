using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrederManagement.Core.Specification
{
    public class CustmorWithOrderSpec : BaseSpecification<Custmor>
    {
        public CustmorWithOrderSpec()
        {
            Includes.Add(C => C.Orders);
            IncludeString.Add($"{nameof(Custmor.Orders)}.{nameof(Order.OrderItems)}");
 

        }
        public CustmorWithOrderSpec(int id):base(C=>C.Id == id)
        {
            Includes.Add(C => C.Orders);
            IncludeString.Add($"{nameof(Custmor.Orders)}.{nameof(Order.OrderItems)}");
        }
    }
}
