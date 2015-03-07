using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    public class SpeakerSetupChannelRepository : RepositoryBase<SpeakerSetupChannel, int>, ISpeakerSetupChannelRepository
    {
        public SpeakerSetupChannelRepository(IContext context)
            : base(context)
        { }
    }
}
