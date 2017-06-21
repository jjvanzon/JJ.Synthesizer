using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetInletCount_WithOperatorType_ExceptCustomOperator : VersatileValidator
    {
        private static readonly HashSet<OperatorTypeEnum> _allowedOperatorTypeEnums = new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.Add,
            OperatorTypeEnum.AverageOverInlets,
            OperatorTypeEnum.ClosestOverInlets,
            OperatorTypeEnum.ClosestOverInletsExp,
            OperatorTypeEnum.InletsToDimension,
            OperatorTypeEnum.MaxOverInlets,
            OperatorTypeEnum.MinOverInlets,
            OperatorTypeEnum.Multiply,
            OperatorTypeEnum.SortOverInlets
        };

        private static readonly IList<string> _allowedOperatorTypeDisplayNames = _allowedOperatorTypeEnums.Select(x => ResourceFormatter.GetDisplayName(x)).ToArray();

        public OperatorValidator_SetInletCount_WithOperatorType_ExceptCustomOperator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            if (!_allowedOperatorTypeEnums.Contains(operatorTypeEnum))
            {
                ValidationMessages.AddNotInListMessage(() => operatorTypeEnum, ResourceFormatter.OperatorType, _allowedOperatorTypeDisplayNames);
            }
        }
    }
}