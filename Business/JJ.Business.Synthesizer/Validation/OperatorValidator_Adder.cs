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
    public class OperatorValidator_Adder : FluentValidator<Operator>
    {
        public OperatorValidator_Adder(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            For(() => op.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).Is(OperatorTypeEnum.Adder);

            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount()).Is(1);

            for (int i = 0; i < op.Inlets.Count; i++)
            {
                string expectedName = String.Format("{0}{1}", PropertyNames.Operand, i + 1);

                For(() => op.Inlets[i].Name, GetPropertyDisplayName_ForInletName(i)).Is(expectedName);
            }

            if (op.Outlets.Count == 1)
            {
                For(() => op.Outlets[0].Name, GetPropertyDisplayName_ForOutletName()).Is(PropertyNames.Result);
            }

            For(() => op.Data, PropertyDisplayNames.Data).IsNull();
        }

        private string GetPropertyDisplayName_ForInletName(int index)
        {
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Inlet, index + 1, CommonTitles.Name);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonTitleFormatter.EntityCount(PropertyDisplayNames.Outlets);
        }

        private string GetPropertyDisplayName_ForOutletName()
        {
            return String.Format("{0}: {1}", PropertyDisplayNames.Outlet, CommonTitles.Name);
        }
    }
}