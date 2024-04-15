using DataAcess.Dao;
using DataAcess.Mapper;
using DTO_POJOS;
using System;
using System.Collections.Generic;


namespace DataAccess.Mapper
{
    public class OTPMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_OTPNUMBER = "OTPNUMBER";
        private const string DB_COL_USERID = "USERID";
        private const string DB_COL_DATETIME = "DATETIME";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_OTP_PR" };

            OTP c = (OTP)entity;
            operation.AddIntParam(DB_COL_USERID, c.UserId);
            operation.AddVarcharParam(DB_COL_OTPNUMBER, c.OTPNumber);
            operation.AddDateTimeParam(DB_COL_DATETIME, c.DateTime);
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_OTP_PR" };

            OTP c = (OTP)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_OTP_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_OTP_BYUSER_PR" };

            OTP c = (OTP)entity;
            operation.AddIntParam(DB_COL_USERID, c.UserId);
            operation.AddVarcharParam(DB_COL_OTPNUMBER, c.OTPNumber);
            operation.AddDateTimeParam(DB_COL_DATETIME, c.DateTime);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_OTP_PR" };

            OTP c = (OTP)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var OTP = BuildObject(row);
                lstResults.Add(OTP);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            OTP OTPNumber = new OTP()
            {
                Id = GetIntValue(row, DB_COL_ID),
                DateTime = GetDateValue(row, DB_COL_DATETIME),
                UserId = GetIntValue(row,DB_COL_USERID),
                OTPNumber = GetStringValue(row,DB_COL_OTPNUMBER)
            };

            return OTPNumber;
        }
    }
}
