using System.Linq;

namespace Bank_of_Habib
{
    internal static class StartUp
    {
        internal static DataBaseMock DataBaseMock { get; private set; }
        internal static User CurrentUser { get; private set; }
        public static void Start()
        {
            bool exit = true;
            DataBaseMock = new DataBaseMock();

            foreach (var us in DataBaseMock.Users) 
            {
                Console.WriteLine($"Логин: {us.Login} Пароль: {us.Password}");
            }

            

            Console.Write("Введите логин: ");
            var login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            var pass = Console.ReadLine();

            var u = DataBaseMock.Users.FirstOrDefault(t => t.Login == login && t.Password == pass);
            if (u != null)
            {
                CurrentUser = u;
            }
            else
            {
                Console.WriteLine("такого нет");
            }
            Console.WriteLine($"{CurrentUser.Name}, Добро пожаловать в банковскую систему Хабибуллы!");
            while (exit)
            {
                Console.WriteLine("Выбери 1.2.3.4");
                if (int.TryParse(Console.ReadLine(), out int imput))
                {
                    switch (imput)
                    {
                        case 1: var bills = DataBaseMock.Banks.SelectMany(b => b.GetBills(CurrentUser)); Console.WriteLine($"Все счета: {string.Join('\n', bills)}"); break;
                        case 2: Console.WriteLine("т"); break;
                        case 3: Console.WriteLine("."); break;
                        case 4: Console.WriteLine("и"); exit = false; break;
                        default: Console.WriteLine("выбери из четырёх цифр"); break;
                    }

                }
                else
                {
                    Console.WriteLine("выбери из четырёх цифр");
                }
            }
        }
    }
}
