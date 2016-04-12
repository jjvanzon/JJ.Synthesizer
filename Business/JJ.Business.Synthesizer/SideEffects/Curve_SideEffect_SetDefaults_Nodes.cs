using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Curve_SideEffect_SetDefaults_Nodes : ISideEffect
    {
        private readonly Curve _curve;
        private readonly INodeRepository _nodeRepository;
        private readonly INodeTypeRepository _nodeTypeRepository;
        private readonly IIDRepository _idRepository;

        public Curve_SideEffect_SetDefaults_Nodes(
            Curve curve, 
            INodeRepository nodeRepository, 
            INodeTypeRepository nodeTypeRepository, 
            IIDRepository idRepository)
        {
            if (curve == null) throw new NullException(() => curve);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _curve = curve;
            _nodeRepository = nodeRepository;
            _nodeTypeRepository = nodeTypeRepository;
            _idRepository = idRepository;
        }

        public void Execute()
        {
            {
                var node = new Node();
                node.ID = _idRepository.GetID();
                node.X = 0;
                node.Y = 1;
                node.SetNodeTypeEnum(NodeTypeEnum.Curve, _nodeTypeRepository);
                node.LinkTo(_curve);
                _nodeRepository.Insert(node);
            }

            {
                var node = new Node();
                node.ID = _idRepository.GetID();
                node.X = 1;
                node.Y = 0;
                node.LinkTo(_curve);
                node.SetNodeTypeEnum(NodeTypeEnum.Curve, _nodeTypeRepository);
                _nodeRepository.Insert(node);
            }
        }
    }
}
