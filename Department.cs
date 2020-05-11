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
using University.BO;
using University.Utils;

namespace University
{
    public partial class Department : UserControl
    {

        Query controller;
        Query controller2;
        Query controller3;

        public Department()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
            controller2 = new Query(ConnectionString.ConnStr);
            controller3 = new Query(ConnectionString.ConnStr);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = controller.UpdateTable("Кафедра");
            dataGridView1.DataSource = dataTable;
        }

        public void displayDisciplinesAndTotalAmountOfExams()
        {
            int DepartmentCode = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Код_каф"].Value.ToString());

            // найти специальности заданной кафедры

            DataTable specialityTable = controller2.UpdateTable("Специальности");
            List<BO.Speciality> specialities = Utils.Utils.SpecialityTableToList(specialityTable);
            List<BO.Speciality> DeptSpecialities = new List<BO.Speciality>();

            foreach (BO.Speciality speciality in specialities)
            {
                if (DepartmentCode == speciality.DepartmentCode)
                {
                    DeptSpecialities.Add(speciality);
                }
            }


            // найти дисциплины заданных специальностей

            DataTable disciplineTable = controller3.UpdateTable("Дисциплина");
            List<Discipline> disciplines = Utils.Utils.DisciplineTableToList(disciplineTable);
            List<Discipline> SpecialityDisciplines = new List<Discipline>();

            int totalAmountOfExams = 0;
            
            foreach (BO.Speciality deptSpeciality in DeptSpecialities)
            {
                foreach (Discipline discipline in disciplines)
                {
                    if (deptSpeciality.Code == discipline.SpecCode)
                    {
                        SpecialityDisciplines.Add(discipline);
                        // TO DO: пересмотреть таблицу Дисциплина -> поле Тип отчета
                        totalAmountOfExams += int.Parse(discipline.ReportType);
                    }
                }
            }

            DataTable disciplinesTable = Utils.Utils.ToDataTable(SpecialityDisciplines);
            Utils.Utils.RenameTableColumns(disciplinesTable,
                "код дисциплины, название, семестр, часы, лабораторные, " +
                "практические, курсовые, тип отчета, код специальности");
            dataGridView2.DataSource = disciplinesTable;

            textBox1.Text = totalAmountOfExams.ToString();
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
            displayDisciplinesAndTotalAmountOfExams();
        }
    }
}
