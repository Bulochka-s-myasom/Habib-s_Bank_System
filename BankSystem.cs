using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Bank_of_Habib
{
    internal static class BankSystem
    {
        //internal static HelperBD? HelperBD { get; set; }
        internal static User? CurrentUser { get; set; }
        public static void Start()
        {
            bool logIn = true;
            while (logIn)
            {
                Console.WriteLine("Выберите действие: 1. Войти в аккаунт 2. Зарегистрироваться 3. Выход");
                Console.Write("Ввод: ");
                if (int.TryParse(Console.ReadLine(), out int imput))
                {
                    switch (imput)
                    {
                        case 1:
                            logIn = LogIn();
                            break;
                        case 2:                            
                            CreateUser();
                            break;
                        case 3:
                            logIn = false;
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

        public static bool LogIn()
        {
            HelperBD.GetAllUsers();
            Console.Write("Введите логин или 1 для выхода: ");
            var login = Console.ReadLine();
            string? pass;
            if (login == "1")
            {
                Console.WriteLine("Всего хорошего");
                return true;
                
            }
            else
            {
                Console.Write("Введите пароль: ");
                pass = Console.ReadLine();
                User user = HelperBD.GetCurrentUser(login!, pass!);
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
            Console.WriteLine($"{CurrentUser!.Name}, Добро пожаловать в банковскую систему Хаби - Рубова!");
            bool exit = false;
            while (!exit)
            {
                var bills = GetBillsOfUser(CurrentUser!);
                Console.WriteLine();
                Console.WriteLine("Выбери 1.Проверить счета 2.Операции со счётом 3.Сменить пользователя");
                Console.Write("Ввод: ");
                if (int.TryParse(Console.ReadLine(), out int imput))
                {
                    switch (imput)
                    {
                        case 1:
                            PrintBillsOfUser(bills, CurrentUser);                            
                            break;
                        case 2:
                            BillOperation(bills); break;
                        case 3:
                            Console.WriteLine();
                            Console.WriteLine($"{CurrentUser!.Name}, Всего хорошего!");
                            Console.WriteLine();
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
        }

        private static bool PrintBillsOfUser(IEnumerable<Bill> bills, User user)
        {
            Console.WriteLine();
            if (bills.Count() == 0)
            {
                Console.WriteLine("У пользователя нет счетов в банках");
                return false;
            }
            Console.WriteLine($"{user.Name}, вам доступны следующие счета:");
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
                bills = GetBillsOfUser(CurrentUser!);
                Console.WriteLine();
                Console.WriteLine("Выбери 1.Открыть счёт в банке 2.Снять 3.Пополнить 4.Перевести 5.Назад");
                Console.Write("Ввод: ");
                if (int.TryParse(Console.ReadLine(), out int imput))
                {
                    switch (imput)
                    {
                        case 1:
                            {                                
                                CreateBill(CurrentUser!, bills);
                            }
                            break;
                        case 2:
                            {
                                Withdraw(bills);
                            }                            
                            break;
                        case 3:
                            {
                                Deposit(bills);
                            }
                            break;
                        case 4:
                            {
                                TransferMoney(bills);
                            }
                            break;
                        case 5:
                            {
                                Console.WriteLine();
                                exit = true;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine();
                                Console.WriteLine("выбери из четырёх цифр");
                                break;
                            }
                    }

                }
                else
                {
                    Console.WriteLine();
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

        private static void CreateUser()
        {
            Console.WriteLine();
            Console.WriteLine("Добро пожаловать в банковскую систему Хаби - Рубова!");
            Console.WriteLine();
            Console.WriteLine("Процесс регитсрации займёт меньше минуты.");
            Console.WriteLine("Внимание! При вводе данных необходимо использовать только русские символы!");
            Console.WriteLine();
            Console.Write("Введите ваше имя: ");
            string[] firstName = Console.ReadLine().Split(" ").ToArray();            
            Console.Write("Введите вашу фамилию: ");
            string[] lastName = Console.ReadLine().Split(" ").ToArray();

            if (firstName.Length > 1 || lastName.Length > 1)
            {
                Console.WriteLine();
                Console.WriteLine("Необходимо вводить имя/фамилию без пробелов");
                Console.WriteLine();
            }
            else
            {
                if (IsRussian(firstName[0]) && IsRussian(lastName[0]))
                {
                    string name = $"{GetCorrectName(firstName)} {GetCorrectName(lastName)}";
                    string login = Translit.Transliting(firstName[0] + lastName[0]).ToLower();
                    string pass = new Random().Next(1000, 9999).ToString();
                    if (HelperBD.AddUser(name, login, pass))
                    {
                        HelperBD.Save();
                        Console.WriteLine();
                        Console.WriteLine($"Пользователь {name} зарегитстрирован.");
                        Console.WriteLine($"Ваш логин: {login}");
                        Console.WriteLine($"Ваш пароль: {pass}");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Такой пользователь уже существует.");
                        Console.WriteLine();
                    }                
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Имя/фамилию необходимо ввести используя только русские символы");
                    Console.WriteLine();
                }
            }
        }

        static bool IsRussian(string input)
        {
            return Regex.IsMatch(input, @"^[а-яА-ЯёЁ]+$");
        }

        static string GetCorrectName(string[] nameArr)
        {
            string name = nameArr[0].ToLower();
            name = char.ToUpper(name[0]) + name.Substring(1);
            return name;
        }

        static void Withdraw(IEnumerable<Bill> bills)
        {
            if (PrintBillsOfUser(bills, CurrentUser))
            {
                Console.WriteLine();
                Console.WriteLine("Введите название банка, со счёта которого вы желаете снять средства");
                Console.Write("Название банка: ");
                var sourceBankName = Console.ReadLine();
                var sourceBank = HelperBD.DataBase.Banks.FirstOrDefault(b => b.Name == sourceBankName);
                if (bills.FirstOrDefault(bill => new BillManager(bill).GetBankName() == sourceBankName) is null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Банк не найден");
                }
                else
                {
                    Console.Write("Введите сумму: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal result))
                    {
                        if (result < 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Сумма должна быть больше нуля!");                           
                        }
                        else if (!HasValidDecimalImput(result, 2))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Сумма должна иметь не более 2х знаков после запятой!");                            
                        }
                        else if (new BankManager(HelperBD.DataBase.Banks.First(b => b.Name == sourceBankName)).WithdrawMoneyFromAccount(CurrentUser!, result, null))
                        {
                            HelperBD.Save();
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Сумма должна быть числом!");
                    }
                }
            }            
        }

        static void Deposit(IEnumerable<Bill> bills)
        {
            if (PrintBillsOfUser(bills, CurrentUser))
            {                
                Console.WriteLine();
                Console.Write("Введите название банка, счёт которого необходимо пополнить: ");
                var currentBankName = Console.ReadLine();
                if (bills.FirstOrDefault(bill => new BillManager(bill).GetBankName() == currentBankName) is null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Банк не найден");                    
                }
                else
                {
                    Console.Write("Введите сумму пополнения: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal result))
                    {
                        if (result < 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Сумма пополнения должна быть больше нуля!");                            
                        }
                        else if (!HasValidDecimalImput(result, 2))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Сумма должна иметь не более 2х знаков после запятой!");
                        }
                        else
                        {
                            new BankManager(HelperBD.DataBase.Banks.First(b => b.Name == currentBankName)).AccountRefill(CurrentUser!, result);
                            HelperBD.Save();
                        }                        
                    }
                    else
                    {
                        Console.WriteLine("Сумма должна быть числом");
                    }
                }
            }
        }

        static void TransferMoney(IEnumerable<Bill> bills)
        {
            if (PrintBillsOfUser(bills, CurrentUser))
            {
                Console.WriteLine();
                Console.WriteLine("Введите название банка со чёта которого будет осуществён перевод");
                Console.Write("Название банка: ");
                var sourceBankName = Console.ReadLine();
                var sourceBank = HelperBD.DataBase.Banks.FirstOrDefault(b => b.Name == sourceBankName);
                if (bills.FirstOrDefault(bill => new BillManager(bill).GetBankName() == sourceBankName) is null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Банк не найден");                    
                }
                else
                {
                    Console.Write("Введите логин получателя: ");
                    var targetUserLogin = Console.ReadLine();
                    var targetUser = HelperBD.DataBase.Users.FirstOrDefault(u => u.Login == targetUserLogin);
                    if (targetUser is null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Получатель не найден");
                        
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Получатель {targetUser.Name}");                        
                        var billsOfTargetUser = GetBillsOfUser(targetUser);
                        if (PrintBillsOfUser(billsOfTargetUser, targetUser))
                        {
                            Console.WriteLine();
                            Console.Write("Введите название банка получателя: ");
                            var targetBankName = Console.ReadLine();
                            var targetBank = HelperBD.DataBase.Banks.FirstOrDefault(b => b.Name == targetBankName);
                            if (billsOfTargetUser.FirstOrDefault(bill => new BillManager(bill).GetBankName() == targetBankName) is null)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Банк не найден");                               
                            }
                            else
                            {
                                Console.Write("Введите сумму перевода: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                                {
                                    if (result < 0)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Сумма перевода должна быть больше нуля");                                        
                                    }
                                    else if (!HasValidDecimalImput(result, 2))
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Сумма должна иметь не более 2х знаков после запятой!");
                                    }
                                    else if (new BankManager(HelperBD.DataBase.Banks.First(b => b.Name == sourceBankName)).WithdrawMoneyFromAccount(CurrentUser!, result, targetBank))
                                    {
                                        new BankManager(HelperBD.DataBase.Banks.First(b => b.Name == targetBankName)).AccountRefill(targetUser, result);
                                        HelperBD.Save();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Сумма должна быть числом");
                                }
                            }
                        }
                    }
                }
            }
        }

        static void CreateBill(User user, IEnumerable<Bill> bills)
        {
            HelperBD.GetAllBanks();            
            Console.WriteLine("Введите название банка, в котором желаете открыть счёт");
            Console.Write("Название банка: ");
            var targetBankName = Console.ReadLine();
            var targetBank = HelperBD.DataBase.Banks.FirstOrDefault(b => new BankManager(b).GetName() == targetBankName);
            if (targetBank is null)
            {
                Console.WriteLine();
                Console.WriteLine("Банк не найден.");
            } 
            else
            {
                if (new BankManager(targetBank).AddBill(user, targetBankName, bills))
                {
                    HelperBD.Save();
                    Console.WriteLine();
                    Console.WriteLine($"Счёт в банке {targetBankName} успешно открыт!");
                    PrintBillsOfUser(bills, CurrentUser);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("У вас уже есть счёт в данном банке.");
                    Console.WriteLine();
                }                
            }
        }
    }
}












