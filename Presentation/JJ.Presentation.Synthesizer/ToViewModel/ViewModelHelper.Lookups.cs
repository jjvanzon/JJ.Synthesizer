using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        public static IList<IDAndName> CreateAudioFileFormatLookupViewModel(IAudioFileFormatRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<AudioFileFormat> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();

            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateChildDocumentTypeLookupViewModel(IChildDocumentTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<ChildDocumentType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToList();

            idNames.Add(new IDAndName { ID = 0, Name = null });

            return idNames;
        }

        public static IList<IDAndName> CreateInterpolationTypesLookupViewModel(IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<InterpolationType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateMainPatchLookupViewModel(Document childDocument)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            var list = new List<IDAndName>(childDocument.Patches.Count + 1);
            list.Add(new IDAndName { ID = 0, Name = null });
            list.AddRange(childDocument.Patches.OrderBy(x => x.Name).Select(x => x.ToIDAndName()));

            return list;
        }

        public static IList<IDAndName> CreateNodeTypesLookupViewModel(INodeTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<NodeType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<OperatorTypeViewModel> CreateOperatorTypesViewModel(IOperatorTypeRepository operatorTypeRepository)
        {
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);

            IList<OperatorType> operatorTypes = operatorTypeRepository.GetAllOrderedBySortOrder();
            IList<OperatorTypeViewModel> operatorTypeViewModels = operatorTypes.Select(x => x.ToViewModel()).ToArray();

            return operatorTypeViewModels;
        }

        public static IList<IDAndName> CreateSampleDataTypeLookupViewModel(ISampleDataTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SampleDataType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateSampleLookupViewModel(Document rootDocument)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            var list = new List<IDAndName>();

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

        public static IList<IDAndName> CreateSpeakerSetupLookupViewModel(ISpeakerSetupRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SpeakerSetup> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateUnderlyingDocumentLookupViewModel(Document rootDocument)
        {
            var list = new List<IDAndName>();

            list.Add(new IDAndName { ID = 0, Name = null });
            list.AddRange(rootDocument.ChildDocuments.OrderBy(x => x.Name).Select(x => x.ToIDAndName()));

            return list;
        }
    }
}
