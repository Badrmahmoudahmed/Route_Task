using OrederManagement.Core.Entities;

namespace Route_Task.Helpers
{
    public class UserDto
    {
        public string Username { get; set; }
        public Roles Role { get; set; }

        public string Token { get; set; }
    }
}
