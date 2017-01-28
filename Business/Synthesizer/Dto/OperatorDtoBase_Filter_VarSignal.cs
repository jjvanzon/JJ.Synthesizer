namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Filter_VarSignal : OperatorDtoBase_VarSignal
    {
        public double SamplingRate { get; set; }
        public double NyquistFrequency { get; set; }
    }
}
