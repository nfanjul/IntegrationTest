using IntegrationTest.Entities;
using IntegrationTest.IT.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.IT.Scenarios
{
    [Collection("Test")]
    public class TeamScenarios
    {
        private readonly TestServerFixture _fixture;
        public TeamScenarios(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Reset]
        public async Task Get_All_Teams_Return_OK()
        {
            var response = await _fixture.Server.CreateRequest(Get.GetAllTeams).GetAsync();
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Team>>(json);
            Assert.True(result.Any());
        }
    }
}
