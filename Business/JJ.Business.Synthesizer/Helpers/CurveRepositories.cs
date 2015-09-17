using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class CurveRepositories
    {
        public ICurveRepository CurveRepository { get; private set; }
        public INodeRepository NodeRepository { get; private set; }
        public INodeTypeRepository NodeTypeRepository { get; private set; }
        public IIDRepository IDRepository { get; private set; }

        public CurveRepositories(RepositoryWrapper repositoryWrapper)
        {
            CurveRepository = repositoryWrapper.CurveRepository;
            NodeRepository = repositoryWrapper.NodeRepository;
            NodeTypeRepository = repositoryWrapper.NodeTypeRepository;
            IDRepository = repositoryWrapper.IDRepository;
        }

        public CurveRepositories(
            ICurveRepository curveRepository,
            INodeRepository nodeRepository,
            INodeTypeRepository nodeTypeRepository,
            IIDRepository idRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            CurveRepository = curveRepository;
            NodeRepository = nodeRepository;
            NodeTypeRepository = nodeTypeRepository;
            IDRepository = idRepository;
        }
    }
}
