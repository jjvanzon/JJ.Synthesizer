using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class AudioFileOutputRepository : RepositoryBase<AudioFileOutput, int>, IAudioFileOutputRepository
    {
        public AudioFileOutputRepository(IContext context)
            : base(context)
        { }
    }
}
