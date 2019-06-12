using IntegrationTest.Api.Models.Teams;
using IntegrationTest.Entities;
using IntegrationTest.IT.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        [Fact]
        [Reset]
        public async Task Create_New_Team_And_Return_OK()
        {
            var team = new CreateTeam()
            {
                Name = "UP Langreo",
            };
            var content = new StringContent(JsonConvert.SerializeObject(team), UTF8Encoding.UTF8, "application/json");
            var response = await _fixture.Server.CreateClient()
                .PostAsync(Post.Team, content);

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Team>(json);
            Assert.Equal("UP Langreo", result.Name);
        }

    }
}
