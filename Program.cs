using System;
using System.IO;
using System.Collections.Generic;

namespace Autoparts
{
    class Suppliers //класс Поставщики
    {
        public int Supplier; //поля класса Поставщики
        public string Name;
        public string Address;
        public int PhoneNumber;
        public Suppliers(int supplier, string name, string address, int phonenumber) //конструктор с параметрами класса Поставщики
        {
            Supplier = supplier;
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
        }
        public void SupplierInfo() //метод получения информации о экземпляре класса
        {
            Console.WriteLine("Номер поставщика: {0}", Supplier);
            Console.WriteLine("Название поставщика: {0}", Name);
            Console.WriteLine("Адрес поставщика: {0}", Address);
            Console.WriteLine("Номер телефона поставщика: {0}", PhoneNumber);
        }
        public void ChangeInformation(List<Suppliers> suppliers = null, Suppliers supplier = null, int index = 0) //метод провери и изменения информации о полях экземпляра класса
        {
            try
            {
                switch (Console.ReadKey(true).Key) //в зависимости от нажатой клавиши изменяется соответствующее поле экземпляра класса
                {
                    case (ConsoleKey)'1':
                        Console.Write("Введите номер поставщика: ");
                        int newSupplier = int.Parse(Console.ReadLine());
                        List<Suppliers> all_suppliers = Program.GetAllSuppliers();
                        if (all_suppliers.Find(spl => spl.Supplier == newSupplier) != null) // проверка есть ли номер поставщика в списке
                        {
                            Console.WriteLine("Поставщик с данным номером уже существует! Введите другой номер!");
                        } else
                        {
                            Supplier = newSupplier;
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            SupplierInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        }
                        ChangeInformation(suppliers, supplier, index);
                        break;
                    case (ConsoleKey)'2':
                        Console.Write("Введите название поставщика: ");
                        string newName = Console.ReadLine();
                        if (newName.Length == 0) //проверка на пустую строку
                        {
                            Console.WriteLine("Поле Название не может быть пустым!");
                        }
                        else
                        {
                            Name = newName;
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            SupplierInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        }
                        ChangeInformation(suppliers, supplier, index);
                        break;
                    case (ConsoleKey)'3':
                        Console.Write("Введите адрес поставщика: ");
                        string newAddress = Console.ReadLine();
                        if (newAddress.Length == 0) //проверка на пустую строку
                        {
                            Console.WriteLine("Поле Адрес не может быть пустым!");
                        }
                        else
                        {
                            Address = newAddress;
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            SupplierInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        }
                        ChangeInformation(suppliers, supplier, index);
                        break;
                    case (ConsoleKey)'4':
                        Console.Write("Введите телефон поставщика: ");
                        PhoneNumber = int.Parse(Console.ReadLine());
                        Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        SupplierInfo();
                        Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        ChangeInformation(suppliers, supplier, index);
                        break;
                    case ConsoleKey.Escape: // возврат в меню поставщиков при нажатии клавиши Escape
                        Program.ShowSuppliersMenu();
                        break;
                    case ConsoleKey.Enter: // запись информации в базу данных при нажатии клавиши Enter
                        if (suppliers != null)  // если информация редактиурется, то база данных перезаписывается
                        {
                            suppliers[index] = supplier;
                            StreamWriter sw = new StreamWriter("suppliers.txt", false);
                            sw.Close();
                            foreach (Suppliers spl in suppliers)
                            {
                                spl.WriteInDatabase();
                            }
                        }
                        else // иначе записывается в конец файла
                        {
                            WriteInDatabase();
                        }
                        Program.ShowSuppliersMenu();
                        break;
                    default:
                        AllInfo();
                        break;
                }
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз!");
                ChangeInformation(suppliers, supplier, index);
            }
        }
        public void InfoTemplate() // шаблон для проверки введённых данных для класса Поставщики
        {
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
            Console.WriteLine("|                              ВВЕДЁННАЯ ИНФОРМАЦИЯ                         |");
            Console.WriteLine("|———————————————————————————————————————————————————————————————————————————|");
            Console.WriteLine("| Проверьте введённые данные, если хотите изменить информацию, то нажмите   |");
            Console.WriteLine("| соответствующую клавишу. Чтобы сохранить введённые данные нажмите Enter.  |");
            Console.WriteLine("| Если хотите вернуться назад без сохранения данных нажмите ESC.            |");
            Console.WriteLine("|———————————————————————————————————————————————————————————————————————————|");
            Console.WriteLine("| 1 - Номер     |     2 - Название    |    3 - Адрес     |    4 - Телефон   |");
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
        }
        public void AllInfo() // метод проверки введённой информации
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставщики - Добавить поставщика - Проверка введённой иноформации";
            InfoTemplate();
            SupplierInfo();
            ChangeInformation();
        }
        public void WriteInDatabase() // метод записи экземпляра класса в базу данных
        {
            StreamWriter sw = new StreamWriter("suppliers.txt", true);
            sw.WriteLine(Supplier);
            sw.WriteLine(Name);
            sw.WriteLine(Address);
            sw.WriteLine(PhoneNumber);
            sw.WriteLine("");
            sw.Close();
        }
    }

    class Details // класс Детали
    {
        public string Name; // поля класса Детали
        public int Article;
        public double Price;
        public string Note;
        public Details(string name, int article, double price, string note) // конструктор с параметрами класса Детали
        {
            Name = name;
            Article = article;
            Price = price;
            Note = note;
        }
        public void DetailsInfo() // вывод информации об экхемпляре класса
        {
            Console.WriteLine("Название детали: {0}", Name);
            Console.WriteLine("Артикул: {0}", Article);
            Console.WriteLine("Цена: {0}", Price);
            Console.WriteLine("Примечание: {0}", Note);
        }
        public void ChangeInformation(List<Details> details = null, Details detail = null, int index = 0) //метод провери и изменения информации о полях экземпляра класса
        {
            try
            {
                switch (Console.ReadKey(true).Key) //в зависимости от нажатой клавиши изменяется соответствующее поле экземпляра класса
                {
                    case (ConsoleKey)'1':
                        Console.Write("Введите название детали: ");
                        string newTitle = Console.ReadLine();
                        if (newTitle.Length == 0) //проверка на пустую строку
                        {
                            Console.WriteLine("Поле Название не может быть пустым!");
                        }
                        else
                        {
                            Name = newTitle;
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            DetailsInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                
                        }
                        ChangeInformation(details, detail, index);
                        break;
                    case (ConsoleKey)'2':
                        Console.Write("Введите артикул детали: ");
                        int newArticle = int.Parse(Console.ReadLine());
                        List<Details> all_details = Program.GetAllDetails();
                        if (all_details.Find(dtl => dtl.Article == newArticle) != null) // проверка существует ли артикул детали
                        {
                            Console.WriteLine("Деталь с данным артикулом уже существует! Введите другой артикул!");
                        }
                        else
                        {
                            Article = newArticle;
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            DetailsInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        }
                        ChangeInformation(details, detail, index);
                        break;
                    case (ConsoleKey)'3':
                        if (details == null)
                        {
                            Console.Write("Введите цену детали: ");
                            Price = int.Parse(Console.ReadLine());
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            DetailsInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        }
                        else
                        {
                            Console.WriteLine("Поле Цена недоступно для редакитрования!");
                        }
                        ChangeInformation(details, detail, index);
                        break;
                    case (ConsoleKey)'4':
                        Console.Write("Введите примечание к детали: ");
                        Note = Console.ReadLine();
                        Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        DetailsInfo();
                        Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        ChangeInformation(details, detail, index);
                        break;
                    case ConsoleKey.Escape: // возврат в меню деталей при нажатии клавиши Escape
                        Program.ShowDetailsMenu();
                        break;
                    case ConsoleKey.Enter:  // запись информации в базу данных при нажатии клавиши Enter
                        if (details != null) // если информация редактиурется, то база данных перезаписывается
                        {
                            details[index] = detail;
                            StreamWriter sw = new StreamWriter("details.txt", false);
                            sw.Close();
                            foreach (Details dts in details)
                            {
                                dts.WriteInDatabase();
                            }
                        }
                        else // иначе записывается в конец файла
                        {
                            WriteInDatabase();
                        }
                        Program.ShowDetailsMenu();
                        break;
                    default:
                        AllInfo();
                        break;
                }
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз!");
                ChangeInformation(details, detail, index);
            }
        }
        public void InfoTemplate() // шаблон для проверки введённых данных для класса Детали
        {
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
            Console.WriteLine("|                              ВВЕДЁННАЯ ИНФОРМАЦИЯ                          |");
            Console.WriteLine("|———————————————————————————————————————————————————————————————————————————-|");
            Console.WriteLine("| Проверьте введённые данные, если хотите изменить информацию, то нажмите    |");
            Console.WriteLine("| соответствующую клавишу. Чтобы сохранить введённые данные нажмите Enter.   |");
            Console.WriteLine("| Если хотите вернуться назад без сохранения данных нажмите ESC.             |");
            Console.WriteLine("|———————————————————————————————————————————————————————————————————————————-|");
            Console.WriteLine("| 1 - Название   |   2 - Артикул    |    3 - Цена     |    4 - Примечание    |");
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————-");
        }
        public void AllInfo() // метод проверки введённой информации
        {
            Console.Clear();
            Console.Title = "Autoparts - Детали - Добавить деталь - Проверка введённой иноформации";
            InfoTemplate();
            DetailsInfo();
            ChangeInformation();
        }

        public void WriteInDatabase() // метод записи экземпляра класса в базу данных
        {
            StreamWriter sw = new StreamWriter("details.txt", true);
            sw.WriteLine(Name);
            sw.WriteLine(Article);
            sw.WriteLine(Price);
            sw.WriteLine(Note);
            sw.WriteLine("");
            sw.Close();
        }
    }
    class Deliveries // класс Поставки
    {
        public int Deliever; // поля класса Поставки
        public int Supplier;
        public int Detail;
        public int Count;
        public DateTime Date;
        public Deliveries(int deliever, int supplier, int detail, int count, DateTime date) // конструктор с параметрами класса поставки
        {
            Deliever = deliever;
            Supplier = supplier;
            Detail = detail;
            Count = count;
            Date = date;
        }
        public void DeliveriesInfo() // вывод информации о экземпляре класса
        {
            Console.WriteLine("Номер поставки: {0}", Deliever);
            Console.WriteLine("Номер поставщика: {0}", Supplier);
            Console.WriteLine("Артикул детали: {0}", Detail);
            Console.WriteLine("Количество деталей: {0}", Count);
            Console.WriteLine("Дата покупки: {0}", Date.ToShortDateString());
        }
        public void ChangeInformation() // метод проверки введённой информации экземпляра класа 
        {
            try
            {
                switch (Console.ReadKey(true).Key) //в зависимости от нажатой клавиши изменяется соответствующее поле экземпляра класса
                {
                    case (ConsoleKey)'1':
                        Console.Write("Введите номер поставки: ");
                        int new_deliever = int.Parse(Console.ReadLine());
                        List<Deliveries> deliveries = Program.GetAllDeliveries();
                        if (deliveries.Find(del => del.Deliever == new_deliever) != null) // проверка есть ли поставка с таким номером
                        {
                            Console.WriteLine("Поставка с указанным номером уже существует!");
                        } else
                        {
                            Deliever = new_deliever;
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            DeliveriesInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");                   
                        }
                        ChangeInformation();
                        break;
                    case (ConsoleKey)'2':
                        Console.Write("Введите номер поставщика: ");
                        int new_supplier = int.Parse(Console.ReadLine());
                        List<Suppliers> suppliers = Program.GetAllSuppliers();
                        if (suppliers.Find(spl => spl.Supplier == new_supplier) == null) // проверка есть ли поставщик с введённым номером
                        {
                            Console.WriteLine("Поставщика с данным номером нет в базе данных! Введите другой номер!");
                        }
                        else
                        {
                            Supplier = new_supplier;
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            DeliveriesInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        }
                        ChangeInformation();
                        break;
                    case (ConsoleKey)'3':
                        Console.Write("Введите артикул детали: ");
                        int new_article = int.Parse(Console.ReadLine());
                        List<Details> details = Program.GetAllDetails();
                        if (details.Find(dtl => dtl.Article == new_article) == null) // проверка есть ли деталь с введённым артикулом
                        {
                            Console.WriteLine("Детали с таким артикулом нет в базе данных! Добавьте деталь или введите другой артикул!");
                        } else
                        {
                            Detail = new_article;
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                            DeliveriesInfo();
                            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        }
                        ChangeInformation();
                        break;
                    case (ConsoleKey)'4':
                        Console.Write("Введите количество купленных деталей: ");
                        Count = int.Parse(Console.ReadLine());
                        Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        DeliveriesInfo();
                        Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        ChangeInformation();
                        break;
                    case (ConsoleKey)'5':
                        Console.Write("Введите примечание к детали: ");
                        Date = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        DeliveriesInfo();
                        Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                        ChangeInformation();
                        break;
                    case ConsoleKey.Escape: // при нажатии клавиши Escape происзодит выход в меню поставок
                        Program.ShowDeliveriesMenu();
                        break;
                    case ConsoleKey.Enter: // при нажатии клавиши Enter происходит запись данных в файл
                        WriteInDatabase();
                        Program.ShowDeliveriesMenu();
                        break;
                    default:
                        AllInfo();
                        break;
                }
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз!"); 
                ChangeInformation();
            }
        }
        public void EditInformation(List<Deliveries> deliveries = null, Deliveries deliever = null, int index = 0) // метод редактирования количества деталей поставки
        {
            try
            {
                Console.Write("Введите количество купленных деталей: ");
                Count = int.Parse(Console.ReadLine());
                Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                DeliveriesInfo();
                Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                Console.WriteLine("Проверьте введённую информацию. Для сохранения нажмите Enter, для изменения количества деталей нажмите 1.");
                switch (Console.ReadKey(true).Key)
                {
                    case (ConsoleKey)'1':
                        EditInformation(deliveries, deliever, index);
                        break;
                    case ConsoleKey.Enter: // при нажатии клавиши Enter происходит перезапись данных о поставках
                        if (deliveries != null)
                        {
                            deliveries[index] = deliever;
                            StreamWriter sw = new StreamWriter("deliveries.txt", false);
                            sw.Close();
                            foreach (Deliveries dlr in deliveries)
                            {
                                dlr.WriteInDatabase();
                            }
                        }
                        else
                        {
                            WriteInDatabase();
                        }
                        Program.ShowDeliveriesMenu();
                        break;
                    default:
                        EditInformation();
                        break;
                }
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз!");
                EditInformation(deliveries, deliever, index);
            }
        }
        public void InfoTemplate() // шаблон для проверки введённых данных для класса Поставки
        {
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————---------------------------------");
            Console.WriteLine("|                                                    ВВЕДЁННАЯ ИНФОРМАЦИЯ                                    |");
            Console.WriteLine("|———————————————————————————————————————————————————————————————————————————---------------------------------|");
            Console.WriteLine("|                            Проверьте введённые данные, если хотите изменить информацию, то нажмите         |");
            Console.WriteLine("|                            соответствующую клавишу. Чтобы сохранить введённые данные нажмите Enter.        |");
            Console.WriteLine("|                            Если хотите вернуться назад без сохранения данных нажмите ESC.                  |");
            Console.WriteLine("|———————————————————————————————————————————————————————————————————————————---------------------------------|");
            Console.WriteLine("| 1 - Номер поставки | 2 - Номер поставщика | 3 - Артикул детали | 4 - Количество деталей | 5 - Дата покупки |");
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————---------------------------------");
        }
        public void AllInfo() // метод проверки введённой информации
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставки - Добавить поставку - Проверка введённой иноформации";
            InfoTemplate();
            DeliveriesInfo();
            ChangeInformation();
        }
        public void WriteInDatabase() // метод записи экземпляра класса в базу данных
        {
            StreamWriter sw = new StreamWriter("deliveries.txt", true);
            sw.WriteLine(Deliever);
            sw.WriteLine(Supplier);
            sw.WriteLine(Detail);
            sw.WriteLine(Count);
            sw.WriteLine(Date);
            sw.WriteLine("");
            sw.Close();
        }
    }
    internal class Program // основная программа
    {
        public static void ShowMenu() // метод, отображающий главное меню программы
        {
            Console.Clear();
            Console.Title = "Autoparts";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
            Console.WriteLine("|                                 AUTOPARTS                                 |");
            Console.WriteLine("|———————————————————————————————————————————————————————————————————————————|");
            Console.WriteLine("| Добро пожаловать в программу финансового учёта фирмы по продаже запчастей!|");
            Console.WriteLine("| Выберите пункт меню (для выхода из программы нажмите ESC):                |");
            Console.WriteLine("|———————————————————————————————————————————————————————————————————————————|");
            Console.WriteLine("| 1 - Поставщики          |        2 - Детали           |      3 - Поставки |");
            Console.WriteLine("———————————————————————————————————————————————————————————————————————————--");
            switch (Console.ReadKey(true).Key) // навигация по меню
            {
                case (ConsoleKey)'1':
                    ShowSuppliersMenu();
                    break;
                case (ConsoleKey)'2':
                    ShowDetailsMenu();
                    break;
                case (ConsoleKey)'3':
                    ShowDeliveriesMenu();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    ShowMenu();
                    break;
            }
        }
        public static void ShowSuppliersMenu() // метод, отображающий меню Поставщиков
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставщики";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————-------------------------------");
            Console.WriteLine("|                                                 ПОСТАВЩИКИ                                               |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————-----------------------------|");
            Console.WriteLine("|                              Выберите пункт меню (для выхода в меню нажмите ESC):                        |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————-----------------------------|");
            Console.WriteLine("| 1 - Добавить поставщика | 2 - Удалить поставщика | 3 - Редактировать поставщика | 4 - Список поставщиков |");
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————-------------------------------");
            switch (Console.ReadKey(true).Key) // выбор пункта меню
            {
                case (ConsoleKey)'1':
                    AddSupplier();
                    break;
                case (ConsoleKey)'2':
                    DeleteSupplierMenu();
                    break;
                case (ConsoleKey)'3':
                    EditSupplierMenu();
                    break;
                case (ConsoleKey)'4':
                    ShowAllSuppliers();
                    break;
                case ConsoleKey.Escape:
                    ShowMenu();
                    break;
                default:
                    ShowSuppliersMenu();
                    break;
            }
        }
        public static void ShowDetailsMenu() // метод, отображающий меню Деталей
        {
            Console.Clear();
            Console.Title = "Autoparts - Детали";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————----------------------");
            Console.WriteLine("|                                                ДЕТАЛИ                                            |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————---------------------|");
            Console.WriteLine("|                          Выберите пункт меню (для выхода в меню нажмите ESC):                    |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————---------------------|");
            Console.WriteLine("| 1 - Добавить деталь | 2 - Удалить деталь | 3 - Редактировать деталь | 4 - Просмотреть все детали |");
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————----------------------");
            switch (Console.ReadKey(true).Key) // выбор пункта меню
            {
                case (ConsoleKey)'1':
                    AddDeatils();
                    break;
                case (ConsoleKey)'2':
                    DeleteDetailMenu();
                    break;
                case (ConsoleKey)'3':
                    EditDetailMenu();
                    break;
                case (ConsoleKey)'4':
                    ShowAllDetails();
                    break;
                case ConsoleKey.Escape:
                    ShowMenu();
                    break;
                default:
                    ShowDetailsMenu();
                    break;
            }
        }
        public static void ShowDeliveriesMenu() // метод, оторбражающий меню Поставок
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставки";
            Console.WriteLine("——————————————————————————————————————————--------------------------------------------");
            Console.WriteLine("|                                    ПОСТАВКИ                                        |");
            Console.WriteLine("|——————————————————————————————————————————————————————————————————————————----------|");
            Console.WriteLine("|                 Выберите пункт меню (для выхода в меню нажмите ESC):               |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————------ |");
            Console.WriteLine("| 1 - Добавить поставку | 2 - Редактировать количество деталей | 3 - Список поставок |");
            Console.WriteLine("| 4 - Просмотр поставок за определённый период времени | 5 - Высчитать цену поставки |");
            Console.WriteLine("———————————————————————————————————————————————---------------------------------------");
            switch (Console.ReadKey(true).Key) // выбор пункта меню
            {
                case (ConsoleKey)'1':
                    AddDeliveries();
                    break;
                case (ConsoleKey)'2':
                    EditDelieverMenu();
                    break;
                case (ConsoleKey)'3':
                    ShowAllDeliveries();
                    break;
                case (ConsoleKey)'4':
                    DeliveriesByTimeInterval();
                    break;
                case (ConsoleKey)'5':
                    PriceDeliever();
                    break;
                case ConsoleKey.Escape:
                    ShowMenu();
                    break;
                default:
                    ShowDeliveriesMenu();
                    break;
            }
        }
        public static void PriceDeliever() // метод расчёта цены поставки
        {
            try
            {
                Console.Clear();
                Console.Title = "Autoparts - Поставки - Высчитать цену поставки";
                Console.WriteLine("——————————————————————————————————————————--------------------------------------------");
                Console.WriteLine("|                                ВЫСЧИТАТЬ ЦЕНУ ПОСТАВКИ                             |");
                Console.WriteLine("|——————————————————————————————————————————————————————————————————————————----------|");
                Console.WriteLine("|                                Введите номер поставки:                             |");
                Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————--------");
                int deliever_number = int.Parse(Console.ReadLine());
                List<Deliveries> deliveries = GetAllDeliveries(); // получение списка поставок
                Deliveries searh_deliever = deliveries.Find(deliever => deliever.Deliever == deliever_number); // проверка есть ли введёный номер поставки в спике
                if (searh_deliever != null) // если есть, то вывод информации о поставке и её стоимость
                {
                    double price = 0;
                    Console.WriteLine("Информация о найденной поставке:");
                    searh_deliever.DeliveriesInfo();
                    List<Details> details = GetAllDetails();
                    Details purchaseddetail = details.Find(detail => detail.Article == searh_deliever.Detail);
                    price += purchaseddetail.Price * searh_deliever.Count;
                    Console.WriteLine("Общая сумма поставки: {0}", price);
                    Console.WriteLine("Для возврата в главное меню нажмите ESC, для того, чтобы попробовать ещё раз нажмите любую клавишу");

                }
                else
                {
                    Console.WriteLine("Поставки с таким номером не существует! Для того, чтобы попробовать ещё раз нажмите любую клавишу, для выхода в меню поставок нажмите ESC");
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        ShowDeliveriesMenu();
                        break;
                    default:
                        PriceDeliever();
                        break;
                }
            }
            catch // вывод текста, при вводе некорректных данных
            {
                Console.WriteLine("Введены некорректные данные! Попробуйте ещё раз!");
                PriceDeliever();
            }
        }
        private static void DeliveriesByTimeInterval() // метод вывода поставок за определённый период времени
        {
            try
            {
                Console.Clear();
                Console.Title = "Autoparts - Поставки - Поставки за определённый период времени";
                Console.WriteLine("——————————————————————————————————————————--------------------------------------------");
                Console.WriteLine("|                        ПОСТАВКИ ЗА ОПРЕДЕЛЁННЫЙ ПЕРИОД ВРЕМЕНИ                     |");
                Console.WriteLine("|——————————————————————————————————————————————————————————————————————————----------|");
                Console.WriteLine("|                              Введите промежутки времени:                           |");
                Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————--------");
                Console.Write("Введите начало периода: ");
                DateTime start_date = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите конец периода: ");
                DateTime end_date = DateTime.Parse(Console.ReadLine());
                List<Deliveries> deliveries = GetAllDeliveries(); // получение списка всех поставок
                List<Deliveries> deliveriesbytime = new List<Deliveries>(); // создаём пустой список, в который будем вносить поставки, подходящие по дате
                foreach (Deliveries dvr in deliveries)
                {
                    if (dvr.Date >= start_date && dvr.Date <= end_date)
                    {
                        deliveriesbytime.Add(dvr); // добавление поставки в список
                    }
                }
                if (deliveriesbytime.Count == 0)
                {
                    Console.WriteLine("У вас нет поставок за указанный период времени! Для возврата в главное меню нажмите ESC, чтобы попробовать ещё раз нажмите Enter");
                }
                else // вывод всех поставок за указанный период времени и общей стоимости этих поставок
                {
                    Console.WriteLine("Список поставок за указанный период времени: ");
                    double price = 0;
                    List<Details> details = GetAllDetails();
                    foreach (Deliveries dvr in deliveriesbytime)
                    {
                        Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————--------");
                        Details purchaseddetail = details.Find(detail => detail.Article == dvr.Detail);
                        price += purchaseddetail.Price * dvr.Count;
                        dvr.DeliveriesInfo();
                        Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————--------");
                    }
                    Console.WriteLine("Общая стоимость поставок за этот период времени: {0}", price);
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        ShowDeliveriesMenu();
                        break;
                    case ConsoleKey.Enter:
                        DeliveriesByTimeInterval();
                        break;
                    default:
                        DeliveriesByTimeInterval();
                        break;
                }
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                DeliveriesByTimeInterval();
            }
        }
        public static List<Suppliers> GetAllSuppliers() // метод, возвращающий список всех поставщиков
        {
            List<Suppliers> suppliers = new List<Suppliers>();
            if (File.Exists("suppliers.txt"))
            {
                StreamReader reader = new StreamReader("suppliers.txt"); // открываем файл для чтения
                List<string> info = new List<string>();
                while (!reader.EndOfStream) // считываем информацию, пока не достигнем конца файла
                {
                    string infoLine = reader.ReadLine();
                    if (infoLine == "") // когда встречается пустая строка, значит заканчивается инофрмация об 1-ом экземпляре класса
                    {
                        suppliers.Add(new Suppliers(int.Parse(info[0]), info[1], info[2], int.Parse(info[3]))); // добавление экземпляра класса в список
                        info.Clear();
                    }
                    else
                    {
                        info.Add(infoLine);
                    }
                }
                reader.Close(); // закрытие файла восле считывания всех данных
            }
            return suppliers; // возвращаем список всех поставщиков
        }
        public static List<Details> GetAllDetails() // метод, возвращающий список все детали
        {
            List<Details> details = new List<Details>();
            if (File.Exists("details.txt"))
            {
                StreamReader reader = new StreamReader("details.txt"); // открываем файл для чтения
                List<string> info = new List<string>();
                while (!reader.EndOfStream) // считываем информацию, пока не достигнем конца файла
                {
                    string infoLine = reader.ReadLine();
                    if (info.Count == 4) // когда количество строк информации достигнет 4, значит заканчивается информация об 1-ом экземпляре класса
                    {
                        details.Add(new Details(info[0], int.Parse(info[1]), double.Parse(info[2]), info[3]));  // добавление экземпляра класса в список
                        info.Clear();
                    }
                    else
                    {
                        info.Add(infoLine);
                    }
                }
                reader.Close(); // закрытие файла восле считывания всех данных
            }
            return details; // возвращаем список всех деталей
        }
        public static List<Deliveries> GetAllDeliveries() // метод, возвращающий список все поставки
        {
            List<Deliveries> deliveries = new List<Deliveries>();
            if (File.Exists("deliveries.txt"))
            {
                StreamReader reader = new StreamReader("deliveries.txt"); // открываем файл для чтения
                List<string> info = new List<string>();
                while (!reader.EndOfStream) // считываем информацию, пока не достигнем конца файла
                {
                    string infoLine = reader.ReadLine();
                    if (infoLine == "") // когда встречается пустая строка, значит заканчивается инофрмация об 1-ом экземпляре класса
                    {
                        deliveries.Add(new Deliveries(int.Parse(info[0]), int.Parse(info[1]), int.Parse(info[2]), int.Parse(info[3]), DateTime.Parse(info[4]))); // добавление экземпляра класса в список
                        info.Clear();
                    }
                    else
                    {
                        info.Add(infoLine);
                    }
                }
                reader.Close(); // закрытие файла восле считывания всех данных
            }
            return deliveries; // возвращаем список всех поставок
        }
        public static void AddSupplier() //метод добавления нового поставщика в базу данных
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставщики - Добавление поставщика";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
            Console.WriteLine("|                                ДОБАВЛЕНИЕ ПОСТАВЩИКА                            |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————----|");
            Console.WriteLine("|                            Введите необходимую информацию:                      |");
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
            try
            {
                Console.Write("Введите номер поставщика: ");
                int number = int.Parse(Console.ReadLine());
                List<Suppliers> suppliers = GetAllSuppliers();
                if (suppliers.Find(spl => spl.Supplier == number) != null) // проверка, есть ли введённый номер в базе данных поставщиков
                {
                    Console.WriteLine("Поставщик с данным номером уже существует! Введите другой номер!");
                    Console.WriteLine("Для продолжения введите любую клавишу...");
                    Console.ReadKey(true);
                    AddSupplier();
                }
                Console.Write("Введите название поставщика: ");
                string title = Console.ReadLine();
                Console.Write("Введите адрес поставщика: ");
                string address = Console.ReadLine();
                Console.Write("Введите телефон поставщика: ");
                int telephone = int.Parse(Console.ReadLine());
                if (title.Length == 0 || address.Length == 0 ) // проверка, являются ли введённые строки пустыми
                {
                    Console.WriteLine("Поля 'Название', 'Адрес' и 'Телефон' не могут быть пустыми! ");
                    Console.WriteLine( "Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                    Console.ReadKey(true);
                    AddSupplier();
                }
                Suppliers supplier = new Suppliers(number, title, address, telephone); // создание экземпляра класса
                supplier.AllInfo();
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                AddSupplier();
            }
        }
        public static void AddDeatils() //метод добавления новой детали в базу данных
        {
            try
            {
                Console.Clear();
                Console.Title = "Autoparts - Детали - Добавление детали";
                Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
                Console.WriteLine("|                                    ДОБАВЛЕНИЕ ДЕТАЛИ                            |");
                Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————----|");
                Console.WriteLine("|                            Введите необходимую информацию:                      |");
                Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
                Console.Write("Введите название детали: ");
                string title = Console.ReadLine();
                if(title.Length == 0)
                {
                    Console.WriteLine("Поле Название не может быть пустым! Попробуйте ещё раз. Для продолжения нажмите любую клавишу...");
                    Console.ReadKey(true);
                    AddDeatils();
                }
                Console.Write("Введите артикул детали: ");
                int article = int.Parse(Console.ReadLine()); // проверка, есть ли введённый артикул в базе данных деталей
                List<Details> details = GetAllDetails();
                if (details.Find(spl => spl.Article == article) != null)
                {
                    Console.WriteLine("Деталь с данным артикулом уже существует! Введите другой артикул!");
                    Console.WriteLine("Для продолжения введите любую клавишу...");
                    Console.ReadKey(true);
                    AddDeatils();
                }
                Console.Write("Введите цену детали: ");
                double price = double.Parse(Console.ReadLine());
                Console.Write("Введите примечание к детали: ");
                string note = Console.ReadLine();
                Details detail = new Details(title, article, price, note); // создание экземпляра класса
                detail.AllInfo();
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                AddDeatils();
            }
        }

        public static void AddDeliveries() //метод добавления новой поставки в базу данных
        {
            try
            {
                Console.Clear();
                Console.Title = "Autoparts - Поставки - Добавление поставки";
                Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
                Console.WriteLine("|                                  ДОБАВЛЕНИЕ ПОСТАВКИ                            |");
                Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————----|");
                Console.WriteLine("|                            Введите необходимую информацию:                      |");
                Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
                Console.Write("Введите номер поставки: ");
                int number_deliever = int.Parse(Console.ReadLine());
                List<Deliveries> deliveries = GetAllDeliveries();
                if (deliveries.Find(del => del.Deliever == number_deliever) != null) // проверка, есть ли введённый номер в базе данных поставок
                {
                    Console.WriteLine("Поставка с указанным номером уже существует! Введите другой номер!");
                    Console.WriteLine("Для продолжения введите любую клавишу...");
                    Console.ReadKey(true);
                    AddDeliveries();
                }
                Console.Write("Введите номер поставщика: ");
                int number_supplier = int.Parse(Console.ReadLine());
                List<Suppliers> suppliers = GetAllSuppliers();
                if (suppliers.Find(spl => spl.Supplier == number_supplier) == null) // проверка, существует ли введённый номер поставщика
                {
                    Console.WriteLine("Поставщика с данным номером нет в базе данных! Добавьте поставщика или введите другой номер!");
                    Console.WriteLine("Для продолжения введите любую клавишу...");
                    Console.ReadKey(true);
                    AddDeliveries();
                }
                Console.Write("Введите артикул детали: ");
                int article = int.Parse(Console.ReadLine());
                List<Details> details = GetAllDetails();
                if (details.Find(dtl => dtl.Article == article) == null) // проверка, существует ли введённый артикул детали
                {
                    Console.WriteLine("Детали с таким артикулом нет в базе данных! Добавьте деталь или введите другой артикул!");
                    Console.WriteLine("Для продолжения введите любую клавишу...");
                    Console.ReadKey(true);
                    AddDeliveries();
                }
                Console.Write("Введите количество деталей: ");
                int count = int.Parse(Console.ReadLine());
                if(count <= 0) // проверка, явялется ли количество деталей положительным
                {
                    Console.WriteLine("Количество деталей не может быть меньше либо равно 0! Введите другое количество деталей");
                    Console.WriteLine("Для продолжения введите любую клавишу...");
                    Console.ReadKey(true);
                    AddDeliveries();
                }
                Console.Write("Введите дату покупки: ");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Deliveries deliever = new Deliveries(number_deliever, number_supplier, article, count, date); // создание экземпляра класса
                deliever.AllInfo();
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                AddDeliveries();
            }
        }
        public static void DeleteSupplierMenu() //метод, отображающий меню удаления поставщиков
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставщики - Удаление поставщика";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
            Console.WriteLine("|                                  УДАЛЕНИЕ ПОСТАВЩИКА                            |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————----|");
            Console.WriteLine("|             1 - Удалить поставщика       |          ESC - Меню поставщиков      |");
            Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
            switch (Console.ReadKey(true).Key) // выбор пункта меню
            {
                case (ConsoleKey)'1':
                    DeleteSupplier();
                    break;
                case ConsoleKey.Escape:
                    ShowSuppliersMenu();
                    break;
                default:
                    DeleteSupplierMenu();
                    break;
            }

        }
        public static void DeleteSupplier() // метод удаления поставщиков
        {
            try
            {
                Console.Clear();
                List<Suppliers> suppliers = GetAllSuppliers(); // получение списка всех поставщиков
                Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————-----");
                Console.WriteLine("|                        Введите номер поставщика для удаления                    |");
                Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
                int number = int.Parse(Console.ReadLine());
                Suppliers searchSupplier = suppliers.Find(supplier => supplier.Supplier == number); // проверка, имеется ли введённый номер в спике поставщиков
                if (searchSupplier != null)
                {
                    Console.WriteLine("Вы действительно хотите удалить этого поставщика? (Да - Enter, Нет - 1)");
                    searchSupplier.SupplierInfo();
                    switch (Console.ReadKey(true).Key)
                    {
                        case (ConsoleKey)'1':
                            DeleteSupplierMenu();
                            break;
                        case ConsoleKey.Enter: // удаление поставщика и перезапись базы данных при нажатии клавиши Enter
                            suppliers.Remove(searchSupplier);
                            StreamWriter sw = new StreamWriter("suppliers.txt", false);
                            sw.Close();
                            foreach (Suppliers supplier in suppliers)
                            {
                                supplier.WriteInDatabase();
                            }
                            DeleteSupplierMenu();
                            break;
                        default: // вывод текста при неверно нажатой клавише
                            Console.WriteLine("Введено неверное значение!");
                            break;
                    }
                }
                else // если номер поставщика отсутствует
                {
                    Console.WriteLine("Данный номер отсутствует в списке поставщиков! Для возврата к меню удаления нажмите любую клавишу");
                    Console.ReadKey(true);
                    DeleteSupplierMenu();
                }
            }
            catch // вывод текста при вводе некорректных данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                DeleteSupplier();
            }

        }
        public static void DeleteDetailMenu() // метод, отображающий меню удаления деталей
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставщики - Удаление поставщика";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
            Console.WriteLine("|                                   УДАЛЕНИЕ ДЕТАЛИ                               |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————----|");
            Console.WriteLine("|             1 - Удалить деталь       |          ESC - Меню деталей              |");
            Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
            switch (Console.ReadKey(true).Key) // выбор пункта меню
            {
                case (ConsoleKey)'1':
                    DeleteDetail();
                    break;
                case ConsoleKey.Escape:
                    ShowDetailsMenu();
                    break;
                default:
                    DeleteDetailMenu();
                    break;
            }

        }
        public static void DeleteDetail() // метод удаления деталей
        {
            try
            {
                Console.Clear();
                List<Details> details = GetAllDetails(); // получение списка всех деталей
                Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————-----");
                Console.WriteLine("|                          Введите артикул детали для удаления                     |");
                Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
                int number = int.Parse(Console.ReadLine());
                Details searchDetail = details.Find(detail => detail.Article == number);
                if (searchDetail != null) // проверка, существует ли введённый артикул детали
                {
                    Console.WriteLine("Вы действительно хотите удалить эту деталь? (Да - Enter, Нет - 1)");
                    searchDetail.DetailsInfo();
                    switch (Console.ReadKey(true).Key)
                    {
                        case (ConsoleKey)'1':
                            DeleteDetailMenu();
                            break;
                        case ConsoleKey.Enter: // удадение детали и перезапись базы данных при нажатии клавиши Enter
                            details.Remove(searchDetail);
                            StreamWriter sw = new StreamWriter("details.txt", false);
                            sw.Close();
                            foreach (Details dts in details)
                            {
                                dts.WriteInDatabase();
                            }
                            DeleteDetailMenu();
                            break;
                        default: // вывод текста при неверно нажатой клавише
                            Console.WriteLine("Введено неверное значение!");
                            Console.ReadKey(true);
                            DeleteDetailMenu();
                            break;
                    }
                }
                else // если артикул детали отсутсвует
                {
                    Console.WriteLine("Данный артикул отсутствует в списке деталей! Для возврата к меню удаления нажмите любую клавишу");
                    Console.ReadKey(true);
                    DeleteDetailMenu();
                }
            }
            catch // вывод текста при некорректно введённых значениях
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                DeleteDetail();
            }
        }
        public static void ShowAllSuppliers(string sortType = "based") // метод, отображающий список всех поставщиков
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставщики - Список всех поставщиков";
            List<Suppliers> suppliers = GetAllSuppliers(); // получение списка всех поставщиков
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————----------------------------------");
            Console.WriteLine("|                                                  СПИСОК ВСЕХ ПОСТАВЩИКОВ                                    |");
            Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————---------------------------------");
            Console.WriteLine("| 1 - Сортировать по номеру | 2 - Сортировать по названию | 3 - В порядке заполнения | ESC - Меню поставщиков |");
            Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————---------------------------------");
            if (suppliers.Count == 0) // если поставщиков ещё нет
            {
                Console.WriteLine("У вас ещё нет поставщиков!");
            }
            else
            {
                switch (sortType) // сортировка списка в зависимости от типа сортировки
                {
                    case "number":
                        suppliers.Sort((x, y) => x.Supplier.CompareTo(y.Supplier));
                        break;
                    case "name":
                        suppliers.Sort((x, y) => x.Name.CompareTo(y.Name));
                        break;
                }
                foreach (Suppliers supplier in suppliers) // вывод списка поставщиков
                {
                    supplier.SupplierInfo();
                    Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                }
            }
            switch (Console.ReadKey(true).Key) // выбор типа сортировки или выход в меню поставщиков
            {
                case (ConsoleKey)'1':
                    ShowAllSuppliers("number");
                    break;
                case (ConsoleKey)'2':
                    ShowAllSuppliers("name");
                    break;
                case (ConsoleKey)'3':
                    ShowAllSuppliers();
                    break;
                case ConsoleKey.Escape:
                    ShowSuppliersMenu();
                    break;
                default:
                    ShowAllSuppliers();
                    break;
            }
        }
        public static void ShowAllDetails(string sortType = "based") // метод, отображающий список всех деталей
        {
            Console.Clear();
            Console.Title = "Autoparts - Детали - Список всех деталей";
            List<Details> details = GetAllDetails(); // получение списка всех деталей
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————-----------------------------");
            Console.WriteLine("|                                                СПИСОК ВСЕХ ДЕТАЛЕЙ                                     |");
            Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————----------------------------");
            Console.WriteLine("| Сортировать по: 1 - Номеру | 2 - Названию | 3 - Цене | 4 - В порядке заполнения |  ESC - Меню деталей  |");
            Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————----------------------------");
            if (details.Count == 0) // если деталей ещё нет
            {
                Console.WriteLine("У вас ещё нет деталей!");
            }
            else
            {
                switch (sortType) // сортировка в зависимости от типа
                {
                    case "article":
                        details.Sort((x, y) => x.Article.CompareTo(y.Article));
                        break;
                    case "name":
                        details.Sort((x, y) => x.Name.CompareTo(y.Name));
                        break;
                    case "price":
                        details.Sort((x, y) => x.Price.CompareTo(y.Price));
                        break;
                }
                foreach (Details dts in details) // вывод списка
                {
                    dts.DetailsInfo();
                    Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                }
            }
            switch (Console.ReadKey(true).Key) // выбор типа сортировки или выход в меню деталей
            {
                case (ConsoleKey)'1':
                    ShowAllDetails("article");
                    break;
                case (ConsoleKey)'2':
                    ShowAllDetails("name");
                    break;
                case (ConsoleKey)'3':
                    ShowAllDetails("price");
                    break;
                case (ConsoleKey)'4':
                    ShowAllDetails();
                    break;
                case ConsoleKey.Escape:
                    ShowDetailsMenu();
                    break;
                default:
                    ShowAllDetails();
                    break;
            }
        }
        public static void ShowAllDeliveries(string sortType = "based") // метод, отображающий список всех поставок
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставки - Список всех поставок";
            List<Deliveries> deliveries = GetAllDeliveries(); // получение списка всех поставок
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————----------------------");
            Console.WriteLine("|                                            СПИСОК ВСЕХ ПОСТАВОК                                 |");
            Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————---------------------");
            Console.WriteLine("| Сортировать по: 1 - Количеству деталей       | 2 - По дате (сначала новые)                      |");
            Console.WriteLine("|                 3 - По дате (сначала старые) | 4 - В порядке заполнения |  ESC - Меню поставок  |");
            Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————---------------------");
            if (deliveries.Count == 0) // если поставок ещё нет
            {
                Console.WriteLine("У вас ещё нет поставок!");
            }
            else
            {
                switch (sortType) // сортировка в зависимости от выбранного типа
                {
                    case "count":
                        deliveries.Sort((x, y) => x.Count.CompareTo(y.Count));
                        break;
                    case "date_new":
                        deliveries.Sort((x, y) => x.Date.CompareTo(y.Date));
                        deliveries.Reverse();
                        break;
                    case "date_old":
                        deliveries.Sort((x, y) => x.Date.CompareTo(y.Date));
                        break;
                }
                foreach (Deliveries dlr in deliveries) // вывод списка
                {
                    dlr.DeliveriesInfo();
                    Console.WriteLine("—————————————————————————————————————————————————————————————————————————————");
                }
            }
            switch (Console.ReadKey(true).Key) // выбор типа сортировки или выход в меню поставок
            {
                case (ConsoleKey)'1':
                    ShowAllDeliveries("count");
                    break;
                case (ConsoleKey)'2':
                    ShowAllDeliveries("date_new");
                    break;
                case (ConsoleKey)'3':
                    ShowAllDeliveries("date_old");
                    break;
                case (ConsoleKey)'4':
                    ShowAllDeliveries();
                    break;
                case ConsoleKey.Escape:
                    ShowDeliveriesMenu();
                    break;
                default:
                    ShowAllDeliveries();
                    break;
            }
        }
        public static void EditSupplierMenu() // метод, отображающий меню редактирования поставщика
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставщики - Редактирование поставщика";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
            Console.WriteLine("|                               РЕДАКТИРОВАНИЕ ПОСТАВЩИКА                         |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————----|");
            Console.WriteLine("|         1 - Редактировать поставщика       |        ESC - Меню поставщиков      |");
            Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
            switch (Console.ReadKey(true).Key) // выбор пункта меню
            {
                case (ConsoleKey)'1':
                    EditSupplier();
                    break;
                case ConsoleKey.Escape:
                    ShowSuppliersMenu();
                    break;
                default:
                    EditSupplierMenu();
                    break;
            }
        }
        public static void EditDetailMenu() // метод, отображающий меню редактирования деталей
        {
            Console.Clear();
            Console.Title = "Autoparts - Детали - Редактирование детали";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————------");
            Console.WriteLine("|                                 РЕДАКТИРОВАНИЕ ДЕТАЛИ                           |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————----|");
            Console.WriteLine("|                   Для редактирования доступны все поля кроме цены!              |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————----|");
            Console.WriteLine("|           1 - Редактировать деталь       |           ESC - Меню деталей         |");
            Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
            switch (Console.ReadKey(true).Key) // выбор пункта меню
            {
                case (ConsoleKey)'1':
                    EditDetail();
                    break;
                case ConsoleKey.Escape:
                    ShowDetailsMenu();
                    break;
                default:
                    EditDetailMenu();
                    break;
            }
        }
        public static void EditDelieverMenu() // метод, отображающий меню редактирования поставок
        {
            Console.Clear();
            Console.Title = "Autoparts - Поставки - Редактирование поставки";
            Console.WriteLine("—————————————————————————————————————————————————————————————————————————————-----------");
            Console.WriteLine("|                                РЕДАКТИРОВАНИЕ ПОСТАВКИ                               |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————---------|");
            Console.WriteLine("|        В поставках для редактирования доступно только поле количества деталей!       |");
            Console.WriteLine("|—————————————————————————————————————————————————————————————————————————————---------|");
            Console.WriteLine("|           1 - Редактировать поставку       |           ESC - Меню поставок           |");
            Console.WriteLine("-————————————————————————————————————————————————————————————————————————————-----------");
            switch (Console.ReadKey(true).Key) // выбор пункта меню
            {
                case (ConsoleKey)'1':
                    EditDeliever();
                    break;
                case ConsoleKey.Escape:
                    ShowDeliveriesMenu();
                    break;
                default:
                    EditDelieverMenu();
                    break;
            }
        }
        public static void EditDeliever() // метод редактирования поставки
        {
            try
            {
                Console.Clear();
                List<Deliveries> deliever = GetAllDeliveries(); // получение списка поставок
                Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————-----");
                Console.WriteLine("|                      Введите номер поставки для редактирования                  |");
                Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
                int number = int.Parse(Console.ReadLine());
                Deliveries searchDeliever = deliever.Find(detail => detail.Deliever == number);
                if (searchDeliever != null) // проверка, существует ли введённый номер поставки
                {
                    int index = deliever.IndexOf(searchDeliever); //поиск индекса искомой поставки 
                    Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————-----");
                    searchDeliever.DeliveriesInfo();
                    Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————-----");
                    searchDeliever.EditInformation(deliever, searchDeliever, index); // вызов метода с передачей параметров для редактирования
                }
                else // если не существует
                {
                    Console.WriteLine("Данный номер поставки отсутствует в списке поставок! Для возврата к меню редактирования нажмите любую клавишу");
                    Console.ReadKey(true);
                    EditDelieverMenu();
                }
            }
            catch // вывод текста при некорректно введённых данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                EditDeliever();
            }
        }
        public static void EditDetail() // метод редактирования детали
        {
            try
            {
                Console.Clear();
                List<Details> details = GetAllDetails(); // получение списка всех деталей
                Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————-----");
                Console.WriteLine("|                       Введите артикул детали для редактирования                  |");
                Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
                int number = int.Parse(Console.ReadLine());
                Details searchDetail = details.Find(detail => detail.Article == number);
                if (searchDetail != null) // проверка, существует ли введённый артикул детали
                {
                    int index = details.IndexOf(searchDetail); //поиск индекса искомой детали
                    searchDetail.InfoTemplate();
                    searchDetail.DetailsInfo();
                    searchDetail.ChangeInformation(details, searchDetail, index); // вызов метода с передачей параметров для редактирования
                }
                else // если не существует
                {
                    Console.WriteLine("Данный артикул отсутствует в списке деталей! Для возврата к меню редактирования нажмите любую клавишу");
                    Console.ReadKey(true);
                    EditDetailMenu();
                }
            }
            catch // вывод текста при некорректно введённых данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                EditDetail();
            }
        }
        public static void EditSupplier() // метод редактирования поставщиков
        {
            try
            {
                Console.Clear();
                List<Suppliers> suppliers = GetAllSuppliers(); // получение списка всех поставщиков
                Console.WriteLine("-—————————————————————————————————————————————————————————————————————————————-----");
                Console.WriteLine("|                      Введите номер поставщика для редактирования                 |");
                Console.WriteLine("-————————————————————————————————————————————————————————————————————————————------");
                int number = int.Parse(Console.ReadLine());
                Suppliers searchSupplier = suppliers.Find(supplier => supplier.Supplier == number);
                if (searchSupplier != null) // проверка, существует ли введённый номер поставщика
                {
                    int index = suppliers.IndexOf(searchSupplier); //поиск индекса искомого поставщика
                    searchSupplier.InfoTemplate();
                    searchSupplier.SupplierInfo();
                    searchSupplier.ChangeInformation(suppliers, searchSupplier, index); // вызов метода с передачей параметров для редактирования
                }
                else // если не существует
                {
                    Console.WriteLine("Данный номер отсутствует в списке поставщиков! Для возврата к меню редактирования нажмите любую клавишу");
                    Console.ReadKey(true);
                    EditSupplierMenu();
                }
            }
            catch // вывод текста при некорректно введённых данных
            {
                Console.WriteLine("Введено некорректное значение. Попробуйте ещё раз! Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                EditSupplier();
            }
        }
        static void Main(string[] args)
        {
            ShowMenu(); // вызов главного меню при запуске программы
        }
    }
}