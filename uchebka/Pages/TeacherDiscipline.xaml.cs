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
    /// Логика взаимодействия для TeacherDiscipline.xaml
    /// </summary>
    public partial class TeacherDiscipline : Page
    {
        int tab;
        public TeacherDiscipline(int TAB)
        {
            InitializeComponent();
            this.tab = TAB;
            var tempUser = Class1.dbo.Employee.FirstOrDefault(u => u.Tab_Number == tab);
            if (tempUser.Position == "преподаватель")
            {
                stackAdd.Visibility = Visibility.Hidden;
                stpan.Visibility = Visibility.Hidden;
            }
            DisciplineDataGrid.ItemsSource = Class1.dbo.Disciplines.ToList();
            txtTeacher.Text = tempUser.Last_Name;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.GoBack();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Disciplines disciplines = DisciplineDataGrid.SelectedItem as Disciplines;
            Class1.dbo.Disciplines.Remove(disciplines);
            Class1.dbo.SaveChanges();
            DisciplineDataGrid.ItemsSource = Class1.dbo.Disciplines.ToList();
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplineDataGrid.SelectedItem != null)
            {
                try
                {
                    Disciplines disciplines = DisciplineDataGrid.SelectedItem as Disciplines;
                    if (cmbx.Text == "Volume")
                    {
                        disciplines.Volume = Convert.ToInt32(txtBox.Text);
                    }
                    if (cmbx.Text == "Name")
                    {
                        disciplines.Name = txtBox.Text;
                    }
                    if (cmbx.Text == "Department")
                    {
                        disciplines.Code_department = txtBox.Text;
                    }
                    Class1.dbo.SaveChanges();
                    DisciplineDataGrid.ItemsSource = Class1.dbo.Disciplines.ToList();
                    return;
                }
                catch
                {
                    MessageBox.Show("Данные неправильные");
                }
            }
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            string volume = txtVolume.Text;
            string name = txtName.Text;
            string dep = txtDep.Text;

            try
            {
                var tempDiscip = new Disciplines()
                {
                    Volume = Convert.ToInt32(volume),
                    Name = name,
                    Code_department = dep
                };
                Class1.dbo.Disciplines.Add(tempDiscip);
                Class1.dbo.SaveChanges();
                MessageBox.Show("Добавлена дисциплина");
                DisciplineDataGrid.ItemsSource = Class1.dbo.Disciplines.ToList();
                return;
            }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
        }
    }
}
