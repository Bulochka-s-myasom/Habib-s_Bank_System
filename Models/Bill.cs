using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class Bill
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public decimal Value { get; set; }        
        public int BankId { get; set; }

        //public Bill(int id, string login, decimal value)
        //{
        //    Id = id;
        //    Login = login;
        //    Value = value;
        //}

        //internal void Register(Bank bank)
        //{
        //    Bank = bank;
        //}

        //public override string ToString()
        //{
        //    return $"Login: {Login}; ID: {Id}; Bank: {Bank.Name}; Value: {Value}";
        //}

        //public bool SetBill(decimal money)
        //{
        //    if (Value + money < 0)
        //    {
        //        Console.WriteLine($"Недостаточно денег на счёте");
        //        return false;
        //    }
        //    else
        //    {
        //        Value += money;
        //        Console.WriteLine($"Операция выполена: {Value}");
        //        return true;
        //    }

        //}
    }

    internal class BillManager : Bill
    {
        public BillManager(Bill bill) 
        {
            base.Id = bill.Id;
        }


        public void SetValue(decimal n)
        {
            base.Value += n;
        }
    }
}
