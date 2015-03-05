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
            NodeType nodeType;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            nodeType = Create();
            nodeType.Name = "Undefined";

            nodeType = Create();
            nodeType.Name = "Off";

            nodeType = Create();
            nodeType.Name = "Block";

            nodeType = Create();
            nodeType.Name = "Line";
        }
    }
}