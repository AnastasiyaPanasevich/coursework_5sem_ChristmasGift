using ChristmasGiftClient.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ChristmasGiftClient.Controller
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

        }

        //private void btnRUS_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btnENG_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            string userEmail = EmailTextBox.Text;

            if (EmailValidator.IsValidEmail(userEmail))
            {
                SelectWindow selectWindow = new SelectWindow();
                selectWindow.Show();

                this.Close();

            }
        }
    }

}
