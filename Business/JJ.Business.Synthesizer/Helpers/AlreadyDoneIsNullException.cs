using System;

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