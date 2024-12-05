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

namespace uchebka.Pages
{
    /// <summary>
    /// Логика взаимодействия для TeacherMenu.xaml
    /// </summary>
    public partial class TeacherMenu : Page
    {
        int tab;
        public TeacherMenu(int TAB)
        {
            InitializeComponent();
            this.tab = TAB;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new TeacherExam(tab));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new Login());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.Navigate(new TeacherDiscipline(tab));
        }
    }
}
