using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.ResourceStrings;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
	internal class OperatorValidator_SetOutletCount : VersatileValidator
	{
		public OperatorValidator_SetOutletCount(Operator op, int newCount)
		{
			if (op == null) throw new NullException(() => op);

			For(newCount, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Outlets)).GreaterThanOrEqual(1);

			if (!op.Outlets.Any(x => x.IsRepeating))
			{
				Messages.Add(ResourceFormatter.CannotSetOutletCountWithoutRepeatingOutlets);
			}

			IList<Outlet> sortedOutlets = op.Outlets.Sort().ToArray();
			for (int i = newCount; i < sortedOutlets.Count; i++)
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
