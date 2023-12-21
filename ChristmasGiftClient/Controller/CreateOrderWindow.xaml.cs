using ChristmasGiftClient.Model;
using GiftLib;
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
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
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
            List<int> selectedIndexes = new List<int>();

            foreach(var listBox in new List<ListBox>{ lbxCandles, lbxClothes, lbxCookies, lbxOrnaments})
            {
                foreach(var item in listBox.SelectedItems)
                {
                    Gift temp = (Gift)item;
                    selectedIndexes.Add(temp.Id);
                }
            }



            // Call the client to send the selected indexes
            await Client.RunClientAsync(selectedIndexes);


            ServerRespondWindow serverRespondWindow = new ServerRespondWindow();
            serverRespondWindow.Show();

            this.Close();
        }
    }
}
