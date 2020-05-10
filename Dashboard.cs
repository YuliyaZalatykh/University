using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace University
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            panel4.Top = button1.Top;
            panel4.Height = button1.Height;
            subject2.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Top = button1.Top;
            panel4.Height = button1.Height;
            subject2.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Top = button2.Top;
            panel4.Height = button2.Height;
            speciality1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Top = button3.Top;
            panel4.Height = button3.Height;
            department3.BringToFront();
        }

        private void faculty4_Load(object sender, EventArgs e)
        {

        }
    }
}
