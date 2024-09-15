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
    /// Логика взаимодействия для UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private readonly IServiceTask _TaskList;
        private int _taskId;
        private int _userId;
        public UpdateWindow(int taskId, int userId)
        {
            InitializeComponent();
            _TaskList = new BdConnectingTaskList();
            _taskId = taskId;
            _userId = userId;
            LoadTask();
        }
        private void LoadTask()
        {
            List<Task> tasks = _TaskList.GetTask(_taskId);
            if (tasks.Count > 0)
            {
                Task task = tasks[0];

                title_TextBox.Text = task.Title;
                description_TextBox.Text = task.Description;

                if (task.DueDate == null)
                {
                    noDateDuo_CheckBox.IsChecked = true;
                    dateDuo_DatePicker.IsEnabled = false;
                    dateDuo_DatePicker.SelectedDate = null;
                }
                else
                {
                    noDateDuo_CheckBox.IsChecked = false;
                    dateDuo_DatePicker.IsEnabled = true;
                    dateDuo_DatePicker.SelectedDate = task.DueDate;
                }

                true_RadioButton.IsChecked = task.IsCompleted;
                false_RadioButton.IsChecked = !task.IsCompleted;
            }
            else
            {

                title_TextBox.Text = string.Empty;
                description_TextBox.Text = string.Empty;
                noDateDuo_CheckBox.IsChecked = false;
                dateDuo_DatePicker.IsEnabled = true;
                dateDuo_DatePicker.SelectedDate = null;
                true_RadioButton.IsChecked = false;
                false_RadioButton.IsChecked = true;
            }
        }

        private void enter_Button_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task
            {
                TaskId = _taskId,
                Title = title_TextBox.Text,
                Description = description_TextBox.Text,
                DueDate = noDateDuo_CheckBox.IsChecked == true ? (DateTime?)null : dateDuo_DatePicker.SelectedDate,
                IsCompleted = true_RadioButton.IsChecked == true
            };

            try
            {
                // Обновление задачи
                _TaskList.updateTask(task);
                MessageBox.Show("Задача успешно обновлена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении задачи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            OrderWindow orderWindow = new OrderWindow(_userId);
            orderWindow.Show();
            this.Close();
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
