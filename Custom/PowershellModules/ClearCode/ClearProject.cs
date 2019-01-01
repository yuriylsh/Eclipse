using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;

namespace ClearCode
{
    [Cmdlet(VerbsCommon.Clear, "Project")]
    public class ClearProject : PSCmdlet
    {
        protected override void EndProcessing()
        {
            var config = ConfigurationLoader.LoadConfiguration(GetVariableValue);
            var utils = new ProjectUtils(config, LogError);
            var projectFile = utils.GetProjectFile(SessionState.Path.CurrentFileSystemLocation.Path);
            if (projectFile != null)
            {
                WriteObject("Clearing project '" + projectFile + "'");
                PerformClearing(projectFile, utils);
            }

            base.EndProcessing();
        }

        private void LogError(string msg)
        {
            WriteError(new ErrorRecord(new Exception(msg), "Clear-Project", ErrorCategory.InvalidOperation, null));
        }

        private void PerformClearing(string projectFile, ProjectUtils utils)
        {
            var (directories, files) = utils.FindEntriesToClear(projectFile);
            ReportEntriesToClear(files, directories);
            Thread.Sleep(500);
            using (var ps = PowerShell.Create(RunspaceMode.NewRunspace))
            {
                ps.AddCommand("Remove-Item")
                    .AddParameter("Path", files.Concat(directories).ToArray())
                    .AddParameter("Force", true)
                    .AddParameter("Recurse", true);
                ps.InvocationStateChanged += ClearingStateChanged;
                ps.Invoke();
                ps.InvocationStateChanged -= ClearingStateChanged;
            }
        }

        private void ReportEntriesToClear(string[] files, string[] directories)
        {
            var entries = new List<object>(files.Length + directories.Length);
            entries.AddRange(files.Select(x => new {Type="File", Path=x}));
            entries.AddRange(directories.Select(x => new {Type="Directory", Path=x}));
            WriteObject("The following entries are going to be deleted:");
            WriteObject(entries, true);
            WriteObject(Environment.NewLine);
        }

        private void ClearingStateChanged(object sender, PSInvocationStateChangedEventArgs args)
        {
            switch (args.InvocationStateInfo.State)
            {
                case PSInvocationState.Completed:
                    WriteObject("Completed.");
                    WriteObject(args.InvocationStateInfo.Reason);
                    break;
                case PSInvocationState.Disconnected:
                    WriteObject("Disconnected.");
                    WriteObject(args.InvocationStateInfo.Reason);
                    break;
                case PSInvocationState.Failed:
                    WriteObject("Failed.");
                    WriteObject(args.InvocationStateInfo.Reason);
                    break;
                case PSInvocationState.NotStarted:
                    WriteObject("Not started.");
                    WriteObject(args.InvocationStateInfo.Reason);
                    break;
                case PSInvocationState.Running:
                    WriteObject("Running.");
                    WriteObject(args.InvocationStateInfo.Reason);
                    break;
                case PSInvocationState.Stopped:
                    WriteObject("Stopped.");
                    WriteObject(args.InvocationStateInfo.Reason);
                    break;
                case PSInvocationState.Stopping:
                    WriteObject("Stopping.");
                    WriteObject(args.InvocationStateInfo.Reason);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
