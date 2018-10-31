using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EAAS.Models;
namespace EAAS.Controllers
{
    public class RegitrationController : ApiController
    {
        BusinessLogic ObjBl = new BusinessLogic();

       
        [Route("api/UserRegistration"), HttpPost]
        public HttpResponseMessage UserRegistration(UserRegistration userreg)
        {
            UserRegistration ObjUser = null;
            try
            {
                ObjUser = new UserRegistration();
                ObjUser = ObjBl.UserRegistration(userreg.EmailId, userreg.Password, userreg.FirstName, userreg.LastName);
                return PrepareResponse(ObjUser, "", "UserRegistration");
            }
            catch (Exception ex)
            {
                return PrepareResponse(ObjUser, ex.Message, "UserRegistration");
            }
            finally
            {
                ObjUser = null;
            }
        }

        [Route("api/AppRegistration"), HttpPost]
        public HttpResponseMessage AppRegistration(AppRegistration Appreg)
        {
            string Result = "";
            try
            {

                Result = ObjBl.AppRegistration(Appreg.UserId,Appreg.AppId,Appreg.AppName, Appreg.AppEncryptionKey,Appreg.Urls);
                return PrepareResponse(Result, "", "AppRegistration");
            }
            catch (Exception ex)
            {
                return PrepareResponse(Result, ex.Message, "AppRegistration");
            }
            finally
            {
                Result = "";
            }
        }

        [Route("api/GetAppDetails"), HttpGet]
        public HttpResponseMessage GetAppDetails(string AppKey,string AppSecret)
        {
            AppRegistration ObjAppreg = null;
            try
            {
                ObjAppreg = new AppRegistration();
                ObjAppreg = ObjBl.GetAppDetails(AppKey, AppSecret);
                return PrepareResponse(ObjAppreg, "", "GetAppDetails");
            }
            catch (Exception ex)
            {
                return PrepareResponse(ObjAppreg, ex.Message, "GetAppDetails");
            }
            finally
            {
                ObjAppreg =null;
            }
        }

        [Route("api/GetUserApps"), HttpGet]
        public HttpResponseMessage GetUserApps(string UserId,string AppId )
        {
            AppDetails ObjAppreg = null;
            try
            {
                ObjAppreg = new AppDetails();
                ObjAppreg = ObjBl.GetUserApps(UserId,AppId);
                return PrepareResponse(ObjAppreg, "", "GetUserApps");
            }
            catch (Exception ex)
            {
                return PrepareResponse(ObjAppreg, ex.Message, "GetUserApps");
            }
            finally
            {
                ObjAppreg = null;
            }
        }

        [Route("api/AuthenticateUser"), HttpGet]
        public HttpResponseMessage AuthenticateUser(string EmailId, string Password)
        {
            UserRegistration ObjUser = null;
            try
            {
                ObjUser = new UserRegistration();
                ObjUser = ObjBl.AuthenticateUser(EmailId, Password);
                return PrepareResponse(ObjUser, "", "AuthenticateUser");
            }
            catch (Exception ex)
            {
                return PrepareResponse(ObjUser, ex.Message, "AuthenticateUser");
            }
            finally
            {
                ObjUser = null;
            }
        }
        private HttpResponseMessage PrepareResponse<T>(T obj,string ErrorMessgae,string ControllerName)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new OutPutResponse<T>()
            {
               payload = obj,
               ErrorMessgae=ErrorMessgae,
               ControllerName=ControllerName
            });
        }

    }
}
