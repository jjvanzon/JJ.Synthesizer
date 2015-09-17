using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using System;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Managers
{
    public class CurveManager
    {
        private CurveRepositories _repositories;

        public CurveManager(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public VoidResult DeleteWithRelatedEntities(int curveID)
        {
            Curve curve = _repositories.CurveRepository.Get(curveID);
            return DeleteWithRelatedEntities(curve);
        }

        public VoidResult DeleteWithRelatedEntities(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            IValidator validator = new CurveValidator_Delete(curve, _repositories.CurveRepository);
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
                curve.DeleteRelatedEntities(_repositories.NodeRepository);
                _repositories.CurveRepository.Delete(curve);

                return new VoidResult
                {
                    Successful = true
                };
            }
        }

        public Curve Create(Document document, bool mustGenerateName)
        {
            var curve = new Curve();
            curve.ID = _repositories.IDRepository.GetID();
            _repositories.CurveRepository.Insert(curve);
            curve.LinkTo(document);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Curve_SideEffect_GenerateName(curve);
                sideEffect.Execute();
            }

            return curve;
        }

        public void DeleteNode(int nodeID)
        {
            Node node = _repositories.NodeRepository.Get(nodeID);
            DeleteNode(node);
        }

        public void DeleteNode(Node node)
        {
            if (node == null) throw new NullException(() => node);
            node.UnlinkRelatedEntities();
            _repositories.NodeRepository.Delete(node);
        }
    }
}
