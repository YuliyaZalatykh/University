using System;
using System.Collections.Generic;
using System.Data;
using University.Controller;

namespace University.BO
{
    class DepartmentTable
    {
        private List<Department> departments = new List<Department>();
        private List<Subject> subjects = new List<Subject>();
        Query departmentQuery = new Query(ConnectionString.ConnStr);
        Query specialityQuery = new Query(ConnectionString.ConnStr);
        Query subjectQuery = new Query(ConnectionString.ConnStr);
        Query subjectQuery1 = new Query(ConnectionString.ConnStr);

        public DepartmentTable()
        {
            RefreshTable();
        }

        public void RefreshTable()
        {
            departments.Clear();
            DataTable dataTable = departmentQuery.UpdateTable("Кафедра");

            foreach (DataRow row in dataTable.Rows)
            {
                int code = int.Parse(row[0].ToString());
                string name = row[1].ToString();
                string phone = row[2].ToString();
                int facultyCode = int.Parse(row[3].ToString());
                Department department = new Department(code, name, phone, facultyCode);
                departments.Add(department);
            }
            Program.DepartmentWindow.dataGridView2.Rows.Clear();
            Program.DepartmentWindow.dataGridView2.Columns.Clear();
        }

        public void FillTable()
        {
            DataTable dataTable = Utils.Utils.ToDataTable(departments);
            Utils.Utils.RenameTableColumns(dataTable,
                "код кафедры, название, телефоны, код факультета");
            Program.DepartmentWindow.dataGridView1.DataSource = dataTable;
        }

        public void DisplayDisciplinesAndTotalAmountOfExams()
        {
            int facultyCode = int.Parse(Program.DepartmentWindow.dataGridView1.Rows[Program.DepartmentWindow.dataGridView1.CurrentRow.Index].Cells["код кафедры"].Value.ToString());

            DataTable dataTable = specialityQuery.SelectSubjectsByDepartmentCode(facultyCode);

            List<Subject> subjects = new List<Subject>();
            
            int totalAmountOfExams = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                int subjectCode = int.Parse(row["код дисциплины"].ToString());
                string subjectName = row["название"].ToString();
                DataTable subjectsDataTable = subjectQuery.SelectSubjectByCode(subjectCode, true);
                foreach (DataRow subjectRow in subjectsDataTable.Rows)
                {
                    string report = subjectRow["отчет"].ToString();
                    Subject subject = new Subject(subjectCode, subjectName, 0);
                    subject.report = report;
                    subjects.Add(subject);

                    if (!subject.report.Equals(""))
                    {
                        totalAmountOfExams += 1;
                    }
                }
            }

            dataTable.Columns.Remove("код кафедры");
            Program.DepartmentWindow.dataGridView2.DataSource = dataTable;

            Program.DepartmentWindow.textBox1.Text = totalAmountOfExams.ToString();
        }
    }
}
