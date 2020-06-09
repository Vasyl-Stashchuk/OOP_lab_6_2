using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_2_
{
    public class Manager
    {
        private List<WorkDay> workDays;
        private ReaderWriter readerWriter;
        public Manager()
        {
            readerWriter = new ReaderWriter();
            workDays = createList();
        }
        public void setComands()
        {
            Console.WriteLine("Додавання записiв: +");
            Console.WriteLine("Редагування записiв: E");
            Console.WriteLine("Знищення записiв: -");
            Console.WriteLine("Виведення записiв: Enter");
            Console.WriteLine("Середня кількість замовлень в день за період: A");
            Console.WriteLine("Дні з максимальною кількостю відвідувань: M");
            Console.WriteLine("Сумарна кількість замовлень для днів з визначеною піцою дня: S");
            Console.WriteLine("Вихiд: Esc");
    
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.OemPlus:
                    Console.WriteLine();
                    addNew();
                    break;

                case ConsoleKey.E:
                    Console.WriteLine();
                    edit();
                    setComands();
                    break;

                case ConsoleKey.OemMinus:
                    Console.WriteLine();
                    delete();
                    setComands();
                    break;

                case ConsoleKey.Enter:
                    Console.WriteLine();
                    showList();
                    setComands();
                    break;

                case ConsoleKey.A:
                    Console.WriteLine();
                    getAvgOrderCountByDate();
                    setComands();
                    break;

                case ConsoleKey.M:
                    Console.WriteLine();
                    maxOrdersDays();
                    setComands();
                    break;

                case ConsoleKey.S:
                    Console.WriteLine();
                    sumOrderCount();
                    setComands();
                    break;

                case ConsoleKey.Escape:
                    return;
            }
        }
        public WorkDay parseInfo(string strInfo)
        {
            string[] words = new string[6];
            words = strInfo.Split(',');
            WorkDay workDay = new WorkDay(DateTime.Parse(words[0]), int.Parse(words[1]), words[2]);
            return workDay;
        }
        public List<WorkDay> createList()
        {
            List<WorkDay> workDays = new List<WorkDay>();
            List<string> strs = readerWriter.readDataFromFile();
            int strCount = 0;
            foreach (string s in strs)
            {   
                workDays.Add(parseInfo(s));
                strCount++;
            }
            return workDays;
        }
        public void showList()
        {
            Console.WriteLine("{0, -12} {1, -21} {2}", "Дата", "Кількість замовлень", "Піца дня");
            foreach (var d in workDays)
                Console.WriteLine("{0, -12} {1, -21} {2}", d.Date.Date.ToString("dd/MM/yyyy"), d.OrdersCount, d.PizzaOfTheDay);
        }

        public void addNew()
        {
            Console.WriteLine("Введiть данi(дата, к-ть замовлень, піца дня) через кому:");
            string strInfo = Console.ReadLine();
            WorkDay workDay = parseInfo(strInfo);
            workDays.Add(workDay);
            readerWriter.saveList(workDays);
            setComands();
        }

        public void delete()
        {
            Console.WriteLine("Введіть дату(DD.MM.YYYY): ");
            string date = Console.ReadLine();
            foreach (var d in workDays)
            {
                if (d.Date.Date.ToString("dd/MM/yyyy") == date)
                {
                    d.showInfo();
                    Console.WriteLine("Видалити? (Y/N)");
                    var key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Y)
                    {
                        workDays.Remove(d);
                        Console.WriteLine("Видалено успішно!");
                        break;
                    }
                }
            }
            readerWriter.saveList(workDays);
        }
        public void edit()
        {
            Console.WriteLine("Введіть дату(DD.MM.YYYY): ");
            string date = Console.ReadLine();
            foreach (var d in workDays)
            {
                if (d.Date.ToString("dd/MM/yyyy") == date)
                {
                    d.showInfo();
                    Console.WriteLine("Введіть нову інформацію через кому");
                    string strInfo = Console.ReadLine();
                    WorkDay edit = parseInfo(strInfo);
                    edit.showInfo();
                    Console.WriteLine("Зберегти зміни(Y/N)");
                    var key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Y)
                    {
                        workDays.Remove(d);
                        workDays.Add(edit);
                        break;
                    }
                }
            }
            readerWriter.saveList(workDays);
        }

        public void getAvgOrderCountByDate()
        {
            Console.WriteLine("Введіть початкову дату: ");
            string fDate = Console.ReadLine();
            Console.WriteLine("Введіть кінцеву дату: ");
            string tDate = Console.ReadLine();

            DateTime fromDate = DateTime.Parse(fDate);
            DateTime toDate = DateTime.Parse(tDate);

            int count = 0;
            int quantity = 0;
            foreach (var day in workDays)
            {
                if (day.Date >= fromDate && day.Date <= toDate)
                {
                    count++;
                    quantity += day.OrdersCount;
                }
            }
            int avgQ = quantity / count;
            Console.WriteLine("Середня кількість замовлень за період з " + fDate + " по " + tDate + ": " + avgQ);
        }

        public int maxOrdersDays()
        {
            int max = workDays[0].OrdersCount;
            for(int i = 1; i < workDays.Count; i++)
            {
                if (workDays[i].OrdersCount > max)
                {
                    max = workDays[i].OrdersCount; 
                }
            }
            getDayByOrder(max);
            return max;
        }

        public void getDayByOrder(int order)
        {
            foreach (var day in workDays)
            {
                if (day.OrdersCount == order)
                    day.showInfo();
            }
        }

        public void sumOrderCount()
        {
            int sum = 0;
            Console.WriteLine("Введіть назву піци:");
            string piza = Console.ReadLine();
            foreach (var day in workDays)
            {
                if (day.PizzaOfTheDay == piza)
                    sum += day.OrdersCount;
            }
            if (sum != 0)
                Console.WriteLine("Сумарна к-сть замовлень з піцою: " + piza + " складає " + sum);
            else
                Console.WriteLine("Піци " + piza + " не була піцою дня");
        }
    }
}
