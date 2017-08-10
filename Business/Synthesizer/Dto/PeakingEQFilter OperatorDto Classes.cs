using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PeakingEQFilter_OperatorDto : PeakingEQFilter_OperatorDto_AllVars
    { }

    internal class PeakingEQFilter_OperatorDto_ConstSound : OperatorDtoBase_ConstSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PeakingEQFilter;
    }

    internal class PeakingEQFilter_OperatorDto_AllVars : OperatorDtoBase_Filter_VarSound
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PeakingEQFilter;

        public InputDto CenterFrequency { get; set; }
        public InputDto Width { get; set; }
        public InputDto DBGain { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[]
            {
                Sound,
                CenterFrequency,
                Width,
                DBGain
            };
            set
            {
                var array = value.ToArray();
                Sound = array[0];
                CenterFrequency = array[1];
                Width = array[2];
                DBGain = array[3];
            }
        }
    }

    internal class PeakingEQFilter_OperatorDto_ManyConsts : OperatorDtoBase_Filter_ManyConsts
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.PeakingEQFilter;

        public override InputDto Frequency => CenterFrequency;

        public InputDto CenterFrequency { get; set; }
        public InputDto Width { get; set; }
        public InputDto DBGain { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[]
            {
                Sound,
                CenterFrequency,
                Width,
                DBGain
            };
            set
            {
                var array = value.ToArray();
                Sound = array[0];
                CenterFrequency = array[1];
                Width = array[2];
                DBGain = array[3];
            }
        }
    }
}