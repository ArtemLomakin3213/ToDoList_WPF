using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_2
{
    class Task
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public Task()
        {
            Title = "";
            Description = "";
            DueDate = null;
            IsCompleted = false; // Задача не выполнена по умолчанию
        }

        public override string ToString()
        {
            return $"{TaskId} - {Title} - {Description} - Due: {DueDate} - Completed: {IsCompleted}";
        }
    }
}
