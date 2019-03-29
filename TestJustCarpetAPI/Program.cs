using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestJustCarpetAPI.Models;

namespace TestJustCarpetAPI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome - Press any key to start tests");
            Console.ReadKey();

            JustCarpetClient client = new JustCarpetClient();
            var customerAccount = client.Register("testMacAddress001").Result;

            Console.WriteLine("Found Customer Account id " + customerAccount.Id);
            Console.WriteLine("Update Customer account ? Press any key");
            Console.ReadKey();

            customerAccount.Name = "Steve Heasman";
            customerAccount.Address = "20 Arbroath Grove , Hartlepool , TS25 5EW";
            customerAccount.EmailAddress = "Test@domain.com";

            var complete = client.UpdateCustomer(customerAccount).Result;

            if (complete)
            {
                Console.WriteLine("Account has been sucesfully updated");
            }
            else
            {
                Console.WriteLine("Account not updates error");
            }

            Console.WriteLine("Customer methods tested.");

            Console.WriteLine("Press any key to search for flooring without search");
            Console.ReadKey();

            List<Flooring> flooringa = client.Search(new Search() {SkipSearchParameters = true}).Result;
            Console.WriteLine("flooring returned  " + flooringa.Count + " Press any key to continue with parameters Pet Friendly");

            Console.ReadKey();

            List<Flooring> flooringb = client.Search(new Search() { SkipSearchParameters = false, Pets = true}).Result;
            Console.WriteLine("flooring returned  " + flooringb.Count + " Press any key to continue Find installers");

            Console.ReadKey();

            List<Installer> installers = client.GetInstallers().Result;

            Console.WriteLine("Installers returned " +installers.Count + " press any button to get available appointments for next seven days.");

            Console.ReadKey();



        }
    }
}
