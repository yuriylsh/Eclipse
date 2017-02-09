using Modules.LoadTestingData;

namespace iModules.LoadTestingResultsViewer.ViewModels
{
    public class ResultIdentifierViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public static ResultIdentifierViewModel FromResultIdentifier(ResultIdentifier result) => new ResultIdentifierViewModel
        {
            Id = result.Id.ToString(),
            Date = result.Date.LocalDateTime.ToString("f"),
            Name = result.Name
        };
    }
}
