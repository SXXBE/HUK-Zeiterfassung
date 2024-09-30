using System;
using System.Data.SQLite;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HUK_Zeiterfassung
{
    public partial class ClockWindow : Window
    {
        DoubleAnimation da = new DoubleAnimation();
        public bool Animation { get; set; }

        public ClockWindow()
        {
            InitializeComponent();
            SetClockStyle();
            UpdateClock();
            Topmost = true;
            this.ShowInTaskbar = false;
            SetReminderConfig();


        }

        private void IFSAnimation(bool value)
        {
            if (value)
            {
                da.From = 15;
                da.To = 20;
                da.AutoReverse = true;
                da.RepeatBehavior = new RepeatBehavior(2);
                da.Duration = new Duration(TimeSpan.FromSeconds(0.5));

                ReminderTextBlock.BeginAnimation(TextBlock.FontSizeProperty, da);

            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        public void UpdateClock()
        {
            if (MainWindow.CurrentTime != null)
            {
                ClockTextBlock.Text = MainWindow.CurrentTime.ToString();
            }
            

            ClockProject.Text = MainWindow.ActiveProject;
            IFSAnimation(Animation);

        }


        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void SetClockStyle()
        {
            string dbBorderColor = "";
            string dbBackgroundColor = "";
            string dbProjectColor = "";
            string dbTimeColor = "";
            string dbReminderColor = "";
            string dbShowIFSReminder = "";
            string dbShowIFSAnmiation = "";
            int dbReminderInterval = -1;

            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = "SELECT timerPopUpBorderColor, timerPopUpBackgroundColor, timerPopUpProjectColor, timerPopUpTimeColor, timerPopUpReminderColor, showIFSReminder, showIFSReminderAnimation, reminderInterval FROM userSettings;";
                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dbBorderColor = reader.GetString(0);
                            dbBackgroundColor = reader.GetString(1);
                            dbProjectColor = reader.GetString(2);
                            dbTimeColor = reader.GetString(3);
                            dbReminderColor = reader.GetString(4);
                            dbShowIFSReminder = reader.GetString(5);
                            dbShowIFSAnmiation = reader.GetString(6);
                            dbReminderInterval = reader.GetInt32(7);

                        }
                    }
                }

            }

            ///SetIFSReminder(bool.Parse(dbShowIFSReminder));
            Animation = bool.Parse(dbShowIFSAnmiation);



            Color standard = Color.FromArgb(0xFF, 0x1B, 0x1B, 0x1B);

            SolidColorBrush Grau = new SolidColorBrush(standard);
            SolidColorBrush Transparent = new SolidColorBrush(Colors.Transparent);
            SolidColorBrush Schwarz = new SolidColorBrush(Colors.Black);
            SolidColorBrush Weiß = new SolidColorBrush(Colors.White);
            SolidColorBrush Blau = new SolidColorBrush(Colors.Blue);
            SolidColorBrush Grün = new SolidColorBrush(Colors.Green);
            SolidColorBrush Rot = new SolidColorBrush(Colors.Red);
            SolidColorBrush Gelb = new SolidColorBrush(Colors.Yellow);

            switch (dbBorderColor)
            {
                case "Grau":
                    clockBorder.BorderBrush = Grau;
                    break;
                case "Transparent":
                    clockBorder.BorderBrush = Transparent;
                    break;
                case "Schwarz":
                    clockBorder.BorderBrush = Schwarz;
                    break;
                case "Weiß":
                    clockBorder.BorderBrush = Weiß;
                    break;
                case "Blau":
                    clockBorder.BorderBrush = Blau;
                    break;
                case "Grün":
                    clockBorder.BorderBrush = Grün;
                    break;
                case "Rot":
                    clockBorder.BorderBrush = Rot;
                    break;
                case "Gelb":
                    clockBorder.BorderBrush = Gelb;
                    break;
            }

            switch (dbBackgroundColor)
            {
                case "Grau":
                    clockBorder.Background = Grau;
                    break;
                case "Transparent":
                    clockBorder.Background = Transparent;
                    break;
                case "Schwarz":
                    clockBorder.Background = Schwarz;
                    break;
                case "Weiß":
                    clockBorder.Background = Weiß;
                    break;
                case "Blau":
                    clockBorder.Background = Blau;
                    break;
                case "Grün":
                    clockBorder.Background = Grün;
                    break;
                case "Rot":
                    clockBorder.Background = Rot;
                    break;
                case "Gelb":
                    clockBorder.Background = Gelb;
                    break;
            }

            switch (dbProjectColor)
            {
                case "Grau":
                    ClockProject.Foreground = Grau;
                    break;
                case "Transparent":
                    ClockProject.Foreground = Transparent;
                    break;
                case "Schwarz":
                    ClockProject.Foreground = Schwarz;
                    break;
                case "Weiß":
                    ClockProject.Foreground = Weiß;
                    break;
                case "Blau":
                    ClockProject.Foreground = Blau;
                    break;
                case "Grün":
                    ClockProject.Foreground = Grün;
                    break;
                case "Rot":
                    ClockProject.Foreground = Rot;
                    break;
                case "Gelb":
                    ClockProject.Foreground = Gelb;
                    break;
            }

            switch (dbTimeColor)
            {
                case "Grau":
                    ClockTextBlock.Foreground = Grau;
                    break;
                case "Transparent":
                    ClockTextBlock.Foreground = Transparent;
                    break;
                case "Schwarz":
                    ClockTextBlock.Foreground = Schwarz;
                    break;
                case "Weiß":
                    ClockTextBlock.Foreground = Weiß;
                    break;
                case "Blau":
                    ClockTextBlock.Foreground = Blau;
                    break;
                case "Grün":
                    ClockTextBlock.Foreground = Grün;
                    break;
                case "Rot":
                    ClockTextBlock.Foreground = Rot;
                    break;
                case "Gelb":
                    ClockTextBlock.Foreground = Gelb;
                    break;
            }

            switch (dbReminderColor)
            {
                case "Grau":
                    ReminderTextBlock.Foreground = Grau;
                    break;
                case "Transparent":
                    ReminderTextBlock.Foreground = Transparent;
                    break;
                case "Schwarz":
                    ReminderTextBlock.Foreground = Schwarz;
                    break;
                case "Weiß":
                    ReminderTextBlock.Foreground = Weiß;
                    break;
                case "Blau":
                    ReminderTextBlock.Foreground = Blau;
                    break;
                case "Grün":
                    ReminderTextBlock.Foreground = Grün;
                    break;
                case "Rot":
                    ReminderTextBlock.Foreground = Rot;
                    break;
                case "Gelb":
                    ReminderTextBlock.Foreground = Gelb;
                    break;
            }
        }

        private void SetIFSReminder(bool value)
        {
            if(value)
            {
                Dispatcher.Invoke(() =>
                {
                    ReminderTextBlock.Visibility = Visibility.Visible;
                    gridrow.Height = new GridLength(21);
                });

            } else
            {
                ReminderTextBlock.Visibility = Visibility.Collapsed;
                gridrow.Height = new GridLength(0);
            }
        }

        public void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = $"UPDATE userSettings SET reminderTimeStamp = '{DateTime.Now}';";
                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.ExecuteReader();
                }
            }

            ReminderTextBlock.Visibility = Visibility.Collapsed;
            gridrow.Height = new GridLength(0);
        }

        private void SetReminderConfig()
        {
            bool ifsReminderIsActive = false;
            int reminderInterval = -1;
            int reminderHour = -1;
            int reminderMinute = -1;
            string reminderTimeStamp = "";

            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = "SELECT showIFSReminder, reminderInterval, reminderHour, reminderMinute, reminderTimeStamp FROM userSettings;";
                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ifsReminderIsActive = bool.Parse(reader.GetString(0));
                            reminderInterval = reader.GetInt32(1);
                            reminderHour = int.Parse(reader.GetString(2));
                            reminderMinute = int.Parse(reader.GetString(3));
                            reminderTimeStamp = reader.GetString(4);

                        }
                    }
                }
            }

            if (ifsReminderIsActive)
            {

                if (reminderInterval == 0)
                {
                    DateTime currentDate = DateTime.Now.Date;

                    var cultureInfo = new CultureInfo("de-DE");
                    DateTime dbTimeStamp = DateTime.Parse(reminderTimeStamp, cultureInfo);
                    DateTime dbDate = dbTimeStamp.Date;
                    if (currentDate > dbDate)
                    {
                        SetIFSReminder(true);
                    } else
                    {
                        SetIFSReminder(false);
                    }


                }
                else if (reminderInterval == 1)
                {
                    SetIFSReminder(false);

                    DateTime currentDate = DateTime.Now.Date;
                    var cultureInfo = new CultureInfo("de-DE");
                    DateTime dbTimeStamp = DateTime.Parse(reminderTimeStamp, cultureInfo);
                    DateTime dbDate = dbTimeStamp.Date;

                    if(currentDate > dbDate)
                    {
                        if(dbDate.Hour <= reminderHour && dbDate.Minute <= reminderMinute) 
                        {
                            Timer ifsTimer = new Timer();
                            ifsTimer.Interval = 1000;
                            ifsTimer.Elapsed += (sender, e) => CheckTime(reminderHour, reminderMinute, ifsTimer, currentDate, dbDate);
                            ifsTimer.Start();

                        }
                    }

                }
            }
        }

        private void CheckTime(int hour, int minute, Timer timer, DateTime currentDate, DateTime dbDate)
        {
            DateTime currentTime = DateTime.Now;
            if (currentTime.Hour == hour && currentTime.Minute == minute && currentDate > dbDate)
            {
                timer.Stop(); // Timer stoppen, um mehrmalige Ausführung zu verhindern
                SetIFSReminder(true);

            }
        }
    }
}
