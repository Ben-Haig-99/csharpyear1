using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee_UI empUI = new Employee_UI();
            empUI.InitialiseData();
            empUI.MainMenu();
        }
    }
}
