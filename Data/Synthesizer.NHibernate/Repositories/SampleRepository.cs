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

        public override byte[] TryGetBytes(int id)
        {
            return _context.TryGetSampleBytes(id);
        }

        public override void SetBytes(int id, byte[] bytes)
        {
            _context.SetSampleBytes(id, bytes);
        }
    }
}