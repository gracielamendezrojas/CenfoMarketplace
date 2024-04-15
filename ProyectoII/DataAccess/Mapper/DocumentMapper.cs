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
    public class DocumentMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_FILEPATH = "FILEPATH";
        private const string DB_COL_DESCRIPTION = "DESCRIPTION";
        private const string DB_COL_SUBSCRIPTION = "SUBSCRIPTION";



        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_TBL_DOCUMENT_PR" };

            Document c = (Document)entity;
            operation.AddVarcharParam(DB_COL_FILEPATH, c.Filepath);
            operation.AddVarcharParam(DB_COL_DESCRIPTION, c.Description);
            operation.AddIntParam(DB_COL_SUBSCRIPTION,c.Subscription);
            return operation;

        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_DOCUMENT_PR" };

            Document c = (Document)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_DOCUMENT_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_DOCUMENT_PR" };

            Document c = (Document)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_FILEPATH, c.Filepath);
            operation.AddVarcharParam(DB_COL_DESCRIPTION, c.Description);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_DOCUMENT_PR" };

            Document c = (Document)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var document = BuildObject(row);
                lstResults.Add(document);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            Document document = new Document()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Filepath = GetStringValue(row, DB_COL_FILEPATH),
                Description = GetStringValue(row, DB_COL_DESCRIPTION),
                Subscription = GetIntValue(row, DB_COL_SUBSCRIPTION)
            };

            return document;
        }
    }
}
