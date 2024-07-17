using OrederManagement.Core.Entities;

namespace Route_Task.Helpers
{
    public class OrderToReturnDto
    {
        public int CustmorId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Statues Status { get; set; } = Statues.Pending;
        public decimal TotalAmount { get; set; }
        public List<OrderItemToReturnDto> OrderItems { get; set; } = new List<OrderItemToReturnDto>();
    }
}
