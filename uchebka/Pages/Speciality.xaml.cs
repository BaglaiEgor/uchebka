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
    /// Логика взаимодействия для Speciality.xaml
    /// </summary>
    public partial class Speciality : Page
    {
        int tab;
        public Speciality(int TAB)
        {
            InitializeComponent();
            this.tab = TAB;
            var tempUser = Class1.dbo.Employee.FirstOrDefault(u => u.Tab_Number == tab);
            txtTeacher.Text = tempUser.Last_Name;
            SpecialityDataGrid.ItemsSource = Class1.dbo.Specialities.ToList();
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialityDataGrid.SelectedItem != null)
            {
                try
                {
                    Specialities specialities = SpecialityDataGrid.SelectedItem as Specialities;
                    if (cmbx.Text == "Direction")
                    {
                        specialities.Direction = txtBox.Text;
                    }
                    if (cmbx.Text == "Code")
                    {
                        specialities.Code_department = txtBox.Text;
                    }
                    Class1.dbo.SaveChanges();
                    SpecialityDataGrid.ItemsSource = Class1.dbo.Specialities.ToList();
                    return;
                }
                catch
                {
                    MessageBox.Show("Данные неправильные");
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Specialities specialities = SpecialityDataGrid.SelectedItem as Specialities;
            Class1.dbo.Specialities.Remove(specialities);
            Class1.dbo.SaveChanges();
            SpecialityDataGrid.ItemsSource = Class1.dbo.Specialities.ToList();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            try
            {
                var tempSpec = new Specialities()
                {
                    Number = txtNumber.Text,
                    Direction = txtDirection.Text,
                    Code_department = txtCode.Text
                };
                Class1.dbo.Specialities.Add(tempSpec);
                Class1.dbo.SaveChanges();
                MessageBox.Show("Добавлена специальность");
                SpecialityDataGrid.ItemsSource = Class1.dbo.Specialities.ToList();
                return;
            }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.GoBack();
        }
    }
}
