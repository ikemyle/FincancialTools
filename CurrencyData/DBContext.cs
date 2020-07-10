using CurrencyData.Models;
using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CurrencyData
{
    /// <summary>
    /// Context of database
    /// </summary>
    public class DBContext
    {
        //only for this test. Otherwise the db would be on a server and path will not be needed
        public static string DevDb= AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { "bin" }, StringSplitOptions.None)[0] + @"CurrencyMain\Data\CurrencyDB.mdf";
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string ConnectionName = "CurrencyDB";
        private string ConnectionString;
        private SqlConnection Connection;

        public DBContext()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString.Replace("##DBPATH##", DevDb);
        }
        /// <summary>
        /// Database connection
        /// </summary>
        /// <returns></returns>
        public SqlConnection DBConnection()
        {
            try
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                return Connection;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Generic method to retriev a recordset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public DataSet GetRecordSet(string sql, CommandType commandType, SqlParameter[] Params = null)
        {
            try
            {
                DataSet ds = new DataSet();
                using (var sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlConn.Open();
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConn;
                        cmd.CommandText = sql;
                        cmd.CommandType = commandType;
                        if (Params != null)
                        {
                            foreach (var prm in Params)
                            {
                                cmd.Parameters.Add(prm);
                            }
                        }
                        using (var da = new SqlDataAdapter())
                        {
                            da.SelectCommand = cmd;
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Return an object from sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public object ExecuteScalarSql(string sql, CommandType commandType, SqlParameter[] Params = null)
        {
            try
            {
                using (var sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlConn.Open();
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConn;
                        cmd.CommandText = sql;
                        cmd.CommandType = commandType;
                        if (Params != null)
                        {
                            foreach (var prm in Params)
                            {
                                cmd.Parameters.Add(prm);
                            }
                        }
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Generic method to execute an SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool ExecuteSql(string sql, CommandType commandType, SqlParameter[] Params = null)
        {
            try
            {
                using (var sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlConn.Open();
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConn;
                        cmd.CommandText = sql;
                        cmd.CommandType = commandType;
                        if (Params != null)
                        {
                            foreach (var prm in Params)
                            {
                                cmd.Parameters.Add(prm);
                            }
                        }
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Generic method to execute a series of SQLS in a transaction
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public bool ExecuteMultipleSql(Sqls[] sqls)
        {
            try
            {
                using (var sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlConn.Open();
                    using (SqlTransaction transaction = sqlConn.BeginTransaction("MultiCommit"))
                    {
                        try
                        {

                            SqlCommand cmd = sqlConn.CreateCommand();
                            cmd.Connection = sqlConn;
                            cmd.Transaction = transaction;
                            for (int i = 0; i < sqls.Length; i++)
                            {
                                cmd.CommandText = sqls[i].sql;
                                //cmd.CommandType = sqls[i].SqlCommandType;
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            if (transaction != null)
                            {
                                transaction.Rollback();
                            }
                            //log error here
                            Log.Error(ex);
                            return false;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
