using System;

namespace iModules.LoadTestingData
{
    public class ResultIdentifier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
