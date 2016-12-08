using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Scaler_OperatorDto : Scaler_OperatorDto_AllVars
    { }

    internal class Scaler_OperatorDto_AllConsts : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Scaler);

        public double Signal { get; set; }
        public double SourceValueA { get; set; }
        public double SourceValueB { get; set; }
        public double TargetValueA { get; set; }
        public double TargetValueB { get; set; }
    }

    internal class Scaler_OperatorDto_ManyConsts : OperatorDtoBase_VarSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Scaler);

        public double SourceValueA { get; set; }
        public double SourceValueB { get; set; }
        public double TargetValueA { get; set; }
        public double TargetValueB { get; set; }
    }

    internal class Scaler_OperatorDto_AllVars : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Scaler);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase SourceValueAOperatorDto { get; set; }
        public OperatorDtoBase SourceValueBOperatorDto { get; set; }
        public OperatorDtoBase TargetValueAOperatorDto { get; set; }
        public OperatorDtoBase TargetValueBOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get
            {
                return new OperatorDtoBase[]
                {
                    SignalOperatorDto,
                    SourceValueAOperatorDto,
                    SourceValueBOperatorDto,
                    TargetValueAOperatorDto,
                    TargetValueBOperatorDto
                };
            }
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