using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using IWshRuntimeLibrary;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;
using MessageBox = System.Windows.MessageBox;
using System.Globalization;

namespace HUK_Zeiterfassung
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const int HWND_TOPMOST = -1;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_FRAMECHANGED = 0x0020;

        private string reminderTimeStamp = "";
        private string doneIFS = "";

        public Settings()
        {
            InitializeComponent();
            CreateComboBoxValues();
            LoadActiveProjects();
            LoadSettingsFromDB();
            GetMonitors();

        }
        
        private void GetMonitors()
        {
            MonitorLR.Items.Add("Rechts");
            MonitorLR.Items.Add("Links");

            foreach (var screen in Screen.AllScreens)
            {
                string monitorName = screen.DeviceName.Replace("\\\\.\\DISPLAY", "Monitor ");
                Monitor.Items.Add(monitorName);
            }

            
        }

        private void LoadSettingsFromDB()
        {
            bool showProjectBox = false;

            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM userSettings;";
                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            showProjectBox = bool.Parse(reader["startTrackingAutostart"].ToString());

                            SetTimePopUpAutostart.IsChecked = bool.Parse(reader["timerPopUp"].ToString());
                            TimerPopUpBorderColor.SelectedValue = reader.GetString(1);
                            TimerPopUpBackgroundColor.SelectedValue = reader.GetString(2);
                            TimerPopUpProjectColor.SelectedValue = reader.GetString(3);
                            TimerPopUpTimeColor.SelectedValue = reader.GetString(4);
                            AppAutostartCB.IsChecked = bool.Parse(reader["autostart"].ToString());
                            AutostartProjectCB.IsChecked = bool.Parse(reader["startTrackingAutostart"].ToString());
                            AutostartStartProject.SelectedValue = reader.GetString(7);
                            Monitor.SelectedIndex = reader.GetInt32(8);
                            MonitorLR.SelectedValue = reader.GetString(9);
                            sliderTimeout.SelectedValue = reader.GetString(10);
                            SysTrayIcon.IsChecked = bool.Parse(reader.GetString(11));
                            TaskbarIcon.IsChecked = bool.Parse(reader.GetString(12));
                            TimerPopUpReminderColor.SelectedValue = reader.GetString(13);
                            IFSReminder.IsChecked = bool.Parse(reader.GetString(14));
                            IFSReminderAnimation.IsChecked = bool.Parse(reader.GetString(15));
                            ReminderInterval.SelectedIndex = reader.GetInt32(16);
                            NormalTimeformat.IsChecked = bool.Parse(reader.GetString(17));
                            ReminderHour.SelectedValue = reader.GetString(18);
                            ReminderMinute.SelectedValue = reader.GetString(19);
                            reminderTimeStamp = reader.GetString(20);
                            doneIFS = reader.GetString(21);
                            Tooltip.IsChecked = bool.Parse(reader.GetString(22));

                        }
                    }
                }

            }

            if (IFSReminder.IsChecked.ToString() == "False") 
            { 
                IFSReminder_Unchecked(null,null);
                
            } else
            {
                IFSReminderConfig();
            }


            for (int i = 1; i < 11; i++)
            {
                sliderTimeout.Items.Add(i.ToString());
            }


            if (showProjectBox)
            {
                StartProjectLabel.Visibility = Visibility.Visible;
                AutostartStartProject.Visibility = Visibility.Visible;
            } else
            {
                StartProjectLabel.Visibility = Visibility.Hidden;
                AutostartStartProject.Visibility = Visibility.Hidden;
            }
        }

        public void IFSReminderConfig()
        {
            

        }


        private void CreateComboBoxValues()
        {
            List<string> ifsReminder = new List<string>();
            
            ifsReminder.Add("Bei ersten Start am Tag");
            ifsReminder.Add("Bestimmte Uhrzeit");

            ReminderInterval.ItemsSource = ifsReminder;


            List<string> colorsList = new List<string>();
            colorsList.Add("Grau");
            colorsList.Add("Transparent");
            colorsList.Add("Schwarz");
            colorsList.Add("Weiß");
            colorsList.Add("Blau");
            colorsList.Add("Grün");
            colorsList.Add("Rot");
            colorsList.Add("Gelb");
                        
            TimerPopUpBorderColor.ItemsSource = colorsList;
            TimerPopUpBackgroundColor.ItemsSource = colorsList;
            TimerPopUpProjectColor.ItemsSource = colorsList;
            TimerPopUpTimeColor.ItemsSource = colorsList;
            TimerPopUpReminderColor.ItemsSource = colorsList;

            List<string> reminderHours = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                string value = i.ToString();
                if(value.Length == 1)
                {
                    value = "0" + i.ToString();
                }

                reminderHours.Add(value);
            }

            List<string> reminderMinutes = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                string value = i.ToString();
                if (value.Length == 1)
                {
                    value = "0" + i.ToString();
                }
                reminderMinutes.Add(value);
            }

            ReminderMinute.ItemsSource = reminderMinutes;

            ReminderHour.ItemsSource = reminderHours;


        }

        private void TimerPopUpBorderColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameCombobox = (((System.Windows.Controls.ComboBox)sender).Name);
            SetColorTimerPopUp(nameCombobox, TimerPopUpBorderColor.SelectedValue.ToString());
        }

        private void TimerPopUpBackgroundColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameCombobox = (((System.Windows.Controls.ComboBox)sender).Name);
            SetColorTimerPopUp(nameCombobox, TimerPopUpBackgroundColor.SelectedValue.ToString());
        }

        private void TimerPopUpProjectColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameCombobox = (((System.Windows.Controls.ComboBox)sender).Name);
            SetColorTimerPopUp(nameCombobox, TimerPopUpProjectColor.SelectedValue.ToString());
        }

        private void TimerPopUpTimeColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameCombobox = (((System.Windows.Controls.ComboBox)sender).Name);
            SetColorTimerPopUp(nameCombobox, TimerPopUpTimeColor.SelectedValue.ToString());
        }

        private void TimerPopUpReminderColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameCombobox = (((System.Windows.Controls.ComboBox)sender).Name);
            SetColorTimerPopUp(nameCombobox, TimerPopUpReminderColor.SelectedValue.ToString());
        }

        private void SetColorTimerPopUp(string sender, string selectedColor)
        {
            Color grey = Color.FromArgb(0xFF, 0x1B, 0x1B, 0x1B);

            SolidColorBrush Grau = new SolidColorBrush(grey);
            SolidColorBrush Transparent = new SolidColorBrush(Colors.Transparent);
            SolidColorBrush Schwarz = new SolidColorBrush(Colors.Black);
            SolidColorBrush Weiß = new SolidColorBrush(Colors.White);
            SolidColorBrush Blau = new SolidColorBrush(Colors.Blue);
            SolidColorBrush Grün = new SolidColorBrush(Colors.Green);
            SolidColorBrush Rot = new SolidColorBrush(Colors.Red);
            SolidColorBrush Gelb = new SolidColorBrush(Colors.Yellow);

            if(sender == "TimerPopUpProjectColor")
            {
                switch (selectedColor)
                {
                    case "Grau":
                        TimerPopUpProjectColorStyle.Foreground = Grau;
                        break;
                    case "Transparent":
                        TimerPopUpProjectColorStyle.Foreground = Transparent;
                        break;
                    case "Schwarz":
                        TimerPopUpProjectColorStyle.Foreground = Schwarz;
                        break;
                    case "Weiß":
                        TimerPopUpProjectColorStyle.Foreground = Weiß;
                        break;
                    case "Blau":
                        TimerPopUpProjectColorStyle.Foreground = Blau;
                        break;
                    case "Grün":
                        TimerPopUpProjectColorStyle.Foreground = Grün;
                        break;
                    case "Rot":
                        TimerPopUpProjectColorStyle.Foreground = Rot;
                        break;
                    case "Gelb":
                        TimerPopUpProjectColorStyle.Foreground = Gelb;
                        break;
                }
            }

            if (sender == "TimerPopUpTimeColor")
            {
                switch (selectedColor)
                {
                    case "Grau":
                        TimerPopUpTimeColorStyle.Foreground = Grau;
                        break;
                    case "Transparent":
                        TimerPopUpTimeColorStyle.Foreground = Transparent;
                        break;
                    case "Schwarz":
                        TimerPopUpTimeColorStyle.Foreground = Schwarz;
                        break;
                    case "Weiß":
                        TimerPopUpTimeColorStyle.Foreground = Weiß;
                        break;
                    case "Blau":
                        TimerPopUpTimeColorStyle.Foreground = Blau;
                        break;
                    case "Grün":
                        TimerPopUpTimeColorStyle.Foreground = Grün;
                        break;
                    case "Rot":
                        TimerPopUpTimeColorStyle.Foreground = Rot;
                        break;
                    case "Gelb":
                        TimerPopUpTimeColorStyle.Foreground = Gelb;
                        break;
                }
            }

            if (sender == "TimerPopUpBackgroundColor")
            {
                switch (selectedColor)
                {
                    case "Grau":
                        TimePopUpBorder.Background = Grau;
                        break;
                    case "Transparent":
                        TimePopUpBorder.Background = Transparent;
                        break;
                    case "Schwarz":
                        TimePopUpBorder.Background = Schwarz;
                        break;
                    case "Weiß":
                        TimePopUpBorder.Background = Weiß;
                        break;
                    case "Blau":
                        TimePopUpBorder.Background = Blau;
                        break;
                    case "Grün":
                        TimePopUpBorder.Background = Grün;
                        break;
                    case "Rot":
                        TimePopUpBorder.Background = Rot;
                        break;
                    case "Gelb":
                        TimePopUpBorder.Background = Gelb;
                        break;
                }
            }

            if (sender == "TimerPopUpBorderColor")
            {
                switch (selectedColor)
                {
                    case "Grau":
                        TimePopUpBorder.BorderBrush = Grau;
                        break;
                    case "Transparent":
                        TimePopUpBorder.BorderBrush = Transparent;
                        break;
                    case "Schwarz":
                        TimePopUpBorder.BorderBrush = Schwarz;
                        break;
                    case "Weiß":
                        TimePopUpBorder.BorderBrush = Weiß;
                        break;
                    case "Blau":
                        TimePopUpBorder.BorderBrush = Blau;
                        break;
                    case "Grün":
                        TimePopUpBorder.BorderBrush = Grün;
                        break;
                    case "Rot":
                        TimePopUpBorder.BorderBrush = Rot;
                        break;
                    case "Gelb":
                        TimePopUpBorder.BorderBrush = Gelb;
                        break;
                }
            }

            if (sender == "TimerPopUpReminderColor")
            {
                switch (selectedColor)
                {
                    case "Grau":
                        TimerPopUpReminderColorStyle.Foreground = Grau;
                        break;
                    case "Transparent":
                        TimerPopUpReminderColorStyle.Foreground = Transparent;
                        break;
                    case "Schwarz":
                        TimerPopUpReminderColorStyle.Foreground = Schwarz;
                        break;
                    case "Weiß":
                        TimerPopUpReminderColorStyle.Foreground = Weiß;
                        break;
                    case "Blau":
                        TimerPopUpReminderColorStyle.Foreground = Blau;
                        break;
                    case "Grün":
                        TimerPopUpReminderColorStyle.Foreground = Grün;
                        break;
                    case "Rot":
                        TimerPopUpReminderColorStyle.Foreground = Rot;
                        break;
                    case "Gelb":
                        TimerPopUpReminderColorStyle.Foreground = Gelb;
                        break;
                }
            }
        }

        private void LoadActiveProjects()
        {
            List<string> autostartProjects = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = "SELECT name FROM projects WHERE isActive = 'True';";
                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            autostartProjects.Add(reader.GetString(0));
                        }
                    }
                }

            }

            AutostartStartProject.ItemsSource = autostartProjects;


        }

        private  void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string appFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            string shortcutFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutName = "HUK-Zeiterfassung";

            if (AppAutostartCB.IsChecked.GetValueOrDefault())
            {
                if(!System.IO.File.Exists(System.IO.Path.Combine(shortcutFolder, shortcutName + ".lnk")))
                {
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(System.IO.Path.Combine(shortcutFolder, shortcutName + ".lnk"));

                    shortcut.TargetPath = Assembly.GetExecutingAssembly().Location;
                    shortcut.Save();
                }

            }
            else
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(shortcutFolder, shortcutName + ".lnk")))
                {
                    System.IO.File.Delete(System.IO.Path.Combine(shortcutFolder, shortcutName + ".lnk"));
                }
            }

            string autostartProject = "";
            if(AutostartStartProject.SelectedValue != null)
            {
                autostartProject = AutostartStartProject.SelectedValue.ToString();
            }

            using (SQLiteConnection connection = new SQLiteConnection(DBHandler.CONNECTION_STRING))
            {
                connection.Open();
                string sqlQuery = $"UPDATE userSettings SET timerPopUp = '{SetTimePopUpAutostart.IsChecked.ToString()}', " +
                                    $"timerPopUpBorderColor = '{TimerPopUpBorderColor.SelectedValue.ToString()}', " +
                                    $"timerPopUpBackgroundColor = '{TimerPopUpBackgroundColor.SelectedValue.ToString()}', " +
                                    $"timerPopUpProjectColor = '{TimerPopUpProjectColor.SelectedValue.ToString()}', " +
                                    $"timerPopUpTimeColor = '{TimerPopUpTimeColor.SelectedValue.ToString()}', " +
                                    $"timerPopUpReminderColor = '{TimerPopUpReminderColor.SelectedValue.ToString()}', " +
                                    $"autostart = '{AppAutostartCB.IsChecked.ToString()}', " +
                                    $"startTrackingAutostart = '{AutostartProjectCB.IsChecked.ToString()}', " +
                                    $"startProject = '{autostartProject}', " +
                                    $"monitor = {Monitor.SelectedIndex}, " +
                                    $"direction = '{MonitorLR.SelectedValue.ToString()}', " +
                                    $"sliderTimeout = '{sliderTimeout.SelectedValue.ToString()}', " +
                                    $"showInTaskbar = '{TaskbarIcon.IsChecked.ToString()}', " +
                                    $"showInSystemtray = '{SysTrayIcon.IsChecked.ToString()}', " +
                                    $"showIFSReminder = '{IFSReminder.IsChecked.ToString()}', " +
                                    $"showIFSReminderAnimation = '{IFSReminderAnimation.IsChecked.ToString()}', " +
                                    $"ReminderInterval = '{ReminderInterval.SelectedIndex}', " +
                                    $"isTimeFormat = '{NormalTimeformat.IsChecked}', " +
                                    $"tooltip = '{Tooltip.IsChecked}', " +
                                    $"reminderHour = '{ReminderHour.SelectedValue}', " +
                                    $"reminderMinute = '{ReminderMinute.SelectedValue}';";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }


            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AutostartProjectCB_Checked(object sender, RoutedEventArgs e)
        {
            StartProjectLabel.Visibility = Visibility.Visible;
            AutostartStartProject.Visibility = Visibility.Visible;
        }

        private void AutostartProjectCB_Unchecked(object sender, RoutedEventArgs e)
        {
            StartProjectLabel.Visibility = Visibility.Hidden;
            AutostartStartProject.Visibility = Visibility.Hidden;
        }

        private void IdentDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            string textToShow = "Hello World!";

            foreach (var screen in Screen.AllScreens)
            {
                DisplayIdent displayIdent = new DisplayIdent();
                displayIdent.Left = screen.WorkingArea.Left;
                displayIdent.Top = screen.WorkingArea.Top;
                displayIdent.Show();
                displayIdent.IdentDisplay(textToShow);
            }



        }

        private void WhiteModeButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(WhitemodeButton.Margin.Left == 330)
            {
                WhitemodeButton.Margin = new Thickness(50, WhitemodeButton.Margin.Top, WhitemodeButton.Margin.Right, WhitemodeButton.Margin.Bottom);

            } else
            {
                WhitemodeButton.Margin = new Thickness(330, WhitemodeButton.Margin.Top, WhitemodeButton.Margin.Right, WhitemodeButton.Margin.Bottom);
            }
        }

        private void IFSReminder_Checked(object sender, RoutedEventArgs e)
        {
            IFSReminderAnimation.Visibility = Visibility.Visible;
            ReminderInterval.Visibility = Visibility.Visible;
            ReminderCBLabel.Visibility = Visibility.Visible;

            if(ReminderInterval.SelectedIndex == 1)
            {
                ReminderTimeGrid.Visibility = Visibility.Visible;
            } else
            {
                ReminderTimeGrid.Visibility = Visibility.Collapsed;
            }
            
        }

        private void IFSReminder_Unchecked(object sender, RoutedEventArgs e)
        {
            IFSReminderAnimation.Visibility = Visibility.Collapsed;
            ReminderInterval.Visibility = Visibility.Collapsed;
            ReminderCBLabel.Visibility = Visibility.Collapsed;
            ReminderTimeGrid.Visibility= Visibility.Collapsed;
        }

        private void ReminderInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReminderInterval.SelectedIndex == 1)
            {
                ReminderTimeGrid.Visibility = Visibility.Visible;
            }
            else
            {
                ReminderTimeGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
