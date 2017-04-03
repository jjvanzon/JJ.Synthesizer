using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class ToneRepository : RepositoryBase<Tone, int>, IToneRepository
    {
        public ToneRepository(IContext context)
            : base(context)
        { }
    }
}
