using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class CurveValidator : FluentValidator<Curve>
    {
        public CurveValidator(Curve obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);
            
            Curve curve = Object;

            For(() => curve.Name, CommonTitles.Name)
                .NotInteger();

            For(() => curve.Nodes.Count, CommonTitlesFormatter.EntityCount(PropertyDisplayNames.Nodes))
                .Min(2);

            int i = 1;
            foreach (Node node in curve.Nodes)
            {
                string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Node, i);
                Execute(new NodeValidator(node), messagePrefix);

                i++;
            }
        }
    }
}
