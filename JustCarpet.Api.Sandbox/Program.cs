using JustCarpet.Api;
using JustCarpet.Api.Models;
using JustCarpet.Api.Models.Flooring;
using JustCarpet.Api.Models.Orders;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestJustCarpetAPI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            Log.Information("Welcome - Press any key to start tests");
            Console.ReadKey();
            
            IJustCarpetClient client = new JustCarpetClient(Log.Logger, clientType: JustCarpet.Api.Enums.ClientTypeEnum.Live);
            var customerAccount = client.Register("testMacAddress001").Result;

            Log.Information("Found Customer Account id {@id}", customerAccount.Id);
            Log.Information("Update Customer account ? Press any key");
            Console.ReadKey();

            customerAccount.Name = "Steve Heasman";
            customerAccount.Address = "20 Arbroath Grove , Hartlepool , TS25 5EW";
            customerAccount.EmailAddress = "Test@domain.com";

            var complete = client.UpdateCustomer(customerAccount).Result;

            if (complete)
            {
                Log.Information("Account has been sucesfully updated");
            }
            else
            {
                Log.Information("Account not updates error");
            }

            Log.Information("Customer methods tested.");

            Log.Information("Press any key to search for flooring without search");
            Console.ReadKey();

            List<Flooring> flooringb = client.Search(new Search() {SkipSearchParameters = true}).Result;
            Log.Information("flooring returned  {@Count}. Press any key to continue with parameters Pet Friendly", flooringb.Count);

            Console.ReadKey();

            //List<Flooring> flooringb = client.Search(new Search() { SkipSearchParameters = false, Pets = true}).Result;
            //Log.Information("flooring returned  " + flooringb.Count + " Press any key to continue Find installers");
            try
            {
                Log.Information("Geting flooring details");
                Flooring floor = client.GetFlooringDetails(flooringb.First().Id).Result;

                if (floor != null)
                {
                    Log.Information("Details ");
                    Log.Information("Name : {@Name}", floor.Name);
                    Log.Information("Desc : {@Desc}", floor.Description);
                    foreach (var prop in floor.Properties)
                    {
                        Log.Information(prop);
                    }
                }
                else
                {
                    Log.Information("No details returned");
                }

            }
            catch (Exception ex)
            {
                Log.Error("An Error has occured getting floor details '{@Error}'", ex.Message);
            }

            Log.Information("Press any key to continue testing.");
            Console.ReadKey();



            Console.ReadKey();

            List<Installer> installers = client.GetInstallers().Result;

            Log.Information("Installers returned {@Count}. Press any button to get available appointments for next seven days.", installers.Count);

            Console.ReadKey();

            Log.Information("Getting Appointments for installer id " + installers.First().LocationId);

            var appointments = client.GetInstallerAppontments(installers.First().LocationId).Result;

            Log.Information("Appointmests are as follows");

            if(appointments.Count == 0)
                Log.Information("That installer has no appointments ");

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

                Log.Information(text);
            }

            Log.Information("All appointments have been retured");

            Log.Information("Press any key to continue testing ordering");

            Console.ReadKey();

            Log.Information("We are now going to send an order to the API");

            Log.Information("Order is for 7 days from today in the morning, with installer {@Installer}", installers.First().Name);

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
                Log.Information("Order Sucessfull - Installer {@InstallerName} will be at your property on {@InstallationDate}", response.InstallerName, response.InstallDate);
            }
            else
            {
                Log.Information("Order failed ");
            }

            Log.Information("Press any key to get your current orders - my account page");
            Console.ReadKey();

            var customer = client.Register("testMacAddress001").Result;

            Log.Information("Customer has {@Count}", customer.Orders.Count);

            foreach (var o in customer.Orders)
            {
                Log.Information("Order {@Id} with installer {@Installer} on {@Date}", o.OrderId, o.InstallerName, o.InstallerShortDateString );
            }



            Log.Information("Semd Review to system for first order. press any key to continue ");

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
                Log.Information("Review has been accepted");
            }
            else
            {
                Log.Information("Review Failed to upload");
            }

            Log.Information("Press any key to finish testing");

            Console.ReadKey();

        

        }
    }
}
