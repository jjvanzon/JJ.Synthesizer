using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;

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

            IValidator validator = new CurveValidator_Delete(curve, _curveRepository);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
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
    }
}
