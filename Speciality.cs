using System;
using System.Windows.Forms;
using University.BO;

namespace University
{
    public partial class Speciality : UserControl
    {

        SpecialityTable specialityTable;

        public Speciality()
        {
            Program.SpecialityWindow = this;
            InitializeComponent();
            specialityTable = new SpecialityTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            specialityTable.RefreshTable();
            specialityTable.FillTable();
        }

        private void Speciality_Load(object sender, EventArgs e)
        {
            specialityTable.RefreshTable();
            specialityTable.FillTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            specialityTable.FindSubjectsBySpeciality();
            specialityTable.CalculateCapacity();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form addForm = new AddSpeciality();
            addForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            specialityTable.DeleteSpeciality();
            specialityTable.RefreshTable();
            specialityTable.FillTable();
        }
    }
}
