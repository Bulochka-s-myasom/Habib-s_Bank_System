using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_of_Habib
{
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
