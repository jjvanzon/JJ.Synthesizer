using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class PatchValidator : FluentValidator<Patch>
    {
        public PatchValidator(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            var alreadyDone = new HashSet<object>();

            string messagePrefix;
            if (String.IsNullOrEmpty(Object.Name))
            {
                messagePrefix = String.Format("{0}: ", PropertyDisplayNames.Patch);
            }
            else
            {
                messagePrefix = String.Format("{0} '{1}': ", PropertyDisplayNames.Patch, Object.Name);
            }

            foreach (Operator op in Object.Operators)
            {
                Execute(new RecursiveOperatorValidator(op, alreadyDone));
            }
        }
    }
}
