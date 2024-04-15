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
    public class TransactionWalletMapper: EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_WALLETSENDER = "WALLETSENDER";
        private const string DB_COL_WALLETRECEIVER = "WALLETRECEIVER";
        private const string DB_COL_AMOUNT = "AMOUNT";
        private const string DB_COL_DESCRIPTION = "DESCRIPTION";
        private const string DB_COL_TRANSACTIONDATE = "TRANSACTIONDATE";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_TBL_TRANSACTIONWALLET_PR" };

            TransactionWallet c = (TransactionWallet)entity;
            operation.AddVarcharParam(DB_COL_WALLETSENDER, c.WalletSender);
            operation.AddVarcharParam(DB_COL_WALLETRECEIVER, c.WalletReceiver);
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);
            operation.AddVarcharParam(DB_COL_DESCRIPTION, c.Description);
            operation.AddDateTimeParam(DB_COL_TRANSACTIONDATE, c.TransactionDate);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_TBL_TRANSACTIONWALLET_PR" };
            TransactionWallet c = (TransactionWallet)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_TBL_TRANSACTIONWALLET_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_TBL_TRANSACTIONWALLET_PR" };

            TransactionWallet c = (TransactionWallet)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_WALLETSENDER, c.WalletSender);
            operation.AddVarcharParam(DB_COL_WALLETRECEIVER, c.WalletReceiver);
            operation.AddDecimalParam(DB_COL_AMOUNT, c.Amount);
            operation.AddVarcharParam(DB_COL_DESCRIPTION, c.Description);
            operation.AddDateTimeParam(DB_COL_TRANSACTIONDATE, c.TransactionDate);


            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_TBL_TRANSACTIONWALLET_PR" };

            TransactionWallet c = (TransactionWallet)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var TransactionWallet = BuildObject(row);
                lstResults.Add(TransactionWallet);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            TransactionWallet TransactionWallet = new TransactionWallet()
            {
                Id=GetIntValue(row, DB_COL_ID),
                WalletSender = GetStringValue(row, DB_COL_WALLETSENDER),
                WalletReceiver = GetStringValue(row, DB_COL_WALLETRECEIVER),
                Amount = GetDecimalValue(row, DB_COL_AMOUNT),
                Description = GetStringValue(row, DB_COL_DESCRIPTION),
                TransactionDate = GetDateValue(row, DB_COL_TRANSACTIONDATE),

        };

            return TransactionWallet;
        }
    }
}
