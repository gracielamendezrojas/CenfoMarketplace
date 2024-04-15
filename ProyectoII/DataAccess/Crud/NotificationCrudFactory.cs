using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class NotificationCrudFactory : CrudFactory
    {
        NotificationMapper mapper;

        public NotificationCrudFactory() : base()
        {
            mapper = new NotificationMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Notification)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Notification Retrieve<Notification>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Notification)Convert.ChangeType(objs, typeof(Notification));
            }

            return default(Notification);
        }

        

        public override List<Notification> RetrieveAll<Notification>()
        {
            var list = new List<Notification>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Notification)Convert.ChangeType(c, typeof(Notification)));
                }
            }

            return list;
        }
        public List<Notification> RetrieveAllUserId<Notification>(BaseEntity entity)
        {
            var list = new List<Notification>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllUserIdStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Notification)Convert.ChangeType(c, typeof(Notification)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Notification)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Notification)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
        
    }
}
