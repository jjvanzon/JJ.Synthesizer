using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Cache_OperatorDto : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Cache;

        public IList<ArrayDto> ArrayDto { get; set; }

        public InputDto Signal { get; set; }
        public InputDto Start { get; set; }
        public InputDto End { get; set; }
        public InputDto SamplingRate { get; set; }

        public int ChannelCount { get; set; }
        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public SpeakerSetupEnum SpeakerSetupEnum { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Start, End, SamplingRate };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Start = array[1];
                End = array[2];
                SamplingRate = array[3];
            }
        }
    }

    internal class Cache_OperatorDto_ConstSignal : Cache_OperatorDto
    { }

    // There is a lot of specialization here, that at one point seemed necessary, but is currently (2016-12-14) not used.
    // But in the future, we might use it again. Still it is a code smell that we have structuring with no purpose.

    internal class Cache_OperatorDto_SingleChannel_Block : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_SingleChannel_Cubic : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_SingleChannel_Hermite : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_SingleChannel_Line : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_SingleChannel_Stripe : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_MultiChannel_Block : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_MultiChannel_Cubic : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_MultiChannel_Hermite : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_MultiChannel_Line : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_MultiChannel_Stripe : Cache_OperatorDto
    { }
}