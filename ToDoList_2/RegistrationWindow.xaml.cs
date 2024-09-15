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
using System.Windows.Shapes;
using ToDoList_2.BdConnecting;

namespace ToDoList_2
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly IServiceUser _IUser;
        private User user;
        public RegistrationWindow()
        {
            InitializeComponent();
            _IUser = new BdConnectingUser();
            user = new User();
        }

        private void registration_Button_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на веденные данные
            if (login_TextBox.Text != "" && email_TextBox.Text != "" && password_TextBox.Text != "")
            {
                user.Username = login_TextBox.Text;
                user.Email = email_TextBox.Text;
                user.Password = password_TextBox.Text;
            }
            else
            {
                MessageBox.Show("Данные не введены");
                return;
            }
            //Проверка на дубликаты данных
            if (_IUser.CheckUserBase(user))
            {
                MessageBox.Show("Пользователь под этим именем уже используется");
                return;
            }
            else if (_IUser.CheckEmailBase(user))
            {
                MessageBox.Show("Почта уже используется");
                return;
            }
            else
            {
                _IUser.Registration(user);
                MessageBox.Show("Регистрация прошла успешно");
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
