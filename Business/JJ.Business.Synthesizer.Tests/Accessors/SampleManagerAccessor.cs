using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Core;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SampleManagerAccessor
    {
        private readonly AccessorCore _accessor;
        
        public SampleManagerAccessor(SampleManager sampleManager)
        {
            _accessor = new AccessorCore(sampleManager);
        }

        public Sample CreateWavSample(WavHeaderStruct wavHeaderStruct)
        {
            return (Sample)_accessor.Call(nameof(CreateWavSample), wavHeaderStruct);
        }
    }
}
