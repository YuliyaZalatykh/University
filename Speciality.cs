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
    public partial class Speciality : UserControl
    {

        Query controller;
        Query controller2;

        public Speciality()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
            controller2 = new Query(ConnectionString.ConnStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = controller.UpdateTable("Специальности");
            dataGridView1.DataSource = dataTable;
        }

        public void displayDisciplinesAndTotal()
        {
            int SpecialityCode = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Код_спец"].Value.ToString());

            DataTable dataTable = controller2.UpdateTable("Дисциплина");

            List<Discipline> disciplines = new List<Discipline>();

            foreach (DataRow row in dataTable.Rows)
            {
                int SpecCode = int.Parse(row["Код_спец"].ToString());
                if (SpecialityCode == SpecCode)
                {
                    int Code = int.Parse(row["Код_дисц"].ToString());
                    string Name = row["Назв_дисц"].ToString();
                    int Semest = int.Parse(row["Семестры"].ToString());
                    int Hours = int.Parse(row["Часы"].ToString());
                    int LabH = int.Parse(row["Лаб_зан"].ToString());
                    int PractiseH = int.Parse(row["Практ_зан"].ToString());
                    int CourseH = int.Parse(row["Курсовые"].ToString());
                    string ReportType = row["Вид_отчет"].ToString();

                    Discipline discipline = new Discipline(Code, Name, Semest, Hours, LabH, PractiseH, CourseH, ReportType, SpecCode);
                    disciplines.Add(discipline);
                }
            }

            DataTable dataTable2 = Utils.Utils.ToDataTable(disciplines);
            dataGridView2.DataSource = dataTable2;

            // найти семестровую нагрузку кафедры по специальности
            int firstSemest = 0;
            int secondSemest = 0;
            foreach (Discipline discipline in disciplines)
            {
                switch (discipline.Semest)
                {
                    case 1:
                        firstSemest += discipline.Hours;
                        break;
                    case 2:
                        secondSemest += discipline.Hours;
                        break;
                }
            }

            textBox1.Text = firstSemest.ToString();
            textBox2.Text = secondSemest.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int SpecialityCode = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Код_спец"].Value.ToString());
            
            DataTable dataTable = controller2.UpdateTable("Дисциплина");

            List<Discipline> disciplines = new List<Discipline>();

            foreach (DataRow row in dataTable.Rows)
            {
                int SpecCode = int.Parse(row["Код_спец"].ToString());
                if (SpecialityCode == SpecCode)
                {
                    int Code = int.Parse(row["Код_дисц"].ToString());
                    string Name = row["Назв_дисц"].ToString();
                    int Semest = int.Parse(row["Семестры"].ToString());
                    int Hours = int.Parse(row["Часы"].ToString());
                    int LabH = int.Parse(row["Лаб_зан"].ToString());
                    int PractiseH = int.Parse(row["Практ_зан"].ToString());
                    int CourseH = int.Parse(row["Курсовые"].ToString());
                    string ReportType = row["Вид_отчет"].ToString();

                    Discipline discipline = new Discipline(Code, Name, Semest, Hours, LabH, PractiseH, CourseH, ReportType, SpecCode);
                    disciplines.Add(discipline);
                }
            }

            DataTable dataTable2 = Utils.Utils.ToDataTable(disciplines);
            dataGridView2.DataSource = dataTable2;

            // найти семестровую нагрузку кафедры по специальности
            int firstSemest = 0;
            int secondSemest = 0;
            foreach (Discipline discipline in disciplines)
            {
                switch (discipline.Semest)
                {
                    case 1: firstSemest += discipline.Hours;
                        break;
                    case 2: secondSemest += discipline.Hours;
                        break;
                }
            }

            textBox1.Text = firstSemest.ToString();
            textBox2.Text = secondSemest.ToString();
        }

        private void Speciality_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            displayDisciplinesAndTotal();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form addForm = new AddSpeciality();
            addForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int SpecCode = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            try
            {
                controller.DeleteSpecility(SpecCode);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка!" + ex.StackTrace, "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            button1_Click(sender, e);
            MessageBox.Show("Запись успешно удалена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);   
        }
    }
}
