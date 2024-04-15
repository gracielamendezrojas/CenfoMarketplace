using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class PhoneCrudFactory : CrudFactory
    {
        PhoneMapper mapper;

        public PhoneCrudFactory() : base()
        {
            mapper = new PhoneMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Phone)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Phone Retrieve<Phone>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetDeleteStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Phone)Convert.ChangeType(objs, typeof(Phone));
            }

            return default(Phone);
        }


        public override List<Phone> RetrieveAll<Phone>()
        {
            var list = new List<Phone>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Phone)Convert.ChangeType(c, typeof(Phone)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Phone)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Phone)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

    }
}
