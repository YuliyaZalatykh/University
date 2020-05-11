using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace University.Controller
{
    class Query
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataAdapter dataAdapter;
        DataTable bufferTable;
        public Query(string Conn)
        {
            connection = new OleDbConnection(Conn);
            bufferTable = new DataTable();
        }

        public DataTable UpdateTable(string tableName)
        {
            connection.Open();
            string selectQuery = String.Format("SELECT * FROM {0}", tableName);
            dataAdapter = new OleDbDataAdapter(selectQuery, connection);
            bufferTable.Clear();
            dataAdapter.Fill(bufferTable);
            connection.Close();
            return bufferTable;
        }

        public void AddSpeciality(int SpecCode, string Name, string Qualification, string StudyForm, int DeptCode, int Duration)
        {
            connection.Open();
            command = new OleDbCommand($"INSERT INTO Специальность([код специальности], название, квалификация, [форма обучения], [код кафедры], продолжительность) VALUES(@SpecCode, @Name, @Qualification, @StudyForm, @DeptCode, @Duration)", connection);
            command.Parameters.AddWithValue("SpecCode", SpecCode);
            command.Parameters.AddWithValue("Name", Name);
            command.Parameters.AddWithValue("Qualification", Qualification);
            command.Parameters.AddWithValue("StudyForm", StudyForm);
            command.Parameters.AddWithValue("DeptCode", DeptCode);
            command.Parameters.AddWithValue("Duration", Duration);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteSpecility(int SpecCode)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM Специальность WHERE [код специальности] = {SpecCode}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
