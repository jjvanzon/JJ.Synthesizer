using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SampleManagerAccessor
    {
        private readonly Accessor _accessor;
        
        public SampleManagerAccessor(SampleManager sampleManager)
        {
            _accessor = new Accessor(sampleManager);
        }

        public Sample CreateWavSample(WavHeaderStruct wavHeaderStruct)
        {
            return (Sample)_accessor.InvokeMethod(nameof(CreateWavSample), wavHeaderStruct);
        }
    }
}
