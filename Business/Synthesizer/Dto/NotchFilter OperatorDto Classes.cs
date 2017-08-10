using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
    internal class NotchFilter_OperatorDto : NotchFilter_OperatorDto_AllVars
    { }

    internal class NotchFilter_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.NotchFilter;
    }

    internal class NotchFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.NotchFilter;

        public InputDto CenterFrequency { get; set; }
        public InputDto Width { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Sound, CenterFrequency, Width };
            set
            {
                var array = value.ToArray();
                Sound = array[0];
                CenterFrequency = array[1];
                Width = array[2];
            }
        }
    }

    internal class NotchFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.LowPassFilter;

        public override InputDto Frequency => CenterFrequency;
        public override InputDto WidthOrBlobVolume => Width;

        public InputDto CenterFrequency { get; set; }
        public InputDto Width { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Sound, CenterFrequency, Width };
            set
            {
                var array = value.ToArray();
                Sound = array[0];
                CenterFrequency = array[1];
                Width = array[2];
            }
        }
    }
}