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

        public override IEnumerable<InputDto> Inputs
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

    internal class RangeOverOutlets_Outlet_OperatorDto_ZeroStep : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.RangeOverOutlets;

        private static readonly InputDto _step = 1;

        public InputDto From { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { From, _step };
            set => From = value.First();
        }
    }

    internal class RangeOverOutlets_Outlet_OperatorDto_VarFrom_VarStep : RangeOverOutlets_Outlet_OperatorDto
    { }

    internal class RangeOverOutlets_Outlet_OperatorDto_VarFrom_ConstStep : RangeOverOutlets_Outlet_OperatorDto
    { }

    internal class RangeOverOutlets_Outlet_OperatorDto_ConstFrom_VarStep : RangeOverOutlets_Outlet_OperatorDto
    { }

    internal class RangeOverOutlets_Outlet_OperatorDto_ConstFrom_ConstStep : RangeOverOutlets_Outlet_OperatorDto
    { }
}