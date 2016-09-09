using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetInletCount : FluentValidator<Operator>
    {
        private static readonly HashSet<OperatorTypeEnum> _allowedOperatorTypeEnums = new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.Add,
            OperatorTypeEnum.AverageOverInlets,
            OperatorTypeEnum.Bundle,
            OperatorTypeEnum.ClosestOverInlets,
            OperatorTypeEnum.ClosestOverInletsExp,
            OperatorTypeEnum.MakeContinuous,
            OperatorTypeEnum.MaxOverInlets,
            OperatorTypeEnum.MinOverInlets,
            OperatorTypeEnum.Multiply,
            OperatorTypeEnum.SortOverInlets
        };

        private static readonly IList<string> _allowedOperatorTypeDisplayNames = _allowedOperatorTypeEnums.Select(x => ResourceHelper.GetDisplayName(x)).ToArray();

        private readonly int _newInletCount;

        public OperatorValidator_SetInletCount(Operator obj, int newInletCount)
            : base(obj, postponeExecute: true)
        {
            _newInletCount = newInletCount;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            if (!_allowedOperatorTypeEnums.Contains(operatorTypeEnum))
            {
                ValidationMessages.AddNotInListMessage(() => operatorTypeEnum, PropertyDisplayNames.OperatorType, _allowedOperatorTypeDisplayNames);
                return;
            }

            For(_newInletCount, PropertyNames.InletCount, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets))
                .GreaterThan(1);

            IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.ListIndex).ToArray();
            for (int i = _newInletCount; i < sortedInlets.Count; i++)
            {
                Inlet inlet = sortedInlets[i];

                if (inlet.InputOutlet != null)
                {
                    string message = MessageFormatter.CannotChangeInletsBecauseOneIsStillFilledIn(i + 1);
                    ValidationMessages.Add(PropertyNames.Inlets, message);
                }
            }
        }
    }
}