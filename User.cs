using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class User
    {
        public string Name { get; }
        public string Login { get; }
        public string Password { get; }

        public User(string name, string login, string pswd)
        {
            Name = name;
            Login = login;
            Password = pswd;
        }
    }
}
