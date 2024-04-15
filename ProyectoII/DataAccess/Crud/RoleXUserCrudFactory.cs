using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;

namespace DataAcess.Crud
{
    public class RoleXUserCrudFactory : CrudFactory
    {
        RoleXUserMapper mapper;

        public RoleXUserCrudFactory() : base()
        {
            mapper = new RoleXUserMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (RoleXUser)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override RoleXUser Retrieve<RoleXUser>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (RoleXUser)Convert.ChangeType(objs, typeof(RoleXUser));
            }

            return default(RoleXUser);
        }

        public override List<RoleXUser> RetrieveAll<RoleXUser>()
        {
            var list = new List<RoleXUser>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((RoleXUser)Convert.ChangeType(c, typeof(RoleXUser)));
                }
            }

            return list;
        }

        //Overload Retrieve ALL by User
        public List<RoleXUser> RetrieveAllByUser<RoleXUser>(BaseEntity entity)
        {
            var list = new List<RoleXUser>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((RoleXUser)Convert.ChangeType(c, typeof(RoleXUser)));
                }
            }
            return list;
        }



        public override void Update(BaseEntity entity)
        {
            var customer = (RoleXUser)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (RoleXUser)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
    }
}
