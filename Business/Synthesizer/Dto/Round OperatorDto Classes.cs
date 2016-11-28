using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Round_OperatorDto : Round_OperatorDto_VarSignal_VarStep_VarOffset
    { }

    internal class Round_OperatorDto_ConstSignal : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public double Signal { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public OperatorDtoBase OffsetOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { StepOperatorDto, OffsetOperatorDto }; }
            set { StepOperatorDto = value[0]; OffsetOperatorDto = value[1]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_StepOne_OffsetZero : OperatorDtoBase_VarSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);
    }

    internal class Round_OperatorDto_VarSignal_VarStep_VarOffset : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public OperatorDtoBase OffsetOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, StepOperatorDto, OffsetOperatorDto }; }
            set { SignalOperatorDto = value[0]; StepOperatorDto = value[1]; OffsetOperatorDto = value[2]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_VarStep_ZeroOffset : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, StepOperatorDto }; }
            set { SignalOperatorDto = value[0]; StepOperatorDto = value[1]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_VarStep_ConstOffset : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public double Offset { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, StepOperatorDto }; }
            set { SignalOperatorDto = value[0]; StepOperatorDto = value[1]; }
        }
    }
    
    internal class Round_OperatorDto_VarSignal_ConstStep_VarOffset : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double StepOperatorDto { get; set; }
        public OperatorDtoBase OffsetOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, OffsetOperatorDto }; }
            set { SignalOperatorDto = value[0]; OffsetOperatorDto = value[1]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_ConstStep_ZeroOffset : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double StepOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_ConstStep_ConstOffset : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double StepOperatorDto { get; set; }
        public double Offset { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto }; }
            set { SignalOperatorDto = value[0]; }
        }
    }
}