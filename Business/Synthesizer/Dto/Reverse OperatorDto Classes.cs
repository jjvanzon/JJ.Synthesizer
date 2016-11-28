using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reverse_OperatorDto : Reverse_OperatorDtoBase_VarSpeed
    { }

    internal class Reverse_OperatorDtoBase_VarSpeed_WithPhaseTracking : Reverse_OperatorDtoBase_VarSpeed
    { }

    internal class Reverse_OperatorDtoBase_VarSpeed_NoPhaseTracking : Reverse_OperatorDtoBase_VarSpeed
    { }

    internal abstract class Reverse_OperatorDtoBase_VarSpeed : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Reverse);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase SpeedOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, SpeedOperatorDto }; }
            set { SignalOperatorDto = value[0]; SpeedOperatorDto = value[1]; }
        }
    }

    internal class Reverse_OperatorDtoBase_ConstSpeed_WithOriginShifting : Reverse_OperatorDtoBase_ConstSpeed
    { }

    internal class Reverse_OperatorDtoBase_ConstSpeed_NoOriginShifting : Reverse_OperatorDtoBase_ConstSpeed
    { }

    internal abstract class Reverse_OperatorDtoBase_ConstSpeed : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Reverse);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Speed { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }
}