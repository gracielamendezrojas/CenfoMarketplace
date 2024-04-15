using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using AppLogic.Utilities;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class OTPManager : BaseManager
    {
        private OTPCrudFactory OTPCrudFactory;
        private UserManager userManager;

        public OTPManager()
        {
            OTPCrudFactory = new OTPCrudFactory();
            userManager = new UserManager();
        }

        public void CreateOTP(OTP OTP)
        {
            // 1 generates TPS // 2 generates Hexagecimal 32 Characters
            OTP.OTPNumber = CodeGenerator.GenerateCode(1);
            OTP.DateTime = DateTime.Now;

            //check if user has an OTP //IF OTP exist update
            List<OTP> OTPs = OTPCrudFactory.RetrieveAll<OTP>();
            string resultupdate = "";

            foreach (var otp in OTPs)
            {
                if (otp.UserId.Equals(OTP.UserId))
                {
                    OTP.Id = otp.Id;
                    resultupdate = UpdateOTP(OTP).Message;
                }
            }

            if (!resultupdate.Equals("Update Sucessfull"))
            {
                OTPCrudFactory.Create(OTP);
            }


        }


        //Hice este nuevo es una propuesta
        public async Task<bool> CreateOTPEmail(Customer a)
        {
            var sm = new NotificationManager();
            return await sm.emailConfirmation(a.Email,a.Name, a.LastName, a.OTP);

        }
        //Hice este nuevo es una propuesta
        public void CreateOTPSMS(Customer c)
        {
            var sm = new NotificationManager();
            sm.phoneConfirmation(c.Phone, c.Name, c.LastName, c.OTP);

            //OTP otp = new OTP();
            //otp.OTPNumber = OTPGe;
            //otp.DateTime = DateTime.Now;
            //otp.UserId = c.Id;
            //OTPCrudFactory.Create(otp);
        }

        public List<OTP> RetrieveOtpList()
        {
            List<OTP> OTPs = OTPCrudFactory.RetrieveAll<OTP>();
            return OTPs;
        }

        public APIResponse RetrieveAllOTPs()
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<OTP> OTPs = OTPCrudFactory.RetrieveAll<OTP>();
            try
            {
                if (OTPs != null)
                {
                    response.Status = 200;
                    response.Data = OTPs;
                    response.Message = "Sucessfull";
                }
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }

            return response;

        }

        public APIResponse RetrieveOTP(OTP OTP)
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            OTP retrieved = OTPCrudFactory.Retrieve<OTP>(OTP);

            try
            {
                if (retrieved != null)
                {
                    response.Status = 200;
                    response.Data = retrieved;
                    response.Message = "Sucessfull";
                }
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }

            return response;
        }

        public APIResponse UpdateOTP(OTP OTP)
        {

            APIResponse response = new APIResponse() { Message = "Not found" };
            var retrieved = OTPCrudFactory.Retrieve<OTP>(OTP);


            response.Status = 404;

            try
            {
                if (retrieved != null)
                {
                    retrieved.DateTime = DateTime.Now;
                    retrieved.OTPNumber = OTP.OTPNumber;
                    OTPCrudFactory.Update(retrieved);
                    response.Status = 200;
                    response.Data = retrieved;
                    response.Message = "Update Sucessfull";
                }


            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }

            return response;
        }

        public APIResponse DeleteOTP(OTP OTP)
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = OTPCrudFactory.Retrieve<OTP>(OTP);


            try
            {

                if (retrieved != null)
                {
                    OTPCrudFactory.Delete(OTP);

                    response.Status = 200;
                    response.Data = retrieved;
                    response.Message = "Delete Sucessfull";
                }

            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }
            return response;
        }

        public bool isValid(OTP OTP)
        {
            bool status = false;

            List<OTP> OTPs = OTPCrudFactory.RetrieveAll<OTP>();
            foreach (OTP item in OTPs)
            {
                if (OTP.OTPNumber.Equals(item.OTPNumber) && OTP.UserId.Equals(item.UserId))
                {
                    status = true;
                }


            }

            return status;
        }

        //Hice este nuevo es una propuesta 


    }
}

