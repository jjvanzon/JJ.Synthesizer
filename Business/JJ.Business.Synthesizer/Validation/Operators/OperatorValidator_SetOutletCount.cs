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
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetOutletCount : FluentValidator<Operator>
    {
        private readonly int _newOutletCount;
        private static OperatorTypeEnum[] _allowedOperatorTypeEnums = new OperatorTypeEnum[]
        {
            OperatorTypeEnum.Unbundle,
            OperatorTypeEnum.DimensionToOutlets,
            OperatorTypeEnum.RangeOverOutlets
        };

        public OperatorValidator_SetOutletCount(Operator obj, int newOutletCount)
            : base(obj, postponeExecute: true)
        {
            _newOutletCount = newOutletCount;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            if (!_allowedOperatorTypeEnums.Contains(operatorTypeEnum))
            {
                string message = GetOperatorTypeNotAllowedMessage();
                ValidationMessages.Add(() => op.OperatorType, message);
            }

            For(_newOutletCount, PropertyNames.OutletCount, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets))
                .GreaterThan(0);

            IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
            for (int i = _newOutletCount; i < sortedOutlets.Count; i++)
            {
                Outlet outlet = sortedOutlets[i];

                if (outlet.ConnectedInlets.Count > 0)
                {
                    string message = MessageFormatter.CannotChangeOutletsBecauseOneIsStillFilledIn(i + 1);
                    ValidationMessages.Add(PropertyNames.Outlets, message);
                }
            }
        }

        private string GetOperatorTypeNotAllowedMessage()
        {
            IList<string> operatorTypeDisplayNames = _allowedOperatorTypeEnums.Select(x => ResourceHelper.GetDisplayName(x)).ToArray();
            string message = ValidationMessageFormatter.NotInList(PropertyDisplayNames.OperatorType, operatorTypeDisplayNames);
            return message;
        }
    }
}
