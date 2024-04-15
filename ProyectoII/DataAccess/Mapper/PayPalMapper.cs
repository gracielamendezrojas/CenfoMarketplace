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
    public class PayPalMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_ORDERID = "ORDERID";
        private const string DB_COL_ORDERSTATUS = "ORDERSTATUS";
        private const string DB_COL_USERID = "USERID";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_PAYPAL_PR" };

            Paypal c = (Paypal)entity;
            operation.AddVarcharParam(DB_COL_ORDERID, c.OrderId);
            operation.AddIntParam(DB_COL_USERID, c.UserId); 
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_PAYPAL_PR" };

            Paypal c = (Paypal)entity;
            operation.AddIntParam(DB_COL_USERID, c.UserId);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_PAYPAL_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_PAYPAL_PR" };

            Paypal c = (Paypal)entity;
            operation.AddIntParam(DB_COL_USERID, c.UserId);
            operation.AddVarcharParam(DB_COL_ORDERID, c.OrderId);
            operation.AddVarcharParam(DB_COL_ORDERSTATUS, c.OrderStatus);


            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_PAYPAL_PR" };

            Paypal c = (Paypal)entity;
            operation.AddVarcharParam(DB_COL_ORDERID, c.OrderId);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Paypal = BuildObject(row);
                lstResults.Add(Paypal);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            Paypal paypal = new Paypal()
            {
                Id = GetIntValue(row, DB_COL_ID),
                OrderId = GetStringValue(row, DB_COL_ORDERID),
                OrderStatus = GetStringValue(row, DB_COL_ORDERSTATUS),
                UserId = GetIntValue(row,DB_COL_USERID ),
            };

            return paypal;
        }
    }
}
