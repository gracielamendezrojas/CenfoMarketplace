﻿using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using DTO_POJOS;
using DataAccess.Mapper;

namespace DataAcess.Crud
{
    public class DocumentCrudFactory : CrudFactory
    {
        DocumentMapper mapper;

        public DocumentCrudFactory() : base()
        {
            mapper = new DocumentMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (Document)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override Document Retrieve<Document>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (Document)Convert.ChangeType(objs, typeof(Document));
            }

            return default(Document);
        }


        public override List<Document> RetrieveAll<Document>()
        {
            var list = new List<Document>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    list.Add((Document)Convert.ChangeType(c, typeof(Document)));
                }
            }

            return list;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Document)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Document)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

    }
}
