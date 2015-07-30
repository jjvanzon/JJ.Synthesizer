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
using JJ.Business.Synthesizer.Exceptions;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Configuration;

namespace JJ.Business.Synthesizer.Validation
{
    public class CurveValidator : FluentValidator<Curve>
    {
        private HashSet<object> _alreadyDone;

        public CurveValidator(Curve obj, HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _alreadyDone = alreadyDone;

            Execute();
        }

        protected override void Execute()
        {
            if (_alreadyDone.Contains(Object))
            {
                return;
            }
            _alreadyDone.Add(Object);

            Curve curve = Object;

            Execute(new NameValidator(curve.Name, required: false));

            For(() => curve.Nodes.Count, CommonTitleFormatter.EntityCount(PropertyDisplayNames.Nodes)).MinValue(2);

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
