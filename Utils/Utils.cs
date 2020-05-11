using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using University.BO;

namespace University.Utils
{
    static class Utils
    {
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        public static List<BO.Speciality> SpecialityTableToList(DataTable Table)
        {
            List<BO.Speciality> specialities = new List<BO.Speciality>();

            foreach (DataRow SpecialityRow in Table.Rows)
            {     
                    int SpecCode = int.Parse(SpecialityRow["код специальности"].ToString());
                    string Name = SpecialityRow["название"].ToString();
                    string Qualification = SpecialityRow["квалификация"].ToString();
                    string StudyForm = SpecialityRow["форма обучения"].ToString();
                    int DeptCode = int.Parse(SpecialityRow["код кафедры"].ToString());
                    int Duration = int.Parse(SpecialityRow["продолжительность"].ToString());

                    BO.Speciality speciality = new BO.Speciality(SpecCode, Name, Qualification, StudyForm, DeptCode, Duration);
                    specialities.Add(speciality);
            }
            return specialities;
        }

        public static List<BO.Discipline> DisciplineTableToList(DataTable Table)
        {
            List<Discipline> disciplines = new List<Discipline>();

            foreach (DataRow row in Table.Rows)
            {
                    int Code = int.Parse(row["код дисциплины"].ToString());
                    string Name = row["название"].ToString();
                    int Semest = int.Parse(row["семестр"].ToString());
                    int Hours = int.Parse(row["часы"].ToString());
                    int LabH = int.Parse(row["лабораторные"].ToString());
                    int PractiseH = int.Parse(row["практические"].ToString());
                    int CourseH = int.Parse(row["курсовые"].ToString());
                    string ReportType = row["отчет"].ToString();
                    int SpecCode = int.Parse(row["код специальности"].ToString());

                    Discipline discipline = new Discipline(Code, Name, Semest, Hours, LabH, PractiseH, CourseH, ReportType, SpecCode);
                    disciplines.Add(discipline);
            }
            return disciplines;
        }

        public static void RenameTableColumns(DataTable table, string columnNames)
        {
            string[] columns = columnNames.Split(',');
            for(int i = 0; i < columns.Length; i++)
            {
                table.Columns[i].ColumnName = columns[i];
            }
        }
    }
}
