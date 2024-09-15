using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_2
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User()
        {
            Username = "";
            Password = "";
            Email = "";
        }
        public override string ToString()
        {
            return $"{Id} - {Username} - {Password} - {Email}";
        }
    }
}
