using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrederManagement.Core.Specification
{
    public class LoginSpecification:BaseSpecification<User>
    {
        public LoginSpecification(string username):base(u=>u.Username.ToLower() == username.ToLower())
        {
            
        }
    }
}
