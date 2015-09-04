namespace JJ.Business.Synthesizer.Calculation.Samples
{
    public interface ISampleCalculator
    {
        double CalculateValue(double time, int channelIndex);

        /// <summary>
        /// For performance, so we can use this value directly.
        /// </summary>
        int ChannelCount { get; }
    }
}
