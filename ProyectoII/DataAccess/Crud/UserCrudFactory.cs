using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class UserCrudFactory : CrudFactory
    {
        UserMapper mapper;

        public UserCrudFactory() : base()
        {
            mapper = new UserMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (User)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override User Retrieve<User>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (User)Convert.ChangeType(objs, typeof(User));
            }

            return default(User);
        }

        public User RetrieveByEmail<User>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveByEmailStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (User)Convert.ChangeType(objs, typeof(User));
            }

            return default(User);
        }

        public override List<User> RetrieveAll<User>()
        {
            var list = new List<User>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((User)Convert.ChangeType(c, typeof(User)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (User)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }
        public void UpdateStatus(BaseEntity entity)
        {
            var customer = (User)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatementStatus(customer));
        }

        public void UpdateNotificationMethod(BaseEntity entity)
        {
            var customer = (User)entity;
            dao.ExecuteProcedure(mapper.GetUpdateNotificationStatement(customer));
        }


        public override void Delete(BaseEntity entity)
        {
            var customer = (User)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
    }
}
