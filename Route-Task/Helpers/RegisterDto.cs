using OrederManagement.Core.Entities;

namespace Route_Task.Helpers
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Roles Role { get; set; }
    }
}
