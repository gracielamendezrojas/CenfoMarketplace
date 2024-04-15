using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class AcquisitionCrudFactory : CrudFactory
    {
        AcquisitionMapper mapper;

        public AcquisitionCrudFactory() : base()
        {
            mapper = new AcquisitionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Acquisition)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Acquisition Retrieve<Acquisition>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Acquisition)Convert.ChangeType(objs, typeof(Acquisition));
            }

            return default(Acquisition);
        }

        public override List<Acquisition> RetrieveAll<Acquisition>()
        {
            var list = new List<Acquisition>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Acquisition)Convert.ChangeType(c, typeof(Acquisition)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Acquisition)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Acquisition)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
    }
}
