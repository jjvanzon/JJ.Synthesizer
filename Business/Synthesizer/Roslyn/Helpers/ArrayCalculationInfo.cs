using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class ArrayCalculationInfo
    {
        public string NameCamelCase { get; set; }
        public string TypeName { get; set; }
        public double[] UnderlyingArray { get; set; }
        public double Rate { get; set; }
        public double MinPosition { get; set; }
        public double ValueBefore { get; set; }
        public double ValueAfter { get; set; }
        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public bool IsRotatingPosition { get; set; }
    }
}
