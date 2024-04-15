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
    public class NFTManager : BaseManager
    {

        private NFTCrudFactory crudNFT;

        public NFTManager()
        {
            crudNFT = new NFTCrudFactory();
        }

        public void CreateNft(NFT nft)
        {
            crudNFT.Create(nft);

            CollectionCrudFactory collectionCrudFactory = new CollectionCrudFactory();

            Collection collection = new Collection();


            collection.Id = nft.Collection;

            collection = collectionCrudFactory.Retrieve<Collection>(collection);

            Console.WriteLine(collection);

            UserActionManager um = new UserActionManager();

            UserAction ua = new UserAction()
            {
                User = collection.User,
                Date = new DateTime(),
                Type = "NFT created"
            };

            um.CreateUserAction(ua);


        }

        public APIResponse RetrieveOne(int id)
        {
            NFT nft = new NFT()
            {
                Id = id,
            }; 
            APIResponse response = new APIResponse() { Message = "NFT not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;
            var retrieved = crudNFT.Retrieve<NFT>(nft); 

            try
            {
                if (retrieved != null)
                {
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

        public APIResponse RetrieveAllNfts()
        {
            APIResponse response = new APIResponse() { Message = "NFT not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<NFT> nfts = crudNFT.RetrieveAll<NFT>();
            try
            {
                    if (nfts != null){
                        response.Status = 200;
                        response.Data = nfts;
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

        public APIResponse RetrieveActive()
        {
            APIResponse response = new APIResponse() { Message = "NFT not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            List<NFT> nfts = new List<NFT>();
            foreach(NFT nft in crudNFT.RetrieveAll<NFT>())
            {
                if(nft.Status == "On Sale")
                {
                    nfts.Add(nft);
                }
            }
                
            try
            {
                if (nfts != null)
                {
                    response.Status = 200;
                    response.Data = nfts;
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

        public APIResponse RetrieveNft(int nft)
        {
            APIResponse response = new APIResponse() { Message = "NFT not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            NFT nftP = new NFT()
            {
                Id = nft
            };

            var retrieveNTF = crudNFT.Retrieve<NFT>(nftP);

            try
            {

                if (retrieveNTF!=null) {
                     response.Status = 200;
                     response.Data = retrieveNTF;
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

        public APIResponse UpdateNFT(NFT nft)
        {
            APIResponse response = new APIResponse() { Message = "NFT not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieveNTF = crudNFT.Retrieve<NFT>(nft);

            try
            {

                if (retrieveNTF != null)
                    {
                        crudNFT.Update(nft);
                        retrieveNTF = crudNFT.Retrieve<NFT>(nft);
                        response.Status = 200;
                        response.Data = retrieveNTF;
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


        public APIResponse UpdateStatus(NFT nft)
        {
            APIResponse response = new APIResponse() { Message = "NFT not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;
            List<NFT> nfts = crudNFT.RetrieveAll<NFT>();
            NFT retrieveNTF = null;

            foreach (var item in nfts)
            {
                if (item.Id.Equals(nft.Id))
                {
                    retrieveNTF = item;
                }
            }
            try
            {

                if (retrieveNTF != null)
                {
                    retrieveNTF.Status = "On Sale"; 
                    crudNFT.Update(retrieveNTF);
                    response.Status = 200;
                    response.Data = retrieveNTF;
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

        public APIResponse DeleteNFT(NFT nft)
        {
            APIResponse response = new APIResponse() { Message = "NFT not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            var retrieveNTF = crudNFT.Retrieve<NFT>(nft);


            try
            {

                if (retrieveNTF != null){
                        crudNFT.Delete(nft);

                        response.Status = 200;
                        response.Data = retrieveNTF;
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
        
        public APIResponse DeleteNFTById(int id)
        {
            APIResponse response = new APIResponse() { Message = "NFT not found" };
            response.TransactionDate = DateTime.Now;
            response.Status = 404;

            NFT nft = new NFT()
            {
                Id = id
            }; 

            var retrieveNTF = crudNFT.Retrieve<NFT>(nft);

            try
            {

                if (retrieveNTF != null)
                {
                    crudNFT.Delete(retrieveNTF);

                    response.Status = 200;
                    response.Data = retrieveNTF;
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
        public APIResponse RetrieveNftsByUser(int id)
        {
            CollectionCrudFactory cm = new CollectionCrudFactory();
            NFTCrudFactory ncrud = new NFTCrudFactory();
            APIResponse response = new APIResponse();
            List<NFT> nftList = new List<NFT>();

            foreach(Collection col in cm.RetrieveAll<Collection>())
            {
                foreach (NFT nft in ncrud.RetrieveAll<NFT>())
                {
                    if (!nftList.Contains(nft) && col.User == id && nft.Collection == col.Id)
                    {
                        nftList.Add(nft);
                    }
                }
                
            }
            response.Data=nftList;
            return response;
        }

        public APIResponse RetrieveNftByName(string name)
        {
            
            NFTCrudFactory ncrud = new NFTCrudFactory();
            APIResponse response = new APIResponse();
            NFT foundNFT = new NFT();
            
                foreach (NFT nft in ncrud.RetrieveAll<NFT>())
                {
                    if (nft.Name == name)
                    {
                        foundNFT = nft;
                    };
                }

            response.Status = 200;
            response.Data = foundNFT;
            return response;
        }


        public APIResponse RetrieveByCollection (int collection)
        {
            NFTCrudFactory ncrud = new NFTCrudFactory();
            APIResponse response = new APIResponse();
            List<NFT> nftList = new List<NFT>();

            foreach (NFT nft in ncrud.RetrieveAll<NFT>())
            {
                if (nft.Collection==collection && nft.Status == "On Sale") {
                    nftList.Add (nft);
                }

            }
            response.Data = nftList;
            return response;
        }

        public APIResponse RetrieveByCollectionContent(int collection)
        {
            NFTCrudFactory ncrud = new NFTCrudFactory();
            APIResponse response = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            List<NFT> nftList = new List<NFT>();
            try
            {

                foreach (NFT nft in ncrud.RetrieveAll<NFT>())
                {
                    if (nft.Collection == collection)
                    {
                        nftList.Add(nft);
                    }

                }
                if(nftList != null)
                {
                    response.Data = nftList;
                    response.Status = 200;
                    response.Message = "Succesful"; 
                }

            }catch (Exception ex)
            {
                response.Status = 400;
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }

        public APIResponse RetrieveByCollectionAuction(int collection)
        {
            NFTCrudFactory ncrud = new NFTCrudFactory();
            APIResponse response = new APIResponse();
            List<NFT> nftList = new List<NFT>();

            foreach (NFT nft in ncrud.RetrieveAll<NFT>())
            {
                if (nft.Collection == collection && nft.Status == "Auction")
                {
                    nftList.Add(nft);
                }

            }
            response.Data = nftList;
            return response;
        }

    }
}
