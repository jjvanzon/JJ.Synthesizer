using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class NodeTypeRepository : RepositoryBase<NodeType, int>, INodeTypeRepository
    {
        public NodeTypeRepository(IContext context)
            : base(context)
        { }
    }
}
