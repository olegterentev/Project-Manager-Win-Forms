using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Model;

namespace ProjectManagerWF
{
    public partial class frmRemoveUser : Form
    {
        public User[] SelectedUsers
        {
            get
            {
                return checkedListBox1.CheckedItems.Cast<User>().ToArray();
            }
        }

        public frmRemoveUser(IEnumerable<User> users)
        {
            InitializeComponent();
            checkedListBox1.Items.AddRange(users.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
