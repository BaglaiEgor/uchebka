using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для TeacherMenu.xaml
    /// </summary>
    public partial class TeacherExam : Page
    {
        int tab;
        public TeacherExam(int TAB)
        {
            InitializeComponent();
            this.tab = TAB;
            var tempUser = Class1.dbo.Employee.FirstOrDefault(u=>u.Tab_Number == tab);
            if (tempUser.Position == "преподаватель")
            {
                ExamDataGrid.ItemsSource = tempUser.Exam.ToList();
                stackAdd.Visibility = Visibility.Hidden;
                stpan.Visibility = Visibility.Hidden;
            }
            else
            {
                ExamDataGrid.ItemsSource = Class1.dbo.Exam.ToList();
                buttUpdate.Visibility = Visibility.Hidden;
                txtBallChanged.Visibility = Visibility.Hidden;
                Ball.Visibility = Visibility.Hidden;
            }
            txtTeacher.Text = tempUser.Last_Name;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tempUser = Class1.dbo.Employee.FirstOrDefault(u => u.Tab_Number == tab);
                if (ExamDataGrid.SelectedItem != null)
                {
                    if (Convert.ToInt32(txtBallChanged.Text) > 0)
                    {
                        Exam selectedExam = ExamDataGrid.SelectedItem as Exam;
                        if (selectedExam != null)
                        {
                            if (Convert.ToInt32(txtBallChanged.Text) >= 1 && Convert.ToInt32(txtBallChanged.Text) <= 5)
                            {
                                selectedExam.Grade = Convert.ToInt32(txtBallChanged.Text);
                                txtBallChanged.Text = null;
                                Class1.dbo.SaveChanges();
                                ExamDataGrid.ItemsSource = tempUser.Exam.ToList();
                                MessageBox.Show("Оценка изменена");
                            }
                            else
                            {
                                MessageBox.Show("Оценка введена неправильно");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Не удалось получить экзамен");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали строку");
                }
            }
            catch
            {
                MessageBox.Show("Данные заполнены неккоректно");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Duble.MainFrame.GoBack();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtGrade.Text) >= 1 && Convert.ToInt32(txtGrade.Text) <= 5)
                {
                    var tempExam = new Exam()
                    {
                        Date = Convert.ToDateTime(txtDate.Text),
                        Code = Convert.ToInt32(txtCode.Text),
                        Reg_number = Convert.ToInt32(txtNumber.Text),
                        Tab_number = Convert.ToInt32(txtTab.Text),
                        Auditorium = txtAud.Text,
                        Grade = Convert.ToInt32(txtGrade.Text)
                    };
                    Class1.dbo.Exam.Add(tempExam);
                    Class1.dbo.SaveChanges();
                    MessageBox.Show("Добавлен экзамен");
                    ExamDataGrid.ItemsSource = Class1.dbo.Exam.ToList();
                    return;
                }
                else
                {
                    MessageBox.Show("Оценка введена неправильно");
                }
            }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (ExamDataGrid.SelectedItem != null)
            {
                try
                {
                    Exam exam = ExamDataGrid.SelectedItem as Exam;
                    if (cmbx.Text == "Date")
                    {
                        exam.Date = Convert.ToDateTime(txtBox.Text);
                    }
                    if (cmbx.Text == "Code")
                    {
                        exam.Code = Convert.ToInt32(txtBox.Text);
                    }
                    if (cmbx.Text == "Number")
                    {
                        exam.Reg_number = Convert.ToInt32(txtBox.Text);
                    }
                    if (cmbx.Text == "Tab")
                    {
                        exam.Tab_number = Convert.ToInt32(txtBox.Text);
                    }
                    if (cmbx.Text == "Aud")
                    {
                        exam.Auditorium = txtBox.Text;
                    }
                    if (cmbx.Text == "Grade")
                    {
                        if (Convert.ToInt32(txtBox.Text) >= 1 && Convert.ToInt32(txtBox.Text) <= 5)
                        exam.Grade = Convert.ToInt32(txtBox.Text);
                        else
                        {
                            MessageBox.Show("Оценка введена неправильно");
                        }
                    }
                    Class1.dbo.SaveChanges();
                    ExamDataGrid.ItemsSource = Class1.dbo.Disciplines.ToList();
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
            Exam exam = ExamDataGrid.SelectedItem as Exam;
            Class1.dbo.Exam.Remove(exam);
            Class1.dbo.SaveChanges();
            ExamDataGrid.ItemsSource = Class1.dbo.Exam.ToList();
        }
    }
}
