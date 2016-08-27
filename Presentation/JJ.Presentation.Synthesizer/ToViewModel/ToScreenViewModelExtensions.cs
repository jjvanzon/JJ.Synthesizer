using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.Converters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Dto;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToScreenViewModelExtensions
    {
        // AudioFileOutput

        public static AudioFileOutputPropertiesViewModel ToPropertiesViewModel(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Document == null) throw new NullException(() => entity.Document);

            var viewModel = new AudioFileOutputPropertiesViewModel
            {
                Entity = entity.ToViewModel(),
                AudioFileFormatLookup = ViewModelHelper.GetAudioFileFormatLookupViewModel(),
                SampleDataTypeLookup = ViewModelHelper.GetSampleDataTypeLookupViewModel(),
                SpeakerSetupLookup = ViewModelHelper.GetSpeakerSetupLookupViewModel(),
                ValidationMessages = new List<Message>()
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
                DocumentID = document.ID,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // AudioOutput

        public static AudioOutputPropertiesViewModel ToPropertiesViewModel(this AudioOutput entity)
        {
            var viewModel = new AudioOutputPropertiesViewModel
            {
                Entity = entity.ToViewModel(),
                SpeakerSetupLookup = ViewModelHelper.GetSpeakerSetupLookupViewModel(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // Curve

        public static CurveDetailsViewModel ToDetailsViewModel(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new CurveDetailsViewModel
            {
                CurveID = entity.ID,
                DocumentID = entity.Document.ID,
                Nodes = entity.Nodes.ToViewModelDictionary(),
                NodeTypeLookup = ViewModelHelper.GetNodeTypeLookupViewModel(),
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
                List = entities.ToListItemViewModels(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static CurvePropertiesViewModel ToPropertiesViewModel(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new CurvePropertiesViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                DocumentID = entity.Document.ID,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static NodePropertiesViewModel ToPropertiesViewModel(this Node entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new NodePropertiesViewModel
            {
                CurveID = entity.Curve.ID,
                Entity = entity.ToViewModel(),
                ValidationMessages = new List<Message>(),
                NodeTypeLookup = ViewModelHelper.GetNodeTypeLookupViewModel()
            };

            return viewModel;
        }

        // Document

        public static DocumentDetailsViewModel ToDetailsViewModel(this Document document)
        {
            var viewModel = new DocumentDetailsViewModel
            {
                Document = document.ToIDAndName(),
                AudioOutput = document.AudioOutput.ToViewModel(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentPropertiesViewModel ToPropertiesViewModel(this Document document)
        {
            var viewModel = new DocumentPropertiesViewModel
            {
                Entity = document.ToIDAndName(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentDeleteViewModel ToDeleteViewModel(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new DocumentDeleteViewModel
            {
                ValidationMessages = new List<Message>(),
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
                ValidationMessages = messages
            };

            return viewModel;
        }

        public static DocumentTreeViewModel ToTreeViewModel(
            this Document document, 
            IList<Patch> grouplessPatches,
            IList<PatchGroupDto> patchGroupDtos)
        {
            if (document == null) throw new NullException(() => document);
            if (grouplessPatches == null) throw new NullException(() => grouplessPatches);
            if (patchGroupDtos == null) throw new NullException(() => patchGroupDtos);

            var viewModel = new DocumentTreeViewModel
            {
                ID = document.ID,
                CurvesNode = ViewModelHelper.CreateTreeLeafViewModel(PropertyDisplayNames.Curves, document.Curves.Count),
                SamplesNode = ViewModelHelper.CreateTreeLeafViewModel(PropertyDisplayNames.Samples, document.Samples.Count),
                ScalesNode = ViewModelHelper.CreateTreeLeafViewModel(PropertyDisplayNames.Scales, document.Scales.Count),
                AudioOutputNode = ViewModelHelper.CreateTreeLeafViewModel(PropertyDisplayNames.AudioOutput),
                AudioFileOutputListNode = ViewModelHelper.CreateTreeLeafViewModel(PropertyDisplayNames.AudioFileOutput, document.AudioFileOutputs.Count),
                ValidationMessages = new List<Message>(),
                PatchesNode = new PatchesTreeNodeViewModel
                {
                    Text = ViewModelHelper.GetTreeNodeText(PropertyDisplayNames.Patches, document.Patches.Count),
                    PatchGroupNodes = new List<PatchGroupTreeNodeViewModel>()
                },
                ReferencedDocumentNode = new ReferencedDocumentsTreeNodeViewModel
                {
                    List = new List<ReferencedDocumentViewModel>()
                }
            };

            viewModel.ReferencedDocumentNode.List = document.DependentOnDocuments.Select(x => x.DependentOnDocument)
                                                                                     .Select(x => x.ToReferencedDocumentViewModelWithRelatedEntities())
                                                                                     .OrderBy(x => x.Name)
                                                                                     .ToList();

            viewModel.PatchesNode.PatchNodes = grouplessPatches.OrderBy(x => x.Name)
                                                               .Select(x => x.ToPatchTreeNodeViewModel())
                                                               .ToList();

            viewModel.PatchesNode.PatchGroupNodes = patchGroupDtos.OrderBy(x => x.GroupName)
                                                                  .Select(x => x.ToTreeNodeViewModel())
                                                                  .ToList();
            return viewModel;
        }

        private static PatchGroupTreeNodeViewModel ToTreeNodeViewModel(this PatchGroupDto patchGroupDto)
        {
            return new PatchGroupTreeNodeViewModel
            {
                GroupName = patchGroupDto.GroupName,
                Text = ViewModelHelper.GetTreeNodeText(patchGroupDto.GroupName, patchGroupDto.Patches.Count),
                PatchNodes = patchGroupDto.Patches.OrderBy(x => x.Name)
                                                  .Select(x => x.ToPatchTreeNodeViewModel())
                                                  .ToList()
            };
        }

        public static DocumentGridViewModel ToGridViewModel(this IList<Document> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new DocumentGridViewModel
            {
                List = entities.Select(x => x.ToIDAndName()).ToList(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // Operator

        /// <summary> Converts to properties view models, the operators that do not have a specialized properties view. </summary>
        public static IList<OperatorPropertiesViewModel> ToOperatorPropertiesViewModelList_WithoutAlternativePropertiesView(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            var viewModels = new List<OperatorPropertiesViewModel>();

            foreach (Operator op in patch.Operators)
            {
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

                if (ViewModelHelper.OperatorTypeEnums_WithoutAlternativePropertiesView.Contains(operatorTypeEnum))
                {
                    OperatorPropertiesViewModel viewModel = op.ToPropertiesViewModel();
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        public static IList<OperatorPropertiesViewModel_ForBundle> ToPropertiesViewModelList_ForBundles(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Bundle)
                        .Select(x => x.ToPropertiesViewModel_ForBundle())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForCache> ToPropertiesViewModelList_ForCaches(this Patch patch, IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Cache)
                        .Select(x => x.ToPropertiesViewModel_ForCache(interpolationTypeRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForCurve> ToPropertiesViewModelList_ForCurves(this Patch patch, ICurveRepository curveRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Curve)
                        .Select(x => x.ToPropertiesViewModel_ForCurve(curveRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForCustomOperator> ToPropertiesViewModelList_ForCustomOperators(
            this Patch patch, IPatchRepository patchRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.CustomOperator)
                        .Select(x => x.ToPropertiesViewModel_ForCustomOperator(patchRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForMakeContinuous> ToPropertiesViewModelList_ForMakeContinuous(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.MakeContinuous)
                        .Select(x => x.ToPropertiesViewModel_ForMakeContinuous())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForNumber> ToPropertiesViewModelList_ForNumbers(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Number)
                        .Select(x => x.ToPropertiesViewModel_ForNumber())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForPatchInlet> ToPropertiesViewModelList_ForPatchInlets(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                        .Select(x => x.ToPropertiesViewModel_ForPatchInlet())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForPatchOutlet> ToPropertiesViewModelList_ForPatchOutlets(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                        .Select(x => x.ToPropertiesViewModel_ForPatchOutlet())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForSample> ToPropertiesViewModelList_ForSamples(this Patch patch, ISampleRepository sampleRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Sample)
                        .Select(x => x.ToPropertiesViewModel_ForSample(sampleRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_WithDimension> ToPropertiesViewModelList_WithDimension(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.Operators.Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                  .Select(x => x.ToPropertiesViewModel_WithDimension())
                                  .ToList();
        }

        public static IList<OperatorPropertiesViewModel_WithDimensionAndInterpolation> ToPropertiesViewModelList_WithDimensionAndInterpolation(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.Operators.Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionAndInterpolationPropertyViews.Contains(x.GetOperatorTypeEnum()))
                        .Select(x => x.ToPropertiesViewModel_WithDimensionAndInterpolation())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> ToPropertiesViewModelList_WithDimensionAndCollectionRecalculation(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.Operators.Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionAndCollectionRecalculationPropertyViews.Contains(x.GetOperatorTypeEnum()))
                        .Select(x => x.ToPropertiesViewModel_WithDimensionAndCollectionRecalculation())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_WithDimensionAndOutletCount> ToPropertiesViewModelList_WithDimensionAndOutletCount(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.Operators.Where(x => ViewModelHelper.OperatorTypeEnums_WithDimensionAndOutletCountPropertyViews.Contains(x.GetOperatorTypeEnum()))
                        .Select(x => x.ToPropertiesViewModel_WithDimensionAndOutletCount())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_WithInletCount> ToPropertiesViewModelList_WithInletCount(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.Operators.Where(x => ViewModelHelper.OperatorTypeEnums_WithInletCountPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                  .Select(x => x.ToPropertiesViewModel_WithInletCount())
                                  .ToList();
        }

        public static OperatorPropertiesViewModel ToPropertiesViewModel(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel>(entity);

            if (entity.OperatorType != null)
            {
                viewModel.OperatorType = entity.OperatorType.ToIDAndDisplayName();
            }
            else
            {
                viewModel.OperatorType = ViewModelHelper.CreateEmptyIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle ToPropertiesViewModel_ForBundle(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForBundle>(entity);

            viewModel.InletCount = entity.Inlets.Count;
            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCache ToPropertiesViewModel_ForCache(this Operator entity, IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForCache>(entity);

            var wrapper = new Cache_OperatorWrapper(entity);

            viewModel.Interpolation = wrapper.InterpolationType.ToIDAndDisplayName();
            viewModel.InterpolationLookup = ViewModelHelper.GetInterpolationTypeLookupViewModel(interpolationTypeRepository);
            viewModel.SpeakerSetup = wrapper.SpeakerSetup.ToIDAndDisplayName();
            viewModel.SpeakerSetupLookup = ViewModelHelper.GetSpeakerSetupLookupViewModel();
            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve ToPropertiesViewModel_ForCurve(this Operator entity, ICurveRepository curveRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForCurve>(entity);

            var wrapper = new Curve_OperatorWrapper(entity, curveRepository);

            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            Curve curve = wrapper.Curve;
            if (curve != null)
            {
                viewModel.Curve = curve.ToIDAndName();
            }
            else
            {
                viewModel.Curve = ViewModelHelper.CreateEmptyIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator ToPropertiesViewModel_ForCustomOperator(this Operator entity, IPatchRepository patchRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForCustomOperator>(entity);

            var wrapper = new CustomOperator_OperatorWrapper(entity, patchRepository);

            Patch underlyingPatch = wrapper.UnderlyingPatch;
            if (underlyingPatch != null)
            {
                viewModel.UnderlyingPatch = underlyingPatch.ToIDAndName();
            }
            else
            {
                viewModel.UnderlyingPatch = ViewModelHelper.CreateEmptyIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForMakeContinuous ToPropertiesViewModel_ForMakeContinuous(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForMakeContinuous>(entity);

            var wrapper = new MakeContinuous_OperatorWrapper(entity);

            viewModel.InletCount = entity.Inlets.Count;
            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();
            viewModel.Interpolation = wrapper.InterpolationType.ToIDAndDisplayName();
            viewModel.InterpolationLookup = ViewModelHelper.GetResampleInterpolationLookupViewModel();

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber ToPropertiesViewModel_ForNumber(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForNumber>(entity);

            var wrapper = new Number_OperatorWrapper(entity);

            viewModel.Number = wrapper.Number.ToString();

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet ToPropertiesViewModel_ForPatchInlet(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForPatchInlet>(entity);

            var wrapper = new PatchInlet_OperatorWrapper(entity);

            viewModel.DefaultValue = Convert.ToString(wrapper.Inlet.DefaultValue);
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            if (wrapper.ListIndex.HasValue)
            {
                viewModel.Number = wrapper.ListIndex.Value + 1;
            }

            DimensionEnum dimensionEnum = wrapper.Inlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                viewModel.Dimension = dimensionEnum.ToIDAndDisplayName();
            }
            else
            {
                viewModel.Dimension = new IDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet ToPropertiesViewModel_ForPatchOutlet(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForPatchOutlet>(entity);

            var wrapper = new PatchOutlet_OperatorWrapper(entity);

            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            if (wrapper.ListIndex.HasValue)
            {
                viewModel.Number = wrapper.ListIndex.Value + 1;
            }

            DimensionEnum dimensionEnum = wrapper.Result.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                viewModel.Dimension = dimensionEnum.ToIDAndDisplayName();
            }
            else
            {
                viewModel.Dimension = new IDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample ToPropertiesViewModel_ForSample(this Operator entity, ISampleRepository sampleRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_ForSample>(entity);

            var wrapper = new Sample_OperatorWrapper(entity, sampleRepository);

            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            Sample sample = wrapper.Sample;
            if (sample != null)
            {
                viewModel.Sample = sample.ToIDAndName();
            }
            else
            {
                viewModel.Sample = ViewModelHelper.CreateEmptyIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimension ToPropertiesViewModel_WithDimension(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_WithDimension>(entity);

            viewModel.OperatorType = entity.OperatorType.ToIDAndDisplayName();
            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndInterpolation ToPropertiesViewModel_WithDimensionAndInterpolation(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_WithDimensionAndInterpolation>(entity);

            var wrapper = new Resample_OperatorWrapper(entity);

            viewModel.OperatorType = entity.OperatorType.ToIDAndDisplayName();
            viewModel.Interpolation = wrapper.InterpolationType.ToIDAndDisplayName();
            viewModel.InterpolationLookup = ViewModelHelper.GetResampleInterpolationLookupViewModel();
            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation ToPropertiesViewModel_WithDimensionAndCollectionRecalculation(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation>(entity);

            var wrapper = new SumOverDimension_OperatorWrapper(entity);

            viewModel.OperatorType = entity.OperatorType.ToIDAndDisplayName();
            viewModel.CollectionRecalculation = wrapper.CollectionRecalculation.ToIDAndDisplayName();
            viewModel.CollectionRecalculationLookup = ViewModelHelper.GetCollectionRecalculationLookupViewModel();
            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndOutletCount ToPropertiesViewModel_WithDimensionAndOutletCount(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_WithDimensionAndOutletCount>(entity);

            viewModel.OperatorType = entity.OperatorType.ToIDAndDisplayName();
            viewModel.OutletCount = entity.Outlets.Count;
            viewModel.Dimension = entity.GetDimensionEnum().ToIDAndDisplayName();
            viewModel.DimensionLookup = ViewModelHelper.GetDimensionLookupViewModel();

            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithInletCount ToPropertiesViewModel_WithInletCount(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = CreateOperatorPropertiesViewModel_Base<OperatorPropertiesViewModel_WithInletCount>(entity);

            viewModel.OperatorType = entity.OperatorType.ToIDAndDisplayName();
            viewModel.InletCount = entity.Inlets.Count;
            viewModel.ValidationMessages = new List<Message>();

            return viewModel;
        }

        private static TViewModel CreateOperatorPropertiesViewModel_Base<TViewModel>(Operator entity)
            where TViewModel : OperatorPropertiesViewModelBase, new()
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new TViewModel
            {
                ID = entity.ID,
                PatchID = entity.Patch.ID,
                Name = entity.Name,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // Patch

        public static PatchDetailsViewModel ToDetailsViewModel(
            this Patch patch,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            EntityPositionManager entityPositionManager)
        {
            var converter = new RecursiveToViewModelConverter(
                sampleRepository,
                curveRepository,
                patchRepository,
                entityPositionManager);

            return converter.ConvertToDetailsViewModel(patch);
        }

        public static PatchPropertiesViewModel ToPropertiesViewModel(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            var viewModel = new PatchPropertiesViewModel
            {
                ID = patch.ID,
                Name = patch.Name,
                Group = patch.GroupName,
                ValidationMessages = new List<Message>(),
                CanAddToCurrentPatches = true
            };

            return viewModel;
        }

        public static PatchGridViewModel ToPatchGridViewModel(
            this IList<Patch> patchesInGroup,
            int documentID,
            string group)
        {
            if (patchesInGroup == null) throw new NullException(() => patchesInGroup);

            var viewModel = new PatchGridViewModel
            {
                DocumentID = documentID,
                Group = group,
                ValidationMessages = new List<Message>(),
                List = patchesInGroup.OrderBy(x => x.Name)
                                     .Select(x => x.ToIDAndName())
                                     .ToList()
            };

            return viewModel;
        }

        // Sample

        public static SamplePropertiesViewModel ToPropertiesViewModel(this Sample entity, SampleRepositories repositories)
        {
            if (entity == null) throw new NullException(() => entity);
            if (repositories == null) throw new NullException(() => repositories);

            var viewModel = new SamplePropertiesViewModel
            {
                DocumentID = entity.Document.ID,
                AudioFileFormatLookup = ViewModelHelper.GetAudioFileFormatLookupViewModel(),
                SampleDataTypeLookup = ViewModelHelper.GetSampleDataTypeLookupViewModel(),
                SpeakerSetupLookup = ViewModelHelper.GetSpeakerSetupLookupViewModel(),
                InterpolationTypeLookup = ViewModelHelper.GetInterpolationTypeLookupViewModel(repositories.InterpolationTypeRepository),
                ValidationMessages = new List<Message>()
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
                List = entities.ToListItemViewModels(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // Scale

        public static ScalePropertiesViewModel ToPropertiesViewModel(this Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ScalePropertiesViewModel
            {
                Entity = entity.ToViewModel(),
                ScaleTypeLookup = ViewModelHelper.GetScaleTypeLookupViewModel(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static ScaleGridViewModel ToGridViewModel(this IList<Scale> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ScaleGridViewModel
            {
                DocumentID = documentID,
                ValidationMessages = new List<Message>(),
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList(),
            };

            return viewModel;
        }

        // Tone

        public static ToneGridEditViewModel ToToneGridEditViewModel(this Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new ToneGridEditViewModel
            {
                ScaleID = entity.ID,
                NumberTitle = ViewModelHelper.GetToneGridEditNumberTitle(entity),
                Tones = entity.Tones.ToToneViewModels(),
                FrequencyVisible = entity.GetScaleTypeEnum() != ScaleTypeEnum.LiteralFrequency,
                ValidationMessages = new List<Message>()
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
    }
}