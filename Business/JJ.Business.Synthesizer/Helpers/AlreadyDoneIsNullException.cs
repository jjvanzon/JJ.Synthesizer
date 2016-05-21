using System;
using System.Diagnostics.CodeAnalysis;

namespace JJ.Business.Synthesizer.Helpers
{
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class AlreadyDoneIsNullException : Exception
    {
        public override string Message
        {
            get { return "alreadyDone is null. Pass a (new) HashSet<object>."; }
        }
    }
}