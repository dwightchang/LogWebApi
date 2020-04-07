using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LogWebApi.Model.Entity;
using Newtonsoft.Json;

namespace LogWebApi.Service
{
    public class SqliteHelper
    {
        private static string _connstr = "Data source=Order.db";        

        /// <summary>
        /// execute a SQL command
        /// </summary>        
        public static IEnumerable<T> Query<T>(string sql, object param)
        {
            // start the timer
            Stopwatch watch = Stopwatch.StartNew();

            try
            {                                   
                using (var connection = new SQLiteConnection(_connstr))
                {
                    connection.Open();

                    return connection.Query<T>(sql, param);                   
                }                
            }
            catch (Exception e)
            {
                // log sql content and parameters
                SysLogger.System.Error($"sql error, sql:{sql}, param: {JsonConvert.SerializeObject(param)}, message: {e.ToString()}");
                throw;
            }
            finally
            {
                watch.Stop();
                float elapsedTime = (float) watch.ElapsedMilliseconds;

                // log duration of sql execution 
                SysLogger.SqlElapsedTime.Info($"{elapsedTime / 1000f:#0.00} {sql}");
            }
        }
    }
}
