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
    }
}
