using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class AdderValidator : FluentValidator<Operator>
    {
        public AdderValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);
            
            Operator op = Object;

            For(() => op.OperatorTypeName, PropertyDisplayNames.OperatorTypeName).Is(PropertyNames.Adder);

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
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Inlet, index + 1, PropertyDisplayNames.Name);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonTitlesFormatter.EntityCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayName_ForOutletName()
        {
            return String.Format("{0}: {1}", PropertyDisplayNames.Outlet, PropertyDisplayNames.Name);
        }
    }
}