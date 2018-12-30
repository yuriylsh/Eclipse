using System;

namespace ClearCode
{
    public class ProjectUtils
    {
        private readonly Action<string> _logError;

        public ProjectUtils(Action<string> logError)
        {
            _logError = logError;
        }
    }
}
