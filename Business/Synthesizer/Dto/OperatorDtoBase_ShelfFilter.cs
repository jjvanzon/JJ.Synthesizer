using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ShelfFilter_AllVars : OperatorDtoBase_Filter_VarSignal
    {
        public IOperatorDto TransitionFrequencyOperatorDto { get; set; }
        public IOperatorDto TransitionSlopeOperatorDto { get; set; }
        public IOperatorDto DBGainOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, TransitionFrequencyOperatorDto, TransitionSlopeOperatorDto, DBGainOperatorDto }; }
            set { SignalOperatorDto = value[0]; TransitionFrequencyOperatorDto = value[1]; TransitionSlopeOperatorDto = value[2]; DBGainOperatorDto = value[3]; }
        }
    }

    internal abstract class OperatorDtoBase_ShelfFilter_ManyConsts : OperatorDtoBase_Filter_ManyConsts
    {
        public override double Frequency => TransitionFrequency;

        public double TransitionFrequency { get; set; }
        public double TransitionSlope { get; set; }
        public double DBGain { get; set; }
    }
}
