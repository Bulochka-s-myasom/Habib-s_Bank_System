using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class DataBaseMock
    {
        public List<Bank> Banks { get; } = new List<Bank>();
        public List<User> Users { get; } = new List<User>();

        public DataBaseMock() 
        {
            Random random = new Random();
            var banksCount = random.Next(3, 7);
            List<string> bankNames = RandomGenerators.GenerateBanksName(banksCount);

            for (int i = 0; i < bankNames.Count; i++)
            {
                Banks.Add(new Bank(bankNames[i], i + 1, random.Next(0, 5)));
            }
            
            var userCount = random.Next(5, 10);
            List<>
        }
    }
}
