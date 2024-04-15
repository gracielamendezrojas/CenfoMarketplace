using System;
using System.Collections.Generic;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;
using DataAcess.Crud;

namespace DataAccess.Crud
{
    public class AutoBidCrudFactory : CrudFactory
    {
        AutoBidMapper mapper;

        public AutoBidCrudFactory() : base()
        {
            mapper = new AutoBidMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var autobid = (AutoBid)entity;
            var sqlOperation = mapper.GetCreateStatement(autobid);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override AutoBid Retrieve<AutoBid>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (AutoBid)Convert.ChangeType(objs, typeof(AutoBid));
            }

            return default(AutoBid);
        }

        public override List<AutoBid> RetrieveAll<AutoBid>()
        {
            var list = new List<AutoBid>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((AutoBid)Convert.ChangeType(c, typeof(AutoBid)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var autobid = (AutoBid)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(autobid));
        }

        public override void Delete(BaseEntity entity)
        {
            var autobid = (AutoBid)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(autobid));
        }


    }
}
