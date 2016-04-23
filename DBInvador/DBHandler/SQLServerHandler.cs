using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Techphernalia.DBInvader.DBHandler
{
    public sealed class MssqlServerHandler : IDBHandler, IDisposable
    {
        #region Private Members

        /// <summary>
        /// SQL Connection for SQL Server
        /// </summary>
        private SqlConnection conn = null;

        /// <summary>
        /// ConnectionString for Given Server
        /// </summary>
        public string ConnectionString { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor to initialize SQL Server handler
        /// </summary>
        /// <param name="connectionString">Connection String of SQL Server</param>
        public MssqlServerHandler(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        #endregion

        #region IDBHandler Members

        /// <summary>
        /// Get All Tables available in a Database
        /// </summary>
        /// <returns>List of Schema Qualified name of all the Tables</returns>
        public IEnumerable<string> GetTables(string database)
        {
            ConnectServer();

            ChangeDatabase(database);

            List<string> result = new List<string>();

            using (SqlCommand cmd = new SqlCommand("select '['+S.name + '].[' +T.name+']' as name from sys.tables T inner join sys.schemas S on T.schema_id = S.schema_id order by name", conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["name"].ToString());
                }
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Get All Database Names available in the Server
        /// </summary>
        /// <returns>List of Database</returns>
        public IEnumerable<string> GetDatabases
        {
            get
            {
                ConnectServer();
                List<String> result = new List<string>();

                using (SqlCommand SqlCom = new SqlCommand("sp_databases", conn))
                {
                    SqlCom.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader SqlDR = SqlCom.ExecuteReader())
                    {
                        while (SqlDR.Read())
                        {
                            result.Add(SqlDR.GetString(0));
                        }
                    }
                    conn.Close();
                }
                return result;
            }
            set { }
        }

        /// <summary>
        /// Get N number of records from given table
        /// </summary>
        /// <param name="tableName">Schema Qualified name of Table</param>
        /// <param name="noOfRecords">Number of Records to Fetch</param>
        /// <returns>DataTable of TOP N records from Given Table</returns>
        public DataTable GetTableData(string dataBase, string tableName, int noOfRecords)
        {
            ConnectServer();
            ChangeDatabase(dataBase);

            DataSet DS = new DataSet();
            (new SqlDataAdapter("SELECT TOP " + noOfRecords.ToString() + " * FROM " + tableName, conn)).Fill(DS);
            conn.Close();
            return DS.Tables[0];
        }

        /// <summary>
        /// Change active Database
        /// </summary>
        /// <param name="database">Name of Database to connect to</param>
        public void ChangeDatabase(string database)
        {
            ConnectServer();
            conn.ChangeDatabase(database);
        }

        /// <summary>
        /// Connects to Given Server
        /// </summary>
        public void ConnectServer()
        {
            if (conn == null)
            {
                conn = new SqlConnection(ConnectionString);
            }
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public void Dispose()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Display Name of Server
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Internal Name of Server
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of Server
        /// </summary>
        public DBServer DBServer
        {
            get
            {
                return DBServer.MssqlServer;
            }
            set
            {
                throw new InvalidOperationException("You can not set DB Server Type");
            }
        }

        #endregion

    }
}
