using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class GetDimension_OperatorDto : OperatorDtoBase_PositionReader
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.GetDimension;

        public int SamplingRate { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Position };
            set => Position = value.FirstOrDefault();
        }
    }
}