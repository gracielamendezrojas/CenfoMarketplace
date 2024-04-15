using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class CollectionManager : BaseManager
    {
        private CollectionCrudFactory crudCollection;

        public CollectionManager()
        {
            crudCollection = new CollectionCrudFactory();
        }

        public void CreateCollection(Collection collection)
        {

            UserActionManager um = new UserActionManager();
            UserAction ua = new UserAction()
            {
                User = collection.User,
                Date = new DateTime(),
                Type = "Collection created"
            };
            um.CreateUserAction(ua);


            crudCollection.Create(collection);
        }

        public APIResponse RetrieveAllCollections()
        {
            APIResponse response = new APIResponse() { Message = "API ERR" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<Collection> categories = crudCollection.RetrieveAll<Collection>();
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
        public APIResponse defaultCollectionBuyer(int buyer)
        {
            APIResponse response = new APIResponse()
            {
                TransactionDate = new DateTime(),
            };
            List<Collection> list = crudCollection.RetrieveAll<Collection>();
            
            try
            {
                foreach (Collection collection in list)
                {
                    if (collection.User.Equals(buyer))
                    {
                        response.Data = collection;
                        break;
                    }
                }
                response.Message = "Succesful";
                response.Status = 200;
            }
            catch (Exception ex)
            {
                response.Message = "error";
                response.Status = 400;
                response.Data = ex; 
            }
            
            return response;
        }

        public APIResponse RetrieveCollectionById(int id)
        {
            APIResponse response = new APIResponse()
            {
                TransactionDate = new DateTime(),
            };

            try
            {
                List<Collection> list = crudCollection.RetrieveAll<Collection>();
                foreach(Collection collection in list)
                {
                    if (collection.Id.Equals(id))
                    {
                        response.Data = collection;
                        break; 
                    }
                }

                response.Status = 200;
                response.Message = "Error";
            }
            catch (Exception ex)
            {
                response.Status = 400; 
                response.Data = ex;
                response.Message = "Error"; 
            }
            return response; 
        }
        public APIResponse RetrieveCollection(Collection col)
        {
            APIResponse response = new APIResponse() { Message = "Collection not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            Collection tempcol = crudCollection.Retrieve<Collection>(col);

            try
            {
                if (tempcol != null)
                {
                    response.Status = 200;
                    response.Data = tempcol;
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

        public APIResponse UpdateCollection(Collection col)
        {
            
            NFTManager nFTManager = new NFTManager();

            APIResponse response = new APIResponse() { Message = "Collection not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            Collection tempcol = crudCollection.Retrieve<Collection>(col);

            if(col.SaleStatus == "Not On Sale")
            {
                foreach(NFT nft in (List<NFT>)nFTManager.RetrieveByCollection(col.Id).Data)
                {
                    nft.Status = "Not On Sale";
                    nFTManager.UpdateNFT(nft);
                }
            }

            try
            {

                if (tempcol != null)
                {
                    crudCollection.Update(col);
                    tempcol = crudCollection.Retrieve<Collection>(col);
                    response.Status = 200;
                    response.Data = tempcol;
                    response.Message = "Update Sucessfull";

                    UserActionManager userActionManager = new UserActionManager();
                    UserAction userAction = new UserAction()
                    {
                        User = col.User, 
                        Date = new DateTime(),
                        Type = "Auction created"
                    };
                    userActionManager.CreateUserAction(userAction);

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



        public APIResponse UpdateCollectionStatus(Collection col)
        {

            APIResponse response = new APIResponse() { Message = "Collection not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            Collection tempcol = crudCollection.Retrieve<Collection>(col);

            try
            {

                if (tempcol != null)
                {
                    tempcol.SaleStatus = "On Sale"; 
                    crudCollection.Update(tempcol);
                    response.Status = 200;
                    response.Data = tempcol;
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

        public APIResponse DeleteCollection(Collection col)
        {
            APIResponse response = new APIResponse() { Message = "Collection not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            Collection tempcol = crudCollection.Retrieve<Collection>(col);


            try
            {

                if (tempcol != null)
                {
                    crudCollection.Delete(col);

                    response.Status = 200;
                    response.Data = tempcol;
                    response.Message = "Delete Sucessfull";
                }

            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = "ERR";
                response.Message = "Error when process the request";

            }

            UserActionManager um = new UserActionManager();
            UserAction ua = new UserAction()
            {
                User = col.User,
                Date = new DateTime(),
                Type = "User " + col.User + " created"
            };
            um.CreateUserAction(ua);

            return response;

        }

        public APIResponse DeleteCollectionById(int id)
        {
            APIResponse response = new APIResponse() {
                Message = "Collection not found",
                TransactionDate = DateTime.Now,
                Status = 400,
            };

            Collection collection = new Collection()
            {
                Id = id,
            }; 
        
            Collection retrieved = crudCollection.Retrieve<Collection>(collection);
            try
            {
                if (retrieved != null)
                {
                    crudCollection.Delete(retrieved);

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



        public APIResponse RetrieveAllByUser(int Id)
        {
            APIResponse response = new APIResponse();

            List<Collection> list = new List<Collection>();
            foreach(Collection col in crudCollection.RetrieveAll<Collection>())
            {
                if(col.User == Id)
                {
                    list.Add(col);
                }

            }
            response.Data = list;
            return response;

        }

        public APIResponse RetrieveAllOnSale()
        {
            APIResponse response = new APIResponse();

            NFTManager nm = new NFTManager();

            List<Collection> list = new List<Collection>();
            foreach (Collection col in crudCollection.RetrieveAll<Collection>())
            {
                if (col.SaleStatus == "On Sale")
                {
                    list.Add(col);
                }

            }

            List<Collection> responseList = new List<Collection>();
            foreach (Collection collection in list)
            {
                if(((List<NFT>)nm.RetrieveByCollection(collection.Id).Data).Count > 0)
                {
                    responseList.Add(collection);
                }
            }
            response.Data = responseList;
            return response;

        }

        public APIResponse RetrieveByName(string name)
        {
            APIResponse response = new APIResponse();

            Collection collection = new Collection();
            foreach (Collection col in crudCollection.RetrieveAll<Collection>())
            {
                if (col.Name == name)
                {
                    collection = col;
                }
            }
            response.Data = collection;
            if(collection != null)
            {
                response.Status = 200;
            }
            return response;

        }

    }
}
