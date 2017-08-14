using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverOutlets_Outlet_OperatorDto : OperatorDtoBase, IOperatorDto_WithOutletPosition
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        public InputDto From { get; set; }
        public InputDto Step { get; set; }
        public int OutletPosition { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { From, Step };
            set
            {
                var array = value.ToArray();
                From = array[0];
                Step = array[1];
            }
        }
    }
}