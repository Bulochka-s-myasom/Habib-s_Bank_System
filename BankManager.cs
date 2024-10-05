namespace Bank_of_Habib
{
    internal class BankManager
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
            return _bank._bills.Where(b => b.Login == user.Login);
        }

        public void AccountRefill(User user, decimal money)
        {
            var bill = new BillManager(_bank._bills.First(l => l.Login == user.Login));
            bill.SetBill(money);
        }

        public bool WithdrawMoneyFromAccount(User user, decimal money, Bank? targetBank = null)
        {
            if (targetBank is null)
            {
                money = -money;
                var bill = _bank._bills.First(l => l.Login == user.Login);
                return CheckOnChangeBill(money, bill);
            }
            else
            {
                money = -money;
                var bill = _bank._bills.First(l => l.Login == user.Login);
                if (new BillManager(bill).GetBankName() == targetBank.Name)
                {
                    return CheckOnChangeBill(money, bill);
                }
                else
                {
                    var commiss = new BillManager(bill).GetBankCommission();
                    Console.WriteLine($"Коммисия за перевод составит {new BillManager(bill).GetBankCommission()}%");
                    money -= Math.Abs(money) / 100 * commiss;
                    return CheckOnChangeBill(money, bill);
                }
            }
        }

        private static bool CheckOnChangeBill(decimal money, Bill bill)
        {
            if (!new BillManager(bill).SetBill(money))
            {
                return false;
            }
            return true;
        }

        public bool AddBill(User user, string targetBankName, IEnumerable<Bill> bills)
        {
            if (bills.FirstOrDefault(bill => new BillManager(bill).GetBankName() == targetBankName) is null)
            {
                Bill bill = new Bill();
                bill.Login = user.Login;
                bill.Value = 0;
                bill.BankId = _bank.Id;
                bill.Id = _bank._bills.Count + 1;
                _bank._bills.Add(bill);
                return true;
            }
            return false;
        }
    }
}
