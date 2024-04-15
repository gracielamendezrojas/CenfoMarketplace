using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class TransactionWalletCrudFactory : CrudFactory
    {
        TransactionWalletMapper mapper;

        public TransactionWalletCrudFactory() : base()
        {
            mapper = new TransactionWalletMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (TransactionWallet)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override TransactionWallet Retrieve<TransactionWallet>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (TransactionWallet)Convert.ChangeType(objs, typeof(TransactionWallet));
            }

            return default(TransactionWallet);
        }


        public override List<TransactionWallet> RetrieveAll<TransactionWallet>()
        {
            var list = new List<TransactionWallet>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((TransactionWallet)Convert.ChangeType(c, typeof(TransactionWallet)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (TransactionWallet)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (TransactionWallet)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

    }
}
