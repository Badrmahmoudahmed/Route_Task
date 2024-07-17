namespace Route_Task.Helpers
{
    public class CreateOrderItemParams
    {
        public int ProductId { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
    }
}
