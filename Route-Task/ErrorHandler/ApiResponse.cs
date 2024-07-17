
using Microsoft.AspNetCore.Http;

namespace Route_Task.ErrorHandler
{
    public class ApiResponse
    {
       

        public int Status { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int status, string? message = null)
        {
            Status = status;
            Message = message is null ? SetMessage(status) : message ;
        }

        private string SetMessage(int status)
        {
            return status switch
            {
                400 => "A Bad Request, you have made",
                401 => "you are not Authorized,",
                403 => "you are not InRole,",
                404 => "Resource was not Found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate.Hate leads to career thange",
                _ => null!

            };
        }
    }
}
