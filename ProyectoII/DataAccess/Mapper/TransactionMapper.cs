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
    public class TransactionMapper : EntityMapper,IObjectMapper,ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_ACQUISITION = "ACQUISITION";
        

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_TBL_TRANSACTION_PR" };

            Transaction c = (Transaction)entity;
            //operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_ACQUISITION, c.Acquisition);
            
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_TRANSACTION_PR" };

            Transaction c = (Transaction)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_TRANSACTION_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_TRANSACTION_PR" };

            Transaction c = (Transaction)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_ACQUISITION, c.Acquisition);            

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_TRANSACTION_PR" };

            Transaction c = (Transaction)entity;
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

            Transaction organization = new Transaction()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Acquisition = GetIntValue(row, DB_COL_ACQUISITION),              

            };

            return organization;
        }
    }
}
