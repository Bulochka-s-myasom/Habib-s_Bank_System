using Newtonsoft.Json;

namespace Bank_of_Habib

{
    internal class Bank
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Commission { get; set; }        
        public List<Bill> _bills { get; set; }
        
    }    
}
