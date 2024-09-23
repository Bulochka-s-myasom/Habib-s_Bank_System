using Bank_of_Habib;
using Newtonsoft.Json;

namespace Bank_of_Khabib
{

    

        internal class Program
        {
            static void Main(string[] args)
            {


            //int m = int.Parse(Console.ReadLine());

            //for (int l = 0; l < m; l++)
            //{
            //    DataBaseMock dataBaseMock = new DataBaseMock();

            //    for (int i = 0; i < dataBaseMock.Banks.Count; i++)
            //    {
            //        Console.WriteLine($"Банк ID: {dataBaseMock.Banks[i].Id} Название: {dataBaseMock.Banks[i].Name}" +
            //            $" Комиссия: {dataBaseMock.Banks[i].Commission}%");

            //        for (int j = 0; j < dataBaseMock.Banks[i]._bills.Count; j++)
            //        {
            //            Console.WriteLine($"ID счёта: {dataBaseMock.Banks[i]._bills[j].ID} Login: {dataBaseMock.Banks[i]._bills[j].Login}" +
            //            $" На счету: {dataBaseMock.Banks[i]._bills[j].Value}");
            //        }

            //        Console.WriteLine();
            //    }               

            //    Console.WriteLine();

            //    for (int i = 0; i < dataBaseMock.Users.Count; i++)
            //    {
            //        Console.WriteLine($"Имя: {dataBaseMock.Users[i].Name} Логин: {dataBaseMock.Users[i].Login} Пароль: {dataBaseMock.Users[i].Password}");
            //        Console.WriteLine();
            //        //Console.WriteLine();
            //    }

            //    Console.WriteLine();
            //}
            //Console.ReadLine();

            //BankSystem.Start();

            DataBaseMock dataBaseMock = new DataBaseMock();
            
            string serialazed = JsonConvert.SerializeObject(dataBaseMock, Formatting.Indented);
            Console.WriteLine(serialazed);
            //Directory.CreateDirectory("TestJson");
            //File.Create("F:\\TestJson\\dataBaseMock.json");
            File.WriteAllText("dataBaseMock.json", serialazed);

            

            }
        }
    }
