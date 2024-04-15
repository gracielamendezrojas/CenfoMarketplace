using AppLogic.Managers;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppi.Controllers
{
    public class EmailController : ApiController
    {
        NotificationManager nm = new NotificationManager();

        [HttpPost]
        public APIResponse Send(Email email)
        {
            nm.paypalNotification(email.EmailAdress, email.Name, email.LastName, email.Message); 
            return new APIResponse(); 
        }

        [HttpPost]
        public APIResponse SendEmailForCenfomarket(Email email)
        {
            nm.contactCenfoMarket(email.EmailAdress, email.Name, email.LastName, email.Message);
            return new APIResponse();
        }

        [HttpPost]
        public APIResponse SendTransactionEmail(Email email)
        {
            nm.transactionNotification(email.EmailAdress);
            return new APIResponse();
        }

        [HttpGet]
        public APIResponse SendTransactionSMS(int userId)
        {
            UserManager userManager = new UserManager();
            User user = new User()
            {
                Id = userId,
            };
            PhoneManager phoneManager = new PhoneManager();
            Phone phone = new Phone();

            user = userManager.GetUser(user);

            foreach(Phone ph in phoneManager.GetPhones())
            {
                if(ph.User == user.Id)
                {
                    phone = ph;
                }
            }

            nm.phoneTransaction(phone.Number);
            return new APIResponse();
        }

    }
}