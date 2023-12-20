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

namespace ChristmasGiftClient.Controller
{
    /// <summary>
    /// Interaction logic for ServerRespondWindow.xaml
    /// </summary>
    public partial class ServerRespondWindow : Window
    {
        public ServerRespondWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnRetry_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
