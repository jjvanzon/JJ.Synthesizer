using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SortOverInlets_Outlet_OperatorDto : OperatorDtoBase_Vars, IOperatorDto_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SortOverInlets;

        public int OutletPosition { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CanonicalCustomDimensionName { get; set; }
        public int DimensionStackLevel { get; set; }
    }
}
