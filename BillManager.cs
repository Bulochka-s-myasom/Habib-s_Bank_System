using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class BillManager : Bill
    {
        public BillManager(Bill bill)
        {
            base.Id = bill.Id;
            base.Login = bill.Login;
            base.Value = bill.Value;
            base.BankId = bill.BankId;
        }

        public override string ToString()
        {             
            return $"Login: {Login}; ID: {Id}; Bank: {GetBankName()}; Value: {Value}";
        }

        public string GetBankName()
        {
            return new HelperBD().DataBase.Banks.FirstOrDefault(b => b.Id == BankId).Name;
        }


        public void SetValue(decimal n)
        {
            base.Value += n;
        }

        public bool SetBill(decimal money)
        {
            if (base.Value + money < 0)
            {
                Console.WriteLine($"Недостаточно денег на счёте");
                return false;
            }
            else
            {
                
                base.Value += money;
                HelperBD bd = new HelperBD();
                bd.Save();
                Console.WriteLine($"Операция выполена: {base.Value}");
                return true;
            }

        }
    }
}
