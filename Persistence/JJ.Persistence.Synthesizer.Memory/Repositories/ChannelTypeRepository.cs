using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class ChannelTypeRepository : JJ.Persistence.Synthesizer.DefaultRepositories.ChannelTypeRepository
    {
        public ChannelTypeRepository(IContext context)
            : base(context)
        {
            ChannelType entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            entity = Create();
            entity.Name = "Undefined";

            entity = Create();
            entity.Name = "Left";

            entity = Create();
            entity.Name = "Right";
        }
    }
}