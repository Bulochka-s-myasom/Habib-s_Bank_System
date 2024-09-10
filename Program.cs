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
                    Console.WriteLine($"{dataBaseMock.Banks[i].Id}. {dataBaseMock.Banks[i].Name}" +
                        $" {dataBaseMock.Banks[i].Commission}%");
                }

                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
