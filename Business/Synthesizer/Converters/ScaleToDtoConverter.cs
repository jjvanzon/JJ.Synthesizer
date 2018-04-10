using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Converters
{
	public class ScaleToDtoConverter
	{
		private readonly ToneToDtoConverter _toneToDtoConverter = new ToneToDtoConverter();
		private readonly ToneCalculator _toneCalculator = new ToneCalculator();

		public ScaleDto Convert(Scale scale)
		{
			if (scale == null) throw new ArgumentNullException(nameof(scale));

			IEnumerable<ToneDto> toneDtos = scale.Tones.Select(x => _toneToDtoConverter.Convert(x));
			IList<ToneDto> toneDtos2 = _toneCalculator.MakeOctavesComplete(toneDtos);

			var dto = new ScaleDto
			{
				ID = scale.ID,
				Name = scale.Name,
				Frequencies = toneDtos2.Select(x => x.Frequency).ToArray()
			};

			return dto;
		}
	}
}