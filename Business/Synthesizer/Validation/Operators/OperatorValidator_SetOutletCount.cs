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
	internal class OperatorValidator_SetOutletCount : VersatileValidator
	{
		public OperatorValidator_SetOutletCount(Operator op, int newOutletCount)
		{
			if (op == null) throw new NullException(() => op);

			For(newOutletCount, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Outlets)).GreaterThanOrEqual(1);

			if (!op.Outlets.Where(x => x.IsRepeating).Any())
			{
				Messages.Add(ResourceFormatter.CannotSetOutletCountWithoutRepeatingOutlets);
			}

			IList<Outlet> sortedOutlets = op.Outlets.Sort().ToArray();
			for (int i = newOutletCount; i < sortedOutlets.Count; i++)
			{
				Outlet outlet = sortedOutlets[i];

				// ReSharper disable once InvertIf
				if (outlet.ConnectedInlets.Count > 0)
				{
					Messages.Add(ResourceFormatter.CannotChangeOutletsBecauseOneIsStillFilledIn(i + 1));
				}
			}
		}
	}
}
