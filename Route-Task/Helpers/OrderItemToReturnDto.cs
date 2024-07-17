using OrederManagement.Core.Entities;

namespace Route_Task.Helpers
{
    public class OrderItemToReturnDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        
    }
}
