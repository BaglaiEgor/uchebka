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
using System.Windows.Navigation;
using System.Windows.Shapes;
using uchebka.Pages;
using uchebka.dbo;

namespace uchebka
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tabn = Convert.ToInt32(txtTab.Text);
                var tempUs = Class1.dbo.Employee.FirstOrDefault(u => u.Tab_Number == tabn);
                if (tempUs != null)
                {
                    if (tempUs.Position == "зав. кафедрой")
                    {
                        MessageBox.Show("Здравствуй, зав. кафедрой!");
                        MainWindow.Duble.MainFrame.Navigate(new ZavMenu(tempUs.Tab_Number));
                    }
                    else if (tempUs.Position == "преподаватель")
                    {
                        MessageBox.Show("Здравствуй, преподаватель!");
                        MainWindow.Duble.MainFrame.Navigate(new TeacherMenu(tempUs.Tab_Number));
                    }
                    else if (tempUs.Position == "инженер")
                    {
                        MessageBox.Show("Здравствуй, инженер!");
                        MainWindow.Duble.MainFrame.Navigate(new ZavMenu(tempUs.Tab_Number));
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь не найден");
                }
            }
            catch
            {
                MessageBox.Show("Данные заполнены неккоректно");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new QrCodePage());
        }
    }
}
