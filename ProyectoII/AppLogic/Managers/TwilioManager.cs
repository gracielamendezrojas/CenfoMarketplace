using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AppLogic.Managers
{
    public class TwilioManager
    {
        public void sendSMS(int phoneNumber, String name, String lastName,String otp)
        {

            var om = new OTPManager();

            string accountSid = "";
            string authToken = "";
            //String phone = "+506" + phoneNumber;

            TwilioClient.Init(accountSid, authToken);
            ;

            var message = MessageResource.Create(
                body: "Hello " + name + " " + lastName + ", welcome to Cenfo-MarketPlace! Please confirm your number by typing this OTP in the registration page: " + otp,
                from: new Twilio.Types.PhoneNumber(""),
                //to: new Twilio.Types.PhoneNumber(phone),
                to: new Twilio.Types.PhoneNumber("+506"+phoneNumber)
            );

        }

        public void sendTransactionSMS(int phoneNumber)
        {

            var om = new OTPManager();

            string accountSid = "";
            string authToken = "";
            //String phone = "+506" + phoneNumber;

            TwilioClient.Init(accountSid, authToken);
            ;

            var message = MessageResource.Create(
                body: "Hello, a transaction has been made in your CenfoMarket account!",
                from: new Twilio.Types.PhoneNumber("+"),
                //to: new Twilio.Types.PhoneNumber(phone),
                to: new Twilio.Types.PhoneNumber("+506" + phoneNumber)
            );

        }

        public void sendbidupdateSMS(int phoneNumber)
        {

            var om = new OTPManager();

            string accountSid = "";
            string authToken = "";
            //String phone = "+506" + phoneNumber;

            TwilioClient.Init(accountSid, authToken);
            ;

            var message = MessageResource.Create(
                body: "Hello, a new bid has been created!",
                from: new Twilio.Types.PhoneNumber(""),
                //to: new Twilio.Types.PhoneNumber(phone),
                to: new Twilio.Types.PhoneNumber("+506" + phoneNumber)
            );

        }


    }
}
