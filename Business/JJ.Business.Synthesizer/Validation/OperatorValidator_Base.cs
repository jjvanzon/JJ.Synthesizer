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
    /// <summary> Validates the configuration of names, inlets and outlets. </summary>
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
                    // TODO: You should really be using a sub validator here.
                    string prefix = String.Format("{0} {1}: ", PropertyDisplayNames.Inlet, i + 1);
                    For(() => sortedInlets[i].ListIndex, prefix + PropertyDisplayNames.ListIndex).Is(i);
                    For(() => sortedInlets[i].Name, prefix + CommonTitles.Name).IsNullOrEmpty();
                }
            }

            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount())
                .Is(_expectedOutletCount);

            if (op.Outlets.Count == _expectedOutletCount)
            {
                IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedOutlets.Count; i++)
                {
                    // TODO: You should really be using a sub validator here.
                    string prefix = String.Format("{0} {1}: ", PropertyDisplayNames.Outlet, i + 1);
                    For(() => sortedOutlets[i].ListIndex, prefix + PropertyDisplayNames.ListIndex).Is(i);
                    For(() => sortedOutlets[i].Name, prefix + CommonTitles.Name).IsNullOrEmpty();
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
