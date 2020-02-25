using FluentAssertions;
using IntegrationTest.Api.Models.Teams;
using IntegrationTest.Entities;
using IntegrationTest.IT.Attributes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.IT.Scenarios
{
    // SHOW 4
    [Collection("Test")]
    public class TeamScenarios
    {
        private readonly TestServerFixture _fixture;
        // SHOW 5
        public TeamScenarios(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        // SHOW 6 -->
        [Reset]
        public async Task Get_All_Teams_Return_OK()
        {
            _fixture.AplicationDbContext.Teams.Add(new Team { Id = Guid.NewGuid(), Name = "Real Sporting de Gijón" });
            await _fixture.AplicationDbContext.SaveChangesAsync();

            // SHOW  7
            var response = await _fixture.Server.CreateClient().GetAsync(Get.GetAllTeams);
            // SHOW 8
            response.Should().NotBeNull();
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);

            var content = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<Team>>(content);

            teams.Any().Should().BeTrue();
            teams.Count().Should().Be(1);
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
            var response = await _fixture.Server.CreateClient().PostAsync(Post.Team, content);

            response.Should().NotBeNull();
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);

            var contentResponse = await response.Content.ReadAsStringAsync();
            var teamResponse = JsonConvert.DeserializeObject<Team>(contentResponse);

            teamResponse.Should().NotBeNull();
            teamResponse.Name.Should().Be("UP Langreo");
        }

    }
}
