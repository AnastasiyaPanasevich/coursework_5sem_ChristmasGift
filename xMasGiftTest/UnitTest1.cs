using ChristmasGiftClient.Controller;
using GiftLib;
using System.Reflection;
using System.Windows.Controls;

namespace xMasGiftTest
{
    public class UnitTest1
    {
        [Fact]
        [STAThread]
        public void RandomGiftPriceTest()
        {
            Thread thread = new Thread(() =>
            { 
                RandomWindow randomWindow = new RandomWindow();
                Type t = typeof(RandomWindow);

                FieldInfo f = t.GetField("BudgetTextBox", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                f.SetValue(TextBlock.TextProperty, 13.1);

                var res = t.InvokeMember("CreateRandomOrder",
                    BindingFlags.InvokeMethod | BindingFlags.NonPublic |
                    BindingFlags.Public | BindingFlags.Instance,
                    null, randomWindow, null);

                Assert.NotEmpty((System.Collections.IEnumerable)res);
            });
            thread.SetApartmentState(ApartmentState.STA);
        }
    }
}