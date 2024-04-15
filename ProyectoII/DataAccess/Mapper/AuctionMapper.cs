using DataAcess.Dao;
using DataAcess.Mapper;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class AuctionMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ACQUISITIONID = "ACQUISITIONID";
        private const string DB_COL_CREATIONDATE = "CREATIONDATE";
        private const string DB_COL_CLOSINGDATE = "CLOSINGDATE";
        private const string DB_COL_AUCTIONPRICE = "AUCTIONPRICE";
        private const string DB_COL_BUYER = "BUYER";
        private const string DB_COL_CREATOR = "CREATOR";
        private const string DB_COL_NFT = "NFT";
        private const string DB_COL_NFTNAME = "NFTNAME";
        private const string DB_COL_NFTSTATUS = "NFTSTATUS";
        private const string DB_COL_NFTPRICE = "NFTPRICE";
        private const string DB_COL_COLLECTION = "COLLECTION";
        private const string DB_COL_COLLECTIONNAME = "COLLECTIONNAME";
        private const string DB_COL_COLLECTIONSALESTATUS = "COLLECTIONSALESTATUS";

        //no está implementado
        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_AUCTION_PR" };

            Auction c = (Auction)entity;
            operation.AddDateTimeParam(DB_COL_CREATIONDATE, c.CreationDate);
            operation.AddDateTimeParam(DB_COL_CLOSINGDATE, c.ClosingDate);
            operation.AddIntParam(DB_COL_CREATOR, c.Creator);
            operation.AddIntParam(DB_COL_BUYER, c.Buyer);
            return operation;
        }

        //no implementado
        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_AUCTION_PR" };

            Auction c = (Auction)entity;

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_AUCTION_PR" };
            return operation;
        }

        //no implementado
        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_AUCTION_PR" };

            Auction c = (Auction)entity;
            operation.AddDateTimeParam(DB_COL_CREATIONDATE, c.CreationDate);
            operation.AddDateTimeParam(DB_COL_CLOSINGDATE, c.ClosingDate);
            operation.AddIntParam(DB_COL_BUYER, c.Buyer);
            operation.AddIntParam(DB_COL_CREATOR, c.Creator);

            return operation;
        }
        //no implementado
        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_AUCTION_PR" };

            Auction c = (Auction)entity;
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Auction = BuildObject(row);
                lstResults.Add(Auction);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            Auction auction = new Auction()
            {
                AcquisitionId = GetIntValue(row, DB_COL_ACQUISITIONID),
                CreationDate = GetDateValue(row, DB_COL_CREATIONDATE),
                ClosingDate = GetDateValue(row, DB_COL_CLOSINGDATE),
                AuctionPrice = GetDecimalValue(row, DB_COL_AUCTIONPRICE),
                Buyer = GetIntValue(row, DB_COL_BUYER),
                Creator = GetIntValue(row, DB_COL_CREATOR),
                NFT = GetIntValue(row, DB_COL_NFT),
                NFTName = GetStringValue(row, DB_COL_NFTNAME),
                NFTStatus = GetStringValue(row, DB_COL_NFTSTATUS),  
                NFTPrice = GetDecimalValue(row, DB_COL_NFTPRICE),
                Collection = GetIntValue(row, DB_COL_COLLECTION),
                CollectionName = GetStringValue(row, DB_COL_COLLECTIONNAME),
                CollectionSaleStatus = GetStringValue(row, DB_COL_COLLECTIONSALESTATUS),
            };

            return auction;
        }

    }
}
