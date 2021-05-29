using System;
using System.Reflection;

namespace JJ.Business.Synthesizer.Helpers
{
    public class CalculatorNotFoundException : Exception
    {
        public CalculatorNotFoundException(MethodBase method)
        {
            if (method != null)
            {
                Message = $"Error in {method.Name} optimization. No appropriate variation on the calculation was found.";
            }
            else
            {
                Message = "Error in optimization. No appropriate variation on the calculation was found.";
            }
        }

        public override string Message { get; }
    }
}
