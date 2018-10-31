using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAAS.Models
{
    public class UserRegistration
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Code { get; set; }

        public string UserId { get; set; }
    }

   
}