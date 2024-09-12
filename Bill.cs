using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class Bill
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public decimal Value { get; set; }
        public Bank? Bank { get; private set; }

        public Bill(int n, string login, decimal value)
        {
            ID = n;
            Login = login;
            Value = value;
        }

        internal void Register(Bank bank)
        {
            Bank = bank;
        }

        public override string ToString()
        {
            return $"Login: {Login}; ID: {ID}; Bank: {Bank.Name}; Value: {Value}";
        }
    }
}
