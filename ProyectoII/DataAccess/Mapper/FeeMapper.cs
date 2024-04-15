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
    public class FeeMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_AMOUNT = "AMOUNT";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_FEE_PR" };

            Fee c = (Fee)entity;
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_FEE_PR" };
            Fee c = (Fee)entity;
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_FEE_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_FEE_PR" };

            Fee c = (Fee)entity;
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_FEE_PR" };

            Fee c = (Fee)entity;
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var fee = BuildObject(row);
                lstResults.Add(fee);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            Fee fee = new Fee()
            {
                Amount = GetDecimalValue(row, DB_COL_AMOUNT),

            };

            return fee;
        }
    }
}
