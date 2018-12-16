using System;
using Gtk;

namespace Haushaltsplan
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            Application.Init();

            new PaymentsWindow();

            Application.Run();

        }
    }
}
