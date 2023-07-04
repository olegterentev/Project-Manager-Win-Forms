using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectManagerWF
{
    public partial class frmLauncher : Form
    {
        public GuiType GuiType
        {
            get
            {
                return radioButton1.Checked ? GuiType.Console : GuiType.Window;
            }
        }

        public frmLauncher()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

}
