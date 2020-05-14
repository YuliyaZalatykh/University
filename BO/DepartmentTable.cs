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
                int chiefCode = int.Parse(row[4].ToString());
                Department department = new Department(code, name, phone, facultyCode, chiefCode);
                departments.Add(department);
            }
        }

        public void FillTable()
        {
            DataTable dataTable = Utils.Utils.ToDataTable(departments);
            Utils.Utils.RenameTableColumns(dataTable,
                "код кафедры, название, телефоны, код факультета, код заведующего");
            Program.DepartmentWindow.dataGridView1.DataSource = dataTable;
        }

        public void DisplayDisciplinesAndTotalAmountOfExams()
        {
            int departmentCode = int.Parse(Program.DepartmentWindow.dataGridView1.Rows[Program.DepartmentWindow.dataGridView1.CurrentRow.Index].Cells["код кафедры"].Value.ToString());

            DataTable dataTable = specialityQuery.SelectSpecialitiesByDeptCode(departmentCode);

            List<Subject> subjects = new List<Subject>();
            
            int totalAmountOfExams = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                int specialityCode = int.Parse(row["код специальности"].ToString());
                DataTable subjectsDataTable = subjectQuery.SelectSubjectByCode(specialityCode, false);
                foreach (DataRow subjectRow in subjectsDataTable.Rows)
                {
                    int subjectCode = int.Parse(subjectRow["код дисциплины"].ToString());
                    string subjectName = subjectQuery1.SelectSubject(subjectCode).Rows[0].ItemArray[1].ToString();
                    int semester = int.Parse(subjectRow["семестры"].ToString());
                    int hours = int.Parse(subjectRow["часы"].ToString());
                    int labHours = int.Parse(subjectRow["лабораторные"].ToString());
                    int practiseHours = int.Parse(subjectRow["практические"].ToString());
                    int courseHours = int.Parse(subjectRow["курсовые"].ToString());
                    string report = subjectRow["отчет"].ToString();
                    Subject subject = new Subject(subjectCode, subjectName);
                    subject.semester = semester;
                    subject.hours = hours;
                    subject.labHours = labHours;
                    subject.practiseHours = practiseHours;
                    subject.courseHours = courseHours;
                    subject.report = report;
                    subjects.Add(subject);

                    if (!subject.report.Equals(""))
                    {
                        totalAmountOfExams += 1;
                    }
                }
            }

            DataTable subjectsTable = Utils.Utils.ToDataTable(subjects);
            subjectsTable.Columns.Remove("totalHours");
            Utils.Utils.RenameTableColumns(subjectsTable,
                    "код дисциплины, название, семестры, часы, лабораторные, практические, курсовые, отчет");
            Program.DepartmentWindow.dataGridView2.DataSource = subjectsTable;

            Program.DepartmentWindow.textBox1.Text = totalAmountOfExams.ToString();
        }
    }
}
