using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_VarSignal_VarDistance : OperatorDtoBase
    {
        public override string OperatorTypeName => OperatorNames.Shift;

        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase DistanceOperatorDto => InputOperatorDtos[1];

        public Shift_OperatorDto_VarSignal_VarDistance(OperatorDtoBase signalOperatorDto, OperatorDtoBase distanceOperatorDto)
            : base(new OperatorDtoBase[] { signalOperatorDto, distanceOperatorDto })
        { }
    }
}
