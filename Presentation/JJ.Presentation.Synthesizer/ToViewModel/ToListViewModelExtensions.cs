using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Configuration;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
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

        public static AudioFileOutputListViewModel ToListViewModel(this IList<AudioFileOutput> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new AudioFileOutputListViewModel
            {
                List = new List<AudioFileOutputListItemViewModel>(entities.Count)
            };

            for (int i = 0; i < entities.Count; i++)
			{
			    AudioFileOutput entity = entities[i];
                AudioFileOutputListItemViewModel listItemViewModel = entity.ToListItemViewModel();
                listItemViewModel.ListIndex = i;
                viewModel.List.Add(listItemViewModel);
			}

            return viewModel;
        }

        public static IList<AudioFileOutputPropertiesViewModel> ToPropertiesListViewModel(
            this IList<AudioFileOutput> entities,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<AudioFileOutputPropertiesViewModel> viewModels = new List<AudioFileOutputPropertiesViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                AudioFileOutput entity = entities[i];
                AudioFileOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository);
                viewModel.AudioFileOutput.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static CurveListViewModel ToListViewModel(this IList<Curve> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new CurveListViewModel
            {
                List = entities.ToIDNameAndListIndexes()
            };

            return viewModel;
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

        public static ChildDocumentListViewModel ToChildDocumentListViewModel(this IList<Document> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ChildDocumentListViewModel
            {
                List = entities.ToChildDocumentLisItemsViewModel()
            };

            return viewModel;
        }

        public static IList<IDNameAndListIndexViewModel> ToChildDocumentLisItemsViewModel(this IList<Document> sourceEntities)
        {
            if (sourceEntities == null) throw new NullException(() => sourceEntities);

            IList<IDNameAndListIndexViewModel> destList = new List<IDNameAndListIndexViewModel>(sourceEntities.Count);

            for (int i = 0; i < sourceEntities.Count; i++)
            {
                Document sourceEntity = sourceEntities[i];
                IDNameAndListIndexViewModel destListItem = sourceEntity.ToIDNameAndListIndex();
                destListItem.ListIndex = i;
                destList.Add(destListItem);
            }

            return destList;
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

        public static PatchListViewModel ToListViewModel(this IList<Patch> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new PatchListViewModel
            {
                List = entities.ToIDNameAndListIndexes()
            };

            return viewModel;
        }

        public static SampleListViewModel ToListViewModel(this IList<Sample> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new SampleListViewModel
            {
                List = entities.ToListItemViewModels()
            };

            return viewModel;
        }
    }
}
