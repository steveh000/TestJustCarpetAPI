using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestJustCarpetAPI.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace TestJustCarpetAPI
{
    public class JustCarpetClient
    {
        private const string URL = "https://localhost:44384/";
       // public const string URL = "https://justcarpet.azurewebsites.net/";
        public const string customerResource = URL +"api/customer/";
        public const string flooringResource = URL + "api/flooring/";
        public const string installerResource = URL + "api/installer/";
        public const string InstallerAppointmentsResource = URL + "api/InstallerAvailability";
        public const string OrderResource = URL + "api/Order";
        public const string ReviewResource = URL + "api/Review";

        public JustCarpetClient()
        {
        }

        #region Customer 

        public async Task<Customer> Register(string macAddress)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    client.BaseAddress = new Uri(URL);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, customerResource + "?macAddress=" + macAddress);

                    request.Content = new StringContent(macAddress,Encoding.UTF8, "application/json");
                    
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(List<Customer>));

                        var customer =
                            JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());

                        return customer;
                    }
                    else
                    {

                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                return new Customer();
            }
        }

        public async Task<bool> UpdateCustomer(Customer model)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    client.BaseAddress = new Uri(URL);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, customerResource);

                    request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                       
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                return false;
            }
        }

        #endregion

        #region search

        public async Task<List<Flooring>> Search(Search search)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    client.BaseAddress = new Uri(URL);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, flooringResource);

                    request.Content = new StringContent(JsonConvert.SerializeObject(search), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var flooring =
                            JsonConvert.DeserializeObject<List<Flooring>>(await response.Content.ReadAsStringAsync());

                        return flooring;
                    }
                    else
                    {
                        return new List<Flooring>();
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                return new List<Flooring>();
            }
        }

        public async Task<Flooring> GetFlooringDetails(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    client.BaseAddress = new Uri(URL);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, flooringResource + "?id=" + id);

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {

                        var content = await response.Content.ReadAsStringAsync();

                        var flooring =
                            JsonConvert.DeserializeObject<Flooring>(content);

                        return flooring;
                    }
                    else
                    {
                        return new Flooring();
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            //should never get here.
            return new Flooring();
        }

        public async Task<List<Installer>> GetInstallers()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(URL);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, installerResource);
                    
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {

                        return JsonConvert.DeserializeObject<List<Installer>>(
                            await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return new List<Installer>();
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                return new List<Installer>();
            }
        }

        public async Task<List<Appointment>> GetInstallerAppontments(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(URL);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, InstallerAppointmentsResource + "?id=" + id);

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {

                        return JsonConvert.DeserializeObject<List<Appointment>>(
                            await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return new List<Appointment>();
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                return new List<Appointment>();
            }
        }

        public async Task<bool> AddOrderReview(Review model)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    client.BaseAddress = new Uri(URL);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ReviewResource);

                    request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                return false;
            }
        }

        public async Task<OrderConfirmation> Neworder(CreateOrderDto model)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    client.BaseAddress = new Uri(URL);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, OrderResource);

                    request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var confirmation =
                            JsonConvert.DeserializeObject<OrderConfirmation>(await response.Content.ReadAsStringAsync());

                        return confirmation;
                    }
                    else
                    {
                        return new OrderConfirmation()
                        {
                            OrderSucess = false
                        };
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                return new OrderConfirmation()
                {
                    OrderSucess = false
                };
            }
        }

        
        #endregion
    }
}
