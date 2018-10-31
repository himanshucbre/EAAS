using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EaaSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        
        public string appKey
        {
            get
            {
                return HttpContext?.Request.Headers["appKey"] ?? string.Empty; 
            }
        }

        public string appSecret
        {
            get
            {
                return HttpContext?.Request.Headers["appSecret"] ?? string.Empty;
            }
        }

    }
}