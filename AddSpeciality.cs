using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using University.Controller;
using University.Utils;

namespace University
{
    public partial class AddSpeciality : Form
    {
        Query controller;

        public AddSpeciality()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int SpecCode = int.Parse(textBox1.Text);
            string Name = textBox2.Text;
            string Qualification = textBox3.Text;
            string StudyForm = textBox4.Text;
            int DeptCode = int.Parse(comboBox2.Text);
            try
            {
                controller.AddSpeciality(SpecCode, Name, Qualification, StudyForm, DeptCode);
                MessageBox.Show("Специальность успешно добавлена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка! Проверьте правильность заполнения полей" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
            clearFields();
        }

        private void clearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddSpeciality_Load(object sender, EventArgs e)
        {
            DataTable SpecialityTable = controller.UpdateTable("Специальность");
            List<BO.Speciality> Specialities = Utils.Utils.SpecialityTableToList(SpecialityTable);

            HashSet<Object> facultyCodes = new HashSet<object>();

            foreach (BO.Speciality Speciality in Specialities)
            {
                facultyCodes.Add(Speciality.facultyCode);
            }
            comboBox2.Items.AddRange(facultyCodes.ToArray());
        }
    }
}
