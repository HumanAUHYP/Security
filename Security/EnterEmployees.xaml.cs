using System;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Security
{
    /// <summary>
    /// Логика взаимодействия для EnterEmployees.xaml
    /// </summary>
    public partial class EnterEmployees : Window
    {
        public static ObservableCollection<employee> employee { get; set; }
        public static ObservableCollection<lateness> latenesss { get; set; }
        public static ObservableCollection<penalty> penalty { get; set; }

        string emp_name { get; set; }
        int employee_id { get; set; }
        int penalty_id { get; set; }
        public EnterEmployees()
        {
            InitializeComponent();
            employee = new ObservableCollection<employee>(db_connection.connection.employee.Where(a => a.came == false));
            this.DataContext = this;

            latenesss = new ObservableCollection<lateness>(db_connection.connection.lateness.Where(a => a.paid == false));
            penalty = new ObservableCollection<penalty>(db_connection.connection.penalty.ToList());
        }
        private void btn_write_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var save = new lateness();
                
                save.login_time = DateTime.Now.TimeOfDay;
                save.id_employee = employee_id;
                save.name = emp_name;
                var late = save.login_time - DateTime.Parse("8:00:00").TimeOfDay;
                MessageBox.Show($"Опаздал на {late}");
                
                foreach (var el in penalty)
                {
                    if (late > el.penalty_time)
                    {
                        penalty_id = el.id_penalty;
                    }
                }
                var come = employee.Where(a => a.id_employee == employee_id).FirstOrDefault();
                come.came = true;

                save.id_penalty = penalty_id;
                db_connection.connection.lateness.Add(save);
                db_connection.connection.SaveChanges();
                MessageBox.Show("all ok");

                EnterEmployees enterEmployees = new EnterEmployees();
                enterEmployees.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void non_come_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ListView).SelectedItem as employee;
            employee_id = a.id_employee;
            emp_name = a.name;
        }

        private void lateness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ListView).SelectedItem as lateness;
            employee_id = a.id_employee;
        }

        private void btn_penalty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employee = new ObservableCollection<employee>(db_connection.connection.employee.Where(a => a.came == true));
                var late = latenesss.Where(a => a.id_employee == employee_id).FirstOrDefault();
                var fine = penalty.Where(a => a.id_penalty == late.id_penalty).FirstOrDefault();
                var emp = employee.Where(a => a.id_employee == late.id_employee).FirstOrDefault();
                emp.balance -= fine.penalty_cost;
                late.paid = true;
                db_connection.connection.SaveChanges();
                MessageBox.Show($"{emp.name} проштрафился на {fine.penalty_cost} рублей");

                EnterEmployees enterEmployees = new EnterEmployees();
                enterEmployees.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
