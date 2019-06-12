using Respawn;

namespace IntegrationTest.IT
{
    public static class DbFixture
    {
        private static readonly Checkpoint Checkpoint = new Checkpoint
        {
            TablesToIgnore = new[]
            {
                "__EFMigrationsHistory",
                "Positions",
                "Teams",
            },
        };

        public static void Reset(string connectionString)
        {
            Checkpoint.Reset(connectionString).Wait();
        }
    }
}
