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
    /// Логика взаимодействия для Teachers.xaml
    /// </summary>
    public partial class Teachers : Page
    {
        int tab;
        public Teachers(int TAB)
        {
            InitializeComponent();
            this.tab = TAB;
            var tempUser = Class1.dbo.Employee.FirstOrDefault(u => u.Tab_Number == tab);
            txtTeacher.Text = tempUser.Last_Name;
            TeachersDataGrid.ItemsSource = Class1.dbo.Employee.ToList();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            try
            {
                string position = txtPosition.Text;
                var emp = Class1.dbo.Employee.FirstOrDefault(a => a.Position == position);
                if (emp != null)
                {
                    if (Convert.ToInt32(txtSalary.Text) > 0)
                    {
                        var tempEmp = new Employee()
                        {
                            Code_department = txtCode.Text,
                            Last_Name = txtLastName.Text,
                            Position = txtPosition.Text,
                            Salary = Convert.ToDecimal(txtSalary.Text),
                            Chief = Convert.ToInt32(txtChief.Text)
                        };
                        Class1.dbo.Employee.Add(tempEmp);
                        if (tempEmp.Position == "преподаватель")
                        {
                            var tempTeach = new Teacher()
                            {
                                Tab_number = tempEmp.Tab_Number,
                                Title = "-",
                                Degree = "-",
                            };
                            Class1.dbo.Teacher.Add(tempTeach);
                        }
                        else if (tempEmp.Position == "зав. кафедрой")
                        {
                            var tempZav = new Zav_Kaf()
                            {
                                Tab_Number = tempEmp.Tab_Number,
                                Code_request = null
                            };
                            Class1.dbo.Zav_Kaf.Add(tempZav);
                        }
                        else
                        {
                            var tempEng = new Engineer()
                            {
                                Tab_Number = tempEmp.Tab_Number,
                                Specialize = null
                            };
                            Class1.dbo.Engineer.Add(tempEng);
                        }
                        Class1.dbo.SaveChanges();
                        MessageBox.Show("Добавлен сотрудник");
                        TeachersDataGrid.ItemsSource = Class1.dbo.Employee.ToList();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Оклад не может быть отрицательным");
                    }
                }
                else
                {
                    MessageBox.Show("Такой должности не существует");
                }
        }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
}

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersDataGrid.SelectedItem != null)
            {
                try
                {
                    Employee employee = TeachersDataGrid.SelectedItem as Employee;
                    if (cmbx.Text == "Code")
                    {
                        employee.Code_department = txtBox.Text;
                    }
                    if (cmbx.Text == "LName")
                    {
                        employee.Last_Name = txtBox.Text;
                    }
                    if (cmbx.Text == "Position")
                    {
                        var emp = Class1.dbo.Employee.FirstOrDefault(a => a.Position == txtBox.Text);
                        if (emp != null)
                        employee.Position = txtBox.Text;
                        else
                        {
                            MessageBox.Show("Такой должности не существует");
                        }
                    }
                    if (cmbx.Text == "Salary")
                    {
                        if (Convert.ToInt32(txtBox.Text) > 0)
                            employee.Salary = Convert.ToDecimal(txtBox.Text);
                        else
                        {
                            MessageBox.Show("Оклад не может меньше 0");
                        }
                    }
                    Class1.dbo.SaveChanges();
                    TeachersDataGrid.ItemsSource = Class1.dbo.Employee.ToList();
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
            if (TeachersDataGrid.SelectedItem != null)
            {
                try
                {
                    var tempUser = Class1.dbo.Employee.FirstOrDefault(u => u.Tab_Number == tab);
                    Employee employeeToDelete = TeachersDataGrid.SelectedItem as Employee;
                    if (tempUser != employeeToDelete)
                    {
                        if (employeeToDelete.Position == "преподаватель")
                        {
                            var teacher = Class1.dbo.Teacher.FirstOrDefault(t => t.Tab_number == employeeToDelete.Tab_Number);
                            if (teacher != null)
                            {
                                Class1.dbo.Teacher.Remove(teacher);
                            }
                        }
                        else if (employeeToDelete.Position == "зав. кафедрой")
                        {
                            var zavKaf = Class1.dbo.Zav_Kaf.FirstOrDefault(z => z.Tab_Number == employeeToDelete.Tab_Number);
                            if (zavKaf != null)
                            {
                                Class1.dbo.Zav_Kaf.Remove(zavKaf);
                            }
                        }
                        else
                        {
                            var engineer = Class1.dbo.Engineer.FirstOrDefault(em => em.Tab_Number == employeeToDelete.Tab_Number);
                            if (engineer != null)
                            {
                                Class1.dbo.Engineer.Remove(engineer);
                            }
                        }
                        Class1.dbo.Employee.Remove(employeeToDelete);
                        Class1.dbo.SaveChanges();
                        TeachersDataGrid.ItemsSource = Class1.dbo.Employee.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Нельзя удалить себя");
                    }
                }   
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.GoBack();
        }

        private void txtfil_TextChanged(object sender, RoutedEventArgs e)
        {
            Employee employee = Class1.employee;
            string searchText = txtfil.Text.ToLower();
            TeachersDataGrid.ItemsSource = Class1.dbo.Employee.ToList().Where(s => s.Last_Name.ToLower().Contains(searchText)).ToList();
        }
    }
}
