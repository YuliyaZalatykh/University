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
                    int SpecCode = int.Parse(SpecialityRow["Код_спец"].ToString());
                    string Name = SpecialityRow["Назв_спец"].ToString();
                    string Qualification = SpecialityRow["Квалифик"].ToString();
                    string StudyForm = SpecialityRow["Форма_обуч"].ToString();
                    int DeptCode = int.Parse(SpecialityRow["Код_каф"].ToString());
                    int Duration = int.Parse(SpecialityRow["Продолжительность"].ToString());

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
