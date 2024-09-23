using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal static class RandomGenerators 
    {
        internal static List<Bank> GenerateBanks(List<User> users, int n)
        {
            string[] first = { "Swift", "Silent", "Mighty", "Brave", "Wild" };
            string[] second = { "Falcon", "Lion", "Tiger", "Eagle", "Wolf" };
            List<string> names = new List<string>();
            List<Bank> banks = new List<Bank>();

            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                int indexFirst = random.Next(first.Length);
                int indexSecond = random.Next(second.Length);
                string name = $"{first[indexFirst]} {second[indexSecond]}";

                while (names.Contains(name)) 
                {
                    indexFirst = random.Next(first.Length);
                    indexSecond = random.Next(second.Length);
                    name = $"{first[indexFirst]} {second[indexSecond]}";
                }

                names.Add(name);
                List<Bill> bills = GenerateBills(users);
                //banks.Add(new Bank(name, i + 1, random.Next(0, 5), bills));
            }
            return banks;
        }

        internal static List<User> GenerateUsers(int n)
        {
            string[] firstNames = { "Владимир", "Сергей", "Михаил", "Кирилл", "Дмитрий", "Хабибула"};
            string[] secondNames = { "Акопов", "Любимов", "Манкевич", "Новиков", "Обрубов", "Темури"};
            List<string> names = new List<string>();            
            List<User> users = new List<User>();

            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                int indexFirst = random.Next(firstNames.Length);
                int indexSecond = random.Next(secondNames.Length);
                string name = $"{firstNames[indexFirst]} {secondNames[indexSecond]}";
                string login = Translit.Transliting(firstNames[indexFirst] + secondNames[indexSecond]).ToLower();

                while (names.Contains(name))
                {
                    indexFirst = random.Next(firstNames.Length);
                    indexSecond = random.Next(secondNames.Length);
                    name = $"{firstNames[indexFirst]} {secondNames[indexSecond]}";
                    login = Translit.Transliting(firstNames[indexFirst] + secondNames[indexSecond]).ToLower();
                }
                names.Add(name);                
                //users.Add(new User(name,login, random.Next(1000, 9999).ToString()));
            }
            return users;                    
        }

        internal static List<Bill> GenerateBills(List<User> users)
        {            
            List<Bill> bills = new List<Bill>();
            List<string> logins = new List<string>();
            Random random = new Random();
            int countBills = random.Next(1, users.Count);

            for (int i = 0; i < countBills; i++)
            {
                int target = random.Next(0, users.Count - 1);

                while (logins.Contains(users[target].Login))
                {
                    target = random.Next(0, users.Count - 1);                    
                }

                logins.Add(users[target].Login);
                decimal value = random.Next(100, 1000);
                Bill targetBill = new Bill(i + 1, users[target].Login, value);
                bills.Add(targetBill);                
            }
            return bills;
        } 

        internal static void WriteToJson<T>(string fileName, T data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, json);  
        }

        internal static T ReadFromJson<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
