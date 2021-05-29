using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OutletWarningValidator : VersatileValidator
    {
        public OutletWarningValidator(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (outlet.IsObsolete && outlet.ConnectedInlets.Count != 0)
            {
                Messages.Add(ResourceFormatter.ObsoleteButStillUsed);
            }
        }
    }
}
