namespace PortfolioManager.Api.DTOs
{
    public class ProjectStatsDto
    {
        public int Total { get; set; }

        public int Active { get; set; }

        public int InProgress { get; set; }

        public int Completed { get; set; }

        public int Paused { get; set; }
    }
}
