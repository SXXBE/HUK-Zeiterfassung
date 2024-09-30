using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static HUK_Zeiterfassung.Projects;
using static HUK_Zeiterfassung.Timeentries;

namespace HUK_Zeiterfassung
{
    /// <summary>
    /// Interaktionslogik für Timeentries.xaml
    /// </summary>
    public partial class Timeentries : Page
    {
        public static ObservableCollection<TimeEntryClass> TimeEntries { get; set; }

        public Timeentries()
        {
            InitializeComponent();
            DataContext = this;
            LoadTimeEntries();
        }

        public async void SetMessageLabel(string message)
        {
            MessageLabel.Content = message;
            await Task.Delay(3000);
            MessageLabel.Content = "";
        }


        private void EditTimeEntryButton_Click(object sender, RoutedEventArgs e)
        {
            string RFC = "";
            string name = "";
            string startTime = "";
            string endTime = "";

            if (ListBoxTimeEntry.SelectedItem != null)
            {
                Grid.SetRowSpan(ListBoxTimeEntry, 1);
                NewTimeEntryGrid.Visibility = Visibility.Collapsed;
                EditTimeEntryGrid.Visibility = Visibility.Visible;

                List<string> projects = new List<string>();

                SQLiteConnection m_dbConnection = new SQLiteConnection(DBHandler.CONNECTION_STRING);
                m_dbConnection.Open();
                string sqlQuery = $"SELECT name FROM projects;";
                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConnection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                projects.Add(reader.GetString(0));
                            }
                        }
                        else
                        {
                            projects.Add("");
                        }
                    }
                }

                editedProjectName.ItemsSource = projects;
                m_dbConnection.Close();


                int selectedTimeIndex = ListBoxTimeEntry.SelectedIndex;
                if (ListBoxTimeEntry.ItemsSource is ObservableCollection<TimeEntryClass> NewEntryList)
                {
                    TimeEntryClass selectedEntry = NewEntryList[selectedTimeIndex];
                    int entryId = int.Parse(selectedEntry.EntryID);

                    SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING);
                    connection.Open();
                    sqlQuery = $"SELECT RFC, projectName, startTime, endTime FROM timesheet WHERE entryID = '{entryId}';";
                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Datensätze lesen und in Variablen speichern
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                    RFC = reader.GetString(0);

                                if (!reader.IsDBNull(1))
                                    name = reader.GetString(1);

                                if (!reader.IsDBNull(2))
                                    startTime = reader.GetString(2);

                                if (!reader.IsDBNull(3))
                                    endTime = reader.GetString(3);
                            
                        }
                        }
                    }

                    editedID.Text = entryId.ToString();
                    editedRFC.Text = RFC;
                    editedProjectName.SelectedValue = name;
                    editedStartTime.Text = startTime;
                    editedEndTime.Text = endTime;


                }
            }
            else { SetMessageLabel("Bitte Projekt auswählen"); }

            int selectedIndex = ListBoxTimeEntry.SelectedIndex;
            if (selectedIndex != -1)
            {
                Grid.SetRowSpan(ListBoxTimeEntry, 1);
                EditTimeEntryGrid.Visibility = Visibility.Visible;

            } else { SetMessageLabel("Kein Eintrag ausgewählt"); }

            
        }

        private void CancelEditTimeEntry_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetRowSpan(ListBoxTimeEntry, 2);
            EditTimeEntryGrid.Visibility = Visibility.Collapsed;
        }

        private bool VerifyUserInput(string startTimeEntered, string endTimeEntered, string date, string type)
        {
            string pattern = @"^([0-1]?[0-9]|2[0-3]):([0-5]?[0-9]):([0-5]?[0-9])$";
            if (Regex.IsMatch(startTimeEntered, pattern))
            {
                if (Regex.IsMatch(endTimeEntered, pattern))
                {
                    DateTime startTime = DateTime.ParseExact(startTimeEntered, "HH:mm:ss", null);
                    DateTime endTime = DateTime.ParseExact(endTimeEntered, "HH:mm:ss", null);

                    if (startTime > endTime)
                    {
                        SetMessageLabel("Startzeit liegt nach Endzeit");
                        return false;
                    }
                    else if (endTime == startTime)
                    {
                        SetMessageLabel("Start- und Endzeit sind identisch");
                        return false;
                    }
                    else
                    {
                        int result = -1;

                        using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
                        {
                            connection.Open();
                            string sqlQuery = $"SELECT COUNT(*) FROM timesheet WHERE date = '{date}' AND " +
                                                $"((startTime <= '{endTimeEntered}' AND endTime >= '{startTimeEntered}') OR " +
                                                $"(startTime >= '{startTimeEntered}' AND startTime < '{endTimeEntered}') OR " +
                                                $"(endTime > '{startTimeEntered}' AND endTime <= '{endTimeEntered}'));";

                            using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                            {
                                object resultObj = command.ExecuteScalar();
                                result = Convert.ToInt32(resultObj);
                            }
                        }

                        if(type == "new")
                        {
                            if(result == 0)
                            {
                                return true;
                            } else 
                            {
                                SetMessageLabel("Eintrag überschneidet sich");
                                return false; 
                            }
                        } else
                        {
                            if(result <= 1)
                            {
                                return true;
                            }
                            else
                            {
                                SetMessageLabel("Eintrag überschneidet sich");
                                return false;
                            }
                        }

                    }
                }
                else
                {
                    SetMessageLabel("Endzeit hat das falsche Format");
                    return false;
                }
            }
            else
            {
                SetMessageLabel("Startzeit hat das falsche Format");
                return false;
            }
        }

        private void SendEditTimeEntry_Click(object sender, RoutedEventArgs e)
        {
            string date = Tracker.GetCalendarDate();

            bool result = VerifyUserInput(editedStartTime.Text, editedEndTime.Text, date, "edit");

            if (result)
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
                {
                    connection.Open();
                    string sqlQuery = $"UPDATE timesheet SET RFC = '{editedRFC.Text}', projectName = '{editedProjectName.Text}', startTime = '{editedStartTime.Text}', endTime = '{editedEndTime.Text}' WHERE entryID = '{editedID.Text}' AND date = '{date}';";

                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        command.ExecuteScalar();
                    }
                }
                Grid.SetRowSpan(ListBoxTimeEntry, 2);
                EditTimeEntryGrid.Visibility = Visibility.Collapsed;
                LoadTimeEntries();

                Tracker.LoadWeeklyProjectsEntries(date);
            }

        }

        public class TimeEntryClass
        {
            public string EntryID { get; set; }
            public string RFC { get; set; }
            public string ProjectName { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Duration { get; set; }
        }

        public static void LoadTimeEntries()
        {
            ObservableCollection<TimeEntryClass> timeEntryClasses = new ObservableCollection<TimeEntryClass>();

            // Neu: Überprüfe, ob TimeEntries null ist, und initialisiere es gegebenenfalls
            if (TimeEntries == null)
            {
                TimeEntries = new ObservableCollection<TimeEntryClass>();
            }
            else
            {
                TimeEntries.Clear();
            }

            string date = Tracker.SelectedDate != null ? Tracker.SelectedDate.ToString() : DateTime.Now.ToString();
            string CalendarDate = Tracker.GetCalendarDate();

            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = $"SELECT entryID, RFC, projectName, startTime, endTime FROM timesheet WHERE date = '{CalendarDate}' ORDER BY startTime DESC ;";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string parsedTimeDuration = "";
                            DateTime startTime = DateTime.Parse(reader["startTime"].ToString());
                            string checkEndTime = reader["endTime"].ToString();
                            if(checkEndTime != "")
                            {
                                DateTime endTime = DateTime.Parse(reader["endTime"].ToString());
                                TimeSpan timeDuration = endTime - startTime;
                                parsedTimeDuration = timeDuration.ToString();
                            }
                            else { parsedTimeDuration = ""; }
                            

                            TimeEntryClass timeEntry = new TimeEntryClass
                            {
                                EntryID = reader["entryID"].ToString(),
                                RFC = reader["RFC"].ToString(),
                                ProjectName = reader["projectName"].ToString(),
                                StartTime = reader["startTime"].ToString(),
                                EndTime = reader["endTime"].ToString(),
                                Duration = parsedTimeDuration
                            };
                            timeEntryClasses.Add(timeEntry);
                        }
                    }
                }
            }
            // Neu: Aktualisiere TimeEntries vor dem Hinzufügen von Daten
            foreach (var entry in timeEntryClasses)
            {
                TimeEntries.Add(entry);
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ListBoxTimeEntry.SelectedIndex;
            if (ListBoxTimeEntry.ItemsSource is ObservableCollection<TimeEntryClass> TimeEntryRemover)
            {
                TimeEntryClass selectedEntry = TimeEntryRemover[selectedIndex];
                int entryId = int.Parse(selectedEntry.EntryID);

                SQLiteConnection dbConnection = new SQLiteConnection(DBHandler.CONNECTION_STRING);
                dbConnection.Open();
                string sqlQuery = $"DELETE FROM timesheet WHERE entryID = {entryId};";
                SQLiteCommand command = new SQLiteCommand(sqlQuery, dbConnection);
                command.ExecuteReader();
                NavigationService.Refresh();
                ConfirmButton.Visibility = Visibility.Collapsed;

                string date = Tracker.GetCalendarDate();
                Tracker.LoadWeeklyProjectsEntries(date);

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelButton.Visibility = Visibility.Collapsed;
            ConfirmButton.Visibility = Visibility.Collapsed;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTimeEntry.SelectedItem != null)
            {
                SetMessageLabel("");
                ConfirmButton.Visibility = Visibility.Visible;
                CancelButton.Visibility = Visibility.Visible;

            } else { SetMessageLabel("Kein EIntrag ausgewählt"); }
        }

        private void SendNewTimeEntry_Click(object sender, RoutedEventArgs e)
        {
            if(newProjectName.SelectedValue != null )
            {
                string date = Tracker.GetCalendarDate();

                bool result = VerifyUserInput(newStartTime.Text, newEndTime.Text, date, "new");
                if (result)
                {

                    using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
                    {
                        connection.Open();
                        string sqlQuery = $"INSERT INTO timesheet (RFC, projectName, startTime, endTime, date) VALUES ('{newRFC.Text}', '{newProjectName.SelectedValue.ToString()}', '{newStartTime.Text}', '{newEndTime.Text}', '{date}');";

                        using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                        {
                            command.ExecuteScalar();
                        }
                    }
                    Grid.SetRowSpan(ListBoxTimeEntry, 2);
                    NewTimeEntryGrid.Visibility = Visibility.Collapsed;
                    LoadTimeEntries();
                    Tracker.LoadWeeklyProjectsEntries(date);

                    newRFC.Text = string.Empty;
                    newProjectName.SelectedValue = string.Empty;
                    newStartTime.Text = string.Empty;
                    newEndTime.Text = string.Empty;

                }
            }  else { SetMessageLabel("Bitte Projekt auswählen"); }         
            
            
        }

        private void CancelNewTimeEntry_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetRowSpan(ListBoxTimeEntry, 2);
            NewTimeEntryGrid.Visibility = Visibility.Collapsed;
            newRFC.Text = string.Empty;
            newProjectName.SelectedValue = string.Empty;
            newStartTime.Text = string.Empty;   
            newEndTime.Text = string.Empty; 
        }

        private void editedProjectName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(editedProjectName.SelectedValue != null)
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
                {
                    connection.Open();
                    string sqlQuery = $"SELECT RFC FROM projects WHERE name = '{editedProjectName.SelectedValue.ToString()}';";
                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        object result = command.ExecuteScalar();
                        editedRFC.Text = result.ToString();
                    }
                }
            }

            
        }

        public void SetDateNewEntry()
        {
            newDate.Text = Tracker.SelectedDate.ToString();
        }

        public void NewEntry_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetRowSpan(ListBoxTimeEntry, 1);
            EditTimeEntryGrid.Visibility = Visibility.Collapsed;
            NewTimeEntryGrid.Visibility = Visibility.Visible;
            newDate.Text = Tracker.GetCalendarDate();
            SetDateNewEntry();

            List<string> projects = new List<string>();

            SQLiteConnection m_dbConnection = new SQLiteConnection(DBHandler.CONNECTION_STRING);
            m_dbConnection.Open();
            string sqlQuery = $"SELECT name FROM projects WHERE isActive = 'True';";
            using (SQLiteCommand command = new SQLiteCommand(sqlQuery, m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Datensätze lesen und in Variablen speichern
                    while (reader.Read())
                    {
                        projects.Add(reader.GetString(0));
                    }
                    newProjectName.ItemsSource = projects;
                }
            }

        }

        private void newProjectName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (newProjectName.SelectedValue != null)
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
                {
                    connection.Open();
                    string sqlQuery = $"SELECT RFC FROM projects WHERE name = '{newProjectName.SelectedValue.ToString()}';";
                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        object result = command.ExecuteScalar();
                        newRFC.Text = result.ToString();
                    }
                }
            }
        }
    }
}
