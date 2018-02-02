using System;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Converters
{
	public class ScaleToDtoConverter
	{
		public ScaleDto Convert(Scale entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var dto = new ScaleDto
			{
				ID = entity.ID,
				Name = entity.Name,
				Frequencies = entity.Tones
				                    .Sort()
				                    .Select(x => x.GetFrequency())
				                    .ToArray()
			};

			return dto;
		}
	}
}
