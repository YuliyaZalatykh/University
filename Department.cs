using System;
using System.Windows.Forms;
using University.BO;

namespace University
{
    public partial class Department : UserControl
    {
        DepartmentTable departmentTable = new DepartmentTable();

        public Department()
        {
            Program.DepartmentWindow = this;
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            departmentTable.RefreshTable();
            departmentTable.FillTable();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Department_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            departmentTable.DisplayDisciplinesAndTotalAmountOfExams();
        }
    }
}
