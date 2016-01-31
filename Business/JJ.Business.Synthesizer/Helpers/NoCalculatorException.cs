using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JJ.Business.Synthesizer.Helpers
{
    public class NoCalculatorException : Exception
    {
        private string _message;

        public NoCalculatorException(MethodBase method)
        {
            if (method != null)
            {
                _message = String.Format("Error in {0} optimization. No appropriate variation on the calculation was found.", method.Name);
            }
            else
            {
                _message = "Error in optimization. No appropriate variation on the calculation was found.";
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
