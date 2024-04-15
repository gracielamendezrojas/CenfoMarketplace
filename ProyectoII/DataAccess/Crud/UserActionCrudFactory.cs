using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{ 
    //userAction
    public class UserActionCrudFactory : CrudFactory
    {
        UserActionMapper mapper;

        public UserActionCrudFactory() : base()
        {
            mapper = new UserActionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (UserAction)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override UserAction Retrieve<UserAction>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (UserAction)Convert.ChangeType(objs, typeof(UserAction));
            }

            return default(UserAction);
        }

        public override List<UserAction> RetrieveAll<UserAction>()
        {
            var list = new List<UserAction>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((UserAction)Convert.ChangeType(c, typeof(UserAction)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (UserAction)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (UserAction)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
    }
}
