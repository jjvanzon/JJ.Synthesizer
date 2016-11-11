using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class PatchOutlet_OperatorDto : OperatorDto
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.PatchOutlet);

        public PatchOutlet_OperatorDto()
            : base(new OperatorDto[0])
        { }

        public int? ListIndex { get; set; }
        public string Name { get; set; }
        public DimensionEnum DimensionEnum { get; set; }
    }
}
