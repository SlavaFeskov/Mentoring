using NetStandardCL;
using System;
using System.Windows.Forms;

namespace NetFrameworkWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Shared.GetMessage(textBox1.Text));
        }
    }
}
