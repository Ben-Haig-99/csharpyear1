using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment
{
    public class ItemManager
    {
        private Dictionary<int, Item> items = new Dictionary<int, Item>();
        private Dictionary<string, Employee> employees = new Dictionary<string, Employee>();
        private List<Item> personalUsage = new List<Item>();
        public Item FindItem(int itemId)
        {
            try
            {
                return items[itemId];
            }
            catch (KeyNotFoundException)
            {
                throw new System.Exception("ERROR: Item not found");
            }
        }

        public void AddItem(Item i)
        {
            items.Add(i.ID, i);
        }

        public void TakeItem(Item i)
        {
            items.Remove(i.ID)
;       }

        public void TotalPrice()
        {
            float total = 0;

            Dictionary<int, Item>.ValueCollection DictItems = items.Values;

            Console.WriteLine("Financial Report:");
            foreach (Item DictItem in DictItems)
            {              
                Console.WriteLine(DictItem.Name + ": " + "Total price of item: £" + (DictItem.Price * DictItem.Quantity));
                total += (DictItem.Price * DictItem.Quantity);
            }

            Console.WriteLine("Total price of all items: £" + total);
        }        

        public int NumItemsInStock()
        {
            return items.Count;
        }

        public Dictionary<int, Item> GetInventory()
        {
            return items;
        }

        public Employee FindEmployee(string EmpName)
        {
            try
            {
                return employees[EmpName];
            }
            catch (KeyNotFoundException)
            {
                throw new System.Exception("ERROR: Employee not found");
            }
        }

        public void AddEmployee(Employee e)
        {
            employees.Add(e.EmpName, e);
        }

        public void CreateUsageEntry(int id, string name, float price, int quantity, string empName, DateTime dateCreated)
        {
            personalUsage.Add(new Item(id, name, price, quantity, empName, dateCreated));
        }
    }
}
