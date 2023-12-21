using System.Windows;

namespace ChristmasGiftClient.Controller
{
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
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();

            this.Close();

        }

        // Метод для обновления значения лейбла
        public void UpdateLabelText(string newText)
        {
            // Присваиваем новое значение Content лейбла
            lblServerRespond.Content = newText;
        }
    }
}
