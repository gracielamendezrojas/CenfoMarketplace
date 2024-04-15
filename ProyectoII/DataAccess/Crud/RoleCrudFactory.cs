using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;

namespace DataAcess.Crud
{
    public class RoleCrudFactory : CrudFactory
    {
        RoleMapper mapper;

        public RoleCrudFactory() : base()
        {
            mapper = new RoleMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Role)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Role Retrieve<Role>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Role)Convert.ChangeType(objs, typeof(Role));
            }

            return default(Role);
        }

        public override List<Role> RetrieveAll<Role>()
        {
            var list = new List<Role>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Role)Convert.ChangeType(c, typeof(Role)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Role)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Role)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
    }
}
