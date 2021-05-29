namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal abstract class OperatorDtoBase_Filter_VarSound : OperatorDtoBase_WithSound
    {
        public double TargetSamplingRate { get; set; }
        public double NyquistFrequency { get; set; }
    }
}
