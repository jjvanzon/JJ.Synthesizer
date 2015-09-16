using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using System;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.LinkTo;

namespace JJ.Business.Synthesizer.Managers
{
    public class CurveManager
    {
        private ICurveRepository _curveRepository;
        private INodeRepository _nodeRepository;
        private IIDRepository _idRepository;

        public CurveManager(ICurveRepository curveRepository, INodeRepository nodeRepository, IIDRepository idRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _curveRepository = curveRepository;
            _nodeRepository = nodeRepository;
            _idRepository = idRepository;
        }

        public VoidResult DeleteWithRelatedEntities(int curveID)
        {
            Curve curve = _curveRepository.Get(curveID);
            return DeleteWithRelatedEntities(curve);
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

        public Curve Create(Document document, bool mustGenerateName)
        {
            var curve = new Curve();
            curve.ID = _idRepository.GetID();
            _curveRepository.Insert(curve);
            curve.LinkTo(document);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Curve_SideEffect_GenerateName(curve);
                sideEffect.Execute();
            }

            return curve;
        }
    }
}
