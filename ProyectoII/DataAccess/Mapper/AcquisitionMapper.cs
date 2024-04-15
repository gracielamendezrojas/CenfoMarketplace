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
    public class AcquisitionMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_CREATIONDATE = "CREATIONDATE";
        private const string DB_COL_CLOSINGDATE = "CLOSINGDATE";
        private const string DB_COL_PRICE = "PRICE";
        private const string DB_COL_BUYER = "BUYER";
        private const string DB_COL_CREATOR = "CREATOR";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_ACQUISITION_PR" };

            Acquisition c = (Acquisition)entity;
            operation.AddDateTimeParam(DB_COL_CREATIONDATE, c.CreationDate);
            operation.AddDateTimeParam(DB_COL_CLOSINGDATE, c.ClosingDate);
            operation.AddDecimalParam(DB_COL_PRICE, c.Price);
            operation.AddIntParam(DB_COL_CREATOR, c.Creator);
            operation.AddIntParam(DB_COL_BUYER, c.Buyer);
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ACQUISITION_PR" };

            Acquisition c = (Acquisition)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_ACQUISITION_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_ACQUISITION_PR" };

            Acquisition c = (Acquisition)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddDateTimeParam(DB_COL_CREATIONDATE, c.CreationDate);
            operation.AddDateTimeParam(DB_COL_CLOSINGDATE, c.ClosingDate);
            operation.AddDecimalParam(DB_COL_PRICE, c.Price);
            operation.AddIntParam(DB_COL_BUYER, c.Buyer);
            operation.AddIntParam(DB_COL_CREATOR, c.Creator);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_ACQUISITION_PR" };

            Acquisition c = (Acquisition)entity;
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

            Acquisition organization = new Acquisition()
            {
                Id = GetIntValue(row, DB_COL_ID),
                CreationDate = GetDateValue(row, DB_COL_CREATIONDATE),
                ClosingDate = GetDateValue(row, DB_COL_CLOSINGDATE),
                Price = GetDecimalValue(row, DB_COL_PRICE),
                Buyer = GetIntValue(row, DB_COL_BUYER),
                Creator = GetIntValue(row, DB_COL_CREATOR)
                
            };

            return organization;
        }

    }
}
