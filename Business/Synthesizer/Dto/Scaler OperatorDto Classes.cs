using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Scaler_OperatorDto : Scaler_OperatorDto_AllVars
    { }

    internal class Scaler_OperatorDto_AllConsts : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Scaler;

        public double Signal { get; set; }
        public double SourceValueA { get; set; }
        public double SourceValueB { get; set; }
        public double TargetValueA { get; set; }
        public double TargetValueB { get; set; }
    }

    internal class Scaler_OperatorDto_ManyConsts : OperatorDtoBase_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Scaler;

        public double SourceValueA { get; set; }
        public double SourceValueB { get; set; }
        public double TargetValueA { get; set; }
        public double TargetValueB { get; set; }
    }

    internal class Scaler_OperatorDto_AllVars : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Scaler;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto SourceValueAOperatorDto { get; set; }
        public IOperatorDto SourceValueBOperatorDto { get; set; }
        public IOperatorDto TargetValueAOperatorDto { get; set; }
        public IOperatorDto TargetValueBOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[]
            {
                SignalOperatorDto,
                SourceValueAOperatorDto,
                SourceValueBOperatorDto,
                TargetValueAOperatorDto,
                TargetValueBOperatorDto
            };
            set
            {
                SignalOperatorDto = value[0];
                SourceValueAOperatorDto = value[1];
                SourceValueBOperatorDto = value[2];
                TargetValueAOperatorDto = value[3];
                TargetValueBOperatorDto = value[4];
            }
        }
    }
}