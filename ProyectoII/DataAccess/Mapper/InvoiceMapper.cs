//using DataAcess.Dao;
//using DTO_POJOS;
//using System.Collections.Generic;

//namespace DataAcess.Mapper
//{
//    public class InvoiceMapper : EntityMapper, ISqlStaments, IObjectMapper
//    {
//        private const string DB_COL_ID = "ID";
//        private const string DB_COL_DETAIL = "DETAIL";
  
  
//        public SqlOperation GetCreateStatement(BaseEntity entity)
//        {
//            var operation = new SqlOperation { ProcedureName = "CRE_TBL_INVOICE_PR" };
  
//            var c = (Invoice)entity;
//            operation.AddVarcharParam(DB_COL_DETAIL, c.Detail);
  
//            return operation;
//        }
  
  
//        public SqlOperation GetRetriveStatement(BaseEntity entity)
//        {
//            var operation = new SqlOperation { ProcedureName = "RET_TBL_INVOICE_PR" };
  
//            var c = (Invoice)entity;
//            operation.AddIntParam(DB_COL_ID, c.Id);
  
//            return operation;
//        }
  
//        public SqlOperation GetRetriveAllStatement()
//        {
//            var operation = new SqlOperation { ProcedureName = "RET_ALL_TBL_INVOICE_PR" };
//            return operation;
//        }
  
//        public SqlOperation GetUpdateStatement(BaseEntity entity)
//        {
//            var operation = new SqlOperation { ProcedureName = "UPD_TBL_INVOICE_PR" };
  
//            var c = (Invoice)entity;
//            operation.AddIntParam(DB_COL_ID, c.Id);
//            operation.AddVarcharParam(DB_COL_DETAIL, c.Detail);
  
//            return operation;
//        }
  
//        public SqlOperation GetDeleteStatement(BaseEntity entity)
//        {
//            var operation = new SqlOperation { ProcedureName = "DEL_TBL_INVOICE_PR" };
  
//            var c = (Invoice)entity;
//            operation.AddIntParam(DB_COL_ID, c.Id);
//            return operation;
//        }
  
//        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
//        {
//            var lstResults = new List<BaseEntity>();
  
//            foreach (var row in lstRows)
//            {
//                var Invoice = BuildObject(row);
//                lstResults.Add(Invoice);
//            }
//            return lstResults;
//        }
  
//        public BaseEntity BuildObject(Dictionary<string, object> row)
//        {
//            var Invoice = new Invoice
//            {
//                Id = GetIntValue(row, DB_COL_ID),
//                Detail = GetStringValue(row, DB_COL_DETAIL),
//            };
  
//            return Invoice;
//        }
  
//    }
//}
