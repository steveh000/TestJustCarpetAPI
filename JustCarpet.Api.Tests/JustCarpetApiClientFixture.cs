using JustCarpet.Api.Models;
using JustCarpet.Api.Models.Flooring;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Serilog;
using System;
using System.Collections.Generic;

namespace JustCarpet.Api.Tests
{
    public class JustCarpetApiClientFixture : IDisposable
    {
        public IJustCarpetClient Client { get; private set; }

        public JustCarpetApiClientFixture()
        {
            var mockHttp = new MockHttpMessageHandler();

            // Register
            mockHttp.When("https://localhost:44384/api/customer/")
                    .WithQueryString("macAddress", "macAddress1")
                    .Respond("application/json", JsonConvert.SerializeObject(Customer));

            // Update
            mockHttp.When("https://localhost:44384/api/customer/")
                    .WithContent(JsonConvert.SerializeObject(Customer))
                    .Respond("application/json", JsonConvert.SerializeObject(true));

            // Search
            mockHttp.When("https://localhost:44384/api/flooring/")
                    .WithContent(JsonConvert.SerializeObject(Search))
                    .Respond("application/json", JsonConvert.SerializeObject(FlooringList));

            var client = mockHttp.ToHttpClient();
            var logger = new Mock<ILogger>().Object;
            Client = new JustCarpetClient(logger, client, Enums.ClientTypeEnum.Test);
        }

        public Customer Customer => new Customer()
        {
            Id = 1,
            Name = "Steve Heasman",
            Address = "20 Arbroath Grove , Hartlepool , TS25 5EW",
            EmailAddress = "Test@domain.com"
        };

        public List<Flooring> FlooringList => new List<Flooring>()
        {
            new Flooring()
            {
                Id = 1,
                Description = "Fuzzy Crap",
                DurabilityFactor = 10,
                Name = "Fuzzyness",
                PetFriendly = false,
                PriceM2 = 10000,
                Style = Enums.CarpetStyleEnum.Contemporary
            },
            new Flooring()
            {
                Id = 2,
                Description = "Smooth Like Chocolate",
                DurabilityFactor = 1,
                Name = "Chocolate",
                PetFriendly = true,
                PriceM2 = 20,
                Style = Enums.CarpetStyleEnum.Unknown
            }
        };

        public Search Search => new Search()
        {
            SkipSearchParameters = true
        };

        public void Dispose()
        { }
    }
}
