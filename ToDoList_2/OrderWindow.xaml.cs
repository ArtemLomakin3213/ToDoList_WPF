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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private readonly int _user_id;
        private readonly IServiceTask _TaskList;
        public OrderWindow(int user_id)
        {
            InitializeComponent();
            _user_id = user_id;
            _TaskList = new BdConnectingTaskList();
            LoadTasks();
        }

        private void LoadTasks()
        {
            List<Task> tasks = _TaskList.GetTasks(_user_id);
            task_ListBox.Items.Clear();
            foreach (var task in tasks)
            {
                string dueDateDisplay = task.DueDate.HasValue ? task.DueDate.Value.ToShortDateString() : "No due date";
                string taskDisplay = $"{task.TaskId}: {task.Title} - {task.Description} - Due: {dueDateDisplay} - Completed: {task.IsCompleted}";
                task_ListBox.Items.Add(taskDisplay);
            }
        }
        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(_user_id);
            addWindow.Show();
            this.Close();
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            if (task_ListBox.SelectedItem != null)
            {
                // Получаем строку из выбранного элемента
                string selectedItem = task_ListBox.SelectedItem.ToString();

                int colonIndex = selectedItem.IndexOf(':');
                if (colonIndex > 0)
                {
                    string taskIdString = selectedItem.Substring(0, colonIndex);
                    if (int.TryParse(taskIdString, out int taskId))
                    {

                        _TaskList.deleteTask(taskId);
                        LoadTasks(); // Обновляем список задач
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления.");
            }
        }


        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            if (task_ListBox.SelectedItem != null)
            {
                // Получаем строку из выбранного элемента
                string selectedItem = task_ListBox.SelectedItem.ToString();

                int colonIndex = selectedItem.IndexOf(':');
                if (colonIndex > 0)
                {
                    string taskIdString = selectedItem.Substring(0, colonIndex).Trim();
                    if (int.TryParse(taskIdString, out int taskId))
                    {
                        UpdateWindow updateWindow = new UpdateWindow(taskId, _user_id);
                        updateWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        // Обработка случая, когда парсинг taskId не удался 
                        MessageBox.Show("Неверный формат ID задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    // Обработка случая, когда формат не соответствует ожиданиям 
                    MessageBox.Show("Формат выбранного элемента некорректен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Обработка случая, когда ничего не выбрано 
                MessageBox.Show("Пожалуйста, выберите задачу для обновления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
