using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Linq;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        // AudioFileFormat

        private static IList<IDAndName> _audioFileFormatLookupViewModel = CreateAudioFileFormatLookupViewModel();

        public static IList<IDAndName> GetAudioFileFormatLookupViewModel()
        {
            return _audioFileFormatLookupViewModel;
        }

        private static IList<IDAndName> CreateAudioFileFormatLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<AudioFileFormatEnum>(mustIncludeUndefined: false);
            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();

            return idAndNames;
        }

        // Curve

        public static IList<IDAndName> CreateCurveLookupViewModel(Document rootDocument, Document childDocument)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);
            if (childDocument == null) throw new NullException(() => childDocument);

            var list = new List<IDAndName>(rootDocument.Curves.Count + childDocument.Curves.Count + 1);

            list.Add(new IDAndName { ID = 0, Name = null });

            list.AddRange(Enumerable.Union(rootDocument.Curves, childDocument.Curves)
                                    .OrderBy(x => x.Name)
                                    .Select(x => x.ToIDAndName()));
            return list;
        }

        // Dimension

        private static IList<IDAndName> _dimensionLookupViewModel = CreateDimensionLookupViewModel();

        public static IList<IDAndName> GetDimensionLookupViewModel()
        {
            return _dimensionLookupViewModel;
        }

        private static IList<IDAndName> CreateDimensionLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<DimensionEnum>(mustIncludeUndefined: true);
            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();
            return idAndNames;
        }

        // FilterType

        private static IList<IDAndName> _filterTypeLookupViewModel = CreateFilterTypeLookupViewModel();

        public static IList<IDAndName> GetFilterTypeLookupViewModel()
        {
            return _filterTypeLookupViewModel;
        }

        private static IList<IDAndName> CreateFilterTypeLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<FilterTypeEnum>(mustIncludeUndefined: true);
            return idAndNames;
        }

        // InterpolationType

        private static object _interpolationTypeLookupViewModelLock = new object();
        private static IList<IDAndName> _interpolationTypeLookupViewModel;

        public static IList<IDAndName> GetInterpolationTypeLookupViewModel(IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            lock (_interpolationTypeLookupViewModelLock)
            {
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

        private static IList<IDAndName> _nodeTypeLookupViewModel = CreateNodeTypeLookupViewModel();

        public static IList<IDAndName> GetNodeTypeLookupViewModel()
        {
            return _nodeTypeLookupViewModel;
        }

        private static IList<IDAndName> CreateNodeTypeLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<NodeTypeEnum>(mustIncludeUndefined: false);
            return idAndNames;
        }

        // OperatorType

        private static IList<IDAndName> _operatorTypesViewModel = CreateOperatorTypesViewModel();

        public static IList<IDAndName> GetOperatorTypesViewModel()
        {
            return _operatorTypesViewModel;
        }

        private static IList<IDAndName> CreateOperatorTypesViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<OperatorTypeEnum>(mustIncludeUndefined: false);

            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();

            return idAndNames;
        }

        // ResampleInterpolationType

        private static IList<IDAndName> _resampleInterpolationLookupViewModel = CreateResampleInterpolationLookupViewModel();

        public static IList<IDAndName> GetResampleInterpolationLookupViewModel()
        {
            return _resampleInterpolationLookupViewModel;
        }

        private static IList<IDAndName> CreateResampleInterpolationLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<ResampleInterpolationTypeEnum>(mustIncludeUndefined: true);
            return idAndNames;
        }


        // ResampleInterpolationType

        private static IList<IDAndName> _aggregateRecalculationLookupViewModel = CreateAggregateRecalculationLookupViewModel();

        public static IList<IDAndName> GetAggregateRecalculationLookupViewModel()
        {
            return _aggregateRecalculationLookupViewModel;
        }

        private static IList<IDAndName> CreateAggregateRecalculationLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<AggregateRecalculationEnum>(mustIncludeUndefined: true);
            return idAndNames;
        }

        // SampleDataType

        private static IList<IDAndName> _sampleDataTypeLookupViewModel = CreateSampleDataTypeLookupViewModel();

        public static IList<IDAndName> GetSampleDataTypeLookupViewModel()
        {
            return _sampleDataTypeLookupViewModel;
        }

        private static IList<IDAndName> CreateSampleDataTypeLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<SampleDataTypeEnum>(mustIncludeUndefined: false);
            return idAndNames;
        }

        // Sample

        public static IList<IDAndName> CreateSampleLookupViewModel(Document rootDocument)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            var list = new List<IDAndName>(rootDocument.Samples.Count + 1);

            list.Add(new IDAndName { ID = 0, Name = null });

            list.AddRange(rootDocument.Samples
                                      .OrderBy(x => x.Name)
                                      .Select(x => x.ToIDAndName()));
            return list;
        }

        public static IList<IDAndName> CreateSampleLookupViewModel(Document rootDocument, Document childDocument)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);
            if (childDocument == null) throw new NullException(() => childDocument);

            var list = new List<IDAndName>();

            list.Add(new IDAndName { ID = 0, Name = null });

            list.AddRange(Enumerable.Union(rootDocument.Samples, childDocument.Samples)
                                    .OrderBy(x => x.Name)
                                    .Select(x => x.ToIDAndName()));
            return list;
        }

        // ScaleType

        private static IList<IDAndName> _scaleTypeLookupViewModel =  CreateScaleTypeLookupViewModel();

        public static IList<IDAndName> GetScaleTypeLookupViewModel()
        {
            return _scaleTypeLookupViewModel;
        }

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

                var idAndName = enumValue.ToIDAndDisplayNamePlural();
                idAndNames.Add(idAndName);
            }

            idAndNames = idAndNames.OrderBy(x => x.Name).ToArray();

            return idAndNames;
        }

        // SpeakerSetup

        private static IList<IDAndName> _speakerSetupLookupViewModel = CreateSpeakerSetupLookupViewModel();

        public static IList<IDAndName> GetSpeakerSetupLookupViewModel()
        {
            return _speakerSetupLookupViewModel;
        }

        private static IList<IDAndName> CreateSpeakerSetupLookupViewModel()
        {
            IList<IDAndName> idAndNames = CreateEnumLookupViewModel<SpeakerSetupEnum>(mustIncludeUndefined: true);
            return idAndNames;
        }

        // UnderlyingPatch

        public static IList<ChildDocumentIDAndNameViewModel> CreateUnderlyingPatchLookupViewModel(IList<Patch> underlyingPatches)
        {
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            var list = new List<ChildDocumentIDAndNameViewModel>(underlyingPatches.Count + 1);
            list.Add(new ChildDocumentIDAndNameViewModel { ChildDocumentID = 0, Name = null });
            list.AddRange(underlyingPatches.OrderBy(x => x.Name).Select(x => x.Document.ToChildDocumentIDAndNameViewModel()));

            return list;
        }

        // Helpers

        private static IList<IDAndName> CreateEnumLookupViewModel<TEnum>(bool mustIncludeUndefined)
            where TEnum : struct
        {
            TEnum[] enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));

            var idAndNames = new List<IDAndName>(enumValues.Length);

            // Add Undefined separately, so it is shown as a null string.
            if (mustIncludeUndefined)
            {
                idAndNames.Add(new IDAndName { ID = 0, Name = null });
            }

            foreach (TEnum enumValue in enumValues)
            {
                int enumValueInt = (int)(object)enumValue;
                bool isUndefined = enumValueInt == 0;
                if (isUndefined)
                {
                    continue;
                }

                string displayName = ResourceHelper.GetPropertyDisplayName(enumValue.ToString());

                var idAndName = new IDAndName
                {
                    ID = enumValueInt,
                    Name = displayName
                };


                idAndNames.Add(idAndName);
            }

            return idAndNames;
        }
    }
}
