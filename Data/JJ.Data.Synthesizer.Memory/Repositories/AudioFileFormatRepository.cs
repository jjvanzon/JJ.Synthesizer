using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class AudioFileFormatRepository : JJ.Data.Synthesizer.DefaultRepositories.AudioFileFormatRepository
    {
        public AudioFileFormatRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Raw");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Wav");
        }
    }
}