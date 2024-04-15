using DataAcess.Dao;
using DataAcess.Mapper;
using DTO_POJOS;
using System;
using System.Collections.Generic;


namespace DataAccess.Mapper
{
    public class OrganizationMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NAME = "NAME";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_ORGANIZATION_PR" };

            Organization c = (Organization)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ORGANIZATION_PR" };

            Organization c = (Organization)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_ORGANIZATION_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_ORGANIZATION_PR" };

            Organization c = (Organization)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_ORGANIZATION_PR" };

            Organization c = (Organization)entity;
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

            Organization organization = new Organization()
            {
                Id =   GetIntValue(row, DB_COL_ID),
                Name = GetStringValue(row, DB_COL_NAME)
            };

            return organization;
        }
    }
}
