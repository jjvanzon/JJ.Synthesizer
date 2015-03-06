using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class ChannelSetupRepository : JJ.Persistence.Synthesizer.DefaultRepositories.ChannelSetupRepository
    {
        public ChannelSetupRepository(IContext context)
            : base(context)
        {
            ChannelSetup entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            entity = Create();
            entity.Name = "Undefined";

            entity = Create();
            entity.Name = "Mono";

            entity = Create();
            entity.Name = "Stereo";
        }
    }
}