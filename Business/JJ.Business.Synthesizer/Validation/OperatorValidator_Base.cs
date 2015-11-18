using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary> Validates the inlet and outlet ListIndexes and that the inlet names are NOT filled in. </summary>
    public abstract class OperatorValidator_Base : FluentValidator<Operator>
    {
        private OperatorTypeEnum _expectedOperatorTypeEnum;
        private int _expectedInletCount;
        private int _expectedOutletCount;

        public OperatorValidator_Base(
            Operator obj, 
            OperatorTypeEnum expectedOperatorTypeEnum, 
            int expectedInletCount, 
            int expectedOutletCount)
            : base(obj, postponeExecute: true)
        {
            if (expectedInletCount < 0) throw new LessThanException(() => expectedInletCount, 0);
            if (expectedOutletCount < 0) throw new LessThanException(() => expectedOutletCount, 0);

            _expectedOperatorTypeEnum = expectedOperatorTypeEnum;
            _expectedInletCount = expectedInletCount;
            _expectedOutletCount = expectedOutletCount;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            For(() => op.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).Is(_expectedOperatorTypeEnum);

            For(() => op.Inlets.Count, GetPropertyDisplayName_ForInletCount())
                .Is(_expectedInletCount);

            if (op.Inlets.Count == _expectedInletCount)
            {
                IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedInlets.Count; i++)
                {
                    Inlet inlet = sortedInlets[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(inlet, i + 1);

                    Execute(new InletValidator_Basic(inlet, i), messagePrefix);
                    Execute(new InletValidator_ForOtherOperator(inlet), messagePrefix);
                }
            }

            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount())
                .Is(_expectedOutletCount);

            if (op.Outlets.Count == _expectedOutletCount)
            {
                IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedOutlets.Count; i++)
                {
                    Outlet outlet = sortedOutlets[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(outlet, i + 1);

                    Execute(new OutletValidator_Basic(outlet, i), messagePrefix);
                    Execute(new OutletValidator_ForOtherOperator(outlet), messagePrefix);
                }
            }
        }

        private string GetPropertyDisplayName_ForInletCount()
        {
            return CommonTitleFormatter.EntityCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonTitleFormatter.EntityCount(PropertyDisplayNames.Outlets);
        }
    }
}
