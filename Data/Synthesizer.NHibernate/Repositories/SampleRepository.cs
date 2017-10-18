using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class SampleRepository : DefaultRepositories.SampleRepository
    {
        private new readonly SynthesizerContext _context;

        public SampleRepository(IContext context)
            : base(context)
        {
            _context = (SynthesizerContext)context;
        }

        public override byte[] TryGetBytes(int sampleID)
        {
            return _context.TryGetSampleBytes(sampleID);
        }

        public override void SetBytes(int sampleID, byte[] bytes)
        {
            _context.SetSampleBytes(sampleID, bytes);
        }
    }
}