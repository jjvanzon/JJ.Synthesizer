using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace JJ.Business.Synthesizer.Helpers
{
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class CalculatorNotFoundException : Exception
    {
        private string _message;

        public CalculatorNotFoundException(MethodBase method)
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
