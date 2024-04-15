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
    public class AutoBidMapper : EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_ACQUISITION = "ACQUISITION";
        private const string DB_COL_AMOUNT = "AMOUNT";
        private const string DB_COL_MAXAMOUNT = "MAXAMOUNT";
        private const string DB_COL_INCREMENT = "INCREMENT";
        private const string DB_COL_USERID = "USERID";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_AUTOBID_PR" };

            AutoBid c = (AutoBid)entity;
            operation.AddIntParam(DB_COL_ACQUISITION, c.AquisitionId);
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);
            operation.AddDecimalParam(DB_COL_MAXAMOUNT, c.MaxAmount);
            operation.AddDecimalParam(DB_COL_INCREMENT, c.Increment);
            operation.AddIntParam(DB_COL_USERID, c.UserId);
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_AUTOBID_PR" };

            AutoBid c = (AutoBid)entity;
            operation.AddIntParam(DB_COL_ID, c.AutoBidId);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_AUTOBID_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_AUTOBID_PR" };

            AutoBid c = (AutoBid)entity;
            operation.AddIntParam(DB_COL_ID, c.AutoBidId);
            operation.AddIntParam(DB_COL_ACQUISITION, c.AquisitionId);
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);
            operation.AddDecimalParam(DB_COL_MAXAMOUNT, c.MaxAmount);
            operation.AddDecimalParam(DB_COL_INCREMENT, c.Increment);
            operation.AddIntParam(DB_COL_USERID, c.UserId);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_AUTOBID_PR" };

            AutoBid c = (AutoBid)entity;
            operation.AddIntParam(DB_COL_ID, c.AutoBidId);
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

            AutoBid autobid = new AutoBid()
            {
                AutoBidId = GetIntValue(row, DB_COL_ID),
                AquisitionId = GetIntValue(row, DB_COL_ACQUISITION),
                Amount = GetDecimalValue(row, DB_COL_AMOUNT),
                MaxAmount = GetDecimalValue(row, DB_COL_MAXAMOUNT),
                Increment = GetDecimalValue(row, DB_COL_INCREMENT),
                UserId = GetIntValue(row, DB_COL_USERID),                

            };

            return autobid;
        }
    }
}
