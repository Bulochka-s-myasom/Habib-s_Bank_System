using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class Bill
    {
        public int Id { get; private set; }
        public string Login { get; private set; }
        public decimal Value { get; private set; }
        public Bank? Bank { get; private set; }

        public Bill(int id, string login, decimal value)
        {
            Id = id;
            Login = login;
            Value = value;
        }

        internal void Register(Bank bank)
        {
            Bank = bank;
        }

        public override string ToString()
        {
            return $"Login: {Login}; ID: {Id}; Bank: {Bank.Name}; Value: {Value}";
        }

        public bool SetBill(decimal money)
        {
            if (Value + money < 0)
            {
                Console.WriteLine($"Недостаточно денег на счёте");  
                return false;
            }
            else
            {
                Value += money;
                Console.WriteLine($"Операция выполена: {Value}");  
                return true;
            }
            
        }        
    }
}
