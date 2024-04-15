using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class PasswordCrudFactory : CrudFactory
    {
        PasswordMapper mapper;

        public PasswordCrudFactory() : base()
        {
            mapper = new PasswordMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Password)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Password Retrieve<Password>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Password)Convert.ChangeType(objs, typeof(Password));
            }

            return default(Password);
        }


        public override List<Password> RetrieveAll<Password>()
        {
            var list = new List<Password>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Password)Convert.ChangeType(c, typeof(Password)));
                }
            }

            return list;
        }

        // OverCharge to return a list of password for one spcific user
        public List<Password> RetrieveAll<Password>(BaseEntity entity)
        {
            var list = new List<Password>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Password)Convert.ChangeType(c, typeof(Password)));
                }
            }

            return list;
        }

        public List<Password> RetrieveByUserAll<Password>(BaseEntity entity)
        {
            var list = new List<Password>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Password)Convert.ChangeType(c, typeof(Password)));
                }
            }

            return list;


        }
        public override void Update(BaseEntity entity)
        {
            var customer = (Password)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Password)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

    }
}
