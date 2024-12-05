using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using QRCoder;

namespace uchebka.Pages
{
    /// <summary>
    /// Логика взаимодействия для QrCodePage.xaml
    /// </summary>
    public partial class QrCodePage : Page
    {
        public QrCodePage()
        {
            InitializeComponent();
            string url = "https://yandex.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fgamoz.ru%2Fwp-content%2Fuploads%2F2020%2F01%2Fnovij-salat-s-krabovimi-palochkami-5vlxmos3.jpg&lr=43&pos=1&rpt=simage&text=%D0%BA%D1%80%D0%B0%D0%B1%D0%BE%D0%B2%D1%8B%D0%B9%20%D1%81%D0%B0%D0%BB%D0%B0%D1%82";
            QrImage.Source = GenerateQrCodeBitmapImage(url);
        }
        private BitmapImage GenerateQrCodeBitmapImage(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q); using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            ms.Position = 0;
                            BitmapImage bitmapImage = new BitmapImage(); bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad; bitmapImage.StreamSource = ms;
                            bitmapImage.EndInit();
                            return bitmapImage;
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.GoBack();
        }
    }
}
