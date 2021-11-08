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
        int i { get; set; }
        public EnterEmployees()
        {
            InitializeComponent();
            employee = new ObservableCollection<employee>(db_connection.connection.employee.ToList());
            this.DataContext = this;
            
        }
        private void btn_write_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var save = new logins();
                save.id_employee = i;
                save.login_time = DateTime.Now.TimeOfDay;
                db_connection.connection.logins.Add(save);
                db_connection.connection.SaveChanges();
                MessageBox.Show("all ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex}", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ListView).SelectedItem as employee;
            i = a.id_employee;
        }
    }
}
