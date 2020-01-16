namespace IntegrationTest.IT
{
    public static class Get
    {
        public static string GetAllTeams => $"api/teams";
        public static string GetFilterTeam(string teamName) => $"api/teams?teamName={teamName}";
    }

    public static class Post
    {
        public static string Team => $"api/teams";
    }
}
