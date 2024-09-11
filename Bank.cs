using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class Bank
    {
        public string Name { get; }
        public int Id { get; }        
        public int Commission { get; }
        public List<Bill> _bills { get; } = new List<Bill>();

        public Bank(string name, int id, int comiss, List<Bill> bills) 
        {
            Name = name;
            Id = id;
            Commission = comiss;
            _bills = bills;
        }
    }
}
