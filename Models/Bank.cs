using Newtonsoft.Json;

namespace Bank_of_Habib

{
    internal class Bank
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Commission { get; set; }        
        public List<Bill> _bills { get; set; }

        //public Bank(string name, int id, int comiss, List<Bill> bills)
        //{
        //    Name = name;
        //    Id = id;
        //    Commission = comiss;
        //    _bills = bills;

        //    foreach (Bill bill in _bills)
        //    {
        //        bill.Register(this);
        //    }
        //}

        

        //public void AccountRefill(User user, decimal money)
        //{
        //    var bill = _bills.First(l => l.Login == user.Login);
        //    bill.SetBill(money);
        //}

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
        //        if (bill.Bank.Name == targetBank.Name)
        //        {                    
        //            return CheckOnChangeBill(money, bill);
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Коммисия за перевод составит {bill.Bank.Commission}");
        //            money -= Math.Abs(money) / 100 * bill.Bank.Commission;
        //            return CheckOnChangeBill(money, bill);
        //        }
        //    }            
        //}

        //private static bool CheckOnChangeBill(decimal money, Bill bill)
        //{
        //    if (!bill.SetBill(money))
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }

    internal class BankManager : Bank
    {
        public BankManager(Bank bank) 
        {
            base._bills = bank._bills;
        }   
        public IEnumerable<Bill> GetBills(User user)
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
    }
}
