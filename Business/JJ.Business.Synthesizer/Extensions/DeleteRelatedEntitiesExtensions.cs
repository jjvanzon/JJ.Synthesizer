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
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            foreach (Document instrument in document.Instruments)
            {
                // Recursive call
                instrument.DeleteRelatedEntities(repositoryWrapper);
                instrument.UnlinkRelatedEntities();
                repositoryWrapper.DocumentRepository.Delete(instrument);
            }

            foreach (Document effect in document.Effects)
            {
                // Recursive call
                effect.DeleteRelatedEntities(repositoryWrapper);
                effect.UnlinkRelatedEntities();
                repositoryWrapper.DocumentRepository.Delete(effect);
            }

            foreach (Curve curve in document.Curves)
            {
                curve.DeleteRelatedEntities(repositoryWrapper.NodeRepository);
                curve.UnlinkRelatedEntities();
                repositoryWrapper.CurveRepository.Delete(curve);
            }

            foreach (Sample sample in document.Samples)
            {
                sample.UnlinkRelatedEntities();
                repositoryWrapper.SampleRepository.Delete(sample);
            }

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                audioFileOutput.DeleteRelatedEntities(repositoryWrapper.AudioFileOutputChannelRepository);
                audioFileOutput.UnlinkRelatedEntities();
                repositoryWrapper.AudioFileOutputRepository.Delete(audioFileOutput);
            }

            foreach (Patch patch in document.Patches)
            {
                patch.DeleteRelatedEntities(repositoryWrapper.OperatorRepository, repositoryWrapper.InletRepository, repositoryWrapper.OutletRepository, repositoryWrapper.EntityPositionRepository);
                patch.UnlinkRelatedEntities();
                repositoryWrapper.PatchRepository.Delete(patch);
            }

            foreach (DocumentReference documentReference in document.DependentOnDocuments)
            {
                documentReference.UnlinkRelatedEntities();
                repositoryWrapper.DocumentReferenceRepository.Delete(documentReference);
            }
        }

        public static void DeleteRelatedEntities(this Curve curve, INodeRepository nodeRepository)
        {
            if (curve == null) throw new NullException(() => curve);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);

            foreach (Node node in curve.Nodes)
            {
                node.UnlinkRelatedEntities();
                nodeRepository.Delete(node);
            }
        }

        public static void DeleteRelatedEntities(this AudioFileOutput audioFileOutput, IAudioFileOutputChannelRepository audioFileOutputChannelRepository)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);

            foreach (AudioFileOutputChannel audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
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

            foreach (Operator op in patch.Operators)
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

            foreach (Inlet inlet in op.Inlets)
            {
                inlet.UnlinkRelatedEntities();
                inletRepository.Delete(inlet);
            }

            foreach (Outlet outlet in op.Outlets)
            {
                outlet.UnlinkRelatedEntities();
                outletRepository.Delete(outlet);
            }

            entityPositionRepository.DeleteByEntityTypeAndEntityID(typeof(Operator).Name, op.ID);
        }
    }
}
