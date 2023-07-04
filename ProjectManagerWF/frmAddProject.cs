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
    public partial class frmAddProject : Form
    {
        public string ProjectName { get; private set; }
        public int MaxTaskCount { get; private set; }

        public frmAddProject()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) ||
               string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }

            if(!int.TryParse(textBox2.Text, out int maxCount))
            {
                MessageBox.Show("Введено некорректное значение");
                return;
            }
            ProjectName = textBox1.Text;
            MaxTaskCount = maxCount;
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
