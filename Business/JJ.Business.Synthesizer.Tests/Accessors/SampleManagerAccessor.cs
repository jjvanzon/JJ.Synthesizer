
namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class SampleManagerAccessor(SampleManager sampleManager) : AccessorCore(sampleManager)
{
    public Sample CreateWavSample(WavHeaderStruct wavHeader) => (Sample)Call(wavHeader);
}