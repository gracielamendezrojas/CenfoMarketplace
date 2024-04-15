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
    public class CategoryXUserManager : BaseManager
    {

        private CategoryXUserCrudFactory categoryXUserCrudFactory;

        public CategoryXUserManager()
        {
            categoryXUserCrudFactory = new CategoryXUserCrudFactory();
        }

        public void CreateCategoryXUser(CategoryXUser categoryXUser)
        {

            categoryXUserCrudFactory.Create(categoryXUser);
        }

        public APIResponse RetrieveAllCategoryXUser()
        {
            APIResponse response = new APIResponse() { Message = "Not found." };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<CategoryXUser> categoryXUser = categoryXUserCrudFactory.RetrieveAll<CategoryXUser>();
            try
            {
                    if (categoryXUser != null){
                        response.Status = 200;
                        response.Data = categoryXUser;
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

        public APIResponse RetrieveCategoryXUser(CategoryXUser categoryXUser)
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = categoryXUserCrudFactory.Retrieve<CategoryXUser>(categoryXUser);

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

        public APIResponse UpdateCategoryXuser(CategoryXUser categoryXUser)
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = categoryXUserCrudFactory.Retrieve<CategoryXUser>(categoryXUser);

            try
            {

                if (retrieved != null)
                {
                    categoryXUserCrudFactory.Update(categoryXUser);
                    response.Status = 200;
                    response.Data = categoryXUser;
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

        public APIResponse DeleteCategoryXUser(CategoryXUser categoryXUser)
        {
            APIResponse response = new APIResponse() { Message = " not found" };
            response.Status = 404;

            var retrieved = categoryXUserCrudFactory.Retrieve<CategoryXUser>(categoryXUser);


            try
            {
                if (retrieved != null){
                    categoryXUserCrudFactory.Delete(categoryXUser);
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
