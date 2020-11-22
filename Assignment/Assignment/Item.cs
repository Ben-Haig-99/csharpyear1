using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Item
    {
        public int ID { get; }
        public string Name { get; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string EmpName { get; }
        public DateTime DateCreated { get; }

        public Item(int id, string name, float price, int quantity, string empName, DateTime dateCreated)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
            this.EmpName = empName;
            this.DateCreated = dateCreated;
        }
    }
}
