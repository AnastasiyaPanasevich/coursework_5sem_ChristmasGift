using ChristmasGiftClient.Controller;
using ChristmasGiftClient.Model;
using Dictionary_Server;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChristmasGiftClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;

            btnRUSLang.Click += ChangeLanguageClick;
            btnRUSLang.Tag = new CultureInfo("ru-RU");
            btnENGLang.Click += ChangeLanguageClick;
            btnENGLang.Tag = new CultureInfo("en-EN");
        }


        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            GetGifts();
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            Button mi = sender as Button;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            GetGifts();
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();

            this.Close();
        }


        // кнопка передает что-то серверу в отдельном потоке
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static void StartClient()
        {
            string message = "example";

            //ristmasGiftClient.Model.Client.RunClientAsync(message).Wait(); // Ждем завершения выполнения клиента

        }

        
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            // Запускаем клиента в отдельном потоке
            Task.Run(() => StartClient());
        }

        private void btnRUSLang_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnENGLang_Click(object sender, RoutedEventArgs e)
        {

        }

        public static void GetGifts()
        {
            List<string[]> gifts = DataBaseReader.ReadEverything(App.Language.ToString());
            if(States.Ornaments.Count > 0)
            {
                States.Candles.Clear();
                States.Clothes.Clear();
                States.Cookies.Clear();
                States.Ornaments.Clear();
            }

            foreach (var gift in gifts)
            {
                switch (Convert.ToInt32(gift[0]))
                {
                    case >= 4000:
                        States.Ornaments.Add(new Ornaments(gift));
                        break;
                    case >= 3000:
                        States.Cookies.Add(new GiftLib.Cookie(gift));
                        break;
                    case >= 2000:
                        States.Clothes.Add(new Clothes(gift));
                        break;
                    case >= 1000:
                        States.Candles.Add(new Candles(gift));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
