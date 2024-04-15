using DataAcess.Dao;
using DTO_POJOS;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class RoleXUserMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_ROLEID = "ROLEID";
        private const string DB_COL_USERID = "USERID";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TBL_ROLEXUSER_PR" };
  
            var c = (RoleXUser)entity;
            operation.AddIntParam(DB_COL_ROLEID, c.RoleId);
            operation.AddIntParam(DB_COL_USERID, c.UserId);

            return operation;
        }
  
  
        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TBL_ROLEXUSER_PR" };
  
            var c = (RoleXUser)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        // Overload retrieve all roles by User
        public SqlOperation GetRetriveAllStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TBL_ROLE_BY_USER_PR" };
            var c = (RoleXUser)entity;
            operation.AddIntParam(DB_COL_USERID, c.UserId);
            return operation;
        }



        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TBL_ROLEXUSER_PR" };
            return operation;
        }
  
        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_TBL_ROLEXUSER_PR" };
  
            var c = (RoleXUser)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_ROLEID, c.RoleId);
            operation.AddIntParam(DB_COL_USERID, c.UserId);

            return operation;
        }
  
        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_TBL_ROLEXUSER_PR" };
  
            var c = (RoleXUser)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }
  
        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();
  
            foreach (var row in lstRows)
            {
                var ROLEXUSER = BuildObject(row);
                lstResults.Add(ROLEXUSER);
            }
            return lstResults;
        }
  
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var ROLEXUSER = new RoleXUser
            {
                Id = GetIntValue(row, DB_COL_ID),
                RoleId = GetIntValue(row, DB_COL_ROLEID),
                UserId = GetIntValue(row, DB_COL_USERID)
            };
  
            return ROLEXUSER;
        }
  
    }
}
