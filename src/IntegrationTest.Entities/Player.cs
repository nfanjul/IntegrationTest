using System;

namespace IntegrationTest.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Position Position { get; set; }
    }
}
