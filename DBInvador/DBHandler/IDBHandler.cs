using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Techphernalia.DBInvader.DBHandler
{
	public interface IDBHandler
	{
		String DisplayName { get; set; }

		String Name { get; set; }

		String ConnectionString { get; set; }

		DBServer DBServer { get; set; }

		/// <summary>
		/// Get All Database Names available in the Server
		/// </summary>
		/// <returns>List of Database</returns>
		IEnumerable<string> GetDatabases { get; set; }

		/// <summary>
		/// Get All Tables available in a Database
		/// </summary>
		/// <returns>List of Schema Qualified name of all the Tables</returns>
		IEnumerable<string> GetTables(string database);
	
		/// <summary>
		/// Get N number of records from given table
		/// </summary>
		/// <param name="tableName">Schema Qualified name of Table</param>
		/// <param name="noOfRecords">Number of Records to Fetch</param>
		/// <returns>DataTable of TOP N records from Given Table</returns>
		DataTable GetTableData(string database,string tableName, int noOfRecords);

		/// <summary>
		/// Change active Database
		/// </summary>
		/// <param name="database">Name of Database to connect to</param>
		void ChangeDatabase(string database);

		/// <summary>
		/// Connects to Given Server
		/// </summary>
		void ConnectServer();
	}
}
