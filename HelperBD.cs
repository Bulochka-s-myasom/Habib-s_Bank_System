using Newtonsoft.Json;
using System;

namespace Bank_of_Habib
{
    internal class HelperBD
    {
        private static readonly object _syncRoot = new();
        private static DataBase? _data;          
        public static DataBase DataBase
        {
            get
            {
                if (_data == null)
                {
                    lock (_syncRoot)
                    {
                        if (_data == null )
                        {
                            LoadData();
                        }
                    }
                }
                return _data!;
            }
            set
            {
                _data = value;                
            }
        }

      

        private static void LoadData()
        {
            string fileName = "dataBaseMock.json";
            string jsonString = File.ReadAllText(fileName);
            _data = JsonConvert.DeserializeObject<DataBase>(jsonString);
        }

        public static void Save()
        {
            string fileName = "dataBaseMock.json";
            string jsonString = JsonConvert.SerializeObject(_data, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
            LoadData();
        }

        public static void GetAllUsers()
        {
            Console.WriteLine();
            Console.WriteLine("Доступные пользователи:");
            foreach (var us in DataBase.Users)
            {
                Console.WriteLine($"Логин: {us.Login} Пароль: {us.Password}");
            }
            Console.WriteLine();
        }

        public static User GetCurrentUser(string login, string pass)
        {
            foreach (var us in DataBase.Users)
            {
                if (login == us.Login && us.Password == pass)
                {
                    return us;
                }
            }
            return null;
        }

        public static bool AddUser(string name, string login, string pass)
        {
            if (DataBase.Users.FirstOrDefault(l => l.Login == login) is null)
            {
                LoadData();
                User newUser = new User();
                newUser.Name = name;
                newUser.Login = login;
                newUser.Password = pass;
                _data!.Users.Add(newUser);
                return true;
            }
            return false;
        }

        
    }
}
