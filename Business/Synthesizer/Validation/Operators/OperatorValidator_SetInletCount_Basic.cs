using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_SetInletCount_Basic : VersatileValidator
    {
        public OperatorValidator_SetInletCount_Basic(Operator op, int newInletCount)
        {
            if (op == null) throw new NullException(() => op);

            For(() => newInletCount, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Inlets)).GreaterThanOrEqual(1);

            IList<Inlet> sortedInlets = op.Inlets.Sort().ToArray();
            for (int i = newInletCount; i < sortedInlets.Count; i++)
            {
                Inlet inlet = sortedInlets[i];

                // ReSharper disable once InvertIf
                if (inlet.InputOutlet != null)
                {
                    string message = ResourceFormatter.CannotChangeInletsBecauseOneIsStillFilledIn(i + 1);
                    ValidationMessages.Add(nameof(op.Inlets), message);
                }
            }
        }
    }
}