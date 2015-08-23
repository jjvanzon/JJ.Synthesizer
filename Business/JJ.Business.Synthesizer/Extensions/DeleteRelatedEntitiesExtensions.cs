using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class DeleteRelatedEntitiesExtensions
    {
        public static void DeleteRelatedEntities(this Document document, RepositoryWrapper repositoryWrapper)
        {
            if (document == null) throw new NullException(() => document);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            foreach (Document childDocument in document.ChildDocuments.ToArray())
            {
                // Recursive call
                childDocument.DeleteRelatedEntities(repositoryWrapper);
                childDocument.UnlinkRelatedEntities();
                repositoryWrapper.DocumentRepository.Delete(childDocument);
            }

            foreach (Curve curve in document.Curves.ToArray())
            {
                curve.DeleteRelatedEntities(repositoryWrapper.NodeRepository);
                curve.UnlinkRelatedEntities();
                repositoryWrapper.CurveRepository.Delete(curve);
            }

            foreach (Sample sample in document.Samples.ToArray())
            {
                sample.UnlinkRelatedEntities();
                repositoryWrapper.SampleRepository.Delete(sample);
            }

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs.ToArray())
            {
                audioFileOutput.DeleteRelatedEntities(repositoryWrapper.AudioFileOutputChannelRepository);
                audioFileOutput.UnlinkRelatedEntities();
                repositoryWrapper.AudioFileOutputRepository.Delete(audioFileOutput);
            }

            foreach (Patch patch in document.Patches.ToArray())
            {
                patch.DeleteRelatedEntities(repositoryWrapper.OperatorRepository, repositoryWrapper.InletRepository, repositoryWrapper.OutletRepository, repositoryWrapper.EntityPositionRepository);
                patch.UnlinkRelatedEntities();
                repositoryWrapper.PatchRepository.Delete(patch);
            }

            foreach (DocumentReference documentReference in document.DependentOnDocuments.ToArray())
            {
                documentReference.UnlinkRelatedEntities();
                repositoryWrapper.DocumentReferenceRepository.Delete(documentReference);
            }
        }

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

        public static void DeleteRelatedEntities(this AudioFileOutput audioFileOutput, IAudioFileOutputChannelRepository audioFileOutputChannelRepository)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);

            foreach (AudioFileOutputChannel audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels.ToArray())
            {
                audioFileOutputChannel.UnlinkRelatedEntities();
                audioFileOutputChannelRepository.Delete(audioFileOutputChannel);
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
    }
}
