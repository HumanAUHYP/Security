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

namespace Security
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string password = "123";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbx_password.Password == password)
                {
                    MessageBox.Show("All OK");
                    EnterEmployees enterEmployees = new EnterEmployees();
                    enterEmployees.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show($"Неверный пароль", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
