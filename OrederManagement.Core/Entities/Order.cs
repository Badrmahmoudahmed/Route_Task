namespace OrederManagement.Core.Entities
{
    public class Order : BaseEntity
    {

        public int CustmorId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Statues Status { get; set; } = Statues.Pending;
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
        
    }
}