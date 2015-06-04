using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
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
    internal static class ToListViewModelExtensions
    {
        private static int _maxVisiblePageNumbers;

        static ToListViewModelExtensions()
        {
            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _maxVisiblePageNumbers = config.MaxVisiblePageNumbers;
        }

        public static AudioFileOutputListViewModel ToAudioFileOutputListViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            AudioFileOutputListViewModel viewModel = document.AudioFileOutputs.ToListViewModel(document.ID);

            return viewModel;
        }

        // TODO: Inline this method, so you cannot accidently call it separately?
        private static AudioFileOutputListViewModel ToListViewModel(this IList<AudioFileOutput> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            entities = entities.OrderBy(x => x.Name).ToArray();

            var viewModel = new AudioFileOutputListViewModel
            {
                List = entities.ToListItemViewModels(),
                Keys = new AudioFileOutputListKeysViewModel
                {
                    DocumentID = documentID
                }
            };

            return viewModel;
        }

        public static IList<AudioFileOutputPropertiesViewModel> ToPropertiesListViewModel(
            this IList<AudioFileOutput> entities,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (entities == null) throw new NullException(() => entities);

            entities = entities.OrderBy(x => x.Name).ToArray();

            IList<AudioFileOutputPropertiesViewModel> viewModels = new List<AudioFileOutputPropertiesViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                AudioFileOutput entity = entities[i];
                AudioFileOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel(i, audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static DocumentListViewModel ToListViewModel(this IList<Document> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new DocumentListViewModel
            {
                List = entities.Select(x => x.ToIDAndName()).ToList(),
                Pager = ViewModelHelper.CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        // TODO: Generalize ToInstrumentListViewModel and ToEffectListViewModel.
        public static ChildDocumentListViewModel ToInstrumentListViewModel(this Document parentDocument)
        {
            if (parentDocument == null) throw new NullException(() => parentDocument);

            ChildDocumentListViewModel viewModel = parentDocument.Instruments.ToChildDocumentListViewModel(parentDocument.ID, ChildDocumentTypeEnum.Instrument);

            return viewModel;
        }

        public static ChildDocumentListViewModel ToEffectListViewModel(this Document parentDocument)
        {
            if (parentDocument == null) throw new NullException(() => parentDocument);

            ChildDocumentListViewModel viewModel = parentDocument.Effects.ToChildDocumentListViewModel(parentDocument.ID, ChildDocumentTypeEnum.Effect);

            return viewModel;
        }

        private static ChildDocumentListViewModel ToChildDocumentListViewModel(
            this IList<Document> entities,
            int parentDocumentID,
            ChildDocumentTypeEnum childDocumentTypeEnum)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ChildDocumentListViewModel
            {
                List = entities.ToChildDocumentListItemsViewModel(),
                Keys = new ChildDocumentListKeysViewModel
                {
                    ParentDocumentID = parentDocumentID,
                    ChildDocumentTypeEnum = childDocumentTypeEnum
                }
            };

            return viewModel;
        }

        // TODO: Remove outcommented code.
        //public static CurveListViewModel ToCurveListViewModel(
        //    this Document document,
        //    int documentID,
        //    ChildDocumentTypeEnum? childDocumentTypeEnum,
        //    int? childDocumentListIndex)
        //{
        //    if (document == null) throw new NullException(() => document);
        //
        //    CurveListViewModel viewModel = document.Curves.ToListViewModel();
        //
        //    viewModel.DocumentID = document.ID;
        //
        //    return viewModel;
        //}

        public static CurveListViewModel ToListViewModel(
            this IList<Curve> entities,
            int documentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new CurveListViewModel
            {
                List = entities.ToListItemViewModels(documentID, childDocumentTypeEnum, childDocumentListIndex),
                Keys = new CurveListKeysViewModel
                {
                    DocumentID = documentID,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex
                }
            };

            return viewModel;
        }

        public static DocumentListViewModel ToListViewModel(this IList<Document> pageOfEntities, int pageIndex, int pageSize, int totalCount)
        {
            if (pageOfEntities == null) throw new NullException(() => pageOfEntities);

            var viewModel = new DocumentListViewModel
            {
                List = pageOfEntities.Select(x => x.ToIDAndName()).ToList(),
                Pager = PagerViewModelFactory.Create(pageIndex, pageSize, totalCount, _maxVisiblePageNumbers)
            };

            return viewModel;
        }

        public static PatchListViewModel ToListViewModel(
            this IList<Patch> entities,
            int documentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new PatchListViewModel
            {
                List = entities.ToListItemViewModels(documentID, childDocumentTypeEnum, childDocumentListIndex),
                Keys = new PatchListKeysViewModel
                {
                    DocumentID = documentID,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex
                }
            };

            return viewModel;
        }

        public static SampleListViewModel ToListViewModel(
            this IList<Sample> entities, 
            int documentID, 
            ChildDocumentTypeEnum? childDocumentTypeEnum, 
            int? childDocumentListIndex)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new SampleListViewModel
            {
                List = entities.ToListItemViewModels(documentID, childDocumentTypeEnum, childDocumentListIndex),
                Keys = new SampleListKeysViewModel
                {
                    DocumentID = documentID,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex
                }
            };

            return viewModel;
        }
    }
}
