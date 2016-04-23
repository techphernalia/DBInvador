using Techphernalia.DBInvader.DBHandler;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Techphernalia.DBInvader
{
    public partial class NewServerForm : Form
    {
        public NewServerForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtConnectionString.Text) && !string.IsNullOrWhiteSpace(cmbServerType.Text))
            {
                ConnectionManager.AddServer(ConnectionManager.GetDBHandler(txtName.Text, cmbServerType.Text, txtConnectionString.Text));
                Close();
            }
            else
            {
                MessageBox.Show("Invalid Configuration");
            }
        }

        private void frmNewServer_Load(object sender, EventArgs e)
        {
            cmbServerType.Items.AddRange(Enum.GetValues(typeof(DBServer)).Cast<object>().ToArray());
        }
    }
}
