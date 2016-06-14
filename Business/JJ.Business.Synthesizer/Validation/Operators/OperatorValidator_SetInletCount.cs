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
            if (operatorTypeEnum != OperatorTypeEnum.Adder &&
                operatorTypeEnum != OperatorTypeEnum.Bundle &&
                operatorTypeEnum != OperatorTypeEnum.MakeContinuous)
            {
                ValidationMessages.Add(() => op.OperatorType, MessageFormatter.OperatorTypeMustBeAdderOrBundle());
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