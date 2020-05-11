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

            int labHoursAmount = 0;
            int semestAmount = 0;
            foreach (Discipline discipline in disciplines)
            {
                labHoursAmount += discipline.LabH;
                semestAmount += discipline.Semest;
            }

            textBox1.Text = labHoursAmount.ToString();
            textBox2.Text = semestAmount.ToString();
        }
    }
}
