using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EAAS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EaaSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegitrationController : ControllerBase
    {
    
        BusinessLogic ObjBl = new BusinessLogic();


        [Route("api/UserRegistration"), HttpPost]
        public IActionResult UserRegistration(UserRegistration userreg)
        {
            UserRegistration ObjUser = null;           
                ObjUser = new UserRegistration();
                ObjUser = ObjBl.UserRegistration(userreg.EmailId, userreg.Password, userreg.FirstName, userreg.LastName);
                return Ok(ObjUser );          
        }

     
        [Route("api/AppRegistration"), HttpPost]
        public IActionResult AppRegistration(AppRegistration Appreg)
        {
               var Result = ObjBl.AppRegistration(Appreg.UserId, Appreg.AppId, Appreg.AppName, Appreg.AppEncryptionKey, Appreg.Urls);
                return Ok(Result);           
        }

        [Route("api/GetAppDetails"), HttpGet]
        public IActionResult GetAppDetails(string AppKey, string AppSecret)
        {
            AppRegistration ObjAppreg = null;
            ObjAppreg = new AppRegistration();
                ObjAppreg = ObjBl.GetAppDetails(AppKey, AppSecret);
                return Ok(ObjAppreg);            
        }

        [Route("api/GetUserApps"), HttpGet]
        public IActionResult GetUserApps(string UserId, string AppId)
        {
            AppDetails ObjAppreg = null;
             ObjAppreg = new AppDetails();
                ObjAppreg = ObjBl.GetUserApps(UserId, AppId);
                return Ok(ObjAppreg);            
        }

        [Route("api/AuthenticateUser"), HttpGet]
        public IActionResult AuthenticateUser(string EmailId, string Password)
        {
            UserRegistration ObjUser = null;            
                ObjUser = new UserRegistration();
                ObjUser = ObjBl.AuthenticateUser(EmailId, Password);
                return Ok(ObjUser);            
        }
      
    }
}
