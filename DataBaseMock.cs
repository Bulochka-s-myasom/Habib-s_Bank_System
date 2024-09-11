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

            var userCount = random.Next(5, 10);
            Users = RandomGenerators.GenerateUsers(userCount);
            var banksCount = random.Next(3, 7);
            Banks = RandomGenerators.GenerateBanks(Users, banksCount);
        }
    }
}