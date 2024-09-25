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
    }

    
}
