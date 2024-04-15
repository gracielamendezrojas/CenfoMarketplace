using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class PasswordManager
    {
        private PasswordCrudFactory pwCrudFactory;

        public PasswordManager()
        {
            pwCrudFactory = new PasswordCrudFactory();
        }

        public void CreatePass(Password pw)
        {

            // Enable Encription //
            pw.Passwordd = encrypt(pw.Passwordd);

            pwCrudFactory.Create(pw);
        }

        public List<Password> GetPasswords()
        {
            List<Password> passwords = pwCrudFactory.RetrieveAll<Password>();

            foreach (Password password in passwords)
            {
                password.Passwordd = decrypt(password.Passwordd);
            }

            return passwords;
        }

        public void DeletePass(Password pw)
        {
            pwCrudFactory.Delete(pw);
        }

        public APIResponse CheckingPassword(Password pw)
        {
            APIResponse api = new APIResponse()
            {
                Message = "Different passwords",
                Status = 400,
                TransactionDate = DateTime.Now,
            };

            pw.Passwordd = encrypt(pw.Passwordd);

            Password currentPassword = null;
            
            List<Password> listAllUserPasswords = GetPasswords();
            List<Password> passwordsUser = new List<Password>();
            foreach (Password password in listAllUserPasswords)
            {
                if (password.User.Equals(pw.User))
                {
                    passwordsUser.Add(password);
                }
            }

            if(passwordsUser.Count > 0)
            {
                var smallestTimeDifference = DateTime.Now - passwordsUser[0].CreationDate;
                currentPassword = passwordsUser[0];

                foreach (Password password in passwordsUser)
                {
                    var timeDifference = DateTime.Now - password.CreationDate;
                    if (timeDifference < smallestTimeDifference)
                    {
                        smallestTimeDifference = timeDifference;
                        currentPassword = password;
                    }
                }
                currentPassword.Passwordd = encrypt(currentPassword.Passwordd);

            }
            if (pw.Passwordd.Equals(currentPassword.Passwordd))
            {
                api.Status = 200;
                api.Message = "Password checked";
            }
            return api;
        }


        public Password GetPass(Password pw)
        {
            pw.Passwordd = decrypt(pw.Passwordd);
            return pwCrudFactory.Retrieve<Password>(pw);
        }

        public APIResponse EditPass(Password pw)
        {
            APIResponse api = new APIResponse()
            {
                Message = "Password Updated.",
                Status = 200,
                TransactionDate = DateTime.Now,
            };

            pw.Passwordd = encrypt(pw.Passwordd);

            List<Password> listAllUserPasswords = GetPasswords();
            List<Password> passwordsUser = new List<Password>();

            foreach (Password password in listAllUserPasswords)
            {
                if (password.User.Equals(pw.User))
                {
                    password.Passwordd = encrypt(password.Passwordd);
                    passwordsUser.Add(password);
                }
            }
            var passwordCount = passwordsUser.Count();

            for (var index = 1; (passwordCount - index >= 0 && index <= 5); index++) {
                var pw1 = passwordsUser.ElementAt(passwordCount - index);
                
                if (pw1.Passwordd.Equals(pw.Passwordd))
                {
                    api.Status = 400;
                    api.Message = "This password cannot be used.";
                    return api;
                }
            }


            UserActionManager um = new UserActionManager();
            UserAction ua = new UserAction()
            {
                User = pw.User,
                Date = new DateTime(),
                Type = "New password created."
            };

            um.CreateUserAction(ua);


            pw.CreationDate = DateTime.Now;
            pwCrudFactory.Create(pw);
            return api;
        }



        public String encrypt(String password)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public String decrypt(String password)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(password);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

    }
}
