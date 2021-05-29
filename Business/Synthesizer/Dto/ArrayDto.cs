using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    public class ArrayDto
    {
        public double[] Array { get; set; }
        public double Rate { get; set; }
        public double MinPosition { get; set; }
        public double ValueBefore { get; set; }
        public double ValueAfter { get; set; }
        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public bool IsRotating { get; set; }
    }
}
