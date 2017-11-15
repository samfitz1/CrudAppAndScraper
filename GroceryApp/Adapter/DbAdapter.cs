using GroceryApp.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GroceryApp.Adapter
{
    public class DbAdapter : IDbAdapter
    {
        public IDbCommand DbCommand { get; private set; }

        public IDbConnection DbConnection { get; private set; }

        int _cmdTimeout = 5000;

        public int CommandTimeOut
        {
            get { return _cmdTimeout; }
            set { _cmdTimeout = value; }

        }

        public DbAdapter(IDbCommand dbCommand, IDbConnection dbConnection)
        {
            DbCommand = dbCommand;
            DbConnection = dbConnection;
        }

        public IEnumerable<T> LoadObject<T>(DbCmdDef cmdDef) where T : class
        {
            try
            {
                if (cmdDef == null)
                    throw new ArgumentException("Missing command definition");

                List<T> itms = new List<T>();
                // does a try, catch, finally
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    // ADO.NET
                    // opeing the connection and sending the values that I set below the if statement.
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdDef.DbCommandType;
                    cmd.CommandText = cmdDef.DbCommandText;
                    cmd.Connection = conn;

                    if (cmdDef.DbParameters != null)
                        foreach (IDbDataParameter param in cmdDef.DbParameters)
                            cmd.Parameters.Add(param);


                    // benefit of datareader is that it's forward looking, always streaming out results 1 at a time
                    // Look up how to set up a singleton, and factory
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        itms.Add(DataMapper<T>.Instance.MapToObject(reader));
                    }
                    return itms;
                }

            }
            catch { throw; }

        }

        public IEnumerable<T> LoadObject<T>(DbCmdDef cmdDef, Func<IDataReader, T> mapper) where T : class
        {
            try
            {
                if (cmdDef == null)
                    throw new ArgumentException("Missing command definition");

                List<T> itms = new List<T>();
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdDef.DbCommandType;
                    cmd.CommandText = cmdDef.DbCommandText;
                    cmd.Connection = conn;

                    if (cmdDef.DbParameters != null)
                        foreach (IDbDataParameter param in cmdDef.DbParameters)
                            cmd.Parameters.Add(param);

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        itms.Add(mapper(reader));
                    }
                    return itms;
                }
            }
            catch { throw; }
        }

        public int ExecuteQuery(IDbCmdDef cmdDef, Action<IDataParameterCollection> returnParameters = null)
        {
            try
            {
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdDef.DbCommandType;
                    cmd.CommandText = cmdDef.DbCommandText;
                    cmd.Connection = conn;

                    if (cmdDef.DbParameters != null)
                        foreach (IDbDataParameter param in cmdDef.DbParameters)
                            cmd.Parameters.Add(param);

                    int returnVal = cmd.ExecuteNonQuery();
                    returnParameters?.Invoke(cmd.Parameters);

                    return returnVal;
                }
            }
            catch { throw; }
        }

        public object ExecuteScalar(IDbCmdDef cmdDef)
        {
            try
            {
                using (IDbConnection conn = DbConnection)
                using (IDbCommand cmd = DbCommand)
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdDef.DbCommandType;
                    cmd.CommandText = cmdDef.DbCommandText;
                    cmd.Connection = conn;

                    if (cmdDef.DbParameters != null)
                        foreach (IDbDataParameter param in cmdDef.DbParameters)
                            cmd.Parameters.Add(param);

                    return cmd.ExecuteScalar();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}