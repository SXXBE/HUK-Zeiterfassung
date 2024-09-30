using Syncfusion.Windows.Shared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static HUK_Zeiterfassung.Timeentries;
using static HUK_Zeiterfassung.Tracker;


namespace HUK_Zeiterfassung
{
    public partial class Tracker : Window, INotifyPropertyChanged
    {
        public static DateTime? SelectedDate { get; private set; }
        private static bool tooltipIsActive = false;

        public static ObservableCollection<WeeklyProject> WeeklyProjectTimes { get; set; }
        public static ObservableCollection<WeeklyTimes> WeeklyTimesSum {  get; set; }

        public class WeeklyTimes
        {
            public string MoSum { get; set; }
            public string DiSum { get; set; }
            public string MiSum { get; set; }
            public string DoSum { get; set; }
            public string FrSum { get; set; }
            public string SaSum { get; set; }
            public string SoSum { get; set; }
        }

        private static bool GetTimeFormat()
        {
            string isTimeFormatString = "";

            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = "SELECT isTimeFormat, tooltip FROM userSettings;";
                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            isTimeFormatString = reader.GetString(0);
                            tooltipIsActive = bool.Parse(reader.GetString(1));

                        }
                    }
                }
            }
            bool isTimeFormat = bool.Parse(isTimeFormatString);
            return isTimeFormat;
        }

        private bool isSettingsOpen = false;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Tracker()
       {
            InitializeComponent();
            DataContext = this;
            MainWindowFrame.Navigate(new System.Uri("Timeentries.xaml", System.UriKind.Relative));
            TimeEntriesPageButton.Foreground = System.Windows.Media.Brushes.Black;
            TimeEntriesPageButton.Background = System.Windows.Media.Brushes.White;
            SetCalendarWeek(DateTime.Now);
            LoadWeeklyProjectsEntries(GetCalendarDate());
            this.Height = 840;


        }

        public static string GetCalendarDate()
        {
        
            if(SelectedDate == null)
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                return date;

            } else 
            {
                string originalDate = SelectedDate?.ToString("yyyy-MM-dd");
                string date = originalDate.Substring(0, originalDate.Length - 9);
                return originalDate;
            }
            
        }

        private void TimeEntriesPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new System.Uri("Timeentries.xaml", System.UriKind.Relative));
            TimeEntriesPageButton.Foreground = System.Windows.Media.Brushes.Black;
            TimeEntriesPageButton.Background = System.Windows.Media.Brushes.White;

            ProjectsArchiveButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(14, 14, 14));
            ProjectsArchiveButton.Foreground = System.Windows.Media.Brushes.White;

            ProjectsPageButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(14, 14, 14));
            ProjectsPageButton.Foreground = System.Windows.Media.Brushes.White;

        }

        private void ProjectsPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new System.Uri("Projects.xaml", System.UriKind.Relative));
            ProjectsPageButton.Foreground = System.Windows.Media.Brushes.Black;
            ProjectsPageButton.Background = System.Windows.Media.Brushes.White;

            TimeEntriesPageButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(14, 14, 14));
            TimeEntriesPageButton.Foreground = System.Windows.Media.Brushes.White;

            ProjectsArchiveButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(14, 14, 14));
            ProjectsArchiveButton.Foreground = System.Windows.Media.Brushes.White;
        }

        private void ProjectsArchiveButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new System.Uri("ProjectArchive.xaml", System.UriKind.Relative));
            ProjectsArchiveButton.Foreground = System.Windows.Media.Brushes.Black;
            ProjectsArchiveButton.Background = System.Windows.Media.Brushes.White;

            TimeEntriesPageButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(14, 14, 14));
            TimeEntriesPageButton.Foreground = System.Windows.Media.Brushes.White;

            ProjectsPageButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(14, 14, 14));
            ProjectsPageButton.Foreground = System.Windows.Media.Brushes.White;
        }

        private void SettingsPageButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();

            if (!isSettingsOpen)
            {

                settings.Closed += (s, args) => isSettingsOpen = false;
                settings.Show();

                isSettingsOpen = true;
            }
            else
            {

                MessageBox.Show("Einstellungen bereits geöffnet");
            }
        }


        public class WeeklyProject
        {
            public string Name { get; set; }
            public string RFC { get; set; }
            public string RFCID { get; set; }
            public string Monday { get; set; }
            public string Tuesday { get; set; }
            public string Wednesday { get; set; }
            public string Thursday { get; set; }
            public string Friday { get; set; }
            public string Saturday { get; set; }
            public string Sunday { get; set; }
        }

        public void UpdateWeeklyTimesByTick()
        {
            ///LoadWeeklyProjectsEntries(CustomCalendar.SelectedDate);
        }

        public static void LoadWeeklyProjectsEntries(string selectedDate)
        {
            double moEntry = 0;
            double diEntry = 0;
            double miEntry = 0;
            double doEntry = 0;
            double frEntry = 0;
            double saEntry = 0;
            double soEntry = 0;
            string moEntryS = "";
            string diEntryS = "";
            string miEntryS = "";
            string doEntryS = "";
            string frEntryS = "";
            string saEntryS = "";
            string soEntryS = "";

            string formattedTime = "";

            ObservableCollection<WeeklyProject> NewProjectTime = new ObservableCollection<WeeklyProject>();
            ObservableCollection<WeeklyTimes> weeklyTimes = new ObservableCollection<WeeklyTimes>();
            if (WeeklyTimesSum == null)
            {
                WeeklyTimesSum = new ObservableCollection<WeeklyTimes>();
            }
            else
            {
                WeeklyTimesSum.Clear();
            }
            if (WeeklyProjectTimes == null)
            {
                WeeklyProjectTimes = new ObservableCollection<WeeklyProject>();
            }
            else
            {
                WeeklyProjectTimes.Clear();
            }

            DateTime dateCW = DateTime.ParseExact(selectedDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);


            DateTime firstDayOfWeekWithTime = dateCW.GetFirstDayOfWeek();
            string firstDayOfWeek = firstDayOfWeekWithTime.ToString("yyyy-MM-dd");

            DateTime lastDayOfWeekWithTime = dateCW.GetLastDayOfWeek();
            string lastDayOfWeek = lastDayOfWeekWithTime.ToString("yyyy-MM-dd");
           
            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = $"SELECT t.date, t.RFC, COALESCE(p.RFCID, pa.RFCID) AS RFCID, t.projectName, SUM(strftime('%s', t.endTime) - strftime('%s', t.startTime)) AS total_duration_seconds FROM timesheet AS t LEFT JOIN projects AS p ON t.RFC = p.RFC LEFT JOIN projectArchive AS pa ON t.RFC = pa.RFC WHERE t.date BETWEEN '{firstDayOfWeek}' AND '{lastDayOfWeek}' GROUP BY t.date, t.RFC ORDER BY t.RFC;";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectName = reader["projectName"].ToString();
                            string RFC = reader["RFC"].ToString();
                            string RFCID = reader["RFCID"].ToString();
                            string dateStr = reader["date"].ToString();
                            DateTime date = DateTime.Parse(dateStr);

                            
                            string totalDurationSecondsString = reader["total_duration_seconds"].ToString();
                            int totalDurationSeconds = 0;
                            if(int.TryParse(totalDurationSecondsString, out int number))
                            {
                                totalDurationSeconds = number;
                            }

                            if (totalDurationSeconds > 16)
                            {
                                formattedTime = FormatTimeFromSeconds(totalDurationSeconds);
                            } else { formattedTime = ""; }
                             

                            // Bestimme den Wochentag des Datums
                            DayOfWeek dayOfWeek = date.DayOfWeek;

                            WeeklyProject weeklyEntry = NewProjectTime.FirstOrDefault(entry => entry.Name == projectName && entry.RFC == RFC && entry.RFCID == RFCID);
                            if (weeklyEntry == null)
                            {
                                weeklyEntry = new WeeklyProject
                                {
                                    Name = projectName,
                                    RFC = RFC,
                                    RFCID = RFCID
                                };
                                NewProjectTime.Add(weeklyEntry);
                            }

                            // Je nach Wochentag, aktualisiere die entsprechende Eigenschaft des WeeklyProject-Objekts
                            switch (dayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    weeklyEntry.Monday = formattedTime;
                                    if (!IsTimeFormat(formattedTime))
                                    {
                                        if (!string.IsNullOrEmpty(formattedTime))
                                        {
                                            moEntry += double.Parse(formattedTime);
                                        }
                                    }
                                    else
                                    {
                                        moEntryS = AddTimes(moEntryS, formattedTime);
                                    }
                                    break;
                                case DayOfWeek.Tuesday:
                                    weeklyEntry.Tuesday = formattedTime;
                                    if (!IsTimeFormat(formattedTime))
                                    {
                                        if (!string.IsNullOrEmpty(formattedTime))
                                        {
                                            diEntry += double.Parse(formattedTime);
                                        }
                                    }
                                    else
                                    {
                                        diEntryS = AddTimes(diEntryS, formattedTime);
                                    }
                                    break;
                                case DayOfWeek.Wednesday:
                                    weeklyEntry.Wednesday = formattedTime;
                                    if (!IsTimeFormat(formattedTime))
                                    {
                                        if (!string.IsNullOrEmpty(formattedTime))
                                        {
                                            miEntry += double.Parse(formattedTime);
                                        }
                                    }
                                    else
                                    {
                                        miEntryS = AddTimes(miEntryS, formattedTime);
                                    }
                                    break;
                                case DayOfWeek.Thursday:
                                    weeklyEntry.Thursday = formattedTime;
                                    if (!IsTimeFormat(formattedTime))
                                    {
                                        if (!string.IsNullOrEmpty(formattedTime))
                                        {
                                            doEntry += double.Parse(formattedTime);
                                        }
                                    }
                                    else
                                    {
                                        doEntryS = AddTimes(doEntryS, formattedTime);
                                    }
                                    break;
                                case DayOfWeek.Friday:
                                    weeklyEntry.Friday = formattedTime;
                                    if (!IsTimeFormat(formattedTime))
                                    {
                                        if (!string.IsNullOrEmpty(formattedTime))
                                        {
                                            frEntry += double.Parse(formattedTime);
                                        }
                                    }
                                    else
                                    {
                                        frEntryS = AddTimes(frEntryS, formattedTime);
                                    }
                                    break;
                                case DayOfWeek.Saturday:
                                    weeklyEntry.Saturday = formattedTime;
                                    if (!IsTimeFormat(formattedTime))
                                    {
                                        if (!string.IsNullOrEmpty(formattedTime))
                                        {
                                            saEntry += double.Parse(formattedTime);
                                        }
                                    }
                                    else
                                    {
                                        saEntryS = AddTimes(saEntryS, formattedTime);
                                    }
                                    break;
                                case DayOfWeek.Sunday:
                                    weeklyEntry.Sunday = formattedTime;
                                    if (!IsTimeFormat(formattedTime))
                                    {
                                        if (!string.IsNullOrEmpty(formattedTime))
                                        {
                                            soEntry += double.Parse(formattedTime);
                                        }
                                    } else
                                    {
                                        soEntryS = AddTimes(soEntryS, formattedTime);
                                    }
                                    break;
                            }
                           
                        }

                        bool time = GetTimeFormat();

                        if (!time)
                        {
                            WeeklyTimes times = new WeeklyTimes
                            {
                                MoSum = moEntry == 0 ? "" : moEntry.ToString("N2"),
                                DiSum = diEntry == 0 ? "" : diEntry.ToString("N2"),
                                MiSum = miEntry == 0 ? "" : miEntry.ToString("N2"),
                                DoSum = doEntry == 0 ? "" : doEntry.ToString("N2"),
                                FrSum = frEntry == 0 ? "" : frEntry.ToString("N2"),
                                SaSum = saEntry == 0 ? "" : saEntry.ToString("N2"),
                                SoSum = soEntry == 0 ? "" : soEntry.ToString("N2")
                            };
                            weeklyTimes.Add(times);
                        } else
                        {
                            WeeklyTimes times = new WeeklyTimes
                            {
                                
                                MoSum = moEntryS,
                                DiSum = diEntryS,
                                MiSum = miEntryS,
                                DoSum = doEntryS,
                                FrSum = frEntryS,
                                SaSum = saEntryS,
                                SoSum = soEntryS
                            };
                            weeklyTimes.Add(times);
                        }

                        
                    }
                }
            }
            // Neu: Aktualisiere TimeEntries vor dem Hinzufügen von Daten
            foreach (var entry in NewProjectTime)
            {
                WeeklyProjectTimes.Add(entry);
            }

            
                     

            foreach(var entry in weeklyTimes)
            {
                if(entry != null)
                {
                    WeeklyTimesSum.Add(entry);
                }
                    
            }

        }

        static string AddTimes(string time1, string time2)
        {
            // Zeiten in Minuten umwandeln
            int totalMinutes = TimeToMinutes(time1) + TimeToMinutes(time2);

            // Stunden und Minuten aus den Gesamtminuten berechnen
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;

            // Ergebnis in das Format "HH:MM" zurückkonvertieren
            return $"{hours:D2}:{minutes:D2}";
        }

        static int TimeToMinutes(string time)
        {
            if(time != "")
            {
                // Die Zeit in Stunden und Minuten trennen
                string[] parts = time.Split(':');
                int hours = int.Parse(parts[0]);
                int minutes = int.Parse(parts[1]);

                // Die Zeit in Minuten umwandeln und zurückgeben
                return hours * 60 + minutes;
            } else { return 0; }

        }

        static bool IsTimeFormat(string input)
        {
            // Das Muster für "HH:MM"
            string pattern = @"^(?:[01]\d|2[0-3]):[0-5]\d$";

            // Überprüfen Sie, ob der Input dem Muster entspricht
            return Regex.IsMatch(input, pattern);
        }

        private static Tracker tracker = new Tracker();

        public static string FormatTimeFromSeconds(int totalSeconds)
        {


            if (GetTimeFormat())
            {
                int hours = totalSeconds / 3600;
                int remainingSeconds = totalSeconds % 3600;
                int minutes = remainingSeconds / 60;

                string formattedTime = $"{hours:D2}:{minutes:D2}";
                return formattedTime;
            } else
            {
                double totalMinutes = (double)totalSeconds / 60; // Die Gesamtanzahl der Minuten berechnen
                double decimalHours = totalMinutes / 60; // Die Gesamtanzahl der Stunden in Dezimal umrechnen
                return decimalHours.ToString("0.00", CultureInfo.GetCultureInfo("de-DE")); // Verwende das Format für Deutschland (Komma als Dezimaltrennzeichen)

            }



        }

        // Event-Handler für den Doppelklick auf ein Listenelement
        private void ListBoxItem_Copy(object sender, MouseButtonEventArgs e)
        {

            // Überprüfe, ob das auslösende Objekt ein Listenelement ist
            if (sender is Label label)
            {
                if (GetTimeFormat())
                {
                    string time = label.Content.ToString();
                    string[] parts = time.Split(':');
                    int hours = int.Parse(parts[0]);
                    int minutes = int.Parse(parts[1]);
                    double decimalHours = Math.Round(hours + (minutes / 60.0), 2);
                    Clipboard.SetText(decimalHours.ToString());

                } else
                {
                    Clipboard.SetText(label.Content.ToString());
                }               
            }
        }

        private void ListBoxItemText_Copy(object sender, MouseButtonEventArgs e)
        {

            // Überprüfe, ob das auslösende Objekt ein Listenelement ist
            if (sender is Label label)
            {
                    Clipboard.SetText(label.Content.ToString());

            }
        }

        private void CustomCalendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedDate = CustomCalendar.SelectedDate;
            Timeentries.LoadTimeEntries();

            SetCalendarWeek(CustomCalendar.SelectedDate);
            LoadWeeklyProjectsEntries(GetCalendarDate());
            Timeentries timeentries = new Timeentries();
            timeentries.SetDateNewEntry();
        }

        public void SetCalendarWeek(DateTime? selectedDate)
        {
            DateTime dateCW = selectedDate.Value;

            CultureInfo culture = CultureInfo.CurrentCulture;
            int calendarWeek = culture.Calendar.GetWeekOfYear(dateCW, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
            CalendarWeek.Content = "Kalenderwoche: "+ calendarWeek;

            DateTime firstDayOfWeek = dateCW.GetFirstDayOfWeek();
            DateTime lastDayOfWeek = dateCW.GetLastDayOfWeek();

           

        }

        private void MainWindowFrame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ///MainWindowFrame.Focus();
        }

        private void CustomCalendar_MouseLeave(object sender, MouseEventArgs e)
        {
            ///ListBoxTimeEntry.Focus();
        }

        private void timeFormat_Checked(object sender, RoutedEventArgs e)
        {
            LoadWeeklyProjectsEntries(GetCalendarDate());
        }

        private void timeFormat_Unchecked(object sender, RoutedEventArgs e)
        {
            LoadWeeklyProjectsEntries(GetCalendarDate());
        }

        private void ToolTip_Opened(object sender, RoutedEventArgs e)
        {
            if (tooltipIsActive)
            {
                if (sender is ToolTip toolTip)
                {
                    if (toolTip.PlacementTarget is Label label)
                    {
                        if (label.Content != null)
                        {
                            if (GetTimeFormat())
                            {
                                string time = label.Content.ToString();
                                string[] parts = time.Split(':');
                                int hours = int.Parse(parts[0]);
                                int minutes = int.Parse(parts[1]);
                                double decimalHours = Math.Round(hours + (minutes / 60.0), 2);

                                toolTip.Content = "Dezimal: " + decimalHours.ToString();
                            }
                            else
                            {
                                double decimalTime = double.Parse(label.Content.ToString());
                                int hours = (int)decimalTime;
                                int minutes = (int)((decimalTime - hours) * 60);
                                string formattedTime = string.Format("{0:D2}:{1:D2}", hours, minutes);

                                toolTip.Content = "24-Stunden: " + formattedTime.ToString();
                            }


                        }

                    }

                }
            } else
            {
                if (sender is ToolTip toolTip)
                {
                    if (toolTip.PlacementTarget is Label label)
                    {
                        label.ToolTip = null;
                    }
                }
            }

            
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tooltipIsActive)
            {
                if (sender is Label label)
                {
                    ToolTip toolTip = new ToolTip();
                    toolTip.Background = System.Windows.Media.Brushes.LightGray;
                    toolTip.Foreground = System.Windows.Media.Brushes.Black;
                    toolTip.FontFamily = new System.Windows.Media.FontFamily("Arial");
                    toolTip.FontSize = 12;

                    label.ToolTip = toolTip;

                    if (GetTimeFormat())
                    {
                        if(label.Content != null && label.Content.ToString() != "")
                        {
                            string time = label.Content.ToString();
                            string[] parts = time.Split(':');
                            int hours = int.Parse(parts[0]);
                            int minutes = int.Parse(parts[1]);
                            double decimalHours = Math.Round(hours + (minutes / 60.0), 2);

                            toolTip.Content = "Dezimal: " + decimalHours.ToString("N2");
                        }
                        
                    }
                    else
                    {
                        if(label.Content != null)
                        {
                            double decimalTime = double.Parse(label.Content.ToString());
                            int hours = (int)decimalTime;
                            int minutes = (int)((decimalTime - hours) * 60);
                            string formattedTime = string.Format("{0:D2}:{1:D2}", hours, minutes);

                            toolTip.Content = "24-Stunden: " + formattedTime.ToString();
                        }
          
                    }

                    label.ToolTip = toolTip;

                }
            }

            
        }
    }

    public static class DateTimeExtensions
    {
        // Methode zur Ermittlung des ersten Tages einer Kalenderwoche
        public static DateTime GetFirstDayOfWeek(this DateTime date, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            int diff = (7 + (date.DayOfWeek - firstDayOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        // Methode zur Ermittlung des letzten Tages einer Kalenderwoche
        public static DateTime GetLastDayOfWeek(this DateTime date, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            return date.GetFirstDayOfWeek(firstDayOfWeek).AddDays(6);
        }
    }
}

