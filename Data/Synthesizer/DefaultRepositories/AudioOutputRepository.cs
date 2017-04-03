using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class AudioOutputRepository : RepositoryBase<AudioOutput, int>, IAudioOutputRepository
    {
        public AudioOutputRepository(IContext context)
            : base(context)
        { }
    }
}
