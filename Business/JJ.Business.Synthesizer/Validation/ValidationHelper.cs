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
                string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(op.OperatorType);
                return operatorTypeDisplayName;
            }

            return op.ID.ToString();
        }
    }
}
