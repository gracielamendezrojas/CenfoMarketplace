using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class UserManager : BaseManager
    {
        private const string StringChars = "0123456789abcdef";
        private UserCrudFactory userCrudFactory;
        private PasswordCrudFactory passwordCrudFactory;
        private RoleXUserCrudFactory RoleXUserCrudFactory;
        private RoleCrudFactory RoleCrudFactory;
        private WalletCrudFactory walletCrudFactory;

        public UserManager()
        {
            userCrudFactory = new UserCrudFactory();
            passwordCrudFactory = new PasswordCrudFactory();
            RoleXUserCrudFactory = new RoleXUserCrudFactory();
            RoleCrudFactory = new RoleCrudFactory();
            walletCrudFactory = new WalletCrudFactory();
        }

        public void CreateUser(User user)
        {

            userCrudFactory.Create(user);
        }

        public APIResponse CreateUserandPhone(Customer c)
        {
            APIResponse response = new APIResponse() { Data = "failed" };
            response.Status = 505;
            response.Message = "failed";
            response.TransactionDate = DateTime.Now;

            try
            {

                User user = new User();
                user.Id = c.Id;
                user.Name = c.Name;
                user.LastName = c.LastName;
                user.NickName = c.NickName;
                user.Email = c.Email;
                user.Avatar = c.Avatar;
                user.DOB = c.DOB;
                user.PreferredMethod = c.PreferredMethod;
                if (c.Role == "Buyer")
                {
                    user.Status = "Active";
                }
                else
                {
                    user.Status = "Inactive";
                }
                if (c.Role == "Admin")
                {
                    user.Status = "Active";
                    user.PreferredMethod = "Email";
                }


                //Primero voy a la BD y registro al user
                CreateUser(user);

                //Segundo, voy a la BD y registro una celular
                Phone phone = new Phone();
                var crudPhone = new PhoneCrudFactory();

                phone.Number = c.Phone;
                phone.Description = "Celular de " + c.Name + " " + c.LastName;
                phone.User = c.Id;

                crudPhone.Create(phone);

                //Registro Password
                Password p = new Password();
                p.Passwordd = c.Password;
                p.User = c.Id;
                p.CreationDate = DateTime.Now;

                PasswordManager passManager = new PasswordManager();
                passManager.CreatePass(p);

                //Registro RoleXUser
                RoleXUser role = new RoleXUser();
                RoleXUserManager roleXUserManager = new RoleXUserManager();
                if (c.Role == "Content Creator")
                {
                    role.RoleId = 3;
                }
                else if (c.Role == "Buyer")
                {
                    role.RoleId = 2;
                }else if (c.Role == "Admin")
                {
                    role.RoleId = 1;
                }
                role.UserId = c.Id;
                roleXUserManager.CreateRoleXUser(role);

                //Registro User Action
                UserActionManager um = new UserActionManager();
                UserAction ua = new UserAction()
                {
                    User = user.Id,
                    Date = new DateTime(),
                    Type = "User created"
                };

                

                um.CreateUserAction(ua);

                CollectionManager cm = new CollectionManager();
                Collection col = new Collection()
                {
                    Name = "Default Collection " + user.Id,
                    SaleStatus = "Not On Sale",
                    Description = "Default collection for User "+ user.Id,
                    Status = "Active",
                    User = user.Id
                };

                cm.CreateCollection(col);

                Random rand = new Random();
                var charList = StringChars.ToArray();
                string hexString = "";

                for (int i = 0; i < 32; i++)
                {
                    int randIndex = rand.Next(0, charList.Length);
                    hexString += charList[randIndex];
                }

                if (c.Role != "Admin")
                {
                    Wallet w = new Wallet()
                    {
                        Id = hexString,
                        UserId = user.Id,
                        Balance = 0,
                    };

                    walletCrudFactory.Create(w);
                }
                //Create Notification
                Notification not = new Notification();
                NotificationsManager nManager = new NotificationsManager();
                not.User = 123456789;
                if (c.Role == "Content Creator")
                {
                    not.Subject = "New content creator created.";
                }
                else if (c.Role == "Buyer")
                {
                    not.Subject = "New buyer created.";
                }
                else if (c.Role == "Admin")
                {
                    not.Subject = "New manager created.";
                }
                not.Message = "User created.";
                nManager.CreateNotifications(not);

                response.Status = 200;
                response.Message = "Sucessfull.";
                response.Data = "";
            }
            catch(Exception ex)
            {
                response.Data = ex;
            }
            return response;

        }
        public List<User> GetUsers()
        {
            return userCrudFactory.RetrieveAll<User>();
        }

        public void DeleteUser(User user)
        {
            userCrudFactory.Delete(user);
        }

        public User GetUser(User user)
        {
            return userCrudFactory.Retrieve<User>(user);
        }



        public APIResponse EditUser(User user)
        {
            APIResponse api = new APIResponse()
            {
                Message = "User not edited",
                Status = 400,
                TransactionDate = DateTime.Now,
            };
            try
            {
                userCrudFactory.Update(user);
                api.Data = user;
                api.Status = 200;

            }catch(Exception ex)
            {
                api.Data = ex;
                api.Status = "ERR";
                api.Message = "Error when process the request";

            }




            UserActionManager um = new UserActionManager();
            UserAction ua = new UserAction()
            {
                User = user.Id,
                Date = new DateTime(),
                Type = "Profile updated"
            };

            um.CreateUserAction(ua);
            return api;
        }

        public void EditUserStatus(Customer customer)
        {
           User user= new User();
            user.Id = customer.Id;
            user.Status = "Active";
            userCrudFactory.UpdateStatus(user);
        }

        public APIResponse EditUserNotification(User user)
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
                Status = 400, 
            }; 

            User retrieved = userCrudFactory.Retrieve<User>(user);
            retrieved.PreferredMethod = user.PreferredMethod; 
            if(retrieved != null)
            {
                try
                {
                    userCrudFactory.UpdateNotificationMethod(retrieved);
                    api.Data = retrieved;
                    api.Status = 200; 
                    api.Message = "Updated succefully"; 
                }
                catch (Exception ex)
                {
                    api.Data = ex;
                    api.Status = "ERR";
                    api.Message = "Error when process the request";

                }
            }
            return api; 
        }


        public APIResponse Login(Login login)
        {

            PasswordManager pm = new PasswordManager();
            login.Password = pm.encrypt(login.Password);

            APIResponse response = new APIResponse() { Data = "failed" };
            response.Status = 505;
            response.Message = "Login Failed!";
            response.TransactionDate = DateTime.Now;

            // Creata Obj For Login Information
            var loginuserinfo = new LoginUserInformation();


            // >>>>>> ADD try Catch for Do not exist  //
            try
            {

                // Retrieve User By Email
                var dbuser = userCrudFactory.RetrieveByEmail<User>(new User() { Email = login.Email });

                if (dbuser == null)
                {
                    response.Message = "User not found, go to sing in!";
                    return response;

                }

                if (dbuser.Status.Equals("Inactive"))
                {
                    response.Message = "Inactive User, Please contact Administrator";
                    return response;

                }


                // Rerno ALl password for a customer
                var listpasswordsdbuser = passwordCrudFactory.RetrieveAll<Password>(new Password() { User = dbuser.Id });



                var currentpassword = new Password();

                foreach (var tempPass in listpasswordsdbuser)
                {
                    if (tempPass.CreationDate > currentpassword.CreationDate)
                    {
                        currentpassword = tempPass;
                    }
                }

                // Return Rolesxuser
                var ListxRoles = RoleXUserCrudFactory.RetrieveAllByUser<RoleXUser>(new RoleXUser() { UserId = dbuser.Id });

                // Obj For store all User ROles
                List<Role> test = new List<Role>();

                foreach (var item in ListxRoles)
                {
                    test.Add(RoleCrudFactory.Retrieve<Role>(new Role() { Id = item.RoleId }));
                }

                // Validate if User Information is ok //
                if (login.Email.Equals(dbuser.Email) & login.Password.Equals(currentpassword.Passwordd))
                {
                    loginuserinfo.User = dbuser;
                    loginuserinfo.Roles = test;
                    response.Status = 200;
                    response.Message = "Sucessfull";
                    // Pending
                    // Category
                    // Organation
                }

                response.Data = loginuserinfo;

                UserActionManager um = new UserActionManager();
                UserAction ua = new UserAction()
                {
                    User = dbuser.Id,
                    Date = new DateTime(),
                    Type = "User logged in"
                };
                um.CreateUserAction(ua);


            }
            catch (Exception ex) {
                response.Data= ex;
            }
           

            return response;
        }

    }
}
