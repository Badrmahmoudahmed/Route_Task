namespace Route_Task.ErrorHandler
{
    public class ValidateError : ApiResponse
    {
        public List<string> Errors { get; set; }

        public ValidateError():base(400)
        {
            Errors = new List<string>();
        }
    }
}
