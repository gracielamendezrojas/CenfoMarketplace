using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class NFTXAcquisitionCrudFactory : CrudFactory
    {
        NFTXAcquisitionMapper mapper;

        public NFTXAcquisitionCrudFactory() : base()
        {
            mapper = new NFTXAcquisitionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (NFTXAcquisition)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override NFTXAcquisition Retrieve<NFTXAcquisition>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (NFTXAcquisition)Convert.ChangeType(objs, typeof(NFTXAcquisition));
            }

            return default(NFTXAcquisition);
        }

     

        public override List<NFTXAcquisition> RetrieveAll<NFTXAcquisition>()
        {
            var list = new List<NFTXAcquisition>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((NFTXAcquisition)Convert.ChangeType(c, typeof(NFTXAcquisition)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (NFTXAcquisition)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (NFTXAcquisition)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

    }
}
