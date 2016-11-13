using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Curve_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Curve);

        public Curve Curve { get; }

        public Curve_OperatorDto(Curve curve)
            : base(new OperatorDtoBase[0])
        {
            Curve = curve;
        }
    }
}