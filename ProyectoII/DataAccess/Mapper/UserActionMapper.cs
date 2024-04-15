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
    public class UserActionMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_USER = "USER";
        private const string DB_COL_TYPE = "TYPE";
        private const string DB_COL_DATE = "DATE";




        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_TBL_USERACTION_PR" };

            UserAction c = (UserAction)entity;
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddDateTimeParam(DB_COL_DATE, c.Date);
            operation.AddVarcharParam(DB_COL_TYPE, c.Type);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_USERACTION_PR" };

            UserAction c = (UserAction)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_USERACTION_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_USERACTION_PR" };

            UserAction c = (UserAction)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddDateTimeParam(DB_COL_DATE, c.Date);
            operation.AddVarcharParam(DB_COL_TYPE, c.Type);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_USERACTION_PR" };

            UserAction c = (UserAction)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var userAction = BuildObject(row);
                lstResults.Add(userAction);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            UserAction userAction = new UserAction()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Type = GetStringValue(row, DB_COL_TYPE),
                Date = GetDateValue(row, DB_COL_DATE),
                User = GetIntValue(row, DB_COL_USER),

            };

            return userAction;
        }
    }
}
