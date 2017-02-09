using System;

namespace Modules.LoadTestingData
{
    public class ResultIdentifier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
