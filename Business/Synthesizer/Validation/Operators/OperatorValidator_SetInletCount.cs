using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
	internal class OperatorValidator_SetInletCount : VersatileValidator
	{
		public OperatorValidator_SetInletCount(Operator op, int newCount)
		{
			if (op == null) throw new NullException(() => op);

			For(newCount, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Inlets)).GreaterThanOrEqual(1);

			if (!op.Inlets.Any(x => x.IsRepeating))
			{
				Messages.Add(ResourceFormatter.CannotSetInletCountWithoutRepeatingInlets);
			}

			IList<Inlet> sortedInlets = op.Inlets.Sort().ToArray();
			for (int i = newCount; i < sortedInlets.Count; i++)
			{
				Inlet inlet = sortedInlets[i];

				// ReSharper disable once InvertIf
				if (inlet.InputOutlet != null)
				{
					Messages.Add(ResourceFormatter.CannotChangeInletsBecauseOneIsStillFilledIn(i + 1));
				}
			}
		}
	}
}