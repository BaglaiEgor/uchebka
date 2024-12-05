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
    /// Логика взаимодействия для Department.xaml
    /// </summary>
    public partial class Departments : Page
    {
        int tab;
        public Departments(int TAB)
        {
            InitializeComponent();
            this.tab = TAB;
            var tempUser = Class1.dbo.Employee.FirstOrDefault(u => u.Tab_Number == tab);
            txtTeacher.Text = tempUser.Last_Name;
            DepartmentDataGrid.ItemsSource = Class1.dbo.Department.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.GoBack();
        }

        private void txtfil_TextChanged(object sender, RoutedEventArgs e)
        {
           Department department = Class1.department;
            string searchText = txtfil.Text.ToLower();
            DepartmentDataGrid.ItemsSource = Class1.dbo.Department.ToList().Where(s => s.Name.ToLower().Contains(searchText)).ToList();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            try
            {
                var tempDep = new Department()
                {
                    Code = txtCode.Text,
                    Name = txtName.Text,
                    FacultyAbbreviation = txtfaculity.Text,
                };
                Class1.dbo.Department.Add(tempDep);
                Class1.dbo.SaveChanges();
                MessageBox.Show("Добавлена кафедра");
                DepartmentDataGrid.ItemsSource = Class1.dbo.Department.ToList();
                return;
        }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
}

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem != null)
            {
                try
                {
                    Department department = DepartmentDataGrid.SelectedItem as Department;
                    if (cmbx.Text == "Name")
                    {
                        department.Name = txtBox.Text;
                    }
                    if (cmbx.Text == "FacultyAbbreviation")
                    {
                        department.FacultyAbbreviation = txtBox.Text;
                    }
                    Class1.dbo.SaveChanges();
                    DepartmentDataGrid.ItemsSource = Class1.dbo.Department.ToList();
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
            Department department = DepartmentDataGrid.SelectedItem as Department;
            Class1.dbo.Department.Remove(department);
            Class1.dbo.SaveChanges();
            DepartmentDataGrid.ItemsSource = Class1.dbo.Department.ToList();
        }
    }
}
