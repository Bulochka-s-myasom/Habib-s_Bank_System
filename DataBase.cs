﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class DataBase
    {
        public List<Bank> Banks { get; set; }
        public List<User> Users { get; set; }
        

        //public DataBaseMock()
        //{
        //    Random random = new Random();

        //    var userCount = random.Next(5, 10);
        //    Users = RandomGenerators.GenerateUsers(userCount);
        //    var banksCount = random.Next(5, 7);
        //    Banks = RandomGenerators.GenerateBanks(Users, banksCount);

        //}
    }    
}