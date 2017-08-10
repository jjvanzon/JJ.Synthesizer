using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class AllPassFilter_OperatorDto : AllPassFilter_OperatorDto_AllVars
    { }

    internal class AllPassFilter_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;
    }

    internal class AllPassFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;

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

    internal class AllPassFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.AllPassFilter;
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