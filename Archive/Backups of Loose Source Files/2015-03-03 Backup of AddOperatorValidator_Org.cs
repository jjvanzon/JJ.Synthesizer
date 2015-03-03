using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class AddOperatorValidator_Org : FluentValidator<Operator>
    {
        public AddOperatorValidator_Org(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            For(() => Object.OperatorTypeName, PropertyDisplayNames.Name)
                .IsValue(PropertyNames.Add);

            For(() => Object.Inlets.Count, GetPropertyDisplayNameInletCount())
                .IsValue(2);

            if (Object.Inlets.Count == 2)
            {
                For(() => Object.Inlets[0].Name, GetPropertyDisplayNameInlet(0))
                    .IsValue(PropertyNames.OperandA);

                For(() => Object.Inlets[1].Name, GetPropertyDisplayNameInlet(1))
                    .IsValue(PropertyNames.OperandB);
            }

            For(() => Object.Outlets.Count, GetPropertyDisplayNameOutletCount())
                .IsValue(1);

            if (Object.Outlets.Count == 1)
            {
                For(() => Object.Outlets[0].Name, GetPropertyDisplayNameOutlet(0))
                    .IsValue(PropertyNames.Result);
            }
        }

        private string GetPropertyDisplayNameInletCount()
        {
            return CommonTitlesFormatter.EntityCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayNameOutletCount()
        {
            return CommonTitlesFormatter.EntityCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayNameInlet(int index)
        {
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Inlet, index + 1, PropertyDisplayNames.Name);
        }

        private string GetPropertyDisplayNameOutlet(int index)
        {
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Outlet, index + 1, PropertyDisplayNames.Name);
        }
    }
}
