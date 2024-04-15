using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class PayPalCrudFactory : CrudFactory
    {
        PayPalMapper mapper;

        public PayPalCrudFactory() : base()
        {
            mapper = new PayPalMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Paypal)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override PayPal Retrieve<PayPal>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (PayPal)Convert.ChangeType(objs, typeof(PayPal));
            }

            return default(PayPal);
        }

        public override List<PayPal> RetrieveAll<PayPal>()
        {
            var list = new List<PayPal>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((PayPal)Convert.ChangeType(c, typeof(PayPal)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Paypal)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Paypal)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
    }
}
