using JJ.Framework.Common;
using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;

namespace JJ.Business.Synthesizer.Managers
{
    public class CurveManager
    {
        private ICurveRepository _curveRepository;
        private INodeRepository _nodeRepository;

        public CurveManager(ICurveRepository curveRepository, INodeRepository nodeRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);

            _curveRepository = curveRepository;
            _nodeRepository = nodeRepository;
        }

        public VoidResult DeleteWithRelatedEntities(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            bool canDelete = EnumerateCurveInOperators(curve).Any();
            if (!canDelete)
            {
                var message = new Message { PropertyKey = PropertyNames.Curve, Text = MessageFormatter.CannotDeleteCurve(curve.Name) };
                return new VoidResult
                {
                    Successful = false,
                    Messages = new Message[] { message }
                };
            }
            else
            {
                curve.UnlinkRelatedEntities();
                curve.DeleteRelatedEntities(_nodeRepository);
                _curveRepository.Delete(curve);

                return new VoidResult
                {
                    Successful = true
                };
            }
        }

        private IEnumerable<Operator> EnumerateCurveInOperators(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            foreach (Operator op in curve.Document.Patches.SelectMany(x => x.Operators))
            {
                if (!String.Equals(op.OperatorTypeName, PropertyNames.CurveIn))
                {
                    continue;
                }

                CurveInWrapper wrapper = new CurveInWrapper(op, _curveRepository);

                if (wrapper.Curve == curve ||
                    wrapper.CurveID == curve.ID)
                {
                    yield return op;
                }
            }
        }
    }
}
