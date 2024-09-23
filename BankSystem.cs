namespace Bank_of_Habib
{
    internal static class BankSystem
    {
        internal static DataBaseMock? DataBaseMock { get; private set; }
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
                    var bills = GetBillsOfUser(CurrentUser);
                    bool exit = true;
                    while (exit)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Выбери 1.Проверить счета 2.Операции со счётом 3.Сменить пользователя 4.Выход");
                        if (int.TryParse(Console.ReadLine(), out int imput))
                        {
                            switch (imput)
                            {
                                case 1:
                                    PrintBillsOfCurrentUser(bills);
                                    break;
                                case 2: BillOperation(bills); break;
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
        private static void BillOperation(IEnumerable<Bill> bills)
        {
            
            bool exit = true;
            while (exit)
            {
                Console.WriteLine();
                Console.WriteLine("Выбери 1.Проверить счета 2.Пополнить 3.Перевести 4.Назад");
                if (int.TryParse(Console.ReadLine(), out int imput))
                {
                    switch (imput)
                    {
                        case 1:                            
                                PrintBillsOfCurrentUser(bills);                            
                            break;
                        case 2:
                            {
                                if (!PrintBillsOfCurrentUser(bills))
                                {
                                    break;
                                }
                                Console.Write("Введите название банка: ");
                                var currentBank = Console.ReadLine();
                                if (bills.FirstOrDefault(n => n.Bank.Name == currentBank) is null)
                                {
                                    Console.WriteLine("Банк не найден");
                                    break;
                                }
                                else
                                {
                                    Console.Write("Введите сумму пополнения: ");
                                    if (decimal.TryParse(Console.ReadLine(), out decimal result))
                                    {
                                        if (result < 0)
                                        {
                                            Console.WriteLine("Сумма пополнения должна быть больше нуля");
                                            break;
                                        }
                                        else if (!HasValidDecimalImput(result, 2))
                                        {
                                            Console.WriteLine("Сумма пополнения должна иметь не более 2х знаков");
                                            Console.WriteLine("после запятой");
                                            break;
                                        }

                                        DataBaseMock.Banks.First(b => b.Name == currentBank).AccountRefill(CurrentUser, result);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Сумма должна быть числом");
                                    }
                                }
                            }
                            break;
                        case 3:
                            {
                                if (!PrintBillsOfCurrentUser(bills))
                                {
                                    break;
                                }
                                Console.WriteLine("Введите название банка со чёта которого будет осуществён перевод: ");
                                var sourceBank = Console.ReadLine();
                                if (bills.First(n => n.Bank.Name == sourceBank) is null)
                                {
                                    Console.WriteLine("Банк не найден");
                                    break;
                                }
                                Console.Write("Введите логин получателя: ");
                                var targetUserLogin = Console.ReadLine();
                                var targetUser = DataBaseMock.Users.First(u => u.Login == targetUserLogin);
                                if (targetUser is null)
                                {
                                    Console.WriteLine("Получатель не найден");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Получатель найден");
                                    Console.WriteLine();
                                    var billsOfTargetUser = GetBillsOfUser(targetUser);
                                    if (!PrintBillsOfCurrentUser(billsOfTargetUser))
                                    {
                                        break;
                                    }
                                    Console.WriteLine();
                                    Console.Write("Введите название банка получателя: ");
                                    var targetBankName = Console.ReadLine();
                                    var targetBank = DataBaseMock.Banks.First(b => b.Name == targetBankName);
                                    if (billsOfTargetUser.First(n => n.Bank.Name == targetBankName) is null)
                                    {
                                        Console.WriteLine("Банк не найден");
                                        break;
                                    }
                                    Console.Write("Введите сумму перевода: ");
                                    if (decimal.TryParse(Console.ReadLine(), out decimal result))
                                    {
                                        if (result < 0)
                                        {
                                            Console.WriteLine("Сумма перевода должна быть больше нуля");
                                            break;
                                        }
                                        else if (!HasValidDecimalImput(result, 2))
                                        {
                                            Console.WriteLine("Сумма перевода должна иметь не более 2х знаков");
                                            Console.WriteLine("после запятой");
                                            break;
                                        }

                                        if (DataBaseMock.Banks.First(b => b.Name == sourceBank).WithdrawMoneyFromAccount(CurrentUser, result, targetBank))
                                        {
                                            DataBaseMock.Banks.First(b => b.Name == targetBankName).AccountRefill(targetUser, result);
                                        }                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("Сумма должна быть числом");
                                    }
                                }
                            }
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

        private static bool PrintBillsOfCurrentUser(IEnumerable<Bill> bills)
        {
            if (bills.Count() == 0)
            {
                Console.WriteLine("У пользователя нет счетов в банках");
                return false;
            }
            Console.WriteLine($"Все счета:");
            Console.WriteLine($"{string.Join('\n', bills)}");            
            return true;
        }

        private static IEnumerable<Bill> GetBillsOfUser(User user)
        {
            if (user == null)
                return Enumerable.Empty<Bill>();
            return DataBaseMock.Banks.SelectMany(b => b.GetBills(user));
        }

        private static bool HasValidDecimalImput(decimal imput, int maxCountAfterDot)
        {
            string strImput = imput.ToString();
            string[] imputStringArray = strImput.Split(new char[] {','}).ToArray();
            if (imputStringArray.Length == 2)
            {
                 return imputStringArray[1].Length <= maxCountAfterDot;
            }
            return true;
        }

    }
}

