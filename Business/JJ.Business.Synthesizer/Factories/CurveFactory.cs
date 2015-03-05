using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Factories
{
    public class CurveFactory
    {
        private ICurveRepository _curveRepository;
        private INodeRepository _nodeRepository;
        private INodeTypeRepository _nodeTypeRepository;

        public CurveFactory(
            ICurveRepository curveRepository,
            INodeRepository nodeRepository,
            INodeTypeRepository nodeTypeRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            _curveRepository = curveRepository;
            _nodeRepository = nodeRepository;
            _nodeTypeRepository = nodeTypeRepository;
        }

        public Curve Create(double timeSpan, params double?[] values)
        {
            if (timeSpan <= 0) throw new Exception("timespan must be greater than 0.");
            if (values.Length < 2) throw new Exception("values must contain at least 2 items.");

            Curve curve = _curveRepository.Create();

            double time = 0;
            double deltaT = timeSpan / (values.Length - 1);
            foreach (double? value in values)
            {
                if (value.HasValue)
                {
                    Node node = _nodeRepository.Create();
                    node.Time = time;
                    node.Value = value.Value;
                    node.SetNodeTypeEnum(NodeTypeEnum.Line, _nodeTypeRepository);
                    node.LinkTo(curve);
                }
                time += deltaT;
            }

            return curve;
        }
    }
}
