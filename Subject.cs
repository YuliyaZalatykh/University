using System;
using System.Windows.Forms;
using University.BO;

namespace University
{
    public partial class Subject : UserControl
    {

        SubjectTable subjectTable;


        public Subject()
        {
            Program.SubjectWindow = this;
            InitializeComponent();
            subjectTable = new SubjectTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            subjectTable.FillTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            subjectTable.ShowMinAndMaxTotalHours();
        }

        private void Subject_Load(object sender, EventArgs e)
        {
            subjectTable.RefreshTable();
            subjectTable.CalculateTotalHours();
            subjectTable.FillTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            subjectTable.FillTable();
        }
    }
}
