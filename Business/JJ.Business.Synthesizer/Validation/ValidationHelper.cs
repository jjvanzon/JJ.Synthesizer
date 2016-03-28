using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal static partial class ValidationHelper
    {
        public static string GetOperatorIdentifier(Operator op)
        {
            if (!String.IsNullOrEmpty(op.Name))
            {
                return op.Name;
            }

            if (op.OperatorType != null)
            {
                string operatorTypeDisplayName = ResourceHelper.GetDisplayName(op.OperatorType);
                return operatorTypeDisplayName;
            }

            return op.ID.ToString();
        }

        public static string GetDataKeyIdentifier(string dataKey)
        {
            // TODO: The order of the placeholders might not work in every language.
            return String.Format("{0} '{1}'", PropertyDisplayNames.DataKey, dataKey);
        }
    }
}
