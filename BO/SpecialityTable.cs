using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using University.Controller;

namespace University.BO
{
    class SpecialityTable
    {
        private List<Speciality> specialities = new List<Speciality>();
        private List<Subject> subjects = new List<Subject>();
        Query specialityQuery = new Query(ConnectionString.ConnStr);
        Query subjectQuery = new Query(ConnectionString.ConnStr);
        Query deleteQuery = new Query(ConnectionString.ConnStr);


        public SpecialityTable()
        {
            RefreshTable();
        }

        public void RefreshTable()
        {
            specialities.Clear();
            DataTable dataTable = specialityQuery.UpdateTable("Специальность");

            foreach (DataRow row in dataTable.Rows)
            {
                int code = int.Parse(row[0].ToString());
                string name = row[1].ToString();
                string qualification = row[2].ToString();
                string educationForm = row[3].ToString();
                int departmentCode = int.Parse(row[4].ToString());
                Speciality speciality = new Speciality(code, name, qualification, educationForm, departmentCode);
                specialities.Add(speciality);
            }
        }

        public void FillTable()
        {
            DataTable dataTable = Utils.Utils.ToDataTable(specialities);
            Utils.Utils.RenameTableColumns(dataTable,
                "код специальности, название, квалификация, форма, код кафедры");
            Program.SpecialityWindow.dataGridView1.DataSource = dataTable;
        }

        public void FindSubjectsBySpeciality()
        {
            int code = int.Parse(Program.SpecialityWindow.dataGridView1.Rows[Program.SpecialityWindow.dataGridView1.CurrentRow.Index].Cells["код специальности"].Value.ToString());
           
            DataTable dataTable = subjectQuery.SelectSubjectByCode(code, false);

            subjects.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                int subjectCode = int.Parse(row["код дисциплины"].ToString());
                DataTable subjectInfo = specialityQuery.SelectSubject(subjectCode);
                string subjectName = subjectInfo.Rows[0].ItemArray[1].ToString();

                int semester = int.Parse(row["семестры"].ToString());
                int hours = int.Parse(row["часы"].ToString());
                int labHours = int.Parse(row["лабораторные"].ToString());
                int practiseHours = int.Parse(row["практические"].ToString());
                int courseHours = int.Parse(row["курсовые"].ToString());
                string report = row["отчет"].ToString();
                Subject subject = new Subject(subjectCode, subjectName, 0);
                subject.semester = semester;
                subject.hours = hours;
                subject.labHours = labHours;
                subject.practiseHours = practiseHours;
                subject.courseHours = courseHours;
                subject.report = report;
                subjects.Add(subject);
            }

            DataTable subjectsTable = Utils.Utils.ToDataTable(subjects);
            subjectsTable.Columns.Remove("departmentCode");
            subjectsTable.Columns.Remove("totalHours");
            Utils.Utils.RenameTableColumns(subjectsTable,
                    "код дисциплины, название, семестры, часы, лабораторные, практические, курсовые, отчет");
            Program.SpecialityWindow.dataGridView2.DataSource = subjectsTable;
        }

        public void CalculateCapacity()
        {
            int firstSemester = 0;
            int secondSemester = 0;
            int labHours = 0;
            foreach (Subject subject in subjects)
            {
                switch (subject.semester)
                {
                    case 1:
                        firstSemester += subject.hours;
                        break;
                    case 2:
                        secondSemester += subject.hours;
                        break;
                }
                labHours += subject.labHours;
            }

            Program.SpecialityWindow.textBox1.Text = firstSemester.ToString();
            Program.SpecialityWindow.textBox2.Text = secondSemester.ToString();
            Program.SpecialityWindow.textBox3.Text = labHours.ToString();
        }

        public void DeleteSpeciality()
        {
            const string message =
                "Вы уверены, что хотите удалить выбранную запись?";
            const string caption = "Предупреждение";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int SpecCode = int.Parse(Program.SpecialityWindow.dataGridView1.Rows[Program.SpecialityWindow.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
                try
                {
                    deleteQuery.DeleteSpecility(SpecCode);
                    MessageBox.Show("Запись успешно удалена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + ex.StackTrace, "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
