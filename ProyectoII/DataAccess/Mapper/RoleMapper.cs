using DataAcess.Dao;
using DTO_POJOS;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class RoleMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NAME = "NAME";
  
  
        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TBL_ROLE_PR" };
  
            var c = (Role)entity;
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
  
            return operation;
        }
  
  
        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TBL_ROLE_PR" };
  
            var c = (Role)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
  
            return operation;
        }
  
        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TBL_ROLE_PR" };
            return operation;
        }
  
        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_TBL_ROLE_PR" };
  
            var c = (Role)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
  
            return operation;
        }
  
        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_TBL_ROLE_PR" };
  
            var c = (Role)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }
  
        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();
  
            foreach (var row in lstRows)
            {
                var Role = BuildObject(row);
                lstResults.Add(Role);
            }
            return lstResults;
        }
  
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Role = new Role
            {
                Id = GetIntValue(row, DB_COL_ID),
                Name = GetStringValue(row, DB_COL_NAME),
            };
  
            return Role;
        }
  
    }
}
