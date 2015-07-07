using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    internal static class ValidationHelper
    {
        public static string GetMessagePrefix(Operator op)
        {
            if (op.OperatorType != null)
            {
                string operatorTypeDisplayName = ResourceHelper.GetPropertyDisplayName(op.OperatorType.Name);
                return ValidationHelper.GetMessagePrefix(operatorTypeDisplayName, op.Name);
            }
            else
            {
                return ValidationHelper.GetMessagePrefix(PropertyDisplayNames.Operator, op.Name);
            }
        }

        public static string GetMessagePrefix(string entityTypeDisplayName, string name)
        {
            string messagePrefix;
            if (String.IsNullOrEmpty(name))
            {
                messagePrefix = String.Format("{0}: ", entityTypeDisplayName);
            }
            else
            {
                messagePrefix = String.Format("{0} '{1}': ", entityTypeDisplayName, name);
            }
            return messagePrefix;
        }
    }
}
