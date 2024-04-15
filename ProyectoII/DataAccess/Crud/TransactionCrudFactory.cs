using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class TransactionCrudFactory : CrudFactory
    {
        TransactionMapper mapper;

        public TransactionCrudFactory() : base()
        {
            mapper = new TransactionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Transaction)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Transaction Retrieve<Transaction>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Transaction)Convert.ChangeType(objs, typeof(Transaction));
            }

            return default(Transaction);
        }

        

        public override List<Transaction> RetrieveAll<Transaction>()
        {
            var list = new List<Transaction>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Transaction)Convert.ChangeType(c, typeof(Transaction)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Transaction)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Transaction)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
        
    }
}
