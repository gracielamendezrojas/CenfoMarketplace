using DataAcess.Dao;
using DataAcess.Mapper;
using DTO_POJOS;
using System;
using System.Collections.Generic;


namespace DataAccess.Mapper
{
    public class BidMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_AMOUNT = "AMOUNT";
        private const string DB_COL_DATE = "DATE";
        private const string DB_COL_ACQUISITION = "ACQUISITION";
        private const string DB_COL_USER = "USER";
        private const string DB_COL_TYPE = "TYPE";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_BID_PR" };

            Bid c = (Bid)entity;
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);
            operation.AddDateTimeParam(DB_COL_DATE, c.Date);
            operation.AddIntParam(DB_COL_ACQUISITION, c.Acquisition);
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddIntParam(DB_COL_TYPE, c.Type);
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_BID_PR" };

            Bid c = (Bid)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_BID_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_BID_PR" };

            Bid c = (Bid)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);
            operation.AddDateTimeParam(DB_COL_DATE, c.Date);
            operation.AddIntParam(DB_COL_ACQUISITION, c.Acquisition);
            operation.AddIntParam(DB_COL_USER, c.User);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_BID_PR" };

            Bid c = (Bid)entity;
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

            Bid organization = new Bid()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Amount = GetDecimalValue(row, DB_COL_AMOUNT),
                Date = GetDateValue(row, DB_COL_DATE),
                Acquisition = GetIntValue(row,DB_COL_ACQUISITION),
                User = GetIntValue(row,DB_COL_USER),
                Type = GetIntValue(row, DB_COL_TYPE)
            };

            return organization;
        }
    }
}
