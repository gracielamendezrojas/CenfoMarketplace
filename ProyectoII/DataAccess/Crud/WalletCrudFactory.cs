using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;

namespace DataAcess.Crud
{
    public class WalletCrudFactory : CrudFactory
    {
        WalletMapper mapper;

        public WalletCrudFactory() : base()
        {
            mapper = new WalletMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Wallet)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override Wallet Retrieve<Wallet>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Wallet)Convert.ChangeType(objs, typeof(Wallet));
            }

            return default(Wallet);
        }

        public Wallet RetrieveByUser<Wallet>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatementByUser(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Wallet)Convert.ChangeType(objs, typeof(Wallet));
            }

            return default(Wallet);
        }
        public override List<Wallet> RetrieveAll<Wallet>()
        {
            var list = new List<Wallet>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Wallet)Convert.ChangeType(c, typeof(Wallet)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Wallet)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public void UpdateByUser(BaseEntity entity)
        {
            var customer = (Wallet)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatementByUser(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Wallet)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
    }
}
