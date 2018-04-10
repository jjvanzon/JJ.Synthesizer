using System;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Converters
{
	public class ToneToDtoConverter
	{
		public ToneDto Convert(Tone entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var dto = new ToneDto
			{
				Value = entity.Value,
				Octave = entity.Octave,
				ID = entity.ID,
				ScaleTypeEnum = entity.GetScaleTypeEnum(),
				ScaleBaseFrequency = entity.GetScaleBaseFrequency(),
				IsFromEntity = true,
				Ordinal = entity.GetOrdinal(),
				Frequency = entity.GetFrequency()
			};

			return dto;
		}
	}
}