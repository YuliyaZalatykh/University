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
    public partial class Subject : UserControl
    {

        Query controller;

        public Subject()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = controller.UpdateTable("Дисциплина");
            dataGridView1.DataSource = dataTable;

            List<Discipline> disciplines = new List<Discipline>();

            foreach (DataRow row in dataTable.Rows)
            {
                int Code = int.Parse(row["Код_дисц"].ToString());
                string Name = row["Назв_дисц"].ToString();
                int Semest = int.Parse(row["Семестры"].ToString());
                int Hours = int.Parse(row["Часы"].ToString());
                int LabH = int.Parse(row["Лаб_зан"].ToString());
                int PractiseH = int.Parse(row["Практ_зан"].ToString());
                int CourseH = int.Parse(row["Курсовые"].ToString());
                string ReportType = row["Вид_отчет"].ToString();
                int SpecCode = int.Parse(row["Код_спец"].ToString());
                Discipline discipline = new Discipline(Code, Name, Semest, Hours, LabH, PractiseH, CourseH, ReportType, SpecCode);
                disciplines.Add(discipline);
            }
            textBox1.Text = disciplines[1].Name;
            
            disciplines.Remove(disciplines[1]);
            
            DataTable dataTable1 = Utils.Utils.ToDataTable(disciplines);
            dataGridView1.DataSource = dataTable1;
            
        }
    }
}
