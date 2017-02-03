using System;
using System.Reflection;

namespace JJ.Business.Synthesizer.Helpers
{
    public class VisitationCannotBeHandledException : Exception
    {
        public VisitationCannotBeHandledException(MethodBase method)
        {
            if (method != null)
            {
                Message = $"Error in {method.Name}. The variation could not be handled.";
            }
            else
            {
                Message = "Error in visitation. The variation could not be handled.";
            }
        }

        public override string Message { get; }
    }
}
