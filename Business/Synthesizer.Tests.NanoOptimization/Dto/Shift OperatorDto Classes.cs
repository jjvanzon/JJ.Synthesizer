using System.Collections.Generic;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Dto
{
    internal class Shift_OperatorDto : Shift_OperatorDto_VarSignal_VarDistance
    { }

    internal class Shift_OperatorDto_ConstSignal_ConstDistance : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public double SignalValue { get; set; }
        public double Distance { get; set; }
    }

    internal class Shift_OperatorDto_ConstSignal_VarDistance : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public double SignalValue { get; set; }
        public OperatorDtoBase DistanceOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { DistanceOperatorDto }; }
            set { DistanceOperatorDto = value[0]; }
        }
    }

    internal class Shift_OperatorDto_VarSignal_ConstDistance : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double Distance { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }

    internal class Shift_OperatorDto_VarSignal_VarDistance : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Shift);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase DistanceOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, DistanceOperatorDto }; }
            set { SignalOperatorDto = value[0]; DistanceOperatorDto = value[1]; }
        }

    }
}
