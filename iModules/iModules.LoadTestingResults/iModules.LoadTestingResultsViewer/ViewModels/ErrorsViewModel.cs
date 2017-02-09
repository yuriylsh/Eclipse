using System.Collections.Generic;
using Modules.LoadTestingData;

namespace iModules.LoadTestingResultsViewer.ViewModels
{
    public class ErrorsViewModel
    {
        public IEnumerable<TestRunMessage> Messages { get; }

        public ErrorsViewModel(IEnumerable<TestRunMessage> messages)
        {
            Messages = messages;
        }
    }
}