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
    internal class OperatorValidator_SetOutletCount : VersatileValidator<Operator>
    {
        private readonly int _newOutletCount;
        private static readonly OperatorTypeEnum[] _allowedOperatorTypeEnums = new[]
        {
            OperatorTypeEnum.DimensionToOutlets,
            OperatorTypeEnum.RangeOverOutlets
        };

        public OperatorValidator_SetOutletCount(Operator obj, int newOutletCount)
            : base(obj, postponeExecute: true)
        {
            _newOutletCount = newOutletCount;

            Execute();
        }

        protected sealed override void Execute()
        {
            Operator op = Obj;

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            if (!_allowedOperatorTypeEnums.Contains(operatorTypeEnum))
            {
                string message = GetOperatorTypeNotAllowedMessage();
                ValidationMessages.Add(() => op.OperatorType, message);
            }

            For(_newOutletCount, PropertyNames.OutletCount, CommonResourceFormatter.ObjectCount(ResourceFormatter.Outlets))
                .GreaterThan(0);

            IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
            for (int i = _newOutletCount; i < sortedOutlets.Count; i++)
            {
                Outlet outlet = sortedOutlets[i];

                // ReSharper disable once InvertIf
                if (outlet.ConnectedInlets.Count > 0)
                {
                    string message = ResourceFormatter.CannotChangeOutletsBecauseOneIsStillFilledIn(i + 1);
                    ValidationMessages.Add(PropertyNames.Outlets, message);
                }
            }
        }

        private string GetOperatorTypeNotAllowedMessage()
        {
            IList<string> operatorTypeDisplayNames = _allowedOperatorTypeEnums.Select(x => ResourceFormatter.GetText(x)).ToArray();
            string message = ValidationMessageFormatter.NotInList(ResourceFormatter.OperatorType, operatorTypeDisplayNames);
            return message;
        }
    }
}
