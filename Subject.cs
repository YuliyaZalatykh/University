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
        List<Discipline> disciplines;


        public Subject()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = controller.UpdateTable("Дисциплина");
            dataGridView1.DataSource = dataTable;

            disciplines = new List<Discipline>();

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

            DataTable dataTable1 = Utils.Utils.ToDataTable(disciplines);
            dataGridView1.DataSource = dataTable1;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int minDuration = int.MaxValue;
            int minDurationIndex = 0;
            int maxDuration = int.MinValue;
            int maxDurationIndex = 0;

            // перебрать дисциплины и найти мин и макс часы
            for(int i = 0; i < disciplines.Count; i++) 
            {
                if(disciplines[i].Hours < minDuration)
                {
                    minDuration = disciplines[i].Hours;
                    minDurationIndex = i;
                }
                if(disciplines[i].Hours > maxDuration)
                {
                    maxDuration = disciplines[i].Hours;
                    maxDurationIndex = i;
                }
            }

            // раскрасить строки таблицы
            for (int i = 0; i < 9; i++)
            {
                dataGridView1.Rows[minDurationIndex].Cells[i].Style.BackColor = Color.GreenYellow;
                dataGridView1.Rows[maxDurationIndex].Cells[i].Style.BackColor = Color.Coral;
            }
        }

        private void Subject_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
