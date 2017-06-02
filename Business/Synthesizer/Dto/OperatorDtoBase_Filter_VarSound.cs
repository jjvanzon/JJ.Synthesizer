namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Filter_VarSound : OperatorDtoBase_VarSound
    {
        public double SamplingRate { get; set; }
        public double NyquistFrequency { get; set; }
    }
}
