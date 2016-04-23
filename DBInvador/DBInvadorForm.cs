using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Techphernalia.DBInvader.DBHandler;

namespace Techphernalia.DBInvader
{
	public partial class DBInvaderForm : Form
	{

        private Array DBTypes = null;

        private Dictionary<DBServer, ToolStripMenuItem> items = new Dictionary<DBServer, ToolStripMenuItem>();

        public DBInvaderForm()
		{
			InitializeComponent();
		}

		private void frmDBInvader_Load(object sender, EventArgs e)
		{
            DBTypes = Enum.GetValues(typeof(DBServer));
            foreach(var t in DBTypes)
            {
                var menuItem = new ToolStripMenuItem(t.ToString());
                menuStrip.Items.Insert(1,menuItem);
                items.Add((DBServer)t, menuItem);
            }
            foreach (var DBHandler in ConnectionManager.DBHandlers)
			{
                items[DBHandler.DBServer].DropDownItems.Add(GetToolStripMenuItem(DBHandler));
                serversToolStripMenuItem.DropDownItems.Add(GetToolStripMenuItem(DBHandler));
            }
        }

        private ToolStripMenuItem GetToolStripMenuItem(IDBHandler DBHandler)
        {
            var menuItem = new ToolStripMenuItem(DBHandler.DisplayName);
            menuItem.Click += ConnectServerMenuItem_Click;
            menuItem.Tag = DBHandler;
            menuItem.Image = global::Techphernalia.DBInvader.Properties.Resources.SQLServer_5728;

            var connectItem = new ToolStripMenuItem("Connect");
            connectItem.Click += ConnectServerMenuItem_Click;
            connectItem.Tag = DBHandler;
            connectItem.Image = global::Techphernalia.DBInvader.Properties.Resources.SQLServer_5728;

            var deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += DeleteServerMenuItem_Click;
            deleteItem.Tag = DBHandler;
            deleteItem.Image = global::Techphernalia.DBInvader.Properties.Resources.Clearallrequests_8816;

            menuItem.DropDownItems.Add(connectItem);
            menuItem.DropDownItems.Add(deleteItem);

            return menuItem;
        }

        private void ConnectServerMenuItem_Click(object sender,EventArgs e)
		{
			var x = new DBExplorer((IDBHandler)(((ToolStripMenuItem)sender).Tag));
			x.MdiParent = this;
			x.Show();
		}
        private void DeleteServerMenuItem_Click(object sender,EventArgs e)
        {
            var x = (IDBHandler)(((ToolStripMenuItem)sender).Tag);
            if (MessageBox.Show("Are you sure you want to delete server [" + x.DisplayName + "] from configuration.", TP_CONSTANTS.GetTitle(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ConnectionManager.DeleteServer(x);
                RecreateMenu();
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new NewServerForm();
            f.ShowDialog();
            RecreateMenu();
        }
        private void RecreateMenu()
        {
            foreach (var x in items.Values)
            {
                x.DropDownItems.Clear();
            }
            while (serversToolStripMenuItem.DropDownItems.Count > 2)
            {
                serversToolStripMenuItem.DropDownItems.RemoveAt(2);
            }
            foreach (var DBHandler in ConnectionManager.DBHandlers)
            {
                items[DBHandler.DBServer].DropDownItems.Add(GetToolStripMenuItem(DBHandler));
                serversToolStripMenuItem.DropDownItems.Add(GetToolStripMenuItem(DBHandler));
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutDBInvader().ShowDialog();
        }
    }
}
