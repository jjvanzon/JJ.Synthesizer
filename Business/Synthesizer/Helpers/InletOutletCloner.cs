using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Business.Synthesizer.Helpers
{
	internal static class InletOutletCloner
	{
		public static void Clone(Inlet source, Inlet dest)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (dest == null) throw new ArgumentNullException(nameof(dest));

			// ReSharper disable RedundantCast
			Clone((IInletOrOutlet)source, (IInletOrOutlet)dest);
			dest.DefaultValue = source.DefaultValue;
			dest.WarnIfEmpty = source.WarnIfEmpty;
		}

		// NOTE: Outlet does not have any extra properties that need to be cloned other than those of IInletOrOutlet.

		public static void Clone<T>(T source, T dest)
			where T : IInletOrOutlet
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (dest == null) throw new ArgumentNullException(nameof(dest));

			dest.Name = source.Name;
			dest.Dimension = source.Dimension;
			dest.Position = source.Position;
			dest.NameOrDimensionHidden = source.NameOrDimensionHidden;
			dest.IsRepeating = source.IsRepeating;
			dest.RepetitionPosition = source.RepetitionPosition;
		}
	}
}
