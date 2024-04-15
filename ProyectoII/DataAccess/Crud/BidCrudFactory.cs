using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class BidCrudFactory : CrudFactory
    {
        BidMapper mapper;

        public BidCrudFactory() : base()
        {
            mapper = new BidMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
             var bid = (Bid)entity;
            var sqlOperation = mapper.GetCreateStatement(bid);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Bid Retrieve<Bid>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Bid)Convert.ChangeType(objs, typeof(Bid));
            }

            return default(Bid);
        }

        

        public override List<Bid> RetrieveAll<Bid>()
        {
            var list = new List<Bid>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Bid)Convert.ChangeType(c, typeof(Bid)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Bid)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Bid)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
    }
}
