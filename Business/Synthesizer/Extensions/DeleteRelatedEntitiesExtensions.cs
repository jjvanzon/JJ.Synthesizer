using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Extensions
{
    /// <summary> Deletes related entities that are inherently part of the entity. </summary>
    internal static class DeleteRelatedEntitiesExtensions
    {
        public static void DeleteRelatedEntities(this Curve curve, INodeRepository nodeRepository)
        {
            if (curve == null) throw new NullException(() => curve);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);

            foreach (Node node in curve.Nodes.ToArray())
            {
                node.UnlinkRelatedEntities();
                nodeRepository.Delete(node);
            }
        }

        public static void DeleteRelatedEntities(this Document document, RepositoryWrapper repositories)
        {
            if (document == null) throw new NullException(() => document);
            if (repositories == null) throw new NullException(() => repositories);

            foreach (Patch patch in document.Patches.ToArray())
            {
                patch.DeleteRelatedEntities(repositories.OperatorRepository, repositories.InletRepository, repositories.OutletRepository, repositories.EntityPositionRepository);
                patch.UnlinkRelatedEntities();
                repositories.PatchRepository.Delete(patch);
            }

            if (document.AudioOutput != null)
            {
                document.AudioOutput.UnlinkRelatedEntities();
                repositories.AudioOutputRepository.Delete(document.AudioOutput);
            }

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs.ToArray())
            {
                audioFileOutput.UnlinkRelatedEntities();
                repositories.AudioFileOutputRepository.Delete(audioFileOutput);
            }

            foreach (Curve curve in document.Curves.ToArray())
            {
                curve.DeleteRelatedEntities(repositories.NodeRepository);
                curve.UnlinkRelatedEntities();
                repositories.CurveRepository.Delete(curve);
            }

            foreach (Sample sample in document.Samples.ToArray())
            {
                sample.UnlinkRelatedEntities();
                repositories.SampleRepository.Delete(sample);
            }

            foreach (Scale scale in document.Scales.ToArray())
            {
                scale.DeleteRelatedEntities(repositories.ToneRepository);
                scale.UnlinkRelatedEntities();
                repositories.ScaleRepository.Delete(scale);
            }

            foreach (DocumentReference documentReference in document.LowerDocumentReferences.ToArray())
            {
                documentReference.UnlinkRelatedEntities();
                repositories.DocumentReferenceRepository.Delete(documentReference);
            }
        }

        public static void DeleteRelatedEntities(
            this Patch patch,
            IOperatorRepository operatorRepository, 
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (patch == null) throw new NullException(() => patch);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            foreach (Operator op in patch.Operators.ToArray())
            {
                op.DeleteRelatedEntities(inletRepository, outletRepository, entityPositionRepository);
                op.UnlinkRelatedEntities();
                operatorRepository.Delete(op);
            }
        }

        public static void DeleteRelatedEntities(
            this Operator op, 
            IInletRepository inletRepository, 
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            foreach (Inlet inlet in op.Inlets.ToArray())
            {
                inlet.UnlinkRelatedEntities();
                inletRepository.Delete(inlet);
            }

            foreach (Outlet outlet in op.Outlets.ToArray())
            {
                outlet.UnlinkRelatedEntities();
                outletRepository.Delete(outlet);
            }

            // Be null-tollerant to be able to get out of trouble if something is missing.
            EntityPosition entityPosition = entityPositionRepository.TryGetByEntityTypeNameAndEntityID(typeof(OperatingSystem).Name, op.ID);
            if (entityPosition != null)
            {
                entityPositionRepository.Delete(entityPosition);
            }
        }

        public static void DeleteRelatedEntities(this Scale scale, IToneRepository toneRepository)
        {
            if (scale == null) throw new NullException(() => scale);

            foreach (Tone tone in scale.Tones.ToArray())
            {
                toneRepository.Delete(tone);
            }
        }
    }
}
