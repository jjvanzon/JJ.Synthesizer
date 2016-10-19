namespace JJ.Business.Synthesizer.Calculation.Samples
{
    public interface ISampleCalculator
    {
        int ChannelCount { get; }
        double CalculateValue(double time, int channelIndex);
    }
}
