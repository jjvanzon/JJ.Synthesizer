using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class SpeakerSetupChannelRepository : RepositoryBase<SpeakerSetupChannel, int>, ISpeakerSetupChannelRepository
    {
        // ReSharper disable once MemberCanBeProtected.Global
        public SpeakerSetupChannelRepository(IContext context)
            : base(context)
        { }

        public virtual IList<SpeakerSetupChannel> GetAll() => _context.Query<SpeakerSetupChannel>().ToArray();
    }
}
