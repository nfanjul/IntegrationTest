using FluentAssertions;
using IntegrationTest.Api.Models.Teams;
using IntegrationTest.Entities;
using IntegrationTest.IT.Attributes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.IT.Scenarios
{
    // ITEST 5
    [Collection("Test")]
    public class TeamScenarios
    {
        private readonly TestServerFixture _fixture;

        // ITEST 6
        public TeamScenarios(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get_All_Teams_Return_OK()
        {
        // ITEST 7
            var response = await _fixture.Server.CreateRequest(Get.GetAllTeams).GetAsync();
            // ITEST 8
            response.Should().NotBeNull();
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);

            var content = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<Team>>(content);
            teams.Any().Should().BeTrue();
            teams.Count().Should().Be(3);
        }

        [Fact]
        // ITEST 9
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

            response.Should().NotBeNull();
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);

            var contentResponse = await response.Content.ReadAsStringAsync();
            var teamResponse = JsonConvert.DeserializeObject<Team>(contentResponse);
            teamResponse.Should().NotBeNull();
            teamResponse.Name.Should().Be("UP Langreo");
        }

    }
}
