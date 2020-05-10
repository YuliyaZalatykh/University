using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using University.Controller;

namespace University
{
    public partial class Department : UserControl
    {

        Query controller;

        public Department()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = controller.UpdateTable("Кафедра");
        }
    }
}
