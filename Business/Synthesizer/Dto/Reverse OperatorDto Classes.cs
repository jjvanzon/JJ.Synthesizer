using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reverse_OperatorDto : Reverse_OperatorDtoBase_VarSpeed
    { }

    internal class Reverse_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reverse;
    }

    internal class Reverse_OperatorDto_VarSpeed_WithPhaseTracking : Reverse_OperatorDtoBase_VarSpeed, IOperatorDto_VarSignal
    { }

    internal class Reverse_OperatorDto_VarSpeed_NoPhaseTracking : Reverse_OperatorDtoBase_VarSpeed
    { }

    internal abstract class Reverse_OperatorDtoBase_VarSpeed : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reverse;

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase SpeedOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, SpeedOperatorDto }; }
            set { SignalOperatorDto = value[0]; SpeedOperatorDto = value[1]; }
        }
    }

    internal class Reverse_OperatorDto_ConstSpeed_WithOriginShifting : Reverse_OperatorDtoBase_ConstSpeed
    { }

    internal class Reverse_OperatorDto_ConstSpeed_NoOriginShifting : Reverse_OperatorDtoBase_ConstSpeed
    { }

    internal abstract class Reverse_OperatorDtoBase_ConstSpeed : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reverse;

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Speed { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }
}