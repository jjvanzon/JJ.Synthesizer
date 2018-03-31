using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
	internal class MidiMappingWarningValidator : VersatileValidator
	{
		public MidiMappingWarningValidator(MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			For(entity.FromPosition, ResourceFormatter.FromPosition).GreaterThanOrEqual(0);
			For(entity.TillPosition, ResourceFormatter.TillPosition).GreaterThanOrEqual(0);
		}
	}
}