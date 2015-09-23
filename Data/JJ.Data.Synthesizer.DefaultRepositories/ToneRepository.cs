using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class ToneRepository : RepositoryBase<Tone, int>, IToneRepository
    {
        public ToneRepository(IContext context)
            : base(context)
        { }
    }
}
