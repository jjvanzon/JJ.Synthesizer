using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.InvalidValues;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Tone_SideEffect_SetDefaults_Versatile : Tone_SideEffect_SetDefaults_Base
    {
        /// <inheritdoc />
        public Tone_SideEffect_SetDefaults_Versatile(Tone tone, Tone previousTone)
            : base(tone, previousTone) { }

        public override void Execute()
        {
            ScaleTypeEnum scaleTypeEnum = _tone.Scale.GetScaleTypeEnum();

            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    new Tone_SideEffect_SetDefaults_LiteralFrequency(_tone, _previousTone).Execute();
                    break;

                case ScaleTypeEnum.Factor:
                    new Tone_SideEffect_SetDefaults_Factor(_tone, _previousTone).Execute();
                    break;

                case ScaleTypeEnum.Exponent:
                    new Tone_SideEffect_SetDefaults_Exponent(_tone, _previousTone).Execute();
                    break;

                case ScaleTypeEnum.SemiTone:
                    new Tone_SideEffect_SetDefaults_SemiTone(_tone, _previousTone).Execute();
                    break;

                case ScaleTypeEnum.Cent:
                    new Tone_SideEffect_SetDefaults_Cent(_tone, _previousTone).Execute();
                    break;

                default:
                    throw new ValueNotSupportedException(scaleTypeEnum);
            }
        }
    }
}