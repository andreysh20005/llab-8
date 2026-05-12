internal class Interface
{
    public static int InputNum(string message, bool IsDiscount = false)
    {
        int result=0;
        bool ok = false;
        while (!ok)
        {
            Console.WriteLine(message);
            ok = int.TryParse(Console.ReadLine(), out result);
            if (!ok)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз:");
            }
            else if (result<0)
            {
                Console.WriteLine("необходимо НАТУРАЛЬНОЕ число!");
                ok = false;
            }
            else if (IsDiscount && result > 100)
            {
                Console.WriteLine("Скидка не может быть больше 100!");
                ok = false;
            }
        }
        return result;
    }

    public static string InputString(string message)
    {
        string result;
        while (true)
        {
            Console.WriteLine(message);
            result = Console.ReadLine();
            if (result != null && result.Length >= 3)
                return result;

            Console.WriteLine("Ошибка: строка должна быть не короче 3-х символов.");
        }
    }
    public static bool InputBool(string message)
    {
        string result;
        while (true)
        {
            Console.WriteLine(message);
            result = Console.ReadLine();
            if (result == "y" || result == "Y")
                return true;
            if (result == "n" || result == "N")
                return false;
            Console.WriteLine("Ошибка: введите один символ без пробелов. допускаются только 'Y' или 'N'");
        }
    }

    private static void PrintData(List<Service> ListOfData)
    {
        int count = 0;
        Console.WriteLine("=====================");
        foreach (var data in ListOfData)
        {
            Console.WriteLine($"{count}: {data}");
        }
        Console.WriteLine("=====================");
    }
    public static bool menu()
    {
        Console.WriteLine(@"Выберите действие:
1 - вывести ВСЮ базу данных (ЦЕЛИКОМ)
2 - вывести ВСЕ активные услуги
3 - вывести ВСЕ услуги с данной скидкой
4 - вывести цену после скидки для ВСЕХ услуг 
5 - найти одну услугу по названию
6 - найти реальную цену для одной услуги по имени
7 - добавить услугу
8 - удалить услугу по имени

");
        string action = Console.ReadLine();
        switch (action)
        {
            case "q":
            {
                    return false;
                    break;
            }
            case "1":
            {
                PrintData(DataBase.ReadAll());
                break;
            }
                
            case "2":
            {
                PrintData(DataBase.GetAllActiveServices());
                break; 
            }
            case "3":
            {
                    int discount = InputNum("Введите искомую скидку (натуральное число от 0 до 100)");
                PrintData(DataBase.GetWithCurrentDiscount(discount));
                break;
            }
            case "4":
            {
                PrintData(DataBase.GetRealPrice());
                break;
            }
            case "5":
            {
                string name = InputString("введите название искомой услуги");
                Console.WriteLine(DataBase.FindByName(name));
                break;
            }
            case "6":
            {
                string name = InputString("введите название искомой услуги");
                Console.WriteLine(DataBase.GetServiceRealPrice(name));
                break;
            }
            case "7":
            {
                DataBase.AddService();
                break;
            }
            case "8":
            {
                string name = InputString("Введите название услуги для удаления:");
                DataBase.DeleateByName(name); 
                break;
            }
            default:
            {
                Console.WriteLine($"Неизвестная команда '{action}'!");
                break;
            }
        }
        return true;
    }

}

