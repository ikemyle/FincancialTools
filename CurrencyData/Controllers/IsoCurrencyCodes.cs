using CurrencyData.Helpers;
using CurrencyData.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace CurrencyData.Controllers
{
    /// <summary>
    /// Bussines logic for VPN Servers data
    /// </summary>
    public class IsoCurrencyCodes
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static DBContext DataContext = new DBContext();
        /// <summary>
        /// Check if a currency exists
        /// </summary>
        /// <param name="IsoCode"></param>
        /// <returns></returns>
        public static bool CurrencyExists(string IsoCode)
        {
            try
            {
                int nCount;
                string sql = "Select Count(1) From ISOCurrencies Where [ISO-4217]=@IsoCode";
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@IsoCode", IsoCode);
                object obj = DataContext.ExecuteScalarSql(sql, CommandType.Text, arrParam);

                return (int.TryParse(obj.ToString(), out nCount) && nCount > 0);
            }
            catch
            {
                throw;
            }
        }

        public static bool DeleteCurrency(string IsoCode)
        {
            try
            {
                //we do not delete, just make the record as not in use
                //string sql = "Delete From ISOCurrencies Where [ISO-4217]=@IsoCode";
                string sql = "Update ISOCurrencies Set InUse=0 Where [ISO-4217]=@IsoCode";
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@IsoCode", IsoCode);
                DataContext.ExecuteSql(sql, CommandType.Text, arrParam);

                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Save currency
        /// </summary>
        /// <param name="key"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static IsoCurrencyModel SaveCurrency(string key, IsoCurrencyModel currency)
        {
            try
            {
                string sql;
                SqlParameter[] arrParam;
                if (!string.IsNullOrEmpty(key) && key != currency.ISOCode)
                {
                    sql = "Update ISOCurrencies set InUse=0 Where [ISO-4217]=@ISOCode";
                    arrParam = new SqlParameter[1];
                    arrParam[0] = new SqlParameter("@IsoCode", key);
                    DataContext.ExecuteSql(sql, CommandType.Text, arrParam);
                }

                if (CurrencyExists(currency.ISOCode))
                {
                    sql = "Update ISOCurrencies set Currency=@Currency, InUse=1 Where [ISO-4217]=@ISOCode";
                }
                else
                {
                    sql = "Insert Into ISOCurrencies([ISO-4217],Currency, InUse) Values (@ISOCode,@Currency,1)";
                }
                arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@IsoCode", currency.ISOCode);
                arrParam[1] = new SqlParameter("@Currency", currency.Currency);
                object obj = DataContext.ExecuteScalarSql(sql, CommandType.Text, arrParam);
                return currency;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get currency object by code
        /// </summary>
        /// <param name="IsoCode"></param>
        /// <returns></returns>
        public static IsoCurrencyModel CurrencyByIsoCode(string IsoCode)
        {
            try
            {
                string sql = @"Select [ISO-4217], ISOC.Currency, IsNull(Rate,0) as Rate, IsNull(Base,'NA') as Base, IsNull(Symbol,'NA') as Symbol
                            From ISOCurrencies ISOC LEFT JOIN (SELECT Currency, Rate, Base From CurrencyRates
                            WHere RateDate =(Select MAX(RateDate) FROM CurrencyRates)) CR on ISOC.[ISO-4217] = CR.Currency
                            Where[ISO-4217]=@IsoCode";

                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@IsoCode", IsoCode);
                var dt = DataContext.GetRecordSet(sql, CommandType.Text, arrParam).Tables[0];
                var currencies = dt.ToEnumerable<IsoCurrencyModel>();
                if (currencies != null && currencies.ToList().Count == 1)
                {
                    return currencies.ToList()[0];
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get all saved servers info
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IsoCurrencyModel> InUseCurrencies()
        {
            try
            {
                string sql = @"Select [ISO-4217], ISOC.Currency, InUse, IsNull(Rate,0) as Rate, IsNull(Base,'NA') as Base
                            From ISOCurrencies ISOC LEFT JOIN (SELECT Currency, Rate, Base From CurrencyRates
                            WHere RateDate =(Select MAX(RateDate) FROM CurrencyRates)) CR on ISOC.[ISO-4217] = CR.Currency
                            Where InUse=1";

                var dt = DataContext.GetRecordSet(sql, CommandType.Text).Tables[0];
                var currencies = dt.ToEnumerable<IsoCurrencyModel>();
                return currencies;
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<IsoCurrencyModel> AllCurrencies()
        {
            try
            {
                string sql = @"Select [ISO-4217], ISOC.Currency, InUse, IsNull(Rate,0) as Rate, IsNull(Base,'NA') as Base, IsNull(Symbol,'NA') as Symbol
                            From ISOCurrencies ISOC LEFT JOIN (SELECT Currency, Rate, Base From CurrencyRates
                            WHere RateDate =(Select MAX(RateDate) FROM CurrencyRates)) CR on ISOC.[ISO-4217] = CR.Currency";

                var dt = DataContext.GetRecordSet(sql, CommandType.Text).Tables[0];
                var currencies = dt.ToEnumerable<IsoCurrencyModel>();
                return currencies;
            }
            catch
            {
                throw;
            }
        }

        public static void WriteToBase(List<CurrencyRateModel> crate)
        {
            if (crate == null || crate.Count == 0)
            {
                return;
            }
            //using (TransactionScope scope = new TransactionScope())
            //{
            string sqlIns = @"INSERT INTO CurrencyRates (Currency, RateDate, Rate, Base) 
                               VALUES(@Currency, @RateDate, @Rate, @Base)";
            string conn = ConfigurationManager.ConnectionStrings["CurrencyDB"].ConnectionString.Replace("##DBPATH##", DBContext.DevDb); ;

            using (var sqlConn = new SqlConnection(conn))
            {
                sqlConn.Open();
                using (SqlTransaction tran = sqlConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdIns = new SqlCommand(sqlIns, sqlConn, tran))
                        {
                            //insert base
                            cmdIns.Parameters.AddWithValue("@Currency", crate[0].Base);
                            cmdIns.Parameters.AddWithValue("@RateDate", crate[0].RateDate);
                            cmdIns.Parameters.AddWithValue("@Rate", 1);
                            cmdIns.Parameters.AddWithValue("@Base", crate[0].Base);
                            cmdIns.ExecuteNonQuery();
                            cmdIns.Parameters.Clear();

                            for (int i = 0; i < crate.Count; i++)
                            {
                                cmdIns.Parameters.AddWithValue("@Currency", crate[i].Currency);
                                cmdIns.Parameters.AddWithValue("@RateDate", crate[i].RateDate);
                                cmdIns.Parameters.AddWithValue("@Rate", crate[i].Rate);
                                cmdIns.Parameters.AddWithValue("@Base", crate[i].Base);
                                cmdIns.ExecuteNonQuery();
                                cmdIns.Parameters.Clear();
                            }
                            tran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (tran != null)
                        {
                            tran.Rollback();
                        }
                        //log error here
                        Log.Error(ex);
                    }
                }
            }

            //    scope.Complete();
            //}
        }

        /// <summary>
        /// Method to check current date is already loaded
        /// </summary>
        /// <param name="RateDate"></param>
        /// <returns></returns>
        public static bool CurrencyDateLoaded(DateTime RateDate)
        {
            try
            {
                int nCount;
                string sql = "Select Count(1) From CurrencyRates Where RateDate=@RateDate";
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@RateDate", RateDate);
                object obj = DataContext.ExecuteScalarSql(sql, CommandType.Text, arrParam);

                return (int.TryParse(obj.ToString(), out nCount) && nCount > 0);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Update in use currencies
        /// </summary>
        /// <param name="servers"></param>
        /// <returns></returns>
        public static bool SaveUserCurrency(string Currency, string CurrencyDescription = null)
        {
            try
            {
                bool bReturn = false;
                string sql = "Select Count(1) From ISOCurrencies Where [ISO-4217]=@Currency";
                int nCount;

                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Currency", Currency);
                object obj = DataContext.ExecuteScalarSql(sql, CommandType.Text, arrParam);

                if (int.TryParse(obj.ToString(), out nCount) && nCount > 0)
                {
                    //record exists, Do update
                    sql = "Update ISOCurrencies Set InUse=1 Where [ISO-4217]=@Currency";
                }
                else
                {
                    //insert
                    sql = "Insert Into ISOCurrencies ([ISO-4217],Currency,InUse) Values (@Currency,@CurrencyDescription,1)";
                    arrParam = new SqlParameter[2];
                    arrParam[0] = new SqlParameter("@Currency", Currency);
                    arrParam[1] = new SqlParameter("@CurrencyDescription", CurrencyDescription);
                }

                bReturn = DataContext.ExecuteSql(sql, CommandType.Text, arrParam);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public static DateTime? LoadedDate()
        {
            try
            {
                DateTime lastDate;
                string sql = "Select Max(RateDate) From CurrencyRates";

                object obj = DataContext.ExecuteScalarSql(sql, CommandType.Text);

                if (!DateTime.TryParse(obj.ToString(), out lastDate))
                {
                    return null;
                }
                return lastDate;
            }
            catch
            {
                throw;
            }
        }
    }
}
