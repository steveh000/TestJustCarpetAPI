using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace JustCarpet.Api.Tests
{
    public class JustCarpetClientTests : IClassFixture<JustCarpetApiClientFixture>
    {
        private readonly JustCarpetApiClientFixture _fixture;

        public JustCarpetClientTests(JustCarpetApiClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task JustCarpetClient_Register_Customer()
        {
            var result = await _fixture.Client.Register("macAddress1");
            
            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Steve Heasman");
        }

        [Fact]
        public async Task JustCarpetClient_Update_Customer()
        {
            var result = await _fixture.Client.UpdateCustomer(_fixture.Customer);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task JustCarpetClient_Search_FlooringList()
        {
            var results = await _fixture.Client.Search(_fixture.Search);

            results.Count.ShouldBe(2);
            results[0].Name.ShouldBe("Fuzzyness");
            results[1].Description.ShouldBe("Smooth Like Chocolate");
        }
    }
}
