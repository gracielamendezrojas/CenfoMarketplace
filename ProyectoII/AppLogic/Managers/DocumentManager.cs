using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class DocumentManager : BaseManager
    {
        private DocumentCrudFactory documentCrudFactory;

        public DocumentManager()
        {
            documentCrudFactory = new DocumentCrudFactory();
        }

        public void CreateDocument(Document document)
        {
            documentCrudFactory.Create(document);
        }

        public APIResponse RetrieveAllDocuments()
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<Document> documents = documentCrudFactory.RetrieveAll<Document>();
            try
            {
                if (documents != null)
                {
                    response.Status = 200;
                    response.Data = documents;
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

        public APIResponse RetrieveDocument(Document document)
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            Document doc = documentCrudFactory.Retrieve<Document>(document);

            try
            {
                if (doc != null)
                {
                    response.Status = 200;
                    response.Data = doc;
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

        public APIResponse UpdateDocument(Document document)
        {

            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = documentCrudFactory.Retrieve<Document>(document);

            try
            {

                if (retrieved != null)
                {
                    documentCrudFactory.Update(document);
                    retrieved = documentCrudFactory.Retrieve<Document>(document);
                    response.Status = 200;
                    response.Data = retrieved;
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

        public APIResponse DeleteDocument(Document document)
        {
            APIResponse response = new APIResponse() { Message = "Not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieved = documentCrudFactory.Retrieve<Document>(document);


            try
            {

                if (retrieved != null)
                {
                    documentCrudFactory.Delete(document);

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
