using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_2
{
    interface IServiceUser
    {
        bool GetToName(string Username, string Password);
        void Registration(User user);
        bool CheckUserBase(User user);
        bool CheckEmailBase(User user);
        int CheckIdBase(string Username);
    }
}
