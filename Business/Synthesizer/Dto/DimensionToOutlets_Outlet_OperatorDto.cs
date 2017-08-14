using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class DimensionToOutlets_Outlet_OperatorDto : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal, IOperatorDto_WithOutletPosition
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.DimensionToOutlets;

        public InputDto Signal { get; set; }
        public int OutletPosition { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal };
            set => Signal = value.ElementAt(0);
        }
    }
}