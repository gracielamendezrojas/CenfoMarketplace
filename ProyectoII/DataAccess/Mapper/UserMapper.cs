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
    public class UserMapper : EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NAME = "NAME";
        private const string DB_COL_LASTNAME = "LASTNAME";
        private const string DB_COL_NICKNAME = "NICKNAME";
        private const string DB_COL_EMAIL = "EMAIL";
        private const string DB_COL_PREFERREDMETHOD = "PREFERREDMETHOD";
        private const string DB_COL_AVATAR = "AVATAR";
        private const string DB_COL_STATUS = "STATUS";
        private const string DB_COL_DOB = "DOB";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_USER_PR" };

            User c = (User)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            operation.AddVarcharParam(DB_COL_LASTNAME, c.LastName);
            operation.AddVarcharParam(DB_COL_NICKNAME, c.NickName);
            operation.AddVarcharParam(DB_COL_EMAIL, c.Email);
            operation.AddVarcharParam(DB_COL_PREFERREDMETHOD, c.PreferredMethod);
            operation.AddVarcharParam(DB_COL_AVATAR, c.Avatar);
            operation.AddDateTimeParam(DB_COL_DOB, c.DOB);
            operation.AddVarcharParam(DB_COL_STATUS, c.Status);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_USER_PR" };

            User c = (User)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveByEmailStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_USER_BY_EMAIL_PR" };

            User c = (User)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, c.Email);

            return operation;
        }



        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_TBL_USER_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_USER_PR" };

            User c = (User)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            operation.AddVarcharParam(DB_COL_LASTNAME, c.LastName);
            operation.AddVarcharParam(DB_COL_NICKNAME, c.NickName);
            operation.AddVarcharParam(DB_COL_EMAIL, c.Email);
            operation.AddVarcharParam(DB_COL_PREFERREDMETHOD, c.PreferredMethod);
            operation.AddVarcharParam(DB_COL_AVATAR, c.Avatar);
            operation.AddDateTimeParam(DB_COL_DOB, c.DOB);
            operation.AddVarcharParam(DB_COL_STATUS, c.Status);




            return operation;
        }
        public SqlOperation GetUpdateStatementStatus(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_USERSTATUS_PR" };

            User c = (User)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_STATUS, c.Status);

            return operation;
        }
        public SqlOperation GetUpdateNotificationStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_USERNOTIFICATION_PR" };

            User c = (User)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_PREFERREDMETHOD, c.PreferredMethod);

            return operation;
        }


        
        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_USER_PR" };

            Organization c = (Organization)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var user = BuildObject(row);
                lstResults.Add(user);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            User organization = new User()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Name = GetStringValue(row, DB_COL_NAME),
                LastName = GetStringValue(row, DB_COL_LASTNAME),
                NickName = GetStringValue(row, DB_COL_NICKNAME),
                Email = GetStringValue(row, DB_COL_EMAIL),
                PreferredMethod = GetStringValue(row, DB_COL_PREFERREDMETHOD),
                Avatar = GetStringValue(row, DB_COL_AVATAR),
                Status = GetStringValue(row, DB_COL_STATUS),
                DOB = GetDateValue(row, DB_COL_DOB)
            };

            return organization;
        }
    }
}
