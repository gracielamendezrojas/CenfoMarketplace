using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class SuscriptionCrudFactory : CrudFactory
    {
        SuscriptionMapper mapper;

        public SuscriptionCrudFactory() : base()
        {
            mapper = new SuscriptionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Suscription)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Suscription Retrieve<Suscription>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Suscription)Convert.ChangeType(objs, typeof(Suscription));
            }

            return default(Suscription);
        }


        public override List<Suscription> RetrieveAll<Suscription>()
        {
            var list = new List<Suscription>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Suscription)Convert.ChangeType(c, typeof(Suscription)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Suscription)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Suscription)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

    }
}
