using JJ.Business.Synthesizer.EntityWrappers;
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

            For(() => Object.OperatorTypeName, PropertyDisplayNames.OperatorTypeName)
                .IsValue(PropertyNames.Adder);

            For(() => Object.Outlets.Count, GetPropertyDisplayName_ForOutletCount())
                .IsValue(1);

            if (Object.Outlets.Count == 1)
            {
                // TODO: ExpressionToValueVisitor chokes on:
                // () => Object.Outlets[i].Name
                var outlet = Object.Outlets[0];
                For(() => outlet.Name, GetPropertyDisplayName_ForOutletName())
                    .IsValue(PropertyNames.Result);
            }
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