using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SortOverInlets_Outlet_OperatorDto : OperatorDtoBase_Vars, IOperatorDtoWithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SortOverInlets);

        public int OutletListIndex { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }
}
