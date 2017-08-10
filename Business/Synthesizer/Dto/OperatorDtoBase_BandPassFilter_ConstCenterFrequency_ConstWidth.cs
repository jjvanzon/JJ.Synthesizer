using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_BandPassFilter_ConstCenterFrequency_ConstWidth : OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume
    {
        public override InputDto Frequency => CenterFrequency;
        public override InputDto WidthOrBlobVolume => Width;

        public InputDto CenterFrequency { get; set; }
        public InputDto Width { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Sound, CenterFrequency, Width };
            set
            {
                var array = value.ToArray();
                Sound = array[0];
                CenterFrequency = array[1];
                Width = array[2];
            }
        }
    }
}