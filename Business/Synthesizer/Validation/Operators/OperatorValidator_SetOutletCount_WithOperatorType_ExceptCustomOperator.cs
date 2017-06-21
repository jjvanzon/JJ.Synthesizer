using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetOutletCount_WithOperatorType_ExceptCustomOperator : VersatileValidator
    {
        private static readonly OperatorTypeEnum[] _allowedOperatorTypeEnums =
        {
            OperatorTypeEnum.DimensionToOutlets,
            OperatorTypeEnum.RangeOverOutlets
        };

        public OperatorValidator_SetOutletCount_WithOperatorType_ExceptCustomOperator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            if (!_allowedOperatorTypeEnums.Contains(operatorTypeEnum))
            {
                string message = GetOperatorTypeNotAllowedMessage();
                ValidationMessages.Add(() => op.OperatorType, message);
            }
        }

        private string GetOperatorTypeNotAllowedMessage()
        {
            IList<string> operatorTypeDisplayNames = _allowedOperatorTypeEnums.Select(x => ResourceFormatter.GetDisplayName(x)).ToArray();
            string message = ValidationResourceFormatter.NotInList(ResourceFormatter.OperatorType, operatorTypeDisplayNames);
            return message;
        }
    }
}
