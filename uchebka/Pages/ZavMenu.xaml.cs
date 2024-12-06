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
using uchebka.dbo;

namespace uchebka.Pages
{
    /// <summary>
    /// Логика взаимодействия для ZavMenu.xaml
    /// </summary>
    public partial class ZavMenu : Page
    {
        int tab;
        public ZavMenu(int TAB)
        {
            InitializeComponent();
            this.tab = TAB; 
            var tempUser = Class1.dbo.Employee.FirstOrDefault(u => u.Tab_Number == tab);
            if (tempUser.Position == "зав. кафедрой")
            {
               buttSpec.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new Login());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new Employees(tab));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new TeacherDiscipline(tab));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new TeacherExam(tab));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new Departments(tab));
        }

        private void buttSpec_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new Speciality(tab));
        }
    }
}
