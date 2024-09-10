using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal static class RandomGenerators 
    {
        internal static List<string> GenerateBanksName(int n)
        {
            string[] first = { "Swift", "Silent", "Mighty", "Brave", "Wild" };
            string[] second = { "Falcon", "Lion", "Tiger", "Eagle", "Wolf" };

            List<string> names = new List<string>();

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
            }
            return names;
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
                users.Add(new User(name,login, random.Next(1000, 9999).ToString()));
            }
            return users;
                    
        }
    }
}
