using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class EntityEnumExtensions
    {
        public static NodeTypeEnum GetNodeTypeEnum(this Node node)
        {
            if (node.NodeType == null) return NodeTypeEnum.Undefined;

            return (NodeTypeEnum)node.NodeType.ID;
        }

        public static void SetNodeTypeEnum(this Node node, NodeTypeEnum nodeTypeEnum, INodeTypeRepository nodeTypeRepository)
        {
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            node.NodeType = nodeTypeRepository.Get((int)nodeTypeEnum);
        }
    }
}
