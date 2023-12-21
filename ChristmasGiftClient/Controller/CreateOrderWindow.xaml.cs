using ChristmasGiftClient.Model;
using GiftLib;
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
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        List<int> selectedIndexes = new List<int>();

        public CreateOrderWindow()
        {
            InitializeComponent();
            lbxCandles.ItemsSource = States.Candles;
            lbxClothes.ItemsSource = States.Clothes;
            lbxCookies.ItemsSource = States.Cookies;
            lbxOrnaments.ItemsSource = States.Ornaments;
        }

        private async void btnSubmitCustomOrder_Click(object sender, RoutedEventArgs e)
        {
            // Call the client to send the selected indexes
            await Client.RunClientAsync(selectedIndexes);


            //ServerRespondWindow serverRespondWindow = new ServerRespondWindow();
            //serverRespondWindow.Show();

            this.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                var a = e.AddedItems[0] as Gift;
                selectedIndexes.Add(a.Id);
                txtBudget.Text = (Double.Parse(txtBudget.Text) + a.Price).ToString();
            }
            else
            {
                var a = e.RemovedItems[0] as Gift;
                selectedIndexes.Remove(a.Id);
                txtBudget.Text = (Double.Parse(txtBudget.Text) - a.Price).ToString();
            }
        }
    }
}
