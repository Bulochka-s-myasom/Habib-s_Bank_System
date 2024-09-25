using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class HelperBD
    {
        private DataBase _data;
        public DataBase DataBase 
        {
            get 
            {
                string fileName = "dataBaseMock.json";
                string jsonString = File.ReadAllText(fileName);
                _data = JsonConvert.DeserializeObject<DataBase>(jsonString);
                return _data;
            }

            set 
            {
                
            } 
        }

        public void GetAllUsers()
        {
            Console.WriteLine("Доступные пользователи:");
            foreach (var us in DataBase.Users)
            {
                Console.WriteLine($"Логин: {us.Login} Пароль: {us.Password}");
            }
            Console.WriteLine();
        }

        public User GetCurrentUser(string login, string pass)
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

        public void Save()
        {
            string fileName = "dataBaseMock.json";
            string jsonString = JsonConvert.SerializeObject(_data, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
