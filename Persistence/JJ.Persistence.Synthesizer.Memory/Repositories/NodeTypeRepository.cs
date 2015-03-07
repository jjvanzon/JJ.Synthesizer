using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class NodeTypeRepository : JJ.Persistence.Synthesizer.DefaultRepositories.NodeTypeRepository
    {
        public NodeTypeRepository(IContext context)
            : base(context)
        {
            NodeType entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            entity = Create();
            entity.Name = "Off";

            entity = Create();
            entity.Name = "Block";

            entity = Create();
            entity.Name = "Line";
        }
    }
}