using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using ToDoList_2.BdConnecting;

namespace ToDoList_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceUser _IUser;
        public MainWindow()
        {
            InitializeComponent();
            _IUser = new BdConnectingUser();
            LoadMain();
        }

        private void entry_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = login_TextBox.Text;
                string password = showPasswordCheckBox.IsChecked == true ? passwordTextBox.Text : passwordBox.Password;
                bool count = _IUser.GetToName(username, password);
                if (count)
                {
                    int user_id = _IUser.CheckIdBase(username);
                    MessageBox.Show("Аунтификация прошла успешно");
                    OrderWindow orderWindow = new OrderWindow(user_id);
                    this.Close();
                    orderWindow.Show();
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пороль");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void showPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Скрываем PasswordBox и показываем TextBox
            LoadMain();
        }

        private void showPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Скрываем TextBox и возвращаем PasswordBox
            passwordBox.Password = passwordTextBox.Text;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
        }

        private void LoadMain()
        {
            passwordBox.Password = passwordTextBox.Text;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
        }

        private void register_Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationwindow = new RegistrationWindow();
            this.Close();
            registrationwindow.Show();
        }


        private void close_Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
