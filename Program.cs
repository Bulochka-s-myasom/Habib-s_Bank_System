using Bank_of_Habib;

namespace Bank_of_Khabib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            int m = int.Parse(Console.ReadLine());

            for (int l = 0; l < m; l++)
            {
                DataBaseMock dataBaseMock = new DataBaseMock();

                for (int i = 0; i < dataBaseMock.Banks.Count; i++)
                {
                    Console.WriteLine($"Банк ID: {dataBaseMock.Banks[i].Id} Название: {dataBaseMock.Banks[i].Name}" +
                        $" Комиссия: {dataBaseMock.Banks[i].Commission}%");
                }               

                Console.WriteLine();

                for (int i = 0; i < dataBaseMock.Users.Count; i++)
                {
                    Console.WriteLine($"Имя: {dataBaseMock.Users[i].Name} Логин: {dataBaseMock.Users[i].Login} Пароль: {dataBaseMock.Users[i].Password}");
                    Console.WriteLine();
                    //Console.WriteLine();
                }

                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
