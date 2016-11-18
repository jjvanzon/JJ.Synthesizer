using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    internal class CustomOperator_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.CustomOperator);

        public string OutletName { get; set; }
        public DimensionEnum OutletDimensionEnum { get; set; }
        public int? OutletListIndex { get; set; }
        public Patch UnderlyingPatch { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; }
    }
}