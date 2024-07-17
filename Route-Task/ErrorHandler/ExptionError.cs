namespace Route_Task.ErrorHandler
{
    public class ExptionError:ApiResponse
    {
        public string? Details { get; set; }
        public ExptionError(int status, string msg = null, string details = null) : base(status, msg)
        {

            Details = details;
        }
    }
}
