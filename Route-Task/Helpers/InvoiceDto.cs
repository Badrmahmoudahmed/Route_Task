namespace Route_Task.Helpers
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        public int OrderId { get; set; }
    }
}
