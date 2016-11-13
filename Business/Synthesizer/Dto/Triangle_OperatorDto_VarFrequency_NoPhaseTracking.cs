using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Triangle_OperatorDto_VarFrequency_NoPhaseTracking : OperatorDto_VarFrequency
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Triangle);

        public Triangle_OperatorDto_VarFrequency_NoPhaseTracking(OperatorDto frequencyOperatorDto)
            : base(frequencyOperatorDto)
        { }
    }
}
