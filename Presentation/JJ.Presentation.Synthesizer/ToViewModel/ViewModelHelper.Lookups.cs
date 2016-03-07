using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        // TODO: Refactor these methods so they do not use repositories, but completely rely on the enum members,
        // like already applied in CreateResampleInterpolationTypeLookupViewModel.

        public static IList<IDAndName> CreateAudioFileFormatLookupViewModel(IAudioFileFormatRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<AudioFileFormat> entities = repository.GetAll().ToArray();

            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndDisplayName()).OrderBy(x => x.Name).ToArray();

            return idNames;
        }

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

        public static IList<IDAndName> CreateInletTypeLookupViewModel(IInletTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<InletType> entities = repository.GetAll().ToArray();

            var idAndNames = new List<IDAndName>(entities.Count + 1);
            idAndNames.Add(new IDAndName { ID = 0, Name = null });
            idAndNames.AddRange(entities.Select(x => x.ToIDAndDisplayName()).OrderBy(x => x.Name));

            return idAndNames;
        }

        public static IList<IDAndName> CreateInterpolationTypeLookupViewModel(IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<InterpolationType> entities = repository.GetAll().OrderBy(x => x.SortOrder).ToArray();

            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndDisplayName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateNodeTypeLookupViewModel(INodeTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<NodeType> entities = repository.GetAll().OrderBy(x => x.ID).ToArray();

            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndDisplayName()).ToArray();

            return idNames;
        }

        public static IList<OperatorTypeViewModel> CreateOperatorTypesViewModel(IOperatorTypeRepository operatorTypeRepository)
        {
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);

            IList<OperatorType> operatorTypes = operatorTypeRepository.GetAll();

            IList<OperatorTypeViewModel> operatorTypeViewModels = operatorTypes.Select(x => x.ToViewModel())
                                                                               .OrderBy(x => x.DisplayName)
                                                                               .ToArray();
            return operatorTypeViewModels;
        }

        public static IList<IDAndName> CreateOutletTypeLookupViewModel(IOutletTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<OutletType> entities = repository.GetAll().ToArray();

            var idAndNames = new List<IDAndName>(entities.Count + 1);
            idAndNames.Add(new IDAndName { ID = 0, Name = null });
            idAndNames.AddRange(entities.Select(x => x.ToIDAndDisplayName()).OrderBy(x => x.Name));

            return idAndNames;
        }

        public static IList<IDAndName> CreateResampleInterpolationLookupViewModel()
        {
            ResampleInterpolationTypeEnum[] enumValues = (ResampleInterpolationTypeEnum[])Enum.GetValues(typeof(ResampleInterpolationTypeEnum));

            var idAndNames = new List<IDAndName>(enumValues.Length);
            idAndNames.Add(new IDAndName { ID = 0, Name = null });

            foreach (ResampleInterpolationTypeEnum enumValue in enumValues)
            {
                if (enumValue == ResampleInterpolationTypeEnum.Undefined)
                {
                    continue;
                }

                var idAndName = enumValue.ToIDAndDisplayName();

                idAndNames.Add(idAndName);
            }

            return idAndNames;
        }

        public static IList<IDAndName> CreateSampleDataTypeLookupViewModel(ISampleDataTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SampleDataType> entities = repository.GetAll().OrderBy(x => x.ID).ToArray();

            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndDisplayName()).ToArray();

            return idNames;
        }

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

        public static IList<IDAndName> CreateScaleTypeLookupViewModel(IScaleTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<ScaleType> entities = repository.GetAll().ToArray();

            var list = new List<IDAndName>(entities.Count + 1);
            list.Add(new IDAndName { ID = 0, Name = null });
            list.AddRange(entities.OrderBy(x => x.ID).Select(x => x.ToIDAndDisplayNamePlural()).OrderBy(x => x.Name));

            return list;
        }

        public static IList<IDAndName> CreateSpeakerSetupLookupViewModel()
        {
            SpeakerSetupEnum[] enumValues = (SpeakerSetupEnum[])Enum.GetValues(typeof(SpeakerSetupEnum));

            var idAndNames = new List<IDAndName>(enumValues.Length);
            idAndNames.Add(new IDAndName { ID = 0, Name = null });

            foreach (SpeakerSetupEnum enumValue in enumValues)
            {
                if (enumValue == SpeakerSetupEnum.Undefined)
                {
                    continue;
                }

                var idAndName = enumValue.ToIDAndDisplayName();

                idAndNames.Add(idAndName);
            }

            return idAndNames;
        }

        public static IList<ChildDocumentIDAndNameViewModel> CreateUnderlyingPatchLookupViewModel(IList<Patch> underlyingPatches)
        {
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            var list = new List<ChildDocumentIDAndNameViewModel>(underlyingPatches.Count + 1);
            list.Add(new ChildDocumentIDAndNameViewModel { ChildDocumentID = 0, Name = null });
            list.AddRange(underlyingPatches.OrderBy(x => x.Name).Select(x => x.Document.ToChildDocumentIDAndNameViewModel()));

            return list;
        }

        public static IList<IDAndName> CreateFilterTypeLookupViewModel()
        {
            FilterTypeEnum[] enumValues = (FilterTypeEnum[])Enum.GetValues(typeof(FilterTypeEnum));

            var idAndNames = new List<IDAndName>(enumValues.Length);
            idAndNames.Add(new IDAndName { ID = 0, Name = null });

            foreach (FilterTypeEnum enumValue in enumValues)
            {
                if (enumValue == FilterTypeEnum.Undefined)
                {
                    continue;
                }

                var idAndName = enumValue.ToIDAndDisplayName();

                idAndNames.Add(idAndName);
            }

            return idAndNames;
        }
    }
}
