﻿using System;
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
        private List<Bill> _bills { get; }

        public Bank(string name, int id, int comiss, List<Bill> bills) 
        {
            Name = name;
            Id = id;
            Commission = comiss;
            _bills = bills;

            foreach (Bill bill in _bills)
            {
                bill.Register(this);
            }
        }

        public IEnumerable <Bill> GetBills(User user) 
        {
            List<Bill> result = new List<Bill>();
            foreach (var b in _bills)
            {
                if (b.Login == user.Login)
                    //yield return b;
                    result.Add(b);
            }
            return result;
            //return _bills.Where(b => b.Login == user.Login);
        }

        public void TransferMoney(User user, decimal money)
        {
            var bill = _bills.Where(l => l.Login == user.Login).ToList();
            bill[0].SetBill(money); 
        }
    }
}
