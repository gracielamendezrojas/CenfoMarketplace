using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class SendGridManager
    {
        public async Task<bool> sendEmail(String email, String emailSubject, String general, String message, String OTP)
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("");
            var to = new EmailAddress(email);
            var subject = emailSubject;
            var plainTextContent = "";
            var htmlContent =
                             "<table style= 'background-color: #f4f5fc; border-radius: 10%; padding: 30px'>" +
                                    "<tr style = 'text-align: center'> <p>" + general + "</p></tr>" +
                                    "<tr style = 'text-align: center'> <p>" + message + "</p><p><strong>" + OTP + "</strong></p></tr>" +
                                    //image
                                    "<tr style = 'text-align: center' ><a href='https://ibb.co/1f2Mp8M'><img style ='height:50px' src='https://i.ibb.co/1f2Mp8M/logo-Canva.png' alt='logo-Canva' border='0'></a></tr>" +
                                    "<tr style = 'text-align: center'><h2><strong>Cenfo-MarketPlace</strong> </h2> <h4>Buy and sell safely</h4></tr>" +
                                    "<tr style = 'text-align: center'><hr style = 'border: 2px solid #811111; margin: 2px'> </hr></tr>" +
                                    "<tr style = 'text-align: center'><p>Costa Rica, San José</p> <p>2022</p></tr>" +
                              "</table>";
            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                plainTextContent,
                htmlContent
                );
            SendGrid.Response response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }


        public void sendEmailNoAsync(String email, String emailSubject, String general, String message, String Code)
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("");
            var to = new EmailAddress(email);
            var subject = emailSubject;
            var plainTextContent = "";
            var htmlContent =
                             "<table style= 'background-color: ##f4f5fc; border-radius: 10%; padding: 30px'>" +
                                    "<tr style = 'text-align: center'> <p>" + general + "</p></tr>" +
                                    "<tr style = 'text-align: center'> <p>" + message + "</p><p><strong>" + Code + "</strong></p></tr>" +
                                    //image
                                    "<tr style = 'text-align: center' ><a href='https://ibb.co/1f2Mp8M'><img style ='height:50px' src='https://i.ibb.co/1f2Mp8M/logo-Canva.png' alt='logo-Canva' border='0'></a></tr>" +
                                    "<tr style = 'text-align: center'><h2><strong>Cenfo-MarketPlace</strong> </h2> <h4>Buy and sell safely</h4></tr>" +
                                    "<tr style = 'text-align: center'><hr style = 'border: 2px solid #811111; margin: 2px'> </hr></tr>" +
                                    "<tr style = 'text-align: center'><p>Costa Rica, San José</p> <p>2022</p></tr>" +
                              "</table>";
            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                plainTextContent,
                htmlContent
                );
            var response = client.SendEmailAsync(msg);
        }
        public async Task<bool> sendEmailInvoice(string email, string name, string lastName, string NFT, string Collection, int price)
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("");
            var to = new EmailAddress(email);
            var subject = "Cenfo-Market Invoice";
            var item="";
            if (NFT == null)
            {
                item = Collection;

            }
            else
            {
                item = NFT;
            }
            var plainTextContent = "";
            var htmlContent=
            "    <div class='invoice-box' style ='max -width: 800px;margin: auto;padding: 30px;border: 1px solid #eee;box-shadow: 0 0 10px rgba(0, 0, 0, .15);font-size: 16px;line-height: 24px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color: #555;'>" +
            "        <table cellpadding='0' cellspacing ='0' style ='width: 100%;line-height: inherit;text-align: left;' > "+
            "            <tr class='top'>"+
            "                <td colspan='2' style ='padding: 5px;vertical-align: top;' > " +
            "                    <table style='width: 100%;line-height: inherit;text-align: left;' > " +
            "                        <tr>"+
            "                            <td class='title' style ='padding: 5px;vertical-align: top;padding-bottom: 20px;font-size: 45px;line-height: 45px;color: #333;' > " +
            "                                <img src='https://i.ibb.co/1f2Mp8M/logo-Canva.png' style ='width:20%; max-width:300px;' > " +
            "                            </td>"+
            "                            <span style='color:black;font-size:2.2rem;font-weight:500;' id='mob'>Invoice</span>" +
            "                            <td style='padding: 5px;vertical-align: top;text-align: right;padding-bottom: 20px;' > " +
            "                                Invoice#: 123<br>"+
            "                                Created: "+DateTime.Now.ToString("MM/dd/yyyy")+"<br>"+
            "                            </td>"+
            "                        </tr>"+
            "                    </table>"+
            "                </td>"+
            "            </tr>"+
            "            <tr class='information' > "+
            "                <td colspan='2' style='padding: 5px;vertical-align: top;' > " +
            "                    <table style='width: 100%;line-height: inherit;text-align: left;' > " +
            "                        <tr>"+
            "                            <td style='padding: 5px;vertical-align: top;padding-bottom: 40px;' > " +
            "                                CenfoMarket, Inc.<br>"+
            "                                San José, Costa Rica<br>"+
            "                            </td>"+
            "                            <td style='padding: 5px;vertical-align: top;text-align: right;padding-bottom: 40px;' > "+
            "                                "+name+" "+lastName+"<br>"+
            "                                "+email+""+
            "                            </td>"+
            "                        </tr>"+
            "                    </table>"+
            "                </td>"+
            "            </tr>"+
            "            <tr class='heading'> "+
            "                <td style='padding: 5px;vertical-align: top;background: #eee;border-bottom: 1px solid #ddd;font-weight: bold;' > "+
            "                    Item"+
            "                </td>"+
            "                <td style='padding: 5px;vertical-align: top;text-align: right;background: #eee;border-bottom: 1px solid #ddd;font-weight: bold;'>" +
            "                    Price" +
            "                </td>"+
            "            </tr>"+
            "            <tr class='item'>"+
            "                <td style='padding: 5px;vertical-align: top;border-bottom: 1px solid #eee;' > "+
            "                   "+item+""+
            "                </td>"+
            "                <td style='padding: 5px;vertical-align: top;text-align: right;border-bottom: 1px solid #eee;' > "+
            "                    " +price +""+
            "                </td>" +
            "            </tr>"+
            "            <tr class='total'>"+
            "                <td style='padding: 5px;vertical-align: top; '></td>" +
            "                <td style='padding: 5px;vertical-align: top;text-align: right;border-top: 2px solid #eee;font-weight: bold;' > " +
            "                   Total: " + price + "" +
            "                </td>"+
            "            </tr>"+
            "        </table>"+
            "    </div>";


            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                plainTextContent,
                htmlContent
                );
            SendGrid.Response response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

    }
}






