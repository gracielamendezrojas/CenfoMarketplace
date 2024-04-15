using DTO_POJOS;
using System;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class NotificationManager
    {

        public async Task<bool> emailConfirmation(String email, String name, String lastName, String OTP)
        {
            String emailSubject = "Cenfo-Market Email Confirmation";
            String general = "Hello " + name + " " + lastName + ", welcome to Cenfo-MarketPlace!";
            String message = "Please confirm your email by typing this OTP in the registration page: "; 
            var sm = new SendGridManager();
            return await sm.sendEmail(email, emailSubject, general, message, OTP);
        }


        public void transactionNotification(String email)
        {
            var date = DateTime.Now.ToString();

            String emailSubject = "Cenfo-Market Transaction";
            String general = "A transaction has been made through Cenfo-MarketPlace.";
            String message = "Date of transaction: " + date; 
            String message2 = "Thank you for your preference";
            var sm = new SendGridManager();
            sm.sendEmailNoAsync(email, emailSubject, general, message, message2);
        }

        public void paypalNotification(String email, String name, String lastName, String code)
        {
            var date = DateTime.Now.ToString();

            String emailSubject = "Cenfo-Market Transaction";
            String general = "Hello "+ name+" "+ lastName+"! A transaction has been made through Paypal.";
            String message = "Date of transaction: " + date;
            String message2 = "Papypal code: " + code;
            var sm = new SendGridManager();
            sm.sendEmailNoAsync(email, emailSubject, general, message, message2);
        }

        public void contactCenfoMarket(String email, String name, String lastName, String customerEmail)
        {
            var date = DateTime.Now.ToString();

            String emailSubject = "Contact the customer";
            String general =  " A customer has questions regarding the App.";
            String message = "Date: " + date;
            String message2 = "Customer´s email: " + customerEmail; 
            var sm = new SendGridManager();
            email = "cenfomarket.info@gmail.com";
            sm.sendEmailNoAsync(email, emailSubject, general, message, message2);
        }
        public async Task<bool> emailInvoice(Invoice invoice)
        {
            var sm = new SendGridManager();
            return await sm.sendEmailInvoice(invoice.Email, invoice.Name, invoice.LastName, invoice.NFT,invoice.Collection,invoice.Price);
        }



        //public async Task paypalNotification(String email, String name, String lastName, String code)
        //{
        //    var date = DateTime.Now.ToString();

        //    String emailSubject = "Cenfo-Market Transaction";
        //    String general = "Hello " + name + " " + lastName + "! A transaction has been made through Paypal.";
        //    String message = "Date of transaction: " + date;
        //    String message2 = "Papypal code: " + code;
        //    var sm = new SendGridManager();
        //    await sm.sendEmail(email, emailSubject, general, message, message2);
        //}

        public void phoneConfirmation(int p, String name, String lastName,String otp)
        {
            var tm = new TwilioManager();
            tm.sendSMS(p, name, lastName, otp);
        }

        public void phoneTransaction(int p)
        {
            var tm = new TwilioManager();
            tm.sendTransactionSMS(p);
        }


        public void phonebidNotification(int p)
        {
            var tm = new TwilioManager();
            tm.sendbidupdateSMS(p);
        }

        public void bidNotification(String email)
        {
            var date = DateTime.Now.ToString();

            String emailSubject = "Cenfo-Market Bid";
            String general = "A new Bid has been made.";
            String message = "Date of transaction: " + date;
            String message2 = "Thank you for your preference";
            var sm = new SendGridManager();
            sm.sendEmailNoAsync(email, emailSubject, general, message, message2);
        }


    }
}
