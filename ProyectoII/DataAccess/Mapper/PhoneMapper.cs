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
    public class PhoneMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NUMBER = "NUMBER";
        private const string DB_COL_DESCRIPTION = "DESCRIPTION";
        private const string DB_COL_USER = "USER";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_PHONE_PR" };

            Phone c = (Phone)entity;
            operation.AddIntParam(DB_COL_NUMBER, c.Number);
            operation.AddVarcharParam(DB_COL_DESCRIPTION, c.Description);
            operation.AddIntParam(DB_COL_USER, c.User);


            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_PHONE_PR" };

            Phone c = (Phone)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_PHONE_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_PHONE_PR" };

            Phone c = (Phone)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddIntParam(DB_COL_NUMBER, c.Number);
            operation.AddVarcharParam(DB_COL_DESCRIPTION, c.Description);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_PHONE_PR" };

            Phone c = (Phone)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Phone = BuildObject(row);
                lstResults.Add(Phone);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            Phone phone = new Phone()
            {
                Id = GetIntValue(row, DB_COL_ID),
                Number = GetIntValue(row, DB_COL_NUMBER),
                Description = GetStringValue(row, DB_COL_DESCRIPTION),
                User = GetIntValue(row, DB_COL_USER)
            };

            return phone;
        }
    }
}
