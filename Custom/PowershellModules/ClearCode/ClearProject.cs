using System;
using System.Management.Automation;

namespace ClearCode
{
    [Cmdlet(VerbsCommon.Clear, "Project")]
    public class ClearProject : PSCmdlet
    {
        protected override void EndProcessing()
        {
           
            base.EndProcessing();
        }
    }
}
