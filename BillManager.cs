using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class BillManager : Bill
    {
        private Bill _bill;
        public BillManager(Bill bill)
        {
            _bill = bill;
        }

        public override string ToString()
        {             
            return $"Login: {_bill.Login}; ID: {_bill.Id}; Bank: {GetBankName()}; Value: {_bill.Value}";
        }

        public string GetBankName()
        {
            return HelperBD.DataBase.Banks.FirstOrDefault(b => b.Id == _bill.BankId)!.Name;
        }

        public decimal GetBankCommission()
        {
            return HelperBD.DataBase.Banks.FirstOrDefault(b => b.Id == _bill.BankId)!.Commission;
        }

        public bool SetBill(decimal money)
        {
            if (_bill.Value + money < 0)
            {
                Console.WriteLine($"Недостаточно денег на счёте");
                return false;
            }
            else
            {

                _bill.Value += money;                
                Console.WriteLine($"Операция выполена: {_bill.Value}");
                return true;
            }

        }
    }
}
