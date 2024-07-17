using OrederManagement.Core.Entities;

namespace Route_Task.Helpers
{
    public class CreateOrderParmanter
    {
        public int CustmorId { get; set; }
        public string PaymentMethod { get; set; }
        public List<CreateOrderItemParams> OrderItems { get; set; }
    }
}
