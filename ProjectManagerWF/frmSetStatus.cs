using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Model;

namespace ProjectManagerWF
{
    public partial class frmSetStatus : Form
    {
        public TaskStatus SelectedStatus
        {
            get
            {
                return (TaskStatus)comboBox1.SelectedIndex;
            }
        }

        public frmSetStatus()
        {
            InitializeComponent();
            foreach (TaskStatus status in Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>())
                comboBox1.Items.Add(status.GetDescription());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран статус!");
                return;
            }
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
