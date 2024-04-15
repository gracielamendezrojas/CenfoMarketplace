using DataAcess.Crud;
using DTO_POJOS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class CategoryManager : BaseManager
    {
        private CategoryCrudFactory crudCategory;

        public CategoryManager()
        {
            crudCategory = new CategoryCrudFactory();
        }

        public void CreateCategory(JObject json)
        {

            Category category = new Category()
            {
                Name = (string)json["Name"]
            };

            crudCategory.Create(category);

            UserActionManager um = new UserActionManager();
            UserAction ua = new UserAction()
            {
                User = (int)json["User"],
                Date = new DateTime(),
                Type = "Category created"
            };

            um.CreateUserAction(ua);
        }

        public APIResponse RetrieveAllCategories()
        {
            APIResponse response = new APIResponse() { Message = "Category not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<Category> categories = crudCategory.RetrieveAll<Category>();
            try
            {
                if (categories != null)
                {
                    response.Status = 200;
                    response.Data = categories;
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
        //comentario

        public APIResponse RetrieveCategory(Category category)
        {
            APIResponse response = new APIResponse() { Message = "Category not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            Category cat = crudCategory.Retrieve<Category>(category);

            try
            {
                if (cat != null)
                {
                    response.Status = 200;
                    response.Data = cat;
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

        public APIResponse UpdateCategory(Category category)
        {

            APIResponse response = new APIResponse() { Message = "Category not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieveCategory = crudCategory.Retrieve<Category>(category);

            try
            {

                if (retrieveCategory != null)
                {
                    crudCategory.Update(category);
                    retrieveCategory = crudCategory.Retrieve<Category>(category);
                    response.Status = 200;
                    response.Data = retrieveCategory;
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

        public APIResponse DeleteCategory(Category category)
        {
            APIResponse response = new APIResponse() { Message = "Category not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieveCategory = crudCategory.Retrieve<Category>(category);


            try
            {

                if (retrieveCategory != null)
                {
                    crudCategory.Delete(category);

                    response.Status = 200;
                    response.Data = retrieveCategory;
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
