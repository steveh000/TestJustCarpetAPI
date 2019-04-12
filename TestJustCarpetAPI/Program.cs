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

            List<Flooring> flooringb = client.Search(new Search() {SkipSearchParameters = true}).Result;
            Console.WriteLine("flooring returned  " + flooringb.Count + " Press any key to continue with parameters Pet Friendly");

            Console.ReadKey();

            //List<Flooring> flooringb = client.Search(new Search() { SkipSearchParameters = false, Pets = true}).Result;
            //Console.WriteLine("flooring returned  " + flooringb.Count + " Press any key to continue Find installers");
            try
            {
                Console.WriteLine("Geting flooring details");
                Flooring floor = client.GetFlooringDetails(flooringb.First().Id).Result;

                if (floor != null)
                {
                    Console.WriteLine("Details ");
                    Console.WriteLine("Name : " + floor.Name);
                    Console.WriteLine("Desc " + floor.Description);
                    foreach (var prop in floor.Properties)
                    {
                        Console.WriteLine(prop);
                    }
                }
                else
                {
                    Console.WriteLine("No details returned");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error has occured getting floor details,");
            }

            Console.WriteLine("Press any key to continue testing.");
            Console.ReadKey();



            Console.ReadKey();

            List<Installer> installers = client.GetInstallers().Result;

            Console.WriteLine("Installers returned " +installers.Count + " press any button to get available appointments for next seven days.");

            Console.ReadKey();

            Console.WriteLine("Getting Appointments for installer id " + installers.First().LocationId);

            var appointments = client.GetInstallerAppontments(installers.First().LocationId).Result;

            Console.WriteLine("Appointmests are as follows");

            if(appointments.Count == 0)
                Console.WriteLine("That installer has no appointments ");

            foreach (var date in appointments )
            {
                string text = "Installer has an apponmtment ";

                if (date.AM)
                {
                    text += "on the morning of " + date.Date;
                }
                else
                {
                    text += "on the afternoon  of " + date.Date;
                }

                Console.WriteLine(text);
            }

            Console.WriteLine("All appointments have been retured");

            Console.WriteLine("Press any key to continue testing ordering");

            Console.ReadKey();

            Console.WriteLine("We are now going to send an order to the API");

            Console.WriteLine("Order is for 7 days from today in the morning, with installer " + installers.First().Name);

            CreateOrderDto order = new CreateOrderDto()
            {
                CustomerId = customerAccount.Id,
                InstallerId = installers.First().LocationId,
                InstallDate = DateTime.Now.AddDays(14),
                AM = true
            };

            Flooring flooring = client.GetFlooringDetails(flooringb.First().Id).Result;

            order.OrderLines.Add(new OrderLine()
            {
                Qty = 10,
                CarpetId = flooring.Id,
                CarpetSizeOptionId = flooring.Sizes.First().Id
            });

            var response = client.Neworder(order).Result;

            if (response.OrderSucess)
            {
                Console.WriteLine("Order Sucessfull - Installer " + response.InstallerName + " will be at your property on " + response.InstallDate);
            }
            else
            {
                Console.WriteLine("Order failed ");
            }

            Console.WriteLine("Press any key to get your current orders - my account page");
            Console.ReadKey();

            var customer = client.Register("testMacAddress001").Result;

            Console.WriteLine("Customer has " + customer.Orders.Count);

            foreach (var o in customer.Orders)
            {
                Console.WriteLine("Order " + o.OrderId + " with installer " + o.InstallerName + " on " + o.InstallerShortDateString );
            }



            Console.WriteLine("Semd Review to system for first order. press any key to continue ");

            var reviewResponse = client.AddOrderReview(new Review()
            {
                CustomerOrderId = customer.Orders.First().OrderId,
                QualityFactor = 5,
                CleanupFactor = 2,
                TimeKeepingFactor = 10,
                Comments = "This is the first tes review on the system"
            }).Result;

            if (reviewResponse)
            {
                Console.WriteLine("Review has been accepted");
            }
            else
            {
                Console.WriteLine("Review Failed to upload");
            }

            Console.WriteLine("Press any key to finish testing");

            Console.ReadKey();

        

        }
    }
}
