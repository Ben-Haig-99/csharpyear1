using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Employee_UI
    {
        private ItemManager itemMgr;
        private TransactionLogManager tlMgr;

        public Employee_UI()
        {
            this.itemMgr = new ItemManager();
            this.tlMgr = new TransactionLogManager();
        }

        public Dictionary<int, Item> ViewAllItems()
        {
            return itemMgr.GetInventory();
        }
        public void InitialiseData()
        {
            itemMgr.AddEmployee(new Employee("Ben"));
            itemMgr.AddEmployee(new Employee("Steve"));
            itemMgr.AddEmployee(new Employee("Marian"));

            itemMgr.AddItem(new Item(1, "Pencil", 1, 50, "Ben", DateTime.Now));
            itemMgr.AddItem(new Item(2, "Eraser", 2, 100, "Ben", DateTime.Now));
        }

        void DisMenu()
        {
            Console.WriteLine("\n1. Add new item");
            Console.WriteLine("2. Add to stock");
            Console.WriteLine("3. Take from stock");
            Console.WriteLine("4. Inventory Report");
            Console.WriteLine("5. Financial Report");
            Console.WriteLine("6. Display Transaction Log");
            Console.WriteLine("7. Report Personal Usage");
            Console.WriteLine("8. Exit");
        }

        public void MainMenu()
        {
            DisMenu();
            int option = Choice();

            while (option != 8)
            {              
                switch (option)
                {
                    case 1:
                        if (AddNewItem())
                        {
                            Dictionary<int, Item> items = itemMgr.GetInventory();
                            Console.WriteLine("Stock Added: ");
                            Console.WriteLine("\t{0, -4} {1, -12} {2, -12} {3, -12} {4, -12}", "ID", "Name", "Price", "Quantity", "Date Added");
                            foreach (Item i in items.Values)
                            {
                                DisplayAdd(i);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Item is already in stock.");
                        }                           
                        break;
                    case 2:
                        AddToStock();
                        break;
                    case 3:
                        TakeFromStock();
                        break;
                    case 4:
                        InventoryReport();
                        break;
                    case 5:
                        FinancialReport();
                        break;
                    case 6:
                        TransactionLog();
                        break;
                    case 7:
                        ReportUsageLog();
                        break;
                    case 8:
                        //Exit();
                        break;
                }
                DisMenu();
                option = Choice();
            }
        }
        private int Choice()
        {
            int option = ReadInteger("\nOption");
            while (option < 1 || option > 8)
            {
                Console.WriteLine("\nChoice not recognised, Please enter again");
                option = ReadInteger("\nOption");
            }
            return option;
        }

        private int ReadInteger(string prompt)
        {
            try
            {
                Console.Write(prompt + ": > ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private float ReadFloat(string prompt)
        {
            try
            {
                Console.Write(prompt + ": > ");
                return Convert.ToSingle(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool AddNewItem()
        {
            int itemId = ReadInteger("\nItem ID");
            Console.Write("Item Name: > ");
            string itemName = Console.ReadLine();
            int itemQuantity = ReadInteger("Item Quantity");
            float itemPrice = ReadFloat("Item Price");
            Console.Write("Employee Name : > ");
            string itemEmpName = Console.ReadLine();
            if (itemId < 0 || itemQuantity < 0 || itemPrice < 0 || itemName == "" || itemEmpName == "")
            {
                Console.WriteLine("ERROR: ID, Quantity or Price below 0 or Item name/ Employee name is empty");
                MainMenu();
                return true;
            }
            else
            {
                if (itemMgr.NumItemsInStock() < 1)
                {
                    Item i = new Item(itemId, itemName, itemPrice, itemQuantity, itemEmpName, DateTime.Now);
                    itemMgr.AddItem(i);
                    tlMgr.CreateLogEntry("Item Added", i, DateTime.Now);
                    return true;
                }
                else
                {
                    try
                    {
                        Item temp = itemMgr.GetInventory()[itemId];
                        return false;
                    }
                    catch (KeyNotFoundException)
                    {
                        Item i = new Item(itemId, itemName, itemPrice, itemQuantity, itemEmpName, DateTime.Now);
                        itemMgr.AddItem(i);
                        tlMgr.CreateLogEntry("Item Added", i, DateTime.Now);
                        return true;
                    }
                }
            }           
        }

        public void AddToStock()
        {
            int itemId = ReadInteger("\nItem ID");
            Item temp;
            try
            {
                temp = itemMgr.FindItem(itemId);
            }
            catch (KeyNotFoundException)
            {
                throw new System.Exception("ERROR: Item not found");
            }
            Console.Write("Employee Name : > ");
            string itemEmpName = Console.ReadLine();
            int itemQuantity = ReadInteger("How many items would you like to add?");
            if (itemQuantity < 0 || itemEmpName == "")
            {
                Console.WriteLine("ERROR: Quantity being added is below 0 or Employee name is empty");
                MainMenu();
            }
            else
            {
                itemMgr.FindItem(itemId).Quantity += itemQuantity;
                Console.WriteLine(itemQuantity + " items have been added to Item ID: " + itemId + " on " + DateTime.Now);
                Item i = new Item(itemId, temp.Name, temp.Price, itemQuantity, itemEmpName, DateTime.Now);
                tlMgr.CreateLogEntry("Stock Updated", i, DateTime.Now);
            }          
        }

        public bool TakeFromStock()
        {          
            Console.Write("Employee Name: > ");
            string empname = Console.ReadLine();
            try
            {
                itemMgr.FindEmployee(empname);
                    
            }
            catch (KeyNotFoundException)
            {
                throw new System.Exception("ERROR: Employee not found");
            }
            int itemId = ReadInteger("\nItem ID");
            Item temp;
            try
            {
                temp = itemMgr.FindItem(itemId);
            }
            catch (KeyNotFoundException)
            {
                throw new System.Exception("ERROR: Item not found");
            }
            int itemQuantity = ReadInteger("How many items would you like to remove?");
            if (itemQuantity > temp.Quantity || itemQuantity < 0)
            {
                Console.WriteLine("ERROR: Quantity too many or below 0");
                return false;
            }
            else
            {
                itemMgr.FindItem(itemId).Quantity -= itemQuantity;
                Console.WriteLine(empname + " has removed " + itemQuantity + " of Item ID: " + itemId + " on " + DateTime.Now);
                Item i = new Item(itemId, temp.Name, temp.Price, itemQuantity, empname, DateTime.Now);
                tlMgr.CreateLogEntry("Item Removed", i, DateTime.Now);
                itemMgr.CreateUsageEntry(itemId, temp.Name, temp.Price, itemQuantity, empname, DateTime.Now);
                return true;
            }
        }

        private void InventoryReport()
        {
            Dictionary<int, Item> items = itemMgr.GetInventory();
            Console.WriteLine("\nAll items");
            Console.WriteLine("\t{0, -4} {1, -20} {2, -20}", "ID", "Name", "Quantity");
            foreach (Item i in items.Values)
            {
                DisplayItem(i);
            }
        }

        private void DisplayItem(Item i)
        {
            Console.WriteLine(
                "\t{0, -4} {1, -20} {2, -20}",
                i.ID,
                i.Name,
                i.Quantity);
        }

        private void DisplayAdd(Item i)
        {
            Console.WriteLine(
                "\t{0, -4} {1, -12} {2, -12} {3, -12} {4, -12}",
                i.ID,
                i.Name,
                i.Price,
                i.Quantity,
                i.DateCreated);
        }

        private void TransactionLog()
        {
            {
                List<TransactionLog> tls = tlMgr.GetTransactions();

                Console.WriteLine("\nTransaction Log:");
                Console.WriteLine("\t{0, -20} {1, -16} {2, -6} {3, -12} {4, -12} {5, -12}", "Date", "Type", "ID", "Name", "Employee", "Price");
                foreach (TransactionLog tl in tls)
                {
                    tlMgr.DisTransactions(tl);
                }
            }
        }

        private void FinancialReport()
        {
            itemMgr.TotalPrice();            
        }

        private void ReportUsageLog()
        {
            {
                tlMgr.Report();
            }
        }
    }
}
