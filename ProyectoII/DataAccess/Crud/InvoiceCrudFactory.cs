//using System;
//using System.Collections.Generic;
//using DataAcess.Mapper;
//using DataAcess.Dao;
//using DTO_POJOS;

//namespace DataAcess.Crud
//{
//    public class InvoiceCrudFactory : CrudFactory
//    {
//        InvoiceMapper mapper;

//        public InvoiceCrudFactory() : base()
//        {
//            mapper = new InvoiceMapper();
//            dao = SqlDao.GetInstance();
//        }

//        public override void Create(BaseEntity entity)
//        {
//            var customer = (Invoice)entity;
//            var sqlOperation = mapper.GetCreateStatement(customer);
//            dao.ExecuteProcedure(sqlOperation);
//        }

//        public override Invoice Retrieve<Invoice>(BaseEntity entity)
//        {
//            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
//            var dic = new Dictionary<string, object>();
//            if (lstResult.Count > 0)
//            {
//                dic = lstResult[0];
//                var objs = mapper.BuildObject(dic);
//                return (Invoice)Convert.ChangeType(objs, typeof(Invoice));
//            }

//            return default(Invoice);
//        }

//        public override List<Invoice> RetrieveAll<Invoice>()
//        {
//            var list = new List<Invoice>();

//            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
//            var dic = new Dictionary<string, object>();
//            if (lstResult.Count > 0)
//            {
//                var objs = mapper.BuildObjects(lstResult);
//                foreach (var c in objs)
//                {
//                    list.Add((Invoice)Convert.ChangeType(c, typeof(Invoice)));
//                }
//            }

//            return list;
//        }

//        public override void Update(BaseEntity entity)
//        {
//            var customer = (Invoice)entity;
//            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
//        }

//        public override void Delete(BaseEntity entity)
//        {
//            var customer = (Invoice)entity;
//            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
//        }
//    }
//}
