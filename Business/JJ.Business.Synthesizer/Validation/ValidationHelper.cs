using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

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

        public static double? TryGetConstantNumberFromInlet(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            // Be tolerant in warning validations.
            if (inlet.InputOutlet == null)
            {
                return null;
            }

            if (inlet.InputOutlet.Operator == null)
            {
                return null;
            }

            if (inlet.InputOutlet.Operator.GetOperatorTypeEnum() != OperatorTypeEnum.Number)
            {
                return null;
            }

            if (!DataPropertyParser.DataIsWellFormed(inlet.InputOutlet.Operator.Data))
            {
                return null;
            }

            string numberString = DataPropertyParser.TryGetString(inlet.InputOutlet.Operator, PropertyNames.Number);
            double number;
            if (!Double.TryParse(numberString, out number))
            {
                return null;
            }

            return number;
        }
    }
}
