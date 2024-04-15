using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class CollectionCrudFactory : CrudFactory
    {
        CollectionMapper mapper;

        public CollectionCrudFactory() : base()
        {
            mapper = new CollectionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Collection)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Collection Retrieve<Collection>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Collection)Convert.ChangeType(objs, typeof(Collection));
            }

            return default(Collection);
        }

        public override List<Collection> RetrieveAll<Collection>()
        {
            var list = new List<Collection>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Collection)Convert.ChangeType(c, typeof(Collection)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Collection)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Collection)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

    }
}
