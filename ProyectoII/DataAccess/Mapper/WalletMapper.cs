using DataAcess.Dao;
using DTO_POJOS;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class WalletMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_BALANCE = "BALANCE";
        private const string DB_COL_USERID = "USERID";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TBL_WALLET_PR" };
  
            var c = (Wallet)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);
            operation.AddDecimalParam(DB_COL_BALANCE, c.Balance);
            operation.AddIntParam(DB_COL_USERID, c.UserId);

            return operation;
        }
  
  
        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TBL_WALLET_PR" };
  
            var c = (Wallet)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveStatementByUser(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_WALLET_BYUSER_PR" };

            var c = (Wallet)entity;
            operation.AddIntParam(DB_COL_USERID, c.UserId);

            return operation;
        }
        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TBL_WALLET_PR" };
            return operation;
        }
  
        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_TBL_WALLET_PR" };
  
            var c = (Wallet)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);
            operation.AddDecimalParam(DB_COL_BALANCE, c.Balance);
            operation.AddIntParam(DB_COL_USERID, c.UserId);

            return operation;
        }

        public SqlOperation GetUpdateStatementByUser(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_WALLET_BYUSER_PR" };

            var c = (Wallet)entity;
            operation.AddDecimalParam(DB_COL_BALANCE, c.Balance);
            operation.AddIntParam(DB_COL_USERID, c.UserId);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_TBL_WALLET_PR" };
  
            var c = (Wallet)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);
            return operation;
        }
  
        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();
  
            foreach (var row in lstRows)
            {
                var Wallet = BuildObject(row);
                lstResults.Add(Wallet);
            }
            return lstResults;
        }
  
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Wallet = new Wallet
            {
                Id = GetStringValue(row, DB_COL_ID),
                Balance = GetDecimalValue(row, DB_COL_BALANCE),
                UserId = GetIntValue(row, DB_COL_USERID)
            };
  
            return Wallet;
        }
  
    }
}
