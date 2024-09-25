using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
    internal class BankManager : Bank
    {
        private Bank _bank;

        public BankManager(Bank bank)
        {
            _bank = bank;
        }

        public string GetName()
        {
            return _bank.Name;
        }
        public IEnumerable<Bill> GetBills(User user)
        {
            List<Bill> result = new List<Bill>();
            foreach (var b in _bank._bills)
            {
                if (b.Login == user.Login)
                    //yield return b;
                    result.Add(b);
            }
            return result;
            //return _bills.Where(b => b.Login == user.Login);
        }

        public void AccountRefill(User user, decimal money)
        {
            var bill = new BillManager(_bank._bills.First(l => l.Login == user.Login));
            bill.SetBill(money);
            //bill.Value += money;
        }

        //public bool WithdrawMoneyFromAccount(User user, decimal money, Bank? targetBank = null)
        //{
        //    if (targetBank is null)
        //    {
        //        money = -money;
        //        var bill = _bills.First(l => l.Login == user.Login);
        //        return CheckOnChangeBill(money, bill);
        //    }
        //    else
        //    {
        //        money = -money;
        //        var bill = _bills.First(l => l.Login == user.Login);
                //if (bill.Bank.Name == targetBank.Name)
                //{
                //    return CheckOnChangeBill(money, bill);
                //}
                //else
                //{
                //    Console.WriteLine($"Коммисия за перевод составит {bill.Bank.Commission}");
                //    money -= Math.Abs(money) / 100 * bill.Bank.Commission;
                //    return CheckOnChangeBill(money, bill);
                //}
            }
        }

        //private static bool CheckOnChangeBill(decimal money, Bill bill)
        //{
        //    if (!bill.SetBill(money))
        //    {
        //        return false;
        //    }
        //    return true;
        //}
//    }
//}
