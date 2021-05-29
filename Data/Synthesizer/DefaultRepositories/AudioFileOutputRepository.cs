using JetBrains.Annotations;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    [UsedImplicitly]
    public class AudioFileOutputRepository : RepositoryBase<AudioFileOutput, int>, IAudioFileOutputRepository
    {
        public AudioFileOutputRepository(IContext context)
            : base(context)
        { }
    }
}
