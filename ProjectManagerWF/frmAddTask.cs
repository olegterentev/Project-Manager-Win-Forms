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
    public partial class frmAddTask : Form
    {
        public IAssignable Task { get; private set; }

        public frmAddTask()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < 0 ||
               string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            int maxPerformers = 10;
            if (comboBox1.SelectedIndex < 2 &&
               !int.TryParse(textBox2.Text, out maxPerformers))
            {
                MessageBox.Show("Введено некорректное значение!");
                return;
            }

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Task = new Epic(maxPerformers)
                    {
                        CreationDate = DateTime.Now,
                        Name = textBox1.Text
                    };
                    break;
                case 1:
                    Task = new Story(maxPerformers)
                    {
                        CreationDate = DateTime.Now,
                        Name = textBox1.Text
                    };
                    break;
                case 2:
                    Task = new Task()
                    {
                        CreationDate = DateTime.Now,
                        Name = textBox1.Text
                    };
                    break;
                case 3:
                    Task = new Bug()
                    {
                        CreationDate = DateTime.Now,
                        Name = textBox1.Text
                    };
                    break;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Visible = comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 1;
            textBox2.Visible = comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 1;
        }
    }
}
