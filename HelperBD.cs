using Newtonsoft.Json;

namespace Bank_of_Habib
{
    internal static class HelperBD
    {
        private static DataBase _data;          
        public static DataBase DataBase
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;                
            }
        }

        public static void Init() 
        {
            LoadData();
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
            Console.WriteLine("Доступные пользователи:");
            foreach (var us in _data.Users)
            {
                Console.WriteLine($"Логин: {us.Login} Пароль: {us.Password}");
            }
            Console.WriteLine();
        }

        public static User GetCurrentUser(string login, string pass)
        {
            foreach (var us in _data.Users)
            {
                if (login == us.Login && us.Password == pass)
                {
                    return us;
                }
            }
            return null;
        }

        
    }
}
