using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class DeleteRelatedEntitiesExtensions
    {
        public static void DeleteRelatedEntities(
            this Document document,
            IDocumentRepository documentRepository,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            ISampleRepository sampleRepository,
            IAudioFileOutputRepository audioFileOutputRepository,
            IDocumentReferenceRepository documentReferenceRepository,
            INodeRepository nodeRepository,
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (document == null) throw new NullException(() => document);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (documentReferenceRepository == null) throw new NullException(() => documentReferenceRepository);

            foreach (Document instrument in document.Instruments)
            {
                // Recursive call
                instrument.DeleteRelatedEntities(documentRepository, curveRepository, patchRepository, sampleRepository, audioFileOutputRepository, documentReferenceRepository, nodeRepository, audioFileOutputChannelRepository, operatorRepository, inletRepository, outletRepository, entityPositionRepository);
                instrument.UnlinkRelatedEntities();
                documentRepository.Delete(instrument);
            }

            foreach (Document effect in document.Effects)
            {
                // Recursive call
                effect.DeleteRelatedEntities(documentRepository, curveRepository, patchRepository, sampleRepository, audioFileOutputRepository, documentReferenceRepository, nodeRepository, audioFileOutputChannelRepository, operatorRepository, inletRepository, outletRepository, entityPositionRepository);
                effect.UnlinkRelatedEntities();
                documentRepository.Delete(effect);
            }

            foreach (Curve curve in document.Curves)
            {
                curve.DeleteRelatedEntities(nodeRepository);
                curve.UnlinkRelatedEntities();
                curveRepository.Delete(curve);
            }

            foreach (Sample sample in document.Samples)
            {
                sample.UnlinkRelatedEntities();
                sampleRepository.Delete(sample);
            }

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                audioFileOutput.DeleteRelatedEntities(audioFileOutputChannelRepository);
                audioFileOutput.UnlinkRelatedEntities();
                audioFileOutputRepository.Delete(audioFileOutput);
            }

            foreach (Patch patch in document.Patches)
            {
                patch.DeleteRelatedEntities(operatorRepository, inletRepository, outletRepository, entityPositionRepository);
                patch.UnlinkRelatedEntities();
                patchRepository.Delete(patch);
            }

            foreach (DocumentReference documentReference in document.DependentOnDocuments)
            {
                documentReference.UnlinkRelatedEntities();
                documentReferenceRepository.Delete(documentReference);
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
            
            throw new NotImplementedException();
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
