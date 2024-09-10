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
    }
}
