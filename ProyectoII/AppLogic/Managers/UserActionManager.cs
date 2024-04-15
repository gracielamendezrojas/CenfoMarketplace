using AppLogic.Managers;
using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class UserActionManager : BaseManager
    {

        private UserActionCrudFactory userActionCrudFactory;

        public UserActionManager()
        {
            userActionCrudFactory = new UserActionCrudFactory();
        }

        public void CreateUserAction(UserAction userAction)
        {
            var date = DateTime.Now;
            userAction.Date = date;

            userActionCrudFactory.Create(userAction);
        }

        public APIResponse RetrieveAllUserAction()
        {
            APIResponse response = new APIResponse() { Message = "UserActions not found." };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<UserAction> userAction = userActionCrudFactory.RetrieveAll<UserAction>();
            try
            {
                    if (userAction != null){
                        response.Status = 200;
                        response.Data = userAction;
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

        public APIResponse RetrieveUserAction(UserAction userAction)
        {
            APIResponse response = new APIResponse() { Message = "UserAction not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = userActionCrudFactory.Retrieve<UserAction>(userAction);

            try
            {

                if (retrieved != null) {
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

        public APIResponse UpdateUserAction(UserAction userAction)
        {
            APIResponse response = new APIResponse() { Message = "UserAction not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = userActionCrudFactory.Retrieve<UserAction>(userAction);

            userAction.Date = DateTime.Now;
            try
            {

                if (retrieved != null)
                {
                    userActionCrudFactory.Update(userAction);
                    response.Status = 200;
                    response.Data = userAction;
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

        public APIResponse DeleteUserAction(UserAction userAction)
        {
            APIResponse response = new APIResponse() { Message = "UserAction not found" };
            response.Status = 404;

            var retrieved = userActionCrudFactory.Retrieve<UserAction>(userAction);


            try
            {
                if (retrieved != null){
                    userActionCrudFactory.Delete(userAction);

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
    }
}
