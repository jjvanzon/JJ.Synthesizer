using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Converters;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToScreenViewModelExtensions
    {
        // TODO: Remove outcommented code.
        //private const int DUMMY_CHILD_DOCUMENT_TYPE_ID = 0;

        private static int _maxVisiblePageNumbers = GetMaxVisiblePageNumbers();

        private static HashSet<OperatorTypeEnum> _operatorTypeEnums_WithTheirOwnPropertyViews = new HashSet<OperatorTypeEnum>
        {
            OperatorTypeEnum.Bundle,
            OperatorTypeEnum.Curve,
            OperatorTypeEnum.CustomOperator,
            OperatorTypeEnum.Number,
            OperatorTypeEnum.PatchInlet,
            OperatorTypeEnum.PatchOutlet,
            OperatorTypeEnum.Sample,
            OperatorTypeEnum.Unbundle
        };

        // AudioFileOutput

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
                AudioFileFormatLookup = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypeLookup = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetupLookup = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            // TODO: Delegate to something in ViewModelHelper_Lookups.cs?
            IList<Outlet> outlets = entity.Document.Patches
                                                   .SelectMany(x => x.Operators)
                                                   .Where(x => x.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
                                                   .SelectMany(x => x.Outlets)
                                                   .ToArray();

            // TODO: This will not cut it, because you only see the operator name on screen, not the patch name.
            viewModel.OutletLookup = outlets.Select(x => x.ToIDAndName())
                                            .OrderBy(x => x.Name)
                                            .ToArray();
            return viewModel;
        }

        public static AudioFileOutputGridViewModel ToAudioFileOutputGridViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<AudioFileOutput> sortedEntities = document.AudioFileOutputs.OrderBy(x => x.Name).ToList();

            var viewModel = new AudioFileOutputGridViewModel
            {
                List = sortedEntities.ToListItemViewModels(),
                DocumentID = document.ID
            };

            return viewModel;
        }

        // ChildDocument

        public static ChildDocumentPropertiesViewModel ToChildDocumentPropertiesViewModel(this Document childDocument, IChildDocumentTypeRepository childDocumentTypeRepository)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            var viewModel = new ChildDocumentPropertiesViewModel
            {
                ID = childDocument.ID,
                Name = childDocument.Name,
                ChildDocumentTypeLookup = ViewModelHelper.CreateChildDocumentTypeLookupViewModel(childDocumentTypeRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            if (childDocument.ChildDocumentType != null)
            {
                viewModel.ChildDocumentType = childDocument.ChildDocumentType.ToIDAndDisplayName();
            }

            return viewModel;
        }

        public static IList<ChildDocumentGridViewModel> ToChildDocumentGridViewModelList(this Document rootDocument)
        {
            var groups = rootDocument.ChildDocuments.GroupBy(x => x.GroupName);

            var list = new List<ChildDocumentGridViewModel>();

            foreach (var group in groups)
            {
                ChildDocumentGridViewModel childDocumentGridViewModel = group.ToChildDocumentGridViewModel(rootDocument.ID, group.Key);

                list.Add(childDocumentGridViewModel);
            }

            return list;

        }

        public static ChildDocumentGridViewModel ToChildDocumentGridViewModel(this Document rootDocument, string group)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            IList<Document> childDocuments = rootDocument.ChildDocuments
                                                         .Where(x => String.Equals(x.GroupName, group))
                                                         .ToList();

            ChildDocumentGridViewModel viewModel = childDocuments.ToChildDocumentGridViewModel(rootDocument.ID, group);

            return viewModel;
        }

        public static ChildDocumentGridViewModel ToChildDocumentGridViewModel(
            this IEnumerable<Document> entities,
            int rootDocumentID,
            string group)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ChildDocumentGridViewModel
            {
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList(),
                RootDocumentID = rootDocumentID,
                Group = group
            };

            return viewModel;
        }

        [Obsolete]
        public static ChildDocumentGridViewModel ToChildDocumentGridViewModel(this Document rootDocument, int childDocumentTypeID)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            IList<Document> childDocuments = rootDocument.ChildDocuments
                                                         .Where(x => x.ChildDocumentType != null &&
                                                                     x.ChildDocumentType.ID == childDocumentTypeID)
                                                         .ToList();

            ChildDocumentGridViewModel viewModel = childDocuments.ToChildDocumentGridViewModel(rootDocument.ID, childDocumentTypeID);

            return viewModel;
        }

        [Obsolete]
        public static ChildDocumentGridViewModel ToChildDocumentGridViewModel(
            this IList<Document> entities,
            int rootDocumentID,
            int childDocumentTypeID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ChildDocumentGridViewModel
            {
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList(),
                RootDocumentID = rootDocumentID,
                ChildDocumentTypeID = childDocumentTypeID
            };

            return viewModel;
        }

        // Curve

        public static CurveDetailsViewModel ToDetailsViewModel(this Curve curve, INodeTypeRepository nodeTypeRepository)
        {
            if (curve == null) throw new NullException(() => curve);

            var viewModel = new CurveDetailsViewModel
            {
                Entity = curve.ToViewModelWithRelatedEntities(),
                NodeTypeLookup = ViewModelHelper.CreateNodeTypeLookupViewModel(nodeTypeRepository),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static CurveGridViewModel ToGridViewModel(this IList<Curve> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new CurveGridViewModel
            {
                DocumentID = documentID,
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList()
            };

            return viewModel;
        }

        public static CurvePropertiesViewModel ToPropertiesViewModel(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new CurvePropertiesViewModel
            {
                Entity = entity.ToIDAndName(),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            return viewModel;
        }

        public static NodePropertiesViewModel ToPropertiesViewModel(this Node entity, INodeTypeRepository nodeTypeRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new NodePropertiesViewModel
            {
                Entity = entity.ToViewModel(),
                ValidationMessages = new List<Message>(),
                NodeTypeLookup = ViewModelHelper.CreateNodeTypeLookupViewModel(nodeTypeRepository),
                Successful = true
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
                Entity = document.ToIDAndName(),
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

        public static DocumentTreeViewModel ToTreeViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new DocumentTreeViewModel
            {
                ID = document.ID,
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                AudioFileOutputsNode = new DummyViewModel(),
                PatchesNode = new PatchesTreeNodeViewModel
                {
                    PatchGroupNodes = new List<PatchGroupTreeNodeViewModel>()
                },
                InstrumentNode = new List<PatchTreeNodeViewModel>(),
                EffectNode = new List<PatchTreeNodeViewModel>(),
                ReferencedDocumentsNode = new ReferencedDocumentsTreeNodeViewModel
                {
                    List = new List<ReferencedDocumentViewModel>()
                }
            };

            viewModel.ReferencedDocumentsNode.List = document.DependentOnDocuments.Select(x => x.DependentOnDocument)
                                                                                  .Select(x => x.ToReferencedDocumentViewModelWithRelatedEntities())
                                                                                  .OrderBy(x => x.Name)
                                                                                  .ToList();
            // Groupless Patches
            IList<Document> grouplessChildDocuments = document.ChildDocuments.Where(x => String.IsNullOrWhiteSpace(x.GroupName)).ToArray();
            viewModel.PatchesNode.PatchNodes = grouplessChildDocuments.Select(x => x.ToChildDocumentTreeNodeViewModel()).ToList();

            // Patch Groups
            var childDocumentGroups = document.ChildDocuments.Where(x => !String.IsNullOrWhiteSpace(x.GroupName))
                                                             .GroupBy(x => x.GroupName);
            foreach (var childDocumentGroup in childDocumentGroups)
            {
                viewModel.PatchesNode.PatchGroupNodes.Add(new PatchGroupTreeNodeViewModel
                {
                    Name = childDocumentGroup.Key,
                    Patches = childDocumentGroup.Select(x => x.ToChildDocumentTreeNodeViewModel()).ToList()
                });
            }

            // Obsolete. Remove code later.
            viewModel.InstrumentNode = document.ChildDocuments.Where(x => x.GetChildDocumentTypeEnum() == ChildDocumentTypeEnum.Instrument)
                                                           .OrderBy(x => x.Name)
                                                           .Select(x => x.ToChildDocumentTreeNodeViewModel())
                                                           .ToList();

            viewModel.EffectNode = document.ChildDocuments.Where(x => x.GetChildDocumentTypeEnum() == ChildDocumentTypeEnum.Effect)
                                                       .OrderBy(x => x.Name)
                                                       .Select(x => x.ToChildDocumentTreeNodeViewModel())
                                                       .ToList();
            return viewModel;
        }

        public static PatchTreeNodeViewModel ToChildDocumentTreeNodeViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new PatchTreeNodeViewModel
            {
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                ChildDocumentID = document.ID
            };

            return viewModel;
        }

        public static DocumentGridViewModel ToGridViewModel(this IList<Document> pageOfEntities, int pageIndex, int pageSize, int totalCount)
        {
            if (pageOfEntities == null) throw new NullException(() => pageOfEntities);

            var viewModel = new DocumentGridViewModel
            {
                List = pageOfEntities.Select(x => x.ToIDAndName()).ToList(),
                Pager = PagerViewModelFactory.Create(pageIndex, pageSize, totalCount, _maxVisiblePageNumbers)
            };

            return viewModel;
        }

        // Operator

        /// <summary> Converts to properties view models, the operators that do not have a specialized properties view. </summary>
        public static IList<OperatorPropertiesViewModel> ToOperatorPropertiesViewModelList(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            var viewModels = new List<OperatorPropertiesViewModel>();

            foreach (Operator op in patch.Operators)
            {
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

                if (!_operatorTypeEnums_WithTheirOwnPropertyViews.Contains(operatorTypeEnum))
                {
                    OperatorPropertiesViewModel viewModel = op.ToPropertiesViewModel();
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        public static IList<OperatorPropertiesViewModel_ForBundle> ToOperatorPropertiesViewModelList_ForBundles(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Bundle)
                        .Select(x => x.ToPropertiesViewModel_ForBundle())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForCurve> ToOperatorPropertiesViewModelList_ForCurves(this Patch patch, ICurveRepository curveRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Curve)
                        .Select(x => x.ToPropertiesViewModel_ForCurve(curveRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForCustomOperator> ToOperatorPropertiesViewModelList_ForCustomOperators(this Patch patch, IDocumentRepository documentRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.CustomOperator)
                        .Select(x => x.ToPropertiesViewModel_ForCustomOperator(documentRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForNumber> ToPropertiesViewModelList_ForNumbers(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Number)
                        .Select(x => x.ToPropertiesViewModel_ForNumber())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForPatchInlet> ToPropertiesViewModelList_ForPatchInlets(
            this Patch patch, IInletTypeRepository inletTypeRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                        .Select(x => x.ToPropertiesViewModel_ForPatchInlet(inletTypeRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForPatchOutlet> ToPropertiesViewModelList_ForPatchOutlets(
            this Patch patch, IOutletTypeRepository outletTypeRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                        .Select(x => x.ToPropertiesViewModel_ForPatchOutlet(outletTypeRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForSample> ToOperatorPropertiesViewModelList_ForSamples(this Patch patch, ISampleRepository sampleRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Sample)
                        .Select(x => x.ToPropertiesViewModel_ForSample(sampleRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForUnbundle> ToOperatorPropertiesViewModelList_ForUnbundles(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Unbundle)
                        .Select(x => x.ToPropertiesViewModel_ForUnbundle())
                        .ToList();
        }

        public static OperatorPropertiesViewModel ToPropertiesViewModel(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OperatorPropertiesViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            if (entity.OperatorType != null)
            {
                viewModel.OperatorType = entity.OperatorType.ToViewModel();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle ToPropertiesViewModel_ForBundle(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var wrapper = new OperatorWrapper_Bundle(entity);

            var viewModel = new OperatorPropertiesViewModel_ForBundle
            {
                ID = entity.ID,
                Name = entity.Name,
                InletCount = entity.Inlets.Count,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve ToPropertiesViewModel_ForCurve(this Operator entity, ICurveRepository curveRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OperatorPropertiesViewModel_ForCurve
            {
                ID = entity.ID,
                Name = entity.Name,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            var wrapper = new OperatorWrapper_Curve(entity, curveRepository);

            Curve curve = wrapper.Curve;
            if (curve != null)
            {
                viewModel.Curve = curve.ToIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator ToPropertiesViewModel_ForCustomOperator(this Operator entity, IDocumentRepository documentRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OperatorPropertiesViewModel_ForCustomOperator
            {
                ID = entity.ID,
                Name = entity.Name,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            var wrapper = new OperatorWrapper_CustomOperator(entity, documentRepository);

            Document underlyingDocument = wrapper.UnderlyingDocument;
            if (underlyingDocument != null)
            {
                viewModel.UnderlyingDocument = underlyingDocument.ToIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber ToPropertiesViewModel_ForNumber(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var wrapper = new OperatorWrapper_Number(entity);

            var viewModel = new OperatorPropertiesViewModel_ForNumber
            {
                ID = entity.ID,
                Name = entity.Name,
                Number = wrapper.Number.ToString(),
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet ToPropertiesViewModel_ForPatchInlet(
            this Operator entity, IInletTypeRepository inletTypeRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (inletTypeRepository == null) throw new NullException(() => inletTypeRepository);

            var wrapper = new OperatorWrapper_PatchInlet(entity);

            var viewModel = new OperatorPropertiesViewModel_ForPatchInlet
            {
                ID = entity.ID,
                Name = entity.Name,
                DefaultValue = Convert.ToString(wrapper.DefaultValue),
                InletTypeLookup = ViewModelHelper.CreateInletTypeLookupViewModel(inletTypeRepository),
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            if (wrapper.ListIndex.HasValue)
            {
                viewModel.Number = wrapper.ListIndex.Value + 1;
            }

            if (wrapper.InletTypeEnum.HasValue)
            {
                viewModel.InletType = wrapper.InletTypeEnum.Value.ToIDAndDisplayName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet ToPropertiesViewModel_ForPatchOutlet(
            this Operator entity, IOutletTypeRepository outletTypeRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (outletTypeRepository == null) throw new NullException(() => outletTypeRepository);

            var wrapper = new OperatorWrapper_PatchOutlet(entity);

            var viewModel = new OperatorPropertiesViewModel_ForPatchOutlet
            {
                ID = entity.ID,
                Name = entity.Name,
                OutletTypeLookup = ViewModelHelper.CreateOutletTypeLookupViewModel(outletTypeRepository),
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            if (wrapper.ListIndex.HasValue)
            {
                viewModel.Number = wrapper.ListIndex.Value + 1;
            }

            if (wrapper.OutletTypeEnum.HasValue)
            {
                viewModel.OutletType = wrapper.OutletTypeEnum.Value.ToIDAndDisplayName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample ToPropertiesViewModel_ForSample(this Operator entity, ISampleRepository sampleRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OperatorPropertiesViewModel_ForSample
            {
                ID = entity.ID,
                Name = entity.Name,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            var wrapper = new OperatorWrapper_Sample(entity, sampleRepository);

            Sample sample = wrapper.Sample;
            if (sample != null)
            {
                viewModel.Sample = sample.ToIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForUnbundle ToPropertiesViewModel_ForUnbundle(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var wrapper = new OperatorWrapper_Unbundle(entity);

            var viewModel = new OperatorPropertiesViewModel_ForUnbundle
            {
                ID = entity.ID,
                Name = entity.Name,
                OutletCount = entity.Outlets.Count,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // Patch

        public static PatchDetailsViewModel ToDetailsViewModel(
            this Patch patch,
            IOperatorTypeRepository operatorTypeRepository, 
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            IDocumentRepository documentRepository,
            EntityPositionManager entityPositionManager)
        {
            var converter = new RecursiveToViewModelConverter(
                operatorTypeRepository, sampleRepository, curveRepository, documentRepository, entityPositionManager);

            return converter.ConvertToDetailsViewModel(patch);
        }

        // Sample

        public static SamplePropertiesViewModel ToPropertiesViewModel(this Sample entity, SampleRepositories repositories)
        {
            if (entity == null) throw new NullException(() => entity);
            if (repositories == null) throw new NullException(() => repositories);

            var viewModel = new SamplePropertiesViewModel
            {
                AudioFileFormatLookup = ViewModelHelper.CreateAudioFileFormatLookupViewModel(repositories.AudioFileFormatRepository),
                SampleDataTypeLookup = ViewModelHelper.CreateSampleDataTypeLookupViewModel(repositories.SampleDataTypeRepository),
                SpeakerSetupLookup = ViewModelHelper.CreateSpeakerSetupLookupViewModel(repositories.SpeakerSetupRepository),
                InterpolationTypeLookup = ViewModelHelper.CreateInterpolationTypesLookupViewModel(repositories.InterpolationTypeRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            byte[] bytes = repositories.SampleRepository.TryGetBytes(entity.ID);
            viewModel.Entity = entity.ToViewModel(bytes);

            return viewModel;
        }

        public static SampleGridViewModel ToGridViewModel(this IList<Sample> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new SampleGridViewModel
            {
                DocumentID = documentID,
                List = entities.ToListItemViewModels()
            };

            return viewModel;
        }

        // Scale

        public static ToneGridEditViewModel ToDetailsViewModel(this Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ToneGridEditViewModel
            {
                ScaleID = entity.ID,
                NumberTitle =  ViewModelHelper.GetToneGridEditNumberTitle(entity),
                Tones = entity.Tones.ToToneViewModels(),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            return viewModel;
        }

        public static IList<ToneViewModel> ToToneViewModels(this IList<Tone> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<ToneViewModel> viewModels = entities.OrderBy(x => x.Octave)
                                                      .ThenBy(x => x.Number)
                                                      .Select(x => x.ToViewModel())
                                                      .ToList();
            return viewModels;
        }

        public static ScalePropertiesViewModel ToPropertiesViewModel(this Scale entity, IScaleTypeRepository scaleTypeRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (scaleTypeRepository == null) throw new NullException(() => scaleTypeRepository);

            var viewModel = new ScalePropertiesViewModel
            {
                Entity = entity.ToViewModel(),
                ScaleTypeLookup = ViewModelHelper.CreateScaleTypeLookupViewModel(scaleTypeRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            return viewModel;
        }

        public static ScaleGridViewModel ToGridViewModel(this IList<Scale> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ScaleGridViewModel
            {
                DocumentID = documentID,
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList()
            };

            return viewModel;
        }

        // Helpers

        private static int GetMaxVisiblePageNumbers()
        {
            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            return config.MaxVisiblePageNumbers;
        }
    }
}