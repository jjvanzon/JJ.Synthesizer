using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions.Basic;

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

        // Dimension

        private static readonly IList<IDAndName> _dimensionLookupViewModel = CreateDimensionLookupViewModel();

        public static IList<IDAndName> GetDimensionLookupViewModel() => _dimensionLookupViewModel;

        private static IList<IDAndName> CreateDimensionLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<DimensionEnum>(mustIncludeUndefined: true);
            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();
            return idAndNames;
        }

        // FollowingMode

        private static readonly IList<IDAndName> _followingModeLookupViewModel = CreateFollowingModeLookupViewModel();

        public static IList<IDAndName> GetFollowingModeLookupViewModel() => _followingModeLookupViewModel;

        private static IList<IDAndName> CreateFollowingModeLookupViewModel() => CreateEnumLookupViewModel<FollowingModeEnum>(mustIncludeUndefined: false);

        // InterpolationType

        private static readonly object _interpolationTypeLookupViewModelLock = new object();
        private static readonly object _interpolationTypeLookupViewModelWithInterpolationTypeOffLock = new object();

        private static IList<IDAndName> _interpolationTypeLookupViewModel;
        private static IList<IDAndName> _interpolationTypeLookupViewModelWithInterpolationTypeOff;

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

        public static IList<IDAndName> GetInterpolationTypeLookupViewModelWithInterpolationTypeOff(IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            lock (_interpolationTypeLookupViewModelWithInterpolationTypeOffLock)
            {
                // ReSharper disable once InvertIf
                if (_interpolationTypeLookupViewModelWithInterpolationTypeOff == null)
                {
                    // Cannot delegate to CreateEnumLookupViewModel, because we need to order by SortOrder.
                    IList<InterpolationType> entities = repository.GetAll().OrderBy(x => x.SortOrder).ToArray();

                    _interpolationTypeLookupViewModelWithInterpolationTypeOff = InterpolationTypeEnum.Off
                                                                                                     .ToIDAndDisplayName()
                                                                                                     .Union(entities.Select(x => x.ToIDAndDisplayName()))
                                                                                                     .ToArray();
                }

                return _interpolationTypeLookupViewModelWithInterpolationTypeOff;
            }
        }

        // MidiMappingType

        private static readonly IList<IDAndName> _midiMappingTypeLookupViewModel = CreateMidiMappingTypeLookupViewModel();

        public static IList<IDAndName> GetMidiMappingTypeLookupViewModel() => _midiMappingTypeLookupViewModel;

        private static IList<IDAndName> CreateMidiMappingTypeLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<MidiMappingTypeEnum>(mustIncludeUndefined: false);
            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();
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

        // Helpers

        private static IList<IDAndName> CreateEnumLookupViewModel<TEnum>(bool mustIncludeUndefined)
            where TEnum : struct
            => EnumToIDAndNameConverter.Convert<TEnum>(ResourceFormatter.ResourceManager, mustIncludeUndefined);
    }
}
