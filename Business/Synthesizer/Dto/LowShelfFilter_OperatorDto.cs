using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class LowShelfFilter_OperatorDto : OperatorDtoBase_ShelfFilter
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.LowShelfFilter);

        public LowShelfFilter_OperatorDto(
            OperatorDtoBase signalOperatorDto, 
            OperatorDtoBase transitionFrequencyOperatorDto, 
            OperatorDtoBase transitionSlopeOperatorDto, 
            OperatorDtoBase dbGainOperatorDto) 
            : base(
                  signalOperatorDto, 
                  transitionFrequencyOperatorDto, 
                  transitionSlopeOperatorDto, 
                  dbGainOperatorDto)
        { }
    }
}
