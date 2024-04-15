using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;


namespace AppLogic.Managers
{
    public class WalletManager : BaseManager
    {
        private WalletCrudFactory WalletCrudFactory;

        public WalletManager()
        {
            WalletCrudFactory = new WalletCrudFactory();
        }

        public void CreateWallet(Wallet Wallet)
        {
            WalletCrudFactory.Create(Wallet);
        }

        public List<Wallet> RetrieveAllWallet()
        {
            List<Wallet> result = WalletCrudFactory.RetrieveAll<Wallet>();
            return result;
        }

        public Wallet RetrieveWallet(BaseEntity entity)
        {
            Wallet result = WalletCrudFactory.Retrieve<Wallet>(entity);
            return result;
        }

        public void DeleteWallet(BaseEntity entity)
        {
            WalletCrudFactory.Delete(entity);
        }

        public void UpdateWallet(BaseEntity entity)
        {
            UserManager userManager = new UserManager();
            List<User> users = new List<User>();
            RoleXUserManager roleXUserManager = new RoleXUserManager();
            foreach(RoleXUser rolex in roleXUserManager.RetrieveAllRoleXUser())
            {
                foreach (User u in userManager.GetUsers())
                {
                    if (rolex.RoleId==1 && u.Id==rolex.UserId)
                    {
                        users.Add(u);

                    }
                }
            }
            
            NotificationManager notificationManager = new NotificationManager();    

            Wallet wallet = (Wallet)entity;

            PhoneManager phoneManager = new PhoneManager();
            Phone phone = new Phone();

          

            if (wallet.Id=="bcc5a83717d83a33fe776db644ad940e")
            {
                Notification not = new Notification();
                NotificationsManager nManager = new NotificationsManager();
                not.User = 123456789;
                not.Subject = "Income Money.";
                not.Message = "Entered money to the app.";
                nManager.CreateNotifications(not);

                foreach (User u in users)
                {
                    foreach (Phone ph in phoneManager.GetPhones())
                    {
                        if (ph.User == u.Id)
                        {
                            phone = ph;
                        }
                    }
                    switch (u.PreferredMethod)
                    {
                        case "SMS":
                            notificationManager.phoneTransaction(phone.Number);
                            break;

                        case "Email":
                            notificationManager.transactionNotification(u.Email);
                            break;

                        case "Both":
                            notificationManager.phoneTransaction(phone.Number);
                            notificationManager.transactionNotification(u.Email);
                            break;

                    }
                }
           
            }
            WalletCrudFactory.Update(entity);
        }

        public void UpdateByUser(BaseEntity entity)
        {
            UserManager userManager = new UserManager();
            List<User> users = new List<User>();
            RoleXUserManager roleXUserManager = new RoleXUserManager();
            foreach (RoleXUser rolex in roleXUserManager.RetrieveAllRoleXUser())
            {
                foreach (User u in userManager.GetUsers())
                {
                    if (rolex.RoleId == 1 && u.Id == rolex.UserId)
                    {
                        users.Add(u);

                    }
                }
            }

            NotificationManager notificationManager = new NotificationManager();

            Wallet walletz = (Wallet)entity;

            PhoneManager phoneManager = new PhoneManager();
            Phone phone = new Phone();



            if (walletz.Id == "bcc5a83717d83a33fe776db644ad940e")
            {
                foreach (User u in users)
                {
                    foreach (Phone ph in phoneManager.GetPhones())
                    {
                        if (ph.User == u.Id)
                        {
                            phone = ph;
                        }
                    }
                    switch (u.PreferredMethod)
                    {
                        case "SMS":
                            notificationManager.phoneTransaction(phone.Number);
                            break;

                        case "Email":
                            notificationManager.transactionNotification(u.Email);
                            break;

                        case "Both":
                            notificationManager.phoneTransaction(phone.Number);
                            notificationManager.transactionNotification(u.Email);
                            break;

                    }
                }

            }
            var wallet = (Wallet)entity;
            Wallet result = WalletCrudFactory.RetrieveByUser<Wallet>(entity);
            wallet.Balance += result.Balance;
            WalletCrudFactory.UpdateByUser(wallet);
        }

        public object RetrieveByUser(Wallet w)
        {
            List<Wallet> result = WalletCrudFactory.RetrieveAll<Wallet>();
            foreach(Wallet wallet in result)
            {
                if(wallet.UserId == w.UserId)
                {
                    w = wallet;
                }
            }
            return w;
        }
    }
}
