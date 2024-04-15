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
        public class PasswordMapper : EntityMapper, IObjectMapper, ISqlStaments
        {
            private const string DB_COL_ID = "ID";
            private const string DB_COL_PASSWORDD = "PASSWORD";
            private const string DB_COL_CREATIONDATE = "CREATIONDATE";
            private const string DB_COL_USER = "USER";

            public SqlOperation GetCreateStatement(BaseEntity entity)
            {
                SqlOperation operation = new SqlOperation { ProcedureName = "CRE_PASSWORD_PR" };

                Password c = (Password)entity;
                operation.AddVarcharParam(DB_COL_PASSWORDD, c.Passwordd);
                operation.AddDateTimeParam(DB_COL_CREATIONDATE, c.CreationDate);
                operation.AddIntParam(DB_COL_USER, c.User);
                return operation;
            }


            public SqlOperation GetRetriveStatement(BaseEntity entity)
            {
                SqlOperation operation = new SqlOperation { ProcedureName = "RET_PASSWORD_PR" };

                Password c = (Password)entity;
                operation.AddIntParam(DB_COL_ID, c.Id);

                return operation;
            }

        public SqlOperation GetRetriveAllStatement()
            {
                SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_PASSWORD_PR" };
                return operation;
            }

            // OverCharge to return a list of password for one spcific user
            public SqlOperation GetRetriveAllStatement(BaseEntity entity)
            {
                SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_PASSWORD_BY_USER_PR" };
                Password c = (Password)entity;
                operation.AddIntParam(DB_COL_USER, c.User);
                return operation;
            }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
            {
                SqlOperation operation = new SqlOperation { ProcedureName = "UPD_PASSWORD_PR" };

                Password c = (Password)entity;
                operation.AddIntParam(DB_COL_ID, c.Id);
                operation.AddVarcharParam(DB_COL_PASSWORDD, c.Passwordd);
                operation.AddDateTimeParam(DB_COL_CREATIONDATE, c.CreationDate);

            return operation;
            }

            public SqlOperation GetDeleteStatement(BaseEntity entity)
            {
                SqlOperation operation = new SqlOperation { ProcedureName = "DEL_PASSWORD_PR" };

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

                Password password = new Password()
                {
                    Id = GetIntValue(row, DB_COL_ID),
                    CreationDate = GetDateValue(row, DB_COL_CREATIONDATE),
                    Passwordd = GetStringValue(row, DB_COL_PASSWORDD),
                    User = GetIntValue(row, DB_COL_USER)

                };

                return password;
            }

        }
    }

