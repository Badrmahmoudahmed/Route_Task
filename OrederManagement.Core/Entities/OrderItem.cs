using System.ComponentModel.DataAnnotations.Schema;

namespace OrederManagement.Core.Entities
{
    public class OrderItem : BaseEntity
    {

        public int OrderId { get; set; }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value > 0 ? value : 1 ; }
        }

        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}