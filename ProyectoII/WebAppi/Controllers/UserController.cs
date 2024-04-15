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
    public class UserController : ApiController
    {
        UserManager sm = new UserManager();
        NFTManager nm = new NFTManager();
        CollectionManager cm = new CollectionManager();

        public APIResponse Post(User sus)
        {
            //User sus = new User()
            //{
            //    Id = id,
            //    Name = name,
            //    LastName = lastname,
            //    NickName = nickname,
            //    PreferredMethod = preferredmethod,
            //    Avatar = avatar,
            //    Status = status
            //};
            sm.CreateUser(sus);
            APIResponse api = new APIResponse();
            api.Message = "Registration successful";
            return api;
        }

        public APIResponse CreateUserPhone(Customer sus)
        {
            return sm.CreateUserandPhone(sus);
        }

        [HttpGet]
        public APIResponse GetAll()
        {
            APIResponse api = new APIResponse();
            api.Data = sm.GetUsers();
            return api;
        }

        [HttpGet]
        public APIResponse Get(int id)
        {
            User sus = new User()
            {
                Id = id
            };

            APIResponse api = new APIResponse()
            {
                Data = sm.GetUser(sus)
            };


            if (api.Data == null)
            {
                api.Status = 404;
            }
            else
            {
                api.Status = 200;
            }

            return api;
        }

        [HttpPut]
        public APIResponse Edit(User sus)
        {
            return sm.EditUser(sus);
        }



        [HttpPut]
        public APIResponse EditStatus(Customer customer)
        {
            sm.EditUserStatus(customer);
            APIResponse api = new APIResponse()
            {
                Message = "User status stupdated."
            };
            return api;
        }


        [HttpPut]
        public APIResponse EditNotificationMethod(User user)
        {
            return sm.EditUserNotification(user);
        }
        [HttpDelete]
        public APIResponse Delete(int id)
        {
            User sus = new User()
            {
                Id = id
            };
            sm.DeleteUser(sus);
            APIResponse api = new APIResponse()
            {
                Message = "User deleted"
            };
            return api;
        }

        [HttpPost]
        public APIResponse Login(Login login)
        {

            return sm.Login(login);
        }

        [HttpGet]
        public APIResponse GetUserbyID(int id)
        {
            APIResponse api = Get(id);

            if (api.Status.Equals(200))
            { 
                api.Message = "Valid User";
                api.TransactionDate = DateTime.Now.ToString();
            }


            return api;
        }


        [HttpGet]
        public APIResponse GetbyNFT(int id)
        {
            
            NFT nft = (NFT)nm.RetrieveNft(id).Data;
            Collection collection = new Collection(); 
            collection.Id = nft.Collection;
            collection =  (Collection)cm.RetrieveCollection(collection).Data;


            User sus = new User()
            {
                Id = collection.User
            };

            APIResponse api = new APIResponse()
            {
                Data = sm.GetUser(sus)
            };


            if (api.Data == null)
            {
                api.Status = 404;
            }
            else
            {
                api.Status = 200;
            }

            return api;
        }
    }
}

