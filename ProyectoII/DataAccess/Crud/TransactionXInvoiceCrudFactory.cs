using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;

namespace DataAcess.Crud
{
    //public class TransactionXInvoiceCrudFactory : CrudFactory
    //{
    //    TransactionXInvoiceMapper mapper;

    //    public TransactionXInvoiceCrudFactory() : base()
    //    {
    //        mapper = new TransactionXInvoiceMapper();
    //        dao = SqlDao.GetInstance();
    //    }

    //    public override void Create(BaseEntity entity)
    //    {
    //        var customer = (TransactionXInvoice)entity;
    //        var sqlOperation = mapper.GetCreateStatement(customer);
    //        dao.ExecuteProcedure(sqlOperation);
    //    }



    //    public override TransactionXInvoice Retrieve<TransactionXInvoice>(BaseEntity entity)
    //    {
    //        var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
    //        var dic = new Dictionary<string, object>();
    //        if (lstResult.Count > 0)
    //        {
    //            dic = lstResult[0];
    //            var objs = mapper.BuildObject(dic);
    //            return (TransactionXInvoice)Convert.ChangeType(objs, typeof(TransactionXInvoice));
    //        }

    //        return default(TransactionXInvoice);
    //    }

    //    public List<TransactionXInvoice> RetrieveId<TransactionXInvoice>(BaseEntity entity)
    //    {
    //        var list = new List<TransactionXInvoice>();

    //        var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveIdStatement(entity));
    //        var dic = new Dictionary<string, object>();
    //        if (lstResult.Count > 0)
    //        {
    //            var objs = mapper.BuildObjects(lstResult);
    //            foreach (var c in objs)
    //            {
    //                list.Add((TransactionXInvoice)Convert.ChangeType(c, typeof(TransactionXInvoice)));
    //            }
    //        }

    //        return list;
    //    }

    //    public override List<TransactionXInvoice> RetrieveAll<TransactionXInvoice>()
    //    {
    //        var list = new List<TransactionXInvoice>();

    //        var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
    //        var dic = new Dictionary<string, object>();
    //        if (lstResult.Count > 0)
    //        {
    //            var objs = mapper.BuildObjects(lstResult);
    //            foreach (var c in objs)
    //            {
    //                list.Add((TransactionXInvoice)Convert.ChangeType(c, typeof(TransactionXInvoice)));
    //            }
    //        }

    //        return list;
    //    }

    //    public override void Update(BaseEntity entity)
    //    {
    //        var customer = (TransactionXInvoice)entity;
    //        dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
    //    }

    //    public override void Delete(BaseEntity entity)
    //    {
    //        var customer = (TransactionXInvoice)entity;
    //        dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
    //    }
    //    public void DeleteId(BaseEntity entity)
    //    {
    //        var customer = (TransactionXInvoice)entity;
    //        dao.ExecuteProcedure(mapper.GetDeleteIdStatement(customer));
    //    }
    //}
}
