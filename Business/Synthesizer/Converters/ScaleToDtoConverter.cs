using System;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Converters
{
	public class ScaleToDtoConverter
	{
		public ScaleDto Convert(Scale scale)
		{
			if (scale == null) throw new ArgumentNullException(nameof(scale));

			var dto = new ScaleDto
			{
				ID = scale.ID,
				Name = scale.Name,
				Frequencies = scale.Tones
				                    .Sort()
				                    .Select(x => x.GetFrequency())
				                    .ToArray()
			};

			return dto;
		}
	}
}
