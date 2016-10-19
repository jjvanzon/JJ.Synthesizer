using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class SpeakerSetupChannelRepository : RepositoryBase<SpeakerSetupChannel, int>, ISpeakerSetupChannelRepository
    {
        public SpeakerSetupChannelRepository(IContext context)
            : base(context)
        { }
    }
}
