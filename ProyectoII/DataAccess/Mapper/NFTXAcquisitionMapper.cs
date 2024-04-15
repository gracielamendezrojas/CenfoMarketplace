using DataAcess.Dao;
using DataAcess.Mapper;
using DTO_POJOS;
using System;
using System.Collections.Generic;


namespace DataAccess.Mapper
{
    public class NFTXAcquisitionMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NFT = "NFT";
        private const string DB_COL_ACQUISITION_ID = "ACQUISITION_ID";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "CRE_NFTXACQUISITION_PR" };

            NFTXAcquisition c = (NFTXAcquisition)entity;
            operation.AddIntParam(DB_COL_NFT, c.NFT);
            operation.AddIntParam(DB_COL_ACQUISITION_ID, c.Acquisition_Id);
      
            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_NFTXACQUISITION_PR" };

            NFTXAcquisition c = (NFTXAcquisition)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "RET_ALL_NFTXACQUISITION_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "UPD_NFTXACQUISITION_PR" };

            NFTXAcquisition c = (NFTXAcquisition)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_NFT, c.NFT);
            operation.AddIntParam(DB_COL_ACQUISITION_ID, c.Acquisition_Id);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            SqlOperation operation = new SqlOperation { ProcedureName = "DEL_NFTXACQUISITION_PR" };

            NFTXAcquisition c = (NFTXAcquisition)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var NFTXAcq = BuildObject(row);
                lstResults.Add(NFTXAcq);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            NFTXAcquisition organization = new NFTXAcquisition()
            {
                Id = GetIntValue(row, DB_COL_ID),
                NFT = GetIntValue(row, DB_COL_NFT),
                Acquisition_Id = GetIntValue(row,DB_COL_ACQUISITION_ID),
            };

            return organization;
        }
    }
}
