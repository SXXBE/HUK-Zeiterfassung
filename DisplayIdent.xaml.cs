using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HUK_Zeiterfassung
{
    /// <summary>
    /// Interaktionslogik für DisplayIdent.xaml
    /// </summary>
    public partial class DisplayIdent : Window
    {
        public DisplayIdent()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            
        }

        public void IdentDisplay(string display)
        {
            DisplayNumber.Content = display;
        }
    }
}
