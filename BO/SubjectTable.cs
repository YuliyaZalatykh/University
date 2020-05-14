using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Controller;

namespace University.BO
{
    class SubjectTable
    {
        private List<Subject> subjects = new List<Subject>();
        Query query = new Query(ConnectionString.ConnStr);

        public SubjectTable()
        {
            RefreshTable();
        }

        public void RefreshTable()
        {
            subjects.Clear();
            DataTable dataTable = query.UpdateTable("Дисциплина");

            foreach (DataRow row in dataTable.Rows)
            {
                int code = int.Parse(row[0].ToString());
                string name = row[1].ToString();
                Subject subject = new Subject(code, name);
                subjects.Add(subject);
            }
        }

        public void FillTable()
        {
            DataTable dataTable = Utils.Utils.ToDataTable(subjects);
            dataTable.Columns.Remove("semester");
            dataTable.Columns.Remove("hours");
            dataTable.Columns.Remove("labHours");
            dataTable.Columns.Remove("practiseHours");
            dataTable.Columns.Remove("courseHours");
            dataTable.Columns.Remove("report");
            Utils.Utils.RenameTableColumns(dataTable,
                "код дисциплины, название, общее кол-во часов");
            Program.SubjectWindow.dataGridView1.DataSource = dataTable;
        }

        // найти общее количество часов для дисциплины
        public void CalculateTotalHours()
        {
            int totalHours = 0;

            foreach(Subject subject in subjects)
            {
                int code = subject.code;
                DataTable dataTable = query.SelectSubjectByCode(code, true);
                foreach(DataRow row in dataTable.Rows)
                {
                    totalHours += int.Parse(row["часы"].ToString());
                }
                subject.totalHours = totalHours;
                totalHours = 0;
            }

        }

        // показать дисциплины с минимальным и максимальным количеством часов
        public void ShowMinAndMaxTotalHours()
        {
            int minHours = int.MaxValue;
            int minHoursIndex = 0;
            int maxHours = int.MinValue;
            int maxHoursIndex = 0;

            for (int i = 0; i < subjects.Count; i++)
            {
                if (subjects[i].totalHours < minHours)
                {
                    minHours = subjects[i].totalHours;
                    minHoursIndex = i;
                }
                if (subjects[i].totalHours > maxHours)
                {
                    maxHours = subjects[i].totalHours;
                    maxHoursIndex = i;
                }
            }

            // раскрасить строки таблицы
            for (int i = 0; i < Program.SubjectWindow.dataGridView1.Columns.Count; i++)
            {
                Program.SubjectWindow.dataGridView1.Rows[minHoursIndex].Cells[i].Style.BackColor = Color.GreenYellow;
                Program.SubjectWindow.dataGridView1.Rows[maxHoursIndex].Cells[i].Style.BackColor = Color.Coral;
            }
        }
    }
} 
