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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private readonly IServiceTask _TaskList;
        private int _userId;
        public AddWindow(int userId)
        {
            InitializeComponent();
            _TaskList = new BdConnectingTaskList();
            _userId = userId;
        }

        private void enter_Button_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task
            {
                Title = title_TextBox.Text,
                Description = description_TextBox.Text,
                DueDate = noDateDuo_CheckBox.IsChecked == true ? (DateTime?)null : dateDuo_DatePicker.SelectedDate,
                IsCompleted = true_RadioButton.IsChecked == true,
                UserId = _userId
            };

            try
            {
                // Добавляем задачу в базу данных
                _TaskList.addTask(task);
                MessageBox.Show("Задача успешно добавлена.");
                OrderWindow orderWindow = new OrderWindow(_userId);
                orderWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении задачи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void exit_ButtonButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow(_userId);
            orderWindow.Show();
            this.Close();
        }

        private void noDateDuo_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            dateDuo_DatePicker.IsEnabled = false;
        }

        private void noDateDuo_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            dateDuo_DatePicker.IsEnabled = true;
        }
    }
}
