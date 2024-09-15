using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_2
{
    interface IServiceTask
    {
        void addTask(Task task);
        void deleteTask(int task_id);
        void updateTask(Task task);
        List<Task> GetTasks(int user_id);
        List<Task> GetTask(int task_id);
    }
}
