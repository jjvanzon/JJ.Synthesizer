using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Resources
{
    public static class MessagesFormatter
    {
        public static string OperandNotSet(string operatorTypeName, string operatorName, string operandName)
        {
            return String.Format(Messages.OperandNotSet, operatorTypeName, operatorName, operandName);
        }

        public static string ValueOperatorValueIs0(string valueOperatorName)
        {
            return String.Format(Messages.ValueOperatorValueIs0, valueOperatorName);
        }

        internal static string UnsupportedOperatorTypeName(string operatorTypeName)
        {
            return String.Format(Messages.UnsupportedOperatorTypeName, operatorTypeName);
        }
    }
}
