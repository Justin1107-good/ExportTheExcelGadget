using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.HelloFrom
{
    public partial class HelloWorld : Form
    {
        public HelloWorld()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_Excel frm_Model = new Frm_Excel();
            this.Hide();
            frm_Model.Show();
        }

        private void HelloWorld_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnClosing(e);
            System.Windows.Forms.Application.Exit(e);
        }
    }
}
