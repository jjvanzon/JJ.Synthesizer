using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    public class VisitationCannotBeHandledException : Exception
    {
        private string _message;

        public VisitationCannotBeHandledException(MethodBase method)
        {
            if (method != null)
            {
                _message = String.Format("Error in {0}. The variation could not be handled.", method.Name);
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
