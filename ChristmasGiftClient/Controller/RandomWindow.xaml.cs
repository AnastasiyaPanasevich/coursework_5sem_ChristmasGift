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
    /// Interaction logic for RandomWindow.xaml
    /// </summary>
    public partial class RandomWindow : Window
    {
        public RandomWindow()
        {
            InitializeComponent();
        }

        private List<Gift> GetRandomGifts(double budget)
        {
            List<Gift> allItems = new List<Gift>(States.Candles);
            allItems.AddRange(States.Clothes);
            allItems.AddRange(States.Cookies);
            allItems.AddRange(States.Ornaments);

            double averagePrice = allItems.Sum(x => x.Price) / allItems.Count;
            var temp = budget / averagePrice;
            int n = (int)temp * 2;
            if(n > allItems.Count)
            {
                n = allItems.Count;
            }
            
            Random rnd = new Random();
            return allItems.OrderBy(x => rnd.Next()).Take(n).ToList();
        }

        private List<int> CreateRandomOrder()
        {
            List<Gift> randomItems = GetRandomGifts(Convert.ToDouble(BudgetTextBox.Text));
            double[] weights = new double[randomItems.Count];
            int[] prices = new int[randomItems.Count];
            for(int i=0;i<randomItems.Count;i++)
            {
                weights[i] = randomItems[i].Price;
                prices[i] = 1;
            }

            double maxWeight = Convert.ToInt32(BudgetTextBox.Text);

            List<int> indexes = new List<int>();
            List<int> result = new List<int>();
            double resultWeight = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                indexes.Add(i);
            }

            while (indexes.Count > 0)
            {
                int maxValue = prices[indexes[0]];
                int maxIndex = indexes[0];

                for (int i = 1; i < indexes.Count; i++)
                {
                    if (maxValue < prices[indexes[i]])
                    {
                        maxValue = prices[indexes[i]];
                        maxIndex = indexes[i];
                    }
                }

                resultWeight += weights[maxIndex];
                if (resultWeight > maxWeight)
                {
                    break;
                }

                result.Add(maxIndex);
                indexes.Remove(maxIndex);
            }

            List<int> giftID = new List<int>();
            foreach (int index in result)
            {
                giftID.Add(randomItems[index].Id);
            }

            return giftID;
        }

        private async void btnSubmitRandomOrder_Click(object sender, RoutedEventArgs e)
        {
            List<int> giftsId = CreateRandomOrder();

            // Call the client to send the selected indexes
            await Client.RunClientAsync(giftsId);

            //ServerRespondWindow serverRespondWindow = new ServerRespondWindow();
            //serverRespondWindow.Show();

            this.Close();
        }
    }
}
