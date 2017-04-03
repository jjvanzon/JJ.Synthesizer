using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class SpeakerSetupChannelRepository : RepositoryBase<SpeakerSetupChannel, int>, ISpeakerSetupChannelRepository
    {
        public SpeakerSetupChannelRepository(IContext context)
            : base(context)
        { }
    }
}
