namespace Bank_of_Habib
{
    internal static class BankSystem
    {
        //internal static HelperBD? HelperBD { get; set; }
        internal static User? CurrentUser { get; set; }
        public static void Start()
        {   
            HelperBD.Init();
            bool logIn = true;
            while (logIn)
            {
                logIn = LogIn();           
            }
        }

        public static bool LogIn()
        {
            HelperBD.GetAllUsers();
            Console.Write("Введите логин или 1 для выхода: ");
            var login = Console.ReadLine();
            string? pass;
            if (login == "1")
            {                
                Console.WriteLine("Всего хорошего");
                return false;
            }
            else
            {
                Console.Write("Введите пароль: ");
                pass = Console.ReadLine();
                User user = HelperBD.GetCurrentUser(login, pass);
                if (user is not null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Пользователь найден");
                    Console.WriteLine();                    
                    CurrentUser = user;
                    PerformActionsWithAccount();
                    return true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Не верный логин или пароль");
                    Console.WriteLine();
                    return true;
                }
            }
        }

        public static void PerformActionsWithAccount()
        {
            Console.WriteLine($"{CurrentUser.Name}, Добро пожаловать в банковскую систему Хабибуллы!");            
            bool exit = false;
            while (!exit)
            {
                var bills = GetBillsOfUser(CurrentUser);
                Console.WriteLine();
                Console.WriteLine("Выбери 1.Проверить счета 2.Операции со счётом 3.Сменить пользователя");
                if (int.TryParse(Console.ReadLine(), out int imput))
                {
                    switch (imput)
                    {
                        case 1:
                            PrintBillsOfCurrentUser(bills);
                            Console.WriteLine("выбор 1");
                            break;
                        case 2:
                            BillOperation(bills); break;
                        case 3:
                            Console.WriteLine("Всего хорошего");
                            exit = true;                            
                            CurrentUser = null;
                            break;                        
                        default: 
                            Console.WriteLine("выбери из четырёх цифр"); 
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("выбери из четырёх цифр");
                }
            }
        }

        private static IEnumerable<Bill> GetBillsOfUser(User user)
        {
            if (user == null)
                return Enumerable.Empty<Bill>();
            return HelperBD.DataBase.Banks.SelectMany(b => new BankManager(b).GetBills(user));

            //HelperBD.DataBase.Banks.SelectMany(b => b.GetBills(user));
        }

        private static bool PrintBillsOfCurrentUser(IEnumerable<Bill> bills)
        {
            if (bills.Count() == 0)
            {
                Console.WriteLine("У пользователя нет счетов в банках");
                return false;
            }
            Console.WriteLine($"Все счета:");
            foreach (Bill bill in bills)
            {
                Console.WriteLine($"{string.Join('\n', new BillManager(bill))}");
            }
            return true;
        }

        private static void BillOperation(IEnumerable<Bill> bills)
        {

            bool exit = false;
            while (!exit)
            {
                bills = GetBillsOfUser(CurrentUser);
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
                                var currentBankName = Console.ReadLine();
                                if (bills.FirstOrDefault(bill => new BillManager(bill).GetBankName() == currentBankName) is null)
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
                                        new BankManager(HelperBD.DataBase.Banks.First(b => b.Name == currentBankName)).AccountRefill(CurrentUser, result);
                                        HelperBD.Save();
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
                                var sourceBankName = Console.ReadLine();
                                var sourceBank = HelperBD.DataBase.Banks.FirstOrDefault(b => b.Name == sourceBankName);
                                if (bills.FirstOrDefault(bill => new BillManager(bill).GetBankName() == sourceBankName) is null)
                                {
                                    Console.WriteLine("Банк не найден");
                                    break;
                                }
                                Console.Write("Введите логин получателя: ");
                                var targetUserLogin = Console.ReadLine();
                                var targetUser = HelperBD.DataBase.Users.FirstOrDefault(u => u.Login == targetUserLogin);
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
                                    var targetBank = HelperBD.DataBase.Banks.FirstOrDefault(b => b.Name == targetBankName);
                                    if (billsOfTargetUser.FirstOrDefault(bill => new BillManager(bill).GetBankName() == targetBankName) is null)
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

                                        if (new BankManager(HelperBD.DataBase.Banks.First(b => b.Name == sourceBankName)).WithdrawMoneyFromAccount(CurrentUser, result, targetBank))
                                        {
                                            new BankManager(HelperBD.DataBase.Banks.First(b => b.Name == targetBankName)).AccountRefill(targetUser, result);
                                            HelperBD.Save();
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
                            exit = true;
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

        private static bool HasValidDecimalImput(decimal imput, int maxCountAfterDot)
        {
            string strImput = imput.ToString();
            string[] imputStringArray = strImput.Split(new char[] { ',' }).ToArray();
            if (imputStringArray.Length == 2)
            {
                return imputStringArray[1].Length <= maxCountAfterDot;
            }
            return true;
        } 
    }
}












