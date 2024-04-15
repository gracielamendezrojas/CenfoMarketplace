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
    public class CollectionMapper : EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NAME = "NAME";
        private const string DB_COL_STATUS = "STATUS";
        private const string DB_COL_DESCRIPTION = "DESCRIPTION";
        private const string DB_COL_USER = "USER";
        private const string DB_COL_SALESTATUS= "SALESTATUS";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_COLLECTION_PR" };

            Collection c = (Collection)entity;         
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            operation.AddVarcharParam(DB_COL_STATUS, c.Status);
            operation.AddVarcharParam(DB_COL_DESCRIPTION, c.Description);
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddVarcharParam(DB_COL_SALESTATUS, c.SaleStatus);
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity){

            SqlOperation operation = new SqlOperation { ProcedureName = "RET_COLLECTION_PR" };

            Collection c = (Collection)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
            
        }

        public SqlOperation GetRetriveAllStatement(){

            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_COLLECTION_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity){

            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_COLLECTION_PR" };

            Collection c = (Collection)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            operation.AddVarcharParam(DB_COL_STATUS, c.Status);
            operation.AddVarcharParam(DB_COL_DESCRIPTION, c.Description);
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddVarcharParam(DB_COL_SALESTATUS, c.SaleStatus);

            return operation;            
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity){
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_COLLECTION_PR" };

            Collection c = (Collection)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows){
                var Org = BuildObject(row);
                lstResults.Add(Org);
            }

            return lstResults;            
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            Collection collection = new Collection()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Name = GetStringValue(row, DB_COL_NAME),
                Status = GetStringValue(row, DB_COL_STATUS),
                Description = GetStringValue(row, DB_COL_DESCRIPTION),
                SaleStatus = GetStringValue(row, DB_COL_SALESTATUS),
                User = GetIntValue(row, DB_COL_USER)

            };

            return collection;
        }

        
    }
}
