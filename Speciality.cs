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
    public partial class Speciality : UserControl
    {

        Query controller;
        Query controller2;
        Query controller3;

        public Speciality()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
            controller2 = new Query(ConnectionString.ConnStr);
            controller3 = new Query(ConnectionString.ConnStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = controller.UpdateTable("Специальность");
            dataGridView1.DataSource = dataTable;
        }

        public void displayDisciplinesAndTotal()
        {
            int SpecialityCode = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["код специальности"].Value.ToString());

            DataTable dataTable = controller2.UpdateTable("Дисциплина");

            List<Discipline> disciplines = Utils.Utils.DisciplineTableToList(dataTable);
            List<Discipline> specialityDisciplines = new List<Discipline>();

            foreach (Discipline discipline in disciplines)
            {
                if (SpecialityCode == discipline.SpecCode)
                {
                    specialityDisciplines.Add(discipline);
                }
            }

            DataTable dataTable2 = Utils.Utils.ToDataTable(specialityDisciplines);
            Utils.Utils.RenameTableColumns(dataTable2,
                    "код дисциплины, название, семестр, часы, лабораторные, " +
                    "практические, курсовые, тип отчета, код специальности");
            dataGridView2.DataSource = dataTable2;

            // найти семестровую нагрузку кафедры по специальности
            int firstSemest = 0;
            int secondSemest = 0;
            foreach (Discipline discipline in disciplines)
            {
                switch (discipline.Semest)
                {
                    case 1:
                        firstSemest += discipline.Hours;
                        break;
                    case 2:
                        secondSemest += discipline.Hours;
                        break;
                }
            }

            textBox1.Text = firstSemest.ToString();
            textBox2.Text = secondSemest.ToString();
        }

        private void Speciality_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            displayDisciplinesAndTotal();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form addForm = new AddSpeciality();
            addForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            const string message =
                "Вы уверены, что хотите удалить выбранную запись?";
            const string caption = "Предупреждение";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int SpecCode = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
                try
                {
                    controller3.DeleteSpecility(SpecCode);
                    MessageBox.Show("Запись успешно удалена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + ex.StackTrace, "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                button1_Click(sender, e);
            }
        }
    }
}
