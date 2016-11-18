using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PeakingEQFilter_OperatorDto : PeakingEQFilter_OperatorDto_AllVars
    { }

    internal class PeakingEQFilter_OperatorDto_AllVars : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.PeakingEQFilter);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase CenterFrequencyOperatorDto { get; set; }
        public OperatorDtoBase BandWidthOperatorDto { get; set; }
        public OperatorDtoBase DBGainOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get
            {
                return new OperatorDtoBase[]
                {
                    SignalOperatorDto,
                    CenterFrequencyOperatorDto,
                    BandWidthOperatorDto,
                    DBGainOperatorDto
                };
            }
            set
            {
                SignalOperatorDto = value[0];
                CenterFrequencyOperatorDto = value[1];
                BandWidthOperatorDto = value[2];
                DBGainOperatorDto = value[3];
            }
        }
    }

    internal class PeakingEQFilter_OperatorDto_ManyConsts : OperatorDtoBase_VarSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.PeakingEQFilter);

        public double CenterFrequency { get; set; }
        public double BandWidth { get; set; }
        public double DBGain { get; set; }
    }
}