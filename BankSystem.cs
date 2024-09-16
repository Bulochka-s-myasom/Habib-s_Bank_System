namespace Bank_of_Habib
{
    internal static class BankSystem
    {
        internal static DataBaseMock DataBaseMock { get; private set; }
        internal static User? CurrentUser { get; private set; }
        public static void Start()
        {

            DataBaseMock = new DataBaseMock();

            bool checkLogin = false;

            while (!checkLogin)
            {
                Console.WriteLine("Доступные пользователи:");
                foreach (var us in DataBaseMock.Users)
                {
                    Console.WriteLine($"Логин: {us.Login} Пароль: {us.Password}");
                }

                Console.WriteLine();
                Console.Write("Введите логин или 1 для выхода: ");
                var login = Console.ReadLine();
                string? pass;
                if (login == "1")
                {
                    checkLogin = true;
                    Console.WriteLine("Всего хорошего");
                    break;
                }
                else
                {
                    Console.Write("Введите пароль: ");
                    pass = Console.ReadLine();
                }
                foreach (var us in DataBaseMock.Users)
                {
                    if (login == us.Login && us.Password == pass)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Пользователь найден");
                        Console.WriteLine();
                        checkLogin = true;
                        CurrentUser = us;
                        break;
                    }
                }

                if (CurrentUser == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Не верный логин или пароль");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"{CurrentUser.Name}, Добро пожаловать в банковскую систему Хабибуллы!");
                    bool exit = true;
                    while (exit)
                    {
                        Console.WriteLine("Выбери 1.Проверить счета 2.Операции со счётом 3.Сменить пользователя 4.Выход");
                        if (int.TryParse(Console.ReadLine(), out int imput))
                        {
                            switch (imput)
                            {
                                case 1:
                                    var bills = DataBaseMock.Banks.SelectMany(b => b.GetBills(CurrentUser));
                                    Console.WriteLine($"Все счета:");
                                    Console.WriteLine($"{string.Join('\n', bills)}");
                                    break;
                                case 2: BillOperation(); break;
                                case 3:
                                    Console.WriteLine("Всего хорошего");
                                    exit = false;
                                    checkLogin = false;
                                    CurrentUser = null;
                                    break;
                                case 4:
                                    Console.WriteLine("");
                                    exit = false;
                                    break;
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



        //var u = DataBaseMock.Users.FirstOrDefault(t => t.Login == login);

        //if (u != null)
        //{
        //    Console.WriteLine(
        //    CurrentUser = u;
        //}
        //else
        //{
        //    Console.WriteLine("такого нет");
        //}

        private static void BillOperation()
        {
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("Выбери 1.Проверить счета 2. 3.Перевести 4.Назад");
                if (int.TryParse(Console.ReadLine(), out int imput))
                {
                    switch (imput)
                    {
                        case 1:
                            var bills = DataBaseMock.Banks.SelectMany(b => b.GetBills(CurrentUser));
                            Console.WriteLine($"Все счета:");
                            Console.WriteLine($"{string.Join('\n', bills)}");
                            break;
                        case 2: 
                            Console.WriteLine("Когда нибудь тут будет реализация"); 
                            break;
                        case 3:
                            Console.WriteLine("Когда нибудь тут тоже будет реализация");
                            break;
                            
                        case 4:
                            Console.WriteLine();
                            exit = false;
                            break;
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

