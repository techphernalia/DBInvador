using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Techphernalia.DBInvader.DBHandler;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

namespace Techphernalia.DBInvader
{
	public partial class DBExplorer : Form
	{
		private IDBHandler DBHandler = null;
        string[] boundTables;
		public DBExplorer(IDBHandler dbHandler)
		{
			InitializeComponent();
			this.DBHandler = dbHandler;
			this.Text = " "+ DBHandler.DisplayName +" - "+ TP_CONSTANTS.DB_Invader;
		}

		private void DBExplorer_Load(object sender, EventArgs e)
		{
			dgData.DataSource = null;
			cmbDatabase.Items.Clear();
			cmbDatabase.Items.AddRange(DBHandler.GetDatabases.ToArray());
		}

		private void cmbDatabase_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				dgData.DataSource = null;
				cmbTables.Items.Clear();
                boundTables = DBHandler.GetTables(cmbDatabase.Text).ToArray();
                txtSearch_KeyUp(sender, null);
			}
			catch (SqlException exc)
			{
				MessageBox.Show("Exception occured : " + Environment.NewLine + "\t" + exc.Message + Environment.NewLine + Environment.NewLine + "Caused by one of the following :" + Environment.NewLine + "Login Failed." + Environment.NewLine + "Incorrect dbName" + Environment.NewLine + "Access Denied.", "DB Explorer : from techPhernalia.com : by Durgesh Chaudhary");
			}
		}

		private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			int noOfRows = 10;
			if (!string.IsNullOrWhiteSpace( txtRecordCount.Text))
			{
				noOfRows = Convert.ToInt32(txtRecordCount.Text,CultureInfo.InvariantCulture);
			}
			try
			{
				dgData.DataSource = null;
				dgData.DataSource = DBHandler.GetTableData(cmbDatabase.Text,cmbTables.Text,noOfRows);
			}
			catch (SqlException exc)
			{
                using (StreamWriter wrtr = new StreamWriter("Error.log", true))
                {
                    wrtr.WriteLine("Table Change" + Environment.NewLine + DateTime.Now.ToLongDateString() + Environment.NewLine + DateTime.Now.ToLongTimeString() + Environment.NewLine);
                    wrtr.WriteLine(exc.Message);
                    wrtr.WriteLine(exc.StackTrace);
                    wrtr.WriteLine(new String('_', 80));
                    wrtr.Flush();
                }
				MessageBox.Show("Exception occured : " + Environment.NewLine + "\t" + exc.Message + Environment.NewLine + Environment.NewLine + "Detailed exception log has been generated as [Error.Log]. Please post the content on the MSDN DB Invader page or techphernalia.com to get it resolved.", "DB Explorer : from techPhernalia.com : by Durgesh Chaudhary");
			}
		}

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ;
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if(boundTables == null)
            {
                return;
            }
            dgData.DataSource = null;
            cmbTables.Items.Clear();
            cmbTables.Items.AddRange(boundTables.Where(x => x.ToLower().Trim().Contains(txtSearch.Text.ToLower().Trim())).ToArray());
        }
    }
}
