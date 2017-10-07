using JJ.Data.Canonical;
using JJ.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Presentation;
using System;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ToViewModelHelper
    {
        // AudioFileFormat

        private static readonly IList<IDAndName> _audioFileFormatLookupViewModel = CreateAudioFileFormatLookupViewModel();

        public static IList<IDAndName> GetAudioFileFormatLookupViewModel() => _audioFileFormatLookupViewModel;

        private static IList<IDAndName> CreateAudioFileFormatLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<AudioFileFormatEnum>(mustIncludeUndefined: false);
            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();

            return idAndNames;
        }

        // CollectionRecalculation

        private static readonly IList<IDAndName> _collectionRecalculationLookupViewModel = CreateCollectionRecalculationLookupViewModel();

        public static IList<IDAndName> GetCollectionRecalculationLookupViewModel() => _collectionRecalculationLookupViewModel;

        private static IList<IDAndName> CreateCollectionRecalculationLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<CollectionRecalculationEnum>(mustIncludeUndefined: false);
            return idAndNames;
        }

        // Curve

        public static IList<IDAndName> CreateCurveLookupViewModel(IList<UsedInDto<Curve>> dtos)
        {
            if (dtos == null) throw new NullException(() => dtos);

            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new List<IDAndName>(dtos.Count + 1);

            list.Add(new IDAndName { ID = 0, Name = null });

            list.AddRange(dtos.OrderBy(x => x.Entity.Name).Select(x => x.ToIDAndNameWithUsedIn()));

            return list;
        }

        // Dimension

        private static readonly IList<IDAndName> _dimensionLookupViewModel = CreateDimensionLookupViewModel();

        public static IList<IDAndName> GetDimensionLookupViewModel() => _dimensionLookupViewModel;

        private static IList<IDAndName> CreateDimensionLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<DimensionEnum>(mustIncludeUndefined: true);
            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();
            return idAndNames;
        }

        // InterpolationType

        private static readonly object _interpolationTypeLookupViewModelLock = new object();

        private static IList<IDAndName> _interpolationTypeLookupViewModel;

        public static IList<IDAndName> GetInterpolationTypeLookupViewModel(IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            lock (_interpolationTypeLookupViewModelLock)
            {
                // ReSharper disable once InvertIf
                if (_interpolationTypeLookupViewModel == null)
                {
                    // Cannot delegate to CreateEnumLookupViewModel, because we need to order by SortOrder.
                    IList<InterpolationType> entities = repository.GetAll().OrderBy(x => x.SortOrder).ToArray();
                    _interpolationTypeLookupViewModel = entities.Select(x => x.ToIDAndDisplayName()).ToArray();
                }

                return _interpolationTypeLookupViewModel;
            }
        }

        // NodeType

        private static readonly IList<IDAndName> _nodeTypeLookupViewModel = CreateNodeTypeLookupViewModel();

        public static IList<IDAndName> GetNodeTypeLookupViewModel() => _nodeTypeLookupViewModel;

        private static IList<IDAndName> CreateNodeTypeLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<NodeTypeEnum>(mustIncludeUndefined: false);
            return idAndNames;
        }

        // ResampleInterpolationType

        private static readonly IList<IDAndName> _resampleInterpolationLookupViewModel = CreateResampleInterpolationLookupViewModel();

        public static IList<IDAndName> GetResampleInterpolationLookupViewModel() => _resampleInterpolationLookupViewModel;

        private static IList<IDAndName> CreateResampleInterpolationLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<ResampleInterpolationTypeEnum>(mustIncludeUndefined: true);
            return idAndNames;
        }

        // SampleDataType

        private static readonly IList<IDAndName> _sampleDataTypeLookupViewModel = CreateSampleDataTypeLookupViewModel();

        public static IList<IDAndName> GetSampleDataTypeLookupViewModel() => _sampleDataTypeLookupViewModel;

        private static IList<IDAndName> CreateSampleDataTypeLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<SampleDataTypeEnum>(mustIncludeUndefined: false);
            return idAndNames;
        }

        // Sample

        public static IList<IDAndName> CreateSampleLookupViewModel(Document document)
        {
            if (document == null) throw new NullException(() => document);

            // ReSharper disable once UseObjectOrCollectionInitializer
            var list = new List<IDAndName>(document.Samples.Count + 1);

            list.Add(new IDAndName { ID = 0, Name = null });

            list.AddRange(document.Samples
                                  .OrderBy(x => x.Name)
                                  .Select(x => x.ToIDAndName()));
            return list;
        }

        // ScaleType

        private static readonly IList<IDAndName> _scaleTypeLookupViewModel =  CreateScaleTypeLookupViewModel();

        public static IList<IDAndName> GetScaleTypeLookupViewModel() => _scaleTypeLookupViewModel;

        private static IList<IDAndName> CreateScaleTypeLookupViewModel()
        {
            // Cannot delegate to CreateEnumLookupViewModel, because plural names are needed.

            IList<ScaleTypeEnum> enumValues = EnumHelper.GetValues<ScaleTypeEnum>();
            IList<IDAndName> idAndNames = new List<IDAndName>(enumValues.Count);

            // Add Undefined separately, so it is shown as a null string.
            idAndNames.Add(new IDAndName { ID = 0, Name = null });

            foreach (ScaleTypeEnum enumValue in enumValues)
            {
                if (enumValue == ScaleTypeEnum.Undefined)
                {
                    continue;
                }

                IDAndName idAndName = enumValue.ToIDAndDisplayNamePlural();
                idAndNames.Add(idAndName);
            }

            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();

            return idAndNames;
        }

        // SpeakerSetup

        private static readonly IList<IDAndName> _speakerSetupLookupViewModel = CreateSpeakerSetupLookupViewModel();

        public static IList<IDAndName> GetSpeakerSetupLookupViewModel() => _speakerSetupLookupViewModel;

        private static IList<IDAndName> CreateSpeakerSetupLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<SpeakerSetupEnum>(mustIncludeUndefined: true);
            return idAndNames;
        }

        // UnderlyingPatch

        public static IList<IDAndName> CreateUnderlyingPatchLookupViewModel(Document document)
        {
            if (document == null) throw new NullException(() => document);

            var list = new List<IDAndName>
            {
                new IDAndName { ID = 0, Name = null }
            };

            list.AddRange(
                PatchGrouper.GetGrouplessPatches(document.Patches, mustIncludeHidden: true)
                            .OrderBy(x => ResourceFormatter.GetDisplayName(x))
                            .Select(x => x.ToIDAndName()));

            list.AddRange(
                from patchGroupDto in PatchGrouper.GetPatchGroupDtos_ExcludingGroupless(document.Patches, mustIncludeHidden: true)
                from patch in patchGroupDto.Patches
                let patchDisplayName = ResourceFormatter.GetDisplayName(patch)
                orderby patchGroupDto.FriendlyGroupName, patchDisplayName
                let name = $"{patchDisplayName} | {patchGroupDto.FriendlyGroupName}"
                select new IDAndName { ID = patch.ID, Name = name });

            IEnumerable<DocumentReference> lowerDocumentReferences = document.LowerDocumentReferences.OrderBy(x => x.GetAliasOrName());
            foreach (DocumentReference lowerDocumentReference in lowerDocumentReferences)
            {
                string lowerDocumentReferenceAliasOrName = lowerDocumentReference.GetAliasOrName();

                list.AddRange(
                    from patch in PatchGrouper.GetGrouplessPatches(lowerDocumentReference.LowerDocument.Patches, mustIncludeHidden: false)
                    let patchDisplayName = ResourceFormatter.GetDisplayName(patch)
                    orderby patchDisplayName
                    let name = $"{patchDisplayName} | {lowerDocumentReferenceAliasOrName}"
                    select new IDAndName { ID = patch.ID, Name = name });

                list.AddRange(
                    from patchGroupDto in PatchGrouper.GetPatchGroupDtos_ExcludingGroupless(lowerDocumentReference.LowerDocument.Patches, mustIncludeHidden: false)
                    from patch in patchGroupDto.Patches
                    let patchDisplayName = ResourceFormatter.GetDisplayName(patch)
                    orderby patchGroupDto.FriendlyGroupName, patchDisplayName
                    let name = $"{patchDisplayName} | {patchGroupDto.FriendlyGroupName} | {lowerDocumentReferenceAliasOrName}"
                    select new IDAndName { ID = patch.ID, Name = name });
            }

            return list;
        }

        // Helpers

        private static IList<IDAndName> CreateEnumLookupViewModel<TEnum>(bool mustIncludeUndefined)
            where TEnum : struct
        {
            return EnumToIDAndNameConverter.Convert<TEnum>(ResourceFormatter.ResourceManager, mustIncludeUndefined);
        }
    }
}
