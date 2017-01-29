using System;
using System.Reflection;

namespace JJ.Business.SynthesizerPrototype.Helpers
{
    public class VisitationCannotBeHandledException : Exception
    {
        private readonly string _message;

        public VisitationCannotBeHandledException(MethodBase method)
        {
            if (method != null)
            {
                _message = string.Format("Error in {0}. The variation could not be handled.", method.Name);
            }
            else
            {
                _message = "Error in visitation. The variation could not be handled.";
            }
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
