using JetBrains.Annotations;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    [UsedImplicitly]
    public class SampleRepository : DefaultRepositories.SampleRepository
    {
        private new readonly SynthesizerContext _context;

        public SampleRepository(IContext context) : base(context) => _context = (SynthesizerContext)context;

        public override byte[] TryGetBytes(int sampleID) => _context.TryGetSampleBytes(sampleID);
        public override void SetBytes(int sampleID, byte[] bytes) => _context.SetSampleBytes(sampleID, bytes);
    }
}