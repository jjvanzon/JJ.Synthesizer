using System;
using System.Reflection;

namespace JJ.Business.Synthesizer.Helpers
{
    public class CalculatorNotFoundException : Exception
    {
        private readonly string _message;

        public CalculatorNotFoundException(MethodBase method)
        {
            if (method != null)
            {
                _message = $"Error in {method.Name} optimization. No appropriate variation on the calculation was found.";
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
