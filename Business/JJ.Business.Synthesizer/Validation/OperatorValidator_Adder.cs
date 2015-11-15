using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Adder : FluentValidator<Operator>
    {
        public OperatorValidator_Adder(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            For(() => op.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).Is(OperatorTypeEnum.Adder);

            for (int i = 0; i < op.Inlets.Count; i++)
            {
                For(() => op.Inlets[i].ListIndex, GetPropertyDisplayName_ForInletListIndex(i)).Is(i);
            }

            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount()).Is(1);

            if (op.Outlets.Count == 1)
            {
                For(() => op.Outlets[0].ListIndex, GetPropertyDisplayName_ForOutletListIndex()).Is(0);
            }

            For(() => op.Data, PropertyDisplayNames.Data).IsNull();
        }

        private string GetPropertyDisplayName_ForInletListIndex(int index)
        {
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Inlet, index + 1, PropertyDisplayNames.ListIndex);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonTitleFormatter.EntityCount(PropertyDisplayNames.Outlets);
        }

        private string GetPropertyDisplayName_ForOutletListIndex()
        {
            return String.Format("{0}: {1}", PropertyDisplayNames.Outlet, PropertyDisplayNames.ListIndex);
        }
    }
}