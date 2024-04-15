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
    public class SuscriptionMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_USER = "USER";
        private const string DB_COL_SUSCRIPTIONDATE = "SUSCRIPTIONDATE";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_TBL_SUSCRIPTION_PR" };

            Suscription c = (Suscription)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_USER, c.User);
            operation.AddDateTimeParam(DB_COL_SUSCRIPTIONDATE, c.SuscriptionDate);
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_SUSCRIPTION_PR" };

            Suscription c = (Suscription)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_SUSCRIPTION_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_SUSCRIPTION_PR" };

            Suscription c = (Suscription)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddDateTimeParam(DB_COL_SUSCRIPTIONDATE, c.SuscriptionDate);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_SUSCRIPTION_PR" };

            Suscription c = (Suscription)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var suscription = BuildObject(row);
                lstResults.Add(suscription);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            Suscription suscription = new Suscription()
            {
                Id = GetIntValue(row, DB_COL_ID),
                SuscriptionDate = GetDateValue(row, DB_COL_SUSCRIPTIONDATE),
                User = GetIntValue(row, DB_COL_USER),

            };

            return suscription;
        }
    }
}
