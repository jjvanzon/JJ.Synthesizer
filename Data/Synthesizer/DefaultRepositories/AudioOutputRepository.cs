using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class AudioOutputRepository : RepositoryBase<AudioOutput, int>, IAudioOutputRepository
    {
        public AudioOutputRepository(IContext context)
            : base(context)
        { }
    }
}
