using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToScreenViewModelExtensions
    {
        // Full Document

        public static DocumentViewModel ToViewModel(this Document document, RepositoryWrapper repositoryWrapper, EntityPositionManager entityPositionManager)
        {
            if (document == null) throw new NullException(() => document);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            var viewModel = new DocumentViewModel
            {
                ID = document.ID,
                DocumentTree = document.ToTreeViewModel(),
                DocumentProperties = document.ToPropertiesViewModel(),
                AudioFileOutputPropertiesList = document.AudioFileOutputs.ToPropertiesViewModels(repositoryWrapper.AudioFileFormatRepository, repositoryWrapper.SampleDataTypeRepository, repositoryWrapper.SpeakerSetupRepository),
                AudioFileOutputList = document.ToAudioFileOutputListViewModel(),
                InstrumentList = document.Instruments.ToChildDocumentListViewModel(document.ID, ChildDocumentTypeEnum.Instrument),
                InstrumentPropertiesList = document.Instruments.ToChildDocumentPropertiesViewModels(),
                InstrumentDocumentList = document.Instruments.ToChildDocumentViewModels(repositoryWrapper, entityPositionManager),
                EffectList = document.Effects.ToChildDocumentListViewModel(document.ID, ChildDocumentTypeEnum.Effect),
                EffectPropertiesList = document.Effects.ToChildDocumentPropertiesViewModels(),
                EffectDocumentList = document.Effects.ToChildDocumentViewModels(repositoryWrapper, entityPositionManager),
                CurveDetailsList = document.Curves.ToDetailsViewModels(repositoryWrapper.NodeTypeRepository),
                CurveList = document.Curves.ToListViewModel(document.ID, null),
                PatchDetailsList = document.Patches.ToDetailsViewModels(repositoryWrapper.OperatorTypeRepository, entityPositionManager),
                PatchList = document.Patches.ToListViewModel(document.ID, null),
                SampleList = document.Samples.ToListViewModel(document.ID, null),
                SamplePropertiesList = document.Samples.ToPropertiesViewModels(new SampleRepositories(repositoryWrapper))
            };

            return viewModel;
        }

        // AudioFileOutput

        public static IList<AudioFileOutputPropertiesViewModel> ToPropertiesViewModels(
            this IList<AudioFileOutput> entities,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            // TODO: Inline this if you do not sort anyway.
            if (entities == null) throw new NullException(() => entities);
            IList<AudioFileOutputPropertiesViewModel> viewModels = entities.Select(x => x.ToPropertiesViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository)).ToList();
            return viewModels;
        }

        public static AudioFileOutputPropertiesViewModel ToPropertiesViewModel(
            this AudioFileOutput entity,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Document == null) throw new NullException(() => entity.Document);

            var viewModel = new AudioFileOutputPropertiesViewModel
            {
                Entity = entity.ToViewModelWithRelatedEntities(),
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            // TODO: Delegate to something in ViewModelHelper_Lookups.cs?
            IList<Outlet> outlets = entity.Document.Patches
                                                   .SelectMany(x => x.Operators)
                                                   .Where(x => x.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
                                                   .SelectMany(x => x.Outlets)
                                                   .ToArray();
            // TODO: Sort by something.

            // TODO: This will not cut it, because you only see the operator name on screen, not the patch name.
            viewModel.OutletLookup = outlets.Select(x => x.ToIDAndName()).ToArray();

            return viewModel;
        }

        // Curve

        public static IList<CurveDetailsViewModel> ToDetailsViewModels(this IList<Curve> entities, INodeTypeRepository nodeTypeRepository)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<CurveDetailsViewModel> viewModels = entities.OrderBy(x => x.Name)
                                                              .Select(x => x.ToDetailsViewModel(nodeTypeRepository))
                                                              .ToList();
            return viewModels;
        }

        public static CurveDetailsViewModel ToDetailsViewModel(this Curve curve, INodeTypeRepository nodeTypeRepository)
        {
            if (curve == null) throw new NullException(() => curve);

            var viewModel = new CurveDetailsViewModel
            {
                Entity = curve.ToViewModelWithRelatedEntities(),
                NodeTypes = ViewModelHelper.CreateNodeTypesLookupViewModel(nodeTypeRepository)
            };

            return viewModel;
        }

        // Document

        public static DocumentDetailsViewModel ToDetailsViewModel(this Document document)
        {
            var viewModel = new DocumentDetailsViewModel
            {
                Document = document.ToIDAndName(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentPropertiesViewModel ToPropertiesViewModel(this Document document)
        {
            var viewModel = new DocumentPropertiesViewModel
            {
                Document = document.ToIDAndName(),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            return viewModel;
        }

        public static DocumentDeleteViewModel ToDeleteViewModel(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new DocumentDeleteViewModel
            {
                Document = new IDAndName
                {
                    ID = entity.ID,
                    Name = entity.Name,
                }
            };

            return viewModel;
        }

        public static DocumentCannotDeleteViewModel ToCannotDeleteViewModel(this Document entity, IList<Message> messages)
        {
            if (messages == null) throw new NullException(() => messages);

            var viewModel = new DocumentCannotDeleteViewModel
            {
                Document = entity.ToIDAndName(),
                Messages = messages
            };

            return viewModel;
        }

        // ChildDocument

        public static IList<ChildDocumentPropertiesViewModel> ToChildDocumentPropertiesViewModels(this IList<Document> childDocuments)
        {
            if (childDocuments == null) throw new NullException(() => childDocuments);

            IList<ChildDocumentPropertiesViewModel> viewModels = childDocuments.OrderBy(x => x.Name)
                                                                               .Select(x => x.ToChildDocumentPropertiesViewModel())
                                                                               .ToList();
            return viewModels;
        }

        public static ChildDocumentPropertiesViewModel ToChildDocumentPropertiesViewModel(this Document childDocument)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            var viewModel = new ChildDocumentPropertiesViewModel
            {
                Name = childDocument.Name,
                ValidationMessages = new List<Message>(),
                ID = childDocument.ID
            };

            return viewModel;
        }

        public static IList<ChildDocumentViewModel> ToChildDocumentViewModels(this IList<Document> childDocuments, RepositoryWrapper repositoryWrapper, EntityPositionManager entityPositionManager)
        {
            if (childDocuments == null) throw new NullException(() => childDocuments);

            IList<ChildDocumentViewModel> viewModels = new List<ChildDocumentViewModel>(childDocuments.Count);

            childDocuments = childDocuments.OrderBy(x => x.Name).ToArray();

            for (int i = 0; i < childDocuments.Count; i++)
            {
                Document entity = childDocuments[i];
                ChildDocumentViewModel viewModel = entity.ToChildDocumentViewModel(repositoryWrapper, entityPositionManager);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static ChildDocumentViewModel ToChildDocumentViewModel(
            this Document childDocument,
            RepositoryWrapper repositoryWrapper,
            EntityPositionManager entityPositionManager)
        {
            if (childDocument == null) throw new NullException(() => childDocument);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            int rootDocumentID = ChildDocumentHelper.GetParentDocumentID(childDocument);

            var viewModel = new ChildDocumentViewModel
            {
                Name = childDocument.Name,
                SampleList = childDocument.Samples.ToListViewModel(rootDocumentID, childDocument.ID),
                SamplePropertiesList = childDocument.Samples.ToPropertiesViewModels(new SampleRepositories(repositoryWrapper)),
                CurveList = childDocument.Curves.ToListViewModel(rootDocumentID, childDocument.ID),
                CurveDetailsList = childDocument.Curves.ToDetailsViewModels(repositoryWrapper.NodeTypeRepository),
                PatchList = childDocument.Patches.ToListViewModel(rootDocumentID, childDocument.ID),
                PatchDetailsList = childDocument.Patches.ToDetailsViewModels(repositoryWrapper.OperatorTypeRepository, entityPositionManager),
                ID = childDocument.ID,
            };

            return viewModel;
        }

        // Sample

        public static IList<SamplePropertiesViewModel> ToPropertiesViewModels(this IList<Sample> entities, SampleRepositories sampleRepositories)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<SamplePropertiesViewModel> viewModels = entities.OrderBy(x => x.Name)
                                                                  .Select(x => x.ToPropertiesViewModel(sampleRepositories))
                                                                  .ToList();
            return viewModels;
        }

        public static SamplePropertiesViewModel ToPropertiesViewModel(this Sample entity, SampleRepositories sampleRepositories)
        {
            if (sampleRepositories == null) throw new NullException(() => sampleRepositories);

            var viewModel = new SamplePropertiesViewModel
            {
                Entity = entity.ToViewModel(),
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(sampleRepositories.AudioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleRepositories.SampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(sampleRepositories.SpeakerSetupRepository),
                InterpolationTypes = ViewModelHelper.CreateInterpolationTypesLookupViewModel(sampleRepositories.InterpolationTypeRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            return viewModel;
        }
    }
}