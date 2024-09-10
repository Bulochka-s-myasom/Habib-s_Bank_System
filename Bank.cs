using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class Bank
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Commission { get; set; }

        public Bank(string name, int id, int comiss) 
        {
            Name = name;
            Id = id;
            Commission = comiss;
        }
    }
}
