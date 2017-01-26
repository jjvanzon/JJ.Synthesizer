using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ShelfFilter_AllVars : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase TransitionFrequencyOperatorDto { get; set; }
        public OperatorDtoBase TransitionSlopeOperatorDto { get; set; }
        public OperatorDtoBase DBGainOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, TransitionFrequencyOperatorDto, TransitionSlopeOperatorDto, DBGainOperatorDto }; }
            set { SignalOperatorDto = value[0]; TransitionFrequencyOperatorDto = value[1]; TransitionSlopeOperatorDto = value[2]; DBGainOperatorDto = value[3]; }
        }
    }

    internal abstract class OperatorDtoBase_ShelfFilter_ManyConsts : OperatorDtoBase_VarSignal
    {
        public double TransitionFrequency { get; set; }
        public double TransitionSlope { get; set; }
        public double DBGain { get; set; }
    }
}
