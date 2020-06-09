using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_2_
{
    public class Pizzeria
    {
        private string name;
        private string address;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { name = value; }
        }

        public Pizzeria()
        {
            this.name = "Чілінтано";
            this.address = "пр. Перемоги, 40";
        }
    }
}
