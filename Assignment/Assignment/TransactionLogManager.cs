using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment
{
    public class TransactionLogManager
    {
        List<TransactionLog> LogItems = new List<TransactionLog>();

        public void CreateLogEntry(string type, Item i, DateTime dateAdded)
        {
            LogItems.Add(new TransactionLog(type, i, dateAdded));
        }

        public void DisTransactions(TransactionLog tl)
        {
            Console.WriteLine(
                "\t{0, -20} {1, -16} {2, -6} {3, -12} {4, -12} {5, -12}",
            tl.DateAdded,
            tl.Type,
            tl.Item.ID,
            tl.Item.Name,
            tl.Item.EmpName,
            tl.Item.Price);
        }

        public List<TransactionLog> GetTransactions()
        {
            List<TransactionLog> list = new List<TransactionLog>();

            foreach (TransactionLog tl in LogItems)
            {
                list.Add(tl);
            }

            return list;
        }

        public void Report()
        {
            Console.WriteLine("Enter employee name : > ");
            string empname = Console.ReadLine();

            Console.WriteLine("\t{0, -20} {1, -10} {2, -12} {3, -12}", "Date Taken", "ID", "Name", "Quantity");

            LogItems.ForEach(delegate(TransactionLog logItem)
            {               
                if (logItem.Type == "Item Removed" && logItem.Item.EmpName == empname)
                {
                    DisPersonalUsage(logItem.Item.DateCreated, logItem.Item.ID, logItem.Item.Name, logItem.Item.Quantity);
                }
            });
        }

        public void DisPersonalUsage(DateTime date, int id, string name, int quantity)
        {
            Console.WriteLine("\t{0, -20} {1, -10} {2, -12} {3, -12}", date, id, name, quantity);
        }
    }
}
