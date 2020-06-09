using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_2_
{
    public class WorkDay : Pizzeria
    {
        private DateTime date;
        private int orders;
        private string pizza;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public int OrdersCount
        {
            get { return orders; }
            set { orders = value; }
        }

        public string PizzaOfTheDay
        {
            get { return pizza; }
            set { pizza = value; }
        }

        public WorkDay()
        {
            date = DateTime.Now.Date;
            orders = 0;
            pizza = "";
        }
        public WorkDay(DateTime date, int orders, string pizza)
        {
            this.date = date;
            this.orders = orders;
            this.pizza = pizza;
        }

        public void showInfo()
        {
            Console.WriteLine("Піцерія: " + Name + " Адреса: " + Address);
            Console.WriteLine("Дата: " + Date.Date.ToString("dd/MM/yyyy"));
            Console.WriteLine("Кількість замовлень: " + OrdersCount);
            Console.WriteLine("Піца дня: " + PizzaOfTheDay);
        }
    }
}
