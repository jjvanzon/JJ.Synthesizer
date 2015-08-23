using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Helpers
{
    public class AlreadyDoneIsNullException : Exception
    {
        public override string Message
        {
            get { return "alreadyDone is null. Pass a (new) HashSet<object>."; }
        }
    }
}