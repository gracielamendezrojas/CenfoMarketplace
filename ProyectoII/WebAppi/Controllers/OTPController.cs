using AppLogic.Managers;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAppi.Controllers
{
    public class OTPController : ApiController
    {
        OTPManager om = new OTPManager();
        UserManager um = new UserManager();

        [HttpPost]
        public APIResponse Create(OTP OTP)
        {
            om.CreateOTP(OTP);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }
        [HttpPost]
        //Lo puse propuesta
        public APIResponse CreateOTPEmail(Customer a)
        {
            om.CreateOTPEmail(a);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpPost]
        //Lo puse propuesta
        public APIResponse CreateOTPSMS(Customer a)
        {
            om.CreateOTPSMS(a);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = om.RetrieveAllOTPs();
            return api;
        }

        [HttpPost]
        public APIResponse Get(int id)
        {
            OTP OTP = new OTP()
            {
                Id = id
            };
            return om.RetrieveOTP(OTP);
        }

        [HttpPut]
        public APIResponse Update(OTP sus)
        {
            //Update by User
            return om.UpdateOTP(sus);
        }

        [HttpDelete]
        public APIResponse Delete(int id)
        {
            OTP sus = new OTP()
            {
                Id = id
            };            
            return om.DeleteOTP(sus);
        }


        // Takes customer ID sents email
        [HttpPost]
        public async Task<APIResponse>  SentOtpByEmail(int id)
        {
            APIResponse api = new APIResponse() { Status = 500, TransactionDate = DateTime.Now };                       
            OTP tempotp = new OTP() { UserId=id};
            string dbotp = "";
            List<OTP> list = om.RetrieveOtpList();

            UserController userController = new UserController();
            var checkuser = userController.Get(id);

            if (checkuser.Status.Equals(404))
            {
                api.Message = "invalid ID";
                api.TransactionDate = DateTime.Now;
                return api;
            }
            
            foreach (OTP otp in list)
            {
                if (otp.UserId.Equals(id))
                    dbotp = otp.OTPNumber;
            }


            om.CreateOTP(tempotp);
            Customer a = new Customer() { Email = um.GetUser(new User() { Id = id }).Email, OTP = tempotp.OTPNumber };
            var resultu = await om.CreateOTPEmail(a);            

            if (resultu)
            {
                api.Data = a.Email;
                api.Status = 200;
                api.Message = "Email Sent successfuly";
                api.TransactionDate = DateTime.Now;
            }           
            
            return api;
        }

        [HttpPost]
        public APIResponse Validate(OTP otp)
        {

            APIResponse api = new APIResponse() { TransactionDate=DateTime.Now};
            api.Message = "Otp invalid";

            if (om.isValid(otp))
            {
                api.Data = otp;
                api.Status = 200;
                api.Message = "Valid";
            }        

            return api;
        }


    }
}

