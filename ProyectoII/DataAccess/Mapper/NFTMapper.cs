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
    public class NFTMapper : EntityMapper, IObjectMapper, ISqlStaments
    {

        private const string DB_COL_ID = "ID";
        private const string DB_COL_ROUTE = "ROUTE";
        private const string DB_COL_CATEGORY = "CATEGORY";
        private const string DB_COL_COLLECTION = "COLLECTION";
        private const string DB_COL_NAME = "NAME";
        private const string DB_COL_STATUS = "STATUS";
        private const string DB_COL_PRICE = "PRICE";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_NFT_PR" };

            NFT c = (NFT)entity;
            operation.AddVarcharParam(DB_COL_ROUTE, c.Route);
            operation.AddIntParam(DB_COL_CATEGORY, c.Category);
            operation.AddIntParam(DB_COL_COLLECTION, c.Collection);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            operation.AddVarcharParam(DB_COL_STATUS, c.Status);
            operation.AddDecimalParam(DB_COL_PRICE, c.Price);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {

            SqlOperation operation = new SqlOperation { ProcedureName = "RET_NFT_PR" };

            NFT c = (NFT)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;

        }

        public SqlOperation GetRetriveAllStatement()
        {

            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_NFT_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {

            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_NFT_PR" };

            NFT c = (NFT)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_ROUTE, c.Route);
            operation.AddIntParam(DB_COL_CATEGORY, c.Category);
            operation.AddIntParam(DB_COL_COLLECTION, c.Collection);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            operation.AddVarcharParam(DB_COL_STATUS, c.Status);
            operation.AddDecimalParam(DB_COL_PRICE, c.Price);

            return operation;
        }


        public SqlOperation GetUpdateStatusStatement(BaseEntity entity)
        {

            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_NFTSTATUS_PR" };

            NFT c = (NFT)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_STATUS, c.Status);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_NFT_PR" };

            NFT c = (NFT)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Org = BuildObject(row);
                lstResults.Add(Org);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            NFT nft = new NFT()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Route = GetStringValue(row, DB_COL_ROUTE),
                Category = GetIntValue(row, DB_COL_CATEGORY),
                Collection = GetIntValue(row, DB_COL_COLLECTION),
                Name = GetStringValue(row, DB_COL_NAME),
                Status = GetStringValue(row, DB_COL_STATUS),
                Price = GetDecimalValue(row, DB_COL_PRICE)

            };

            return nft;
        }

    }
}
