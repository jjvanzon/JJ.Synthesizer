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
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    /// <summary> Validates the inlet and outlet ListIndexes and that the inlet names are NOT filled in. </summary>
    public abstract class OperatorValidator_Base : FluentValidator<Operator>
    {
        private OperatorTypeEnum _expectedOperatorTypeEnum;
        private int _expectedInletCount;
        private int _expectedOutletCount;

        private IList<string> _allowedDataKeys;

        public OperatorValidator_Base(
            Operator obj,
            OperatorTypeEnum expectedOperatorTypeEnum,
            int expectedInletCount,
            int expectedOutletCount,
            IList<string> allowedDataKeys)
            : base(obj, postponeExecute: true)
        {
            if (expectedInletCount < 0) throw new LessThanException(() => expectedInletCount, 0);
            if (expectedOutletCount < 0) throw new LessThanException(() => expectedOutletCount, 0);
            if (allowedDataKeys == null) throw new NullException(() => allowedDataKeys);

            int uniqueExpectedDataPropertyKeyCount = allowedDataKeys.Distinct().Count();
            if (uniqueExpectedDataPropertyKeyCount != allowedDataKeys.Count)
            {
                throw new NotUniqueException(() => allowedDataKeys);
            }

            _expectedOperatorTypeEnum = expectedOperatorTypeEnum;
            _expectedInletCount = expectedInletCount;
            _expectedOutletCount = expectedOutletCount;
            _allowedDataKeys = allowedDataKeys;

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
                    Execute(new InletValidator_ForOtherOperator(inlet, i), messagePrefix);
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
                    Execute(new OutletValidator_ForOtherOperator(outlet, i), messagePrefix);
                }
            }

            Execute(new OperatorValidator_Data(op, _allowedDataKeys));
        }

        private string GetPropertyDisplayName_ForInletCount()
        {
            return CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets);
        }
    }
}
