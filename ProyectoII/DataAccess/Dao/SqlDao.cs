﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DataAcess.Dao
{
    public class SqlDao
    {   
                     
       private string CONNECTION_STRING = "";
       
       private static SqlDao instance;

        private SqlDao()
        {
            CONNECTION_STRING = "Data Source=brainware-tech.database.windows.net;Initial Catalog=brainware-tech;Persist Security Info=True;User ID=brainwareadmin;Password=brainadmin1!;";
        }

        //IMPLEMENTA EL PATRON LLAMADO SINGLETON
        //INVESTIGAR EL PATRON
        public static SqlDao GetInstance()
        {
            if (instance == null)
                instance = new SqlDao();

            return instance;
        }


       public void ExecuteProcedure(SqlOperation sqlOperation)
       {
            using (var conn = new SqlConnection(CONNECTION_STRING))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {                          
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }

                conn.Open();
                command.ExecuteNonQuery();
            }
       }

       public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {
            var lstResult=new List<Dictionary<string,object>>();

            using (var conn = new SqlConnection(CONNECTION_STRING))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            { 
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }

                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        for (var lp = 0; lp < reader.FieldCount; lp++)
                        {
                            dict.Add(reader.GetName(lp), reader.GetValue(lp));
                        }
                        lstResult.Add(dict);
                    }
                }
            }

            return lstResult;
        }      
    }
}
