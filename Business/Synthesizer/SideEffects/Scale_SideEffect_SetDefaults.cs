using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
	/// <summary> Sets the scale properties to practical defaults, also slightly dependent on the scale type. </summary>
	internal class Scale_SideEffect_SetDefaults : ISideEffect
	{
		private readonly Scale _scale;
		private readonly IScaleTypeRepository _scaleTypeRepository;

		private static readonly double _defaultBaseFrequency = GetDefaultBaseFrequency();

		/// <summary> Sets the scale properties to practical defaults, also slightly dependent on the scale type. </summary>
		public Scale_SideEffect_SetDefaults(Scale scale, IScaleTypeRepository scaleTypeRepository)
		{
			_scale = scale ?? throw new NullException(() => scale);
			_scaleTypeRepository = scaleTypeRepository ?? throw new NullException(() => scaleTypeRepository);
		}

		public void Execute()
		{
			ScaleTypeEnum scaleTypeEnum = _scale.GetScaleTypeEnum();

			if (scaleTypeEnum == ScaleTypeEnum.Undefined)
			{
				_scale.SetScaleTypeEnum(ScaleTypeEnum.SemiTone, _scaleTypeRepository);
			}

			_scale.BaseFrequency = _defaultBaseFrequency;
		}

		private static double GetDefaultBaseFrequency()
		{
			// Start with A
			const double a = 440;
			// Then turn it into a C
			double c = a * Math.Pow(2, 3.0 / 12.0);
			// Then lower it to the sub-contra octave, which according to Wikipedia
			// is octave 0, even though you can go to octave -1 to get the sub-sub-contra octave.
			c = c / Math.Pow(2.0, 5.0);
			return c;
		}
	}
}
