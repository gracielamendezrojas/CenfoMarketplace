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
    public class NotificationMapper : EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_USER = "USER";
        private const string DB_COL_MESSAGE = "MESSAGE";
        private const string DB_COL_SUBJECT = "SUBJECT";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_TBL_NOTIFICATIONS_PR" };

            Notification c = (Notification)entity;
            //operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddVarcharParam(DB_COL_MESSAGE, c.Message);
            operation.AddVarcharParam(DB_COL_SUBJECT, c.Subject);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_NOTIFICATIONS_PR" };

            Notification c = (Notification)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_NOTIFICATIONS_PR" };
            return operation;
        }

        public SqlOperation GetRetriveAllUserIdStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALLUSERID_NOTIFICATIONS_PR" };
            Notification c = (Notification)entity;
            operation.AddIntParam(DB_COL_USER, c.User);
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_NOTIFICATIONS_PR" };

            Notification c = (Notification)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddVarcharParam(DB_COL_MESSAGE, c.Message);
            operation.AddVarcharParam(DB_COL_SUBJECT, c.Subject);


            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_NOTIFICATIONS_PR" };

            Notification c = (Notification)entity;
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

            Notification organization = new Notification()
            {
                Id = GetIntValue(row, DB_COL_ID),
                User = GetIntValue(row, DB_COL_USER),
                Message = GetStringValue(row, DB_COL_MESSAGE),
                Subject = GetStringValue(row, DB_COL_SUBJECT),
            };

            return organization;
        }

    }
}
