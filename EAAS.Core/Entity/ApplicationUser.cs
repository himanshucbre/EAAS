using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EAAS.Core.Entity
{
    public class ApplicationUser : IdentityUser
    {
       

        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string Configuration { get; set; }   
        
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

    }
}