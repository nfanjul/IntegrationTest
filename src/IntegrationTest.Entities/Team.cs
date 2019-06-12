using System;
using System.Collections.Generic;

namespace IntegrationTest.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}
