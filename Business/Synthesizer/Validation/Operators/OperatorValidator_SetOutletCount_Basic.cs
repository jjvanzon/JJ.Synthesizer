using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetOutletCount_Basic : VersatileValidator
    {
        public OperatorValidator_SetOutletCount_Basic(Operator op, int newOutletCount)
        {
            if (op == null) throw new NullException(() => op);

            For(() => newOutletCount, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Outlets)).GreaterThan(0);

            IList<Outlet> sortedOutlets = op.Outlets.Sort().ToArray();
            for (int i = newOutletCount; i < sortedOutlets.Count; i++)
            {
                Outlet outlet = sortedOutlets[i];

                // ReSharper disable once InvertIf
                if (outlet.ConnectedInlets.Count > 0)
                {
                    string message = ResourceFormatter.CannotChangeOutletsBecauseOneIsStillFilledIn(i + 1);
                    ValidationMessages.Add(nameof(op.Outlets), message);
                }
            }
        }
    }
}
