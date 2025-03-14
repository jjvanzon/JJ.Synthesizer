using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Core.Reflection;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SampleManagerAccessor
    {
        private readonly AccessorEx _accessor;
        
        public SampleManagerAccessor(SampleManager sampleManager)
        {
            _accessor = new AccessorEx(sampleManager);
        }

        public Sample CreateWavSample(WavHeaderStruct wavHeaderStruct)
        {
            return (Sample)_accessor.InvokeMethod(nameof(CreateWavSample), wavHeaderStruct);
        }
    }
}
