using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Trigger_VarPassThrough_VarReset : OperatorDtoBase
    {
        public IOperatorDto PassThroughInputOperatorDto { get; set; }
        public IOperatorDto ResetOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { PassThroughInputOperatorDto, ResetOperatorDto };
            set { PassThroughInputOperatorDto = value[0]; ResetOperatorDto = value[1]; }
        }
    }

    internal abstract class OperatorDtoBase_Trigger_VarPassThrough_ConstReset : OperatorDtoBase
    {
        public IOperatorDto PassThroughInputOperatorDto { get; set; }
        public double Reset { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { PassThroughInputOperatorDto };
            set => PassThroughInputOperatorDto = value[0];
        }
    }

    internal abstract class OperatorDtoBase_Trigger_ConstPassThrough_VarReset : OperatorDtoBase
    {
        public double PassThrough { get; set; }
        public IOperatorDto ResetOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { ResetOperatorDto };
            set => ResetOperatorDto = value[0];
        }
    }

    internal abstract class OperatorDtoBase_Trigger_ConstPassThrough_ConstReset : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double PassThrough { get; set; }
        public double Reset { get; set; }
    }
}
