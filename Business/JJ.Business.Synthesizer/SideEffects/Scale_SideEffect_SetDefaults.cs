using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    /// <summary> Sets the scale properties to practical defaults, also slightly dependent on the scale type. </summary>
    internal class Scale_SideEffect_SetDefaults : ISideEffect
    {
        private readonly Scale _scale;
        private readonly IScaleTypeRepository _scaleTypeRepository;

        private static double _defaultBaseFrequency = GetDefaultBaseFrequency();

        /// <summary> Sets the scale properties to practical defaults, also slightly dependent on the scale type. </summary>
        public Scale_SideEffect_SetDefaults(Scale scale, IScaleTypeRepository scaleTypeRepository)
        {
            if (scale == null) throw new NullException(() => scale);
            if (scaleTypeRepository == null) throw new NullException(() => scaleTypeRepository);

            _scale = scale;
            _scaleTypeRepository = scaleTypeRepository;
        }

        public void Execute()
        {
            ScaleTypeEnum scaleTypeEnum = _scale.GetScaleTypeEnum();

            if (scaleTypeEnum == ScaleTypeEnum.Undefined)
            {
                _scale.SetScaleTypeEnum(ScaleTypeEnum.SemiTone, _scaleTypeRepository);
            }

            if (scaleTypeEnum == ScaleTypeEnum.LiteralFrequency)
            {
                _scale.BaseFrequency = null;
            }
            else
            {
                _scale.BaseFrequency = _defaultBaseFrequency;
            }
        }

        private static double GetDefaultBaseFrequency()
        {
            // Start with A
            double a = 440;
            // Then turn it into a C
            double c = a * Math.Pow(2, 3.0 / 12.0);
            // Then lower it to the lowest octave (sub-sub-contra).
            // TODO: I wonder if we shouldn't make it the sub-contra octave.
            c = c / Math.Pow(2.0, 6.0);
            return c;
        }
    }
}
