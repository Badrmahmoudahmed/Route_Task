using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrederManagement.Core.Entities
{
    public class Invoice : BaseEntity
    {
        
       
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        public int OrderId { get; set; }

        public Order order { get; set; }
    }
}
