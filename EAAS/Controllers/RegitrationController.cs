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

        [Route("api/InsertRegistrationInfo"), HttpPost]
        public HttpResponseMessage InsertUserInfo(InsertRegistrationInfo RegParam)
        {
            RegistrationResponse Regresponse = null;
            try
            {
                Regresponse = ObjBl.InsertUserInfo(RegParam.UrlList, RegParam.AppName, RegParam.EncryptionKey);
                return PrepareResponse(Regresponse,"", "InsertRegistrationInfo");               
            }
            catch (Exception ex)
            {
                return PrepareResponse(Regresponse,ex.Message, "InsertRegistrationInfo");
            }
            finally
            {
                Regresponse = null;
            }
        }
        [Route("api/UpdateRegistrationInfo"), HttpPut]
        public HttpResponseMessage UpdateRegistrationInfo(UpdateRegistrationInfo UpdRegParam)
        {
            RegistrationResponse Regresponse = null;
            try
            {
                Regresponse = ObjBl.UpdateUserInfo(UpdRegParam.UrlList, UpdRegParam.AppName, UpdRegParam.EncryptionKey, UpdRegParam.SecretKey, UpdRegParam.RegistartionID);
                return PrepareResponse(Regresponse, "", "UpdateRegistrationInfo");
            }
            catch (Exception ex)
            {
                return PrepareResponse(Regresponse, ex.Message, "UpdateRegistrationInfo");
            }
            finally
            {
                Regresponse = null;
            }
        }
        //[Route("api/FetchRegistrationInfo"), HttpPut]
        //public HttpResponseMessage FetchRegistrationInfo(FetchUserDetails FetchParam)
        //{
        //    FetchUserInfoResponse FetchRes = null;
        //    try
        //    {
        //        FetchRes = ObjBl.FetchUserInfo(FetchParam.SecretKey,FetchParam.RegistartionID);
        //        return PrepareResponse(FetchRes,"", "FetchRegistrationInfo");
        //    }
        //    catch (Exception ex)
        //    {
        //        return PrepareResponse(FetchRes,ex.Message, "FetchRegistrationInfo");
        //    }
        //    finally
        //    {
        //        FetchRes = null;
        //    }
        //}
        [Route("api/FetchRegistrationInfo"), HttpGet]
        public HttpResponseMessage FetchRegistrationInfo(string SecretKey,string RegistartionID)
        {
            FetchUserInfoResponse FetchRes = null;
            try
            {
                FetchRes = ObjBl.FetchUserInfo(SecretKey, RegistartionID);
                return PrepareResponse(FetchRes, "", "FetchRegistrationInfo");
            }
            catch (Exception ex)
            {
                return PrepareResponse(FetchRes, ex.Message, "FetchRegistrationInfo");
            }
            finally
            {
                FetchRes = null;
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
