using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.ViewModels;
using System;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        public static IList<IDAndName> CreateAudioFileFormatLookupViewModel(IAudioFileFormatRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<AudioFileFormat> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();

            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndDisplayName()).ToArray();

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

            IList<InletType> entities = repository.GetAll().OrderBy(x => x.ID).ToArray();

            var idAndNames = new List<IDAndName>(entities.Count + 1);
            idAndNames.Add(new IDAndName { ID = 0, Name = null });
            idAndNames.AddRange(entities.Select(x => x.ToIDAndDisplayName()));

            return idAndNames;
        }

        public static IList<IDAndName> CreateInterpolationTypesLookupViewModel(IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<InterpolationType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();

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

            IList<OperatorType> operatorTypes = operatorTypeRepository.GetAllOrderedBySortOrder();

            IList<OperatorTypeViewModel> operatorTypeViewModels = operatorTypes.Select(x => x.ToViewModel())
                                                                               .OrderBy(x => x.DisplayName)
                                                                               .ToArray();
            return operatorTypeViewModels;
        }

        public static IList<IDAndName> CreateOutletTypeLookupViewModel(IOutletTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<OutletType> entities = repository.GetAll().OrderBy(x => x.ID).ToArray();

            var idAndNames = new List<IDAndName>(entities.Count + 1);
            idAndNames.Add(new IDAndName { ID = 0, Name = null });
            idAndNames.AddRange(entities.Select(x => x.ToIDAndDisplayName()));

            return idAndNames;
        }

        public static PatchDetailsViewModel CreateEmptyPatchDetailsViewModel()
        {
            var viewModel = new PatchDetailsViewModel
            {
                Entity = CreateEmptyPatchViewModel(),
                OperatorToolboxItems = new List<OperatorTypeViewModel>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        private static PatchViewModel CreateEmptyPatchViewModel()
        {
            var viewModel = new PatchViewModel
            {
                Operators = new List<OperatorViewModel>()
            };

            return viewModel;
        }

        public static IList<IDAndName> CreateSampleDataTypeLookupViewModel(ISampleDataTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SampleDataType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();

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

            IList<ScaleType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();

            var list = new List<IDAndName>(entities.Count + 1);
            list.Add(new IDAndName { ID = 0, Name = null });
            list.AddRange(entities.OrderBy(x => x.ID).Select(x => x.ToIDAndName()));

            return list;
        }

        public static IList<IDAndName> CreateSpeakerSetupLookupViewModel(ISpeakerSetupRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SpeakerSetup> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();

            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndDisplayName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateUnderlyingPatchLookupViewModel(IList<Patch> underlyingPatches)
        {
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            var list = new List<IDAndName>(underlyingPatches.Count + 1);
            list.Add(new IDAndName { ID = 0, Name = null });
            list.AddRange(underlyingPatches.OrderBy(x => x.Name).Select(x => x.ToIDAndName()));

            return list;
        }
    }
}
