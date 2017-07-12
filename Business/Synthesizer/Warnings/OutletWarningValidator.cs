using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OutletWarningValidator : VersatileValidator
    {
        public OutletWarningValidator([NotNull] Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (outlet.IsObsolete && outlet.ConnectedInlets.Count != 0)
            {
                Messages.Add(ResourceFormatter.ObsoleteButStillUsed);
            }
        }
    }
}
