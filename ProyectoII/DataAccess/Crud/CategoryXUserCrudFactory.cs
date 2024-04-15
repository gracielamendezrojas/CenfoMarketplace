using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;


namespace DataAcess.Crud
{
    public class CategoryXUserCrudFactory : CrudFactory
    {
        CategoryXUserMapper mapper;

        public CategoryXUserCrudFactory() : base()
        {
            mapper = new CategoryXUserMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (CategoryXUser)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }


        public override CategoryXUser Retrieve<CategoryXUser>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (CategoryXUser)Convert.ChangeType(objs, typeof(CategoryXUser));
            }

            return default(CategoryXUser);
        }

        public override List<CategoryXUser> RetrieveAll<CategoryXUser>()
        {
            var list = new List<CategoryXUser>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((CategoryXUser)Convert.ChangeType(c, typeof(CategoryXUser)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (CategoryXUser)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (CategoryXUser)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

    }
}
