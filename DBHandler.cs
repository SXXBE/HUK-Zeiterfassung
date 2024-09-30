using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HUK_Zeiterfassung
{
    internal class DBHandler
    { 

        public const string DATABASE_FILENAME = "database.sqlite";

        public static string CONNECTION_STRING
        {
            get
            {
                string databaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nerd-Work");
                string databasePath = Path.Combine(databaseDirectory, DATABASE_FILENAME);

                return $"Data Source={databasePath};Version=3;";
            }
        }

        public static bool CreateNewDatabase()
        {
            try
            {
                List<string> sqlCommands = new List<string>
                {
                    "CREATE TABLE projects (projectID INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(200), RFC VARCHAR(20), RFCID VARCHAR(50), isActive BOOL, isSecured BOOL)",
                    "CREATE TABLE timesheet (entryID INTEGER PRIMARY KEY AUTOINCREMENT, RFC VARCHAR(20), projectName VARCHAR(200), startTime TEXT, endTime TEXT, date TEXT)",
                    "CREATE TABLE userSettings (timerPopUp BOOL,timerPopUpBorderColor VARCHAR(30), timerPopUpBackgroundColor VARCHAR(30), timerPopUpProjectColor VARCHAR(30), timerPopUpTimeColor VARCHAR(30), timerPopUpReminderColor VARCHAR(39), autostart BOOL, startTrackingAutostart BOOL, startProject VARCHAR(200), monitor INTEGER, direction TEXT, sliderTimeout TEXT, showInSystemtray BOOL, showInTaskbar BOOL)",
                    "CREATE TABLE projectArchive (projectID INTEGER PRIMARY KEY , name VARCHAR(200), RFC VARCHAR(20), RFCID VARCHAR(50), isSecured BOOL)"
                };


                SQLiteConnection.CreateFile("database.sqlite");
                using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING))
                {
                    connection.Open();

                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        foreach (string sqlCommand in sqlCommands)
                        {
                            using (SQLiteCommand sqlliteCommand = new SQLiteCommand(sqlCommand, connection, transaction))
                            {
                                sqlliteCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                }
                MessageBox.Show("Datenbank wurde erstellt");
                return true;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return false;
                throw;
            }

        } 

        public static bool CreateNewProject(string name, string RFC, string RFCID, string isActive, string isSecured)
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection(CONNECTION_STRING);
                m_dbConnection.Open();
                string sqlQuery = $"INSERT INTO projects (name, RFC, RFCID, isActive, isSecured) VALUES ('{name}','{RFC}','{RFCID}','{isActive}', '{isSecured}');";
                SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        } 

        public static void UpdateDB(int version)
        {
            string query = "";

            List<string> columnsToCheck = new List<string> { "timerPopUpReminderColor" };

            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING))
            {
                connection.Open();

                foreach (string column in columnsToCheck)
                {
                    query = $"SELECT name FROM pragma_table_info('userSettings') WHERE name='{column}';";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result.ToString() != column)
                        {
                            query = $"";
                        }

                    }
                }
            }
        }


    }
}
