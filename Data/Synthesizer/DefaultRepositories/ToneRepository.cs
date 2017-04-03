using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class ToneRepository : RepositoryBase<Tone, int>, IToneRepository
    {
        public ToneRepository(IContext context)
            : base(context)
        { }
    }
}
