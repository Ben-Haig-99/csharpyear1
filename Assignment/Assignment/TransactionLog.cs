using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class TransactionLog
    {
        public string Type { get; }
        public Item Item { get; }
        public DateTime DateAdded { get; }
        public TransactionLog(string type, Item i, DateTime dateAdded)
        {
            this.Type = type;
            this.Item = i;
            this.DateAdded = dateAdded;
        }
    }
}
