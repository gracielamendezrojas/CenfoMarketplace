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
    public class CategoryMapper : EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NAME = "NAME";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_CATEGORY_PR" };

            Category c = (Category)entity;
            operation.AddVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {

            SqlOperation operation = new SqlOperation { ProcedureName = "RET_CATEGORY_PR" };

            Category c = (Category)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;

        }

        public SqlOperation GetRetriveAllStatement()
        {

            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_CATEGORY_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {

            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_CATEGORY_PR" };

            Category c = (Category)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_CATEGORY_PR" };

            Category c = (Category)entity;
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

            Category category = new Category()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Name = GetStringValue(row, DB_COL_NAME),

            };

            return category;
        }




    }
}
