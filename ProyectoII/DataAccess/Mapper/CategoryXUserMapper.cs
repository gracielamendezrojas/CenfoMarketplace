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
    public class CategoryXUserMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_USER = "USER";
        private const string DB_COL_CATEGORY = "CATEGORY";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_TBL_CATEGORYXUSER_PR" };

            CategoryXUser c = (CategoryXUser)entity;
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddIntParam(DB_COL_CATEGORY, c.Category);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_CATEGORYXUSER_PR" };

            CategoryXUser c = (CategoryXUser)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_CATEGORYXUSER_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_CATEGORYXUSER_PR" };

            CategoryXUser c = (CategoryXUser)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_CATEGORY, c.Category);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_CATEGORYXUSER_PR" };

            CategoryXUser c = (CategoryXUser)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var categoryXUser = BuildObject(row);
                lstResults.Add(categoryXUser);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            CategoryXUser categoryXuser = new CategoryXUser()
            {
                Id = GetIntValue(row, DB_COL_ID),
                User = GetIntValue(row, DB_COL_USER),
                Category = GetIntValue(row, DB_COL_CATEGORY)
            };

            return categoryXuser;
        }
    }
}
