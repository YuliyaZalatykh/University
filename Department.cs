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

        private void button2_Click(object sender, EventArgs e)
        {
            int DepartmentCode = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Код_каф"].Value.ToString());

            DataTable specialityTable = controller2.UpdateTable("Специальности");
            List<BO.Speciality> specialities = new List<BO.Speciality>();
            
            foreach(DataRow SpecialityRow in specialityTable.Rows) {
                int DeptCode = int.Parse(SpecialityRow["Код_каф"].ToString());
                if(DepartmentCode == DeptCode)
                {
                    int SpecCode = int.Parse(SpecialityRow["Код_спец"].ToString());
                    string Name = SpecialityRow["Назв_спец"].ToString();
                    string Qualification = SpecialityRow["Квалифик"].ToString();
                    string StudyForm = SpecialityRow["Форма_обуч"].ToString();
                    int Duration = int.Parse(SpecialityRow["Продолжительность"].ToString());

                    BO.Speciality speciality = new BO.Speciality(SpecCode, Name, Qualification, StudyForm, DeptCode, Duration);
                    specialities.Add(speciality);
                }
            }
            
            
            DataTable dataTable = controller3.UpdateTable("Дисциплина");

            List<Discipline> disciplines = new List<Discipline>();

            int totalAmountOfExams = 0;

            foreach (BO.Speciality speciality in specialities)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    int SpecCode = int.Parse(row["Код_спец"].ToString());
                    if (speciality.Code == SpecCode)
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
                        totalAmountOfExams += int.Parse(discipline.ReportType);
                    }
                }
            }

            DataTable dataTable2 = Utils.Utils.ToDataTable(disciplines);
            dataGridView2.DataSource = dataTable2;


            textBox1.Text = totalAmountOfExams.ToString();
        }

        private void Department_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button2_Click(sender, e);
        }
    }
}
