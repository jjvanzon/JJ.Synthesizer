using JetBrains.Annotations;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    [UsedImplicitly]
    public class AudioFileFormatRepository : RepositoryBase<AudioFileFormat, int>, IAudioFileFormatRepository
    {
        public AudioFileFormatRepository(IContext context)
            : base(context)
        { }
    }
}
