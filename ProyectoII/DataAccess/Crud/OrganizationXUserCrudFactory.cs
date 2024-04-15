using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;

namespace DataAcess.Crud
{
    //public class OrganizationXUserCrudFactory : CrudFactory
    //{
    //    OrganizationXUserMapper mapper;

    //    public OrganizationXUserCrudFactory() : base()
    //    {
    //        mapper = new OrganizationXUserMapper();
    //        dao = SqlDao.GetInstance();
    //    }

    //    public override void Create(BaseEntity entity)
    //    {
    //        var customer = (OrganizationXUser)entity;
    //        var sqlOperation = mapper.GetCreateStatement(customer);
    //        dao.ExecuteProcedure(sqlOperation);
    //    }



    //    public override OrganizationXUser Retrieve<OrganizationXUser>(BaseEntity entity)
    //    {
    //        var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
    //        var dic = new Dictionary<string, object>();
    //        if (lstResult.Count > 0)
    //        {
    //            dic = lstResult[0];
    //            var objs = mapper.BuildObject(dic);
    //            return (OrganizationXUser)Convert.ChangeType(objs, typeof(OrganizationXUser));
    //        }

    //        return default(OrganizationXUser);
    //    }

    //    public List<OrganizationXUser> RetrieveId<OrganizationXUser>(BaseEntity entity)
    //    {
    //        var list = new List<OrganizationXUser>();

    //        var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveIdStatement(entity));
    //        var dic = new Dictionary<string, object>();
    //        if (lstResult.Count > 0)
    //        {
    //            var objs = mapper.BuildObjects(lstResult);
    //            foreach (var c in objs)
    //            {
    //                list.Add((OrganizationXUser)Convert.ChangeType(c, typeof(OrganizationXUser)));
    //            }
    //        }

    //        return list;
    //    }

    //    public override List<OrganizationXUser> RetrieveAll<OrganizationXUser>()
    //    {
    //        var list = new List<OrganizationXUser>();

    //        var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
    //        var dic = new Dictionary<string, object>();
    //        if (lstResult.Count > 0)
    //        {
    //            var objs = mapper.BuildObjects(lstResult);
    //            foreach (var c in objs)
    //            {
    //                list.Add((OrganizationXUser)Convert.ChangeType(c, typeof(OrganizationXUser)));
    //            }
    //        }

    //        return list;
    //    }

    //    public override void Update(BaseEntity entity)
    //    {
    //        var customer = (OrganizationXUser)entity;
    //        dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
    //    }

    //    public override void Delete(BaseEntity entity)
    //    {
    //        var customer = (OrganizationXUser)entity;
    //        dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
    //    }
    //    public void DeleteId(BaseEntity entity)
    //    {
    //        var customer = (OrganizationXUser)entity;
    //        dao.ExecuteProcedure(mapper.GetDeleteIdStatement(customer));
    //    }
    //}
}
