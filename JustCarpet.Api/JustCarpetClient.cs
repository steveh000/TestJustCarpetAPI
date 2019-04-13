using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using JustCarpet.Api.Enums;
using JustCarpet.Api.Models.Flooring;
using JustCarpet.Api.Models.Orders;
using JustCarpet.Api.Models;
using Serilog;

namespace JustCarpet.Api
{
    public class JustCarpetClient : IJustCarpetClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        private readonly string URL;
        private string CustomerResource => $"{URL}/api/customer/";
        private string FlooringResource => $"{URL}/api/flooring/";
        private string InstallerResource => $"{URL}/api/installer/";
        private string InstallerAppointmentsResource => $"{URL}/api/InstallerAvailability";
        private string OrderResource => $"{URL}/api/Order";
        private string ReviewResource => $"{URL}/api/Review";

        public JustCarpetClient(ILogger logger, HttpClient httpclient = null, ClientTypeEnum clientType = ClientTypeEnum.Live)
        {
            URL = clientType == ClientTypeEnum.Live ?
                "https://justcarpet.azurewebsites.net" :
                "https://localhost:44384";
            _logger = logger;
            _httpClient = httpclient ?? new HttpClient { BaseAddress = new Uri(URL) };
        }

        public async Task<Customer> Register(string macAddress)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, CustomerResource + "?macAddress=" + macAddress)
                {
                    Content = new StringContent(macAddress, Encoding.UTF8, "application/json")
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var serializer = new DataContractJsonSerializer(typeof(List<Customer>));

                    var customer =
                        JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());

                    return customer;
                }
            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error: {@error}", e.Message);
            }

            return null;
        }

        public async Task<bool> UpdateCustomer(Customer model)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, CustomerResource)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error: {@error}", e.Message);
            }

            return false;
        }

        public async Task<List<Flooring>> Search(Search search)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, FlooringResource)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(search), Encoding.UTF8, "application/json")
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var flooring =
                        JsonConvert.DeserializeObject<List<Flooring>>(await response.Content.ReadAsStringAsync());

                    return flooring;
                }
            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error: {@error}", e.Message);
            }

            return new List<Flooring>();
        }

        public async Task<Flooring> GetFlooringDetails(int id)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, FlooringResource + "?id=" + id);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();

                    var flooring = JsonConvert.DeserializeObject<Flooring>(content);

                    return flooring;
                }

            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error: {@error}", e.Message);
            }

            return new Flooring();
        }

        public async Task<List<Installer>> GetInstallers()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, InstallerResource);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<List<Installer>>(
                        await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error: {@error}", e.Message);
            }

            return new List<Installer>();
        }

        public async Task<List<Appointment>> GetInstallerAppontments(int id)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, InstallerAppointmentsResource + "?id=" + id);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<List<Appointment>>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error: {@error}", e.Message);
            }

            return new List<Appointment>();
        }

        public async Task<bool> AddOrderReview(Review model)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ReviewResource)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error: {@error}", e.Message);
            }

            return false;
        }

        public async Task<OrderConfirmation> Neworder(CreateOrderDto model)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, OrderResource)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var confirmation =
                        JsonConvert.DeserializeObject<OrderConfirmation>(await response.Content.ReadAsStringAsync());

                    return confirmation;
                }
            }
            catch (HttpRequestException e)
            {
                _logger.Error("Error: {@error}", e.Message);
            }

            return new OrderConfirmation()
            {
                OrderSucess = false
            };
        }
    }
}