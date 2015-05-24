using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.Configuration;
using JJ.Presentation.Synthesizer.ViewModels;
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
            var viewModel = new AudioFileOutputListViewModel
            {
                List = entities.Select(x => x.ToListItemViewModel()).ToList()
            };

            return viewModel;
        }

        public static CurveListViewModel ToListViewModel(this IList<Curve> entities)
        {
            var viewModel = new CurveListViewModel
            {
                List = entities.Select(x => x.ToIDAndName()).ToList()
            };

            return viewModel;
        }

        public static DocumentListViewModel ToListViewModel(this IList<Document> entities)
        {
            var viewModel = new DocumentListViewModel
            {
                List = entities.Select(x => x.ToIDAndName()).ToList(),
                Pager = ViewModelHelper.CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        public static ChildDocumentListViewModel ToChildDocumentListViewModel(this IList<Document> entities)
        {
            var viewModel = new ChildDocumentListViewModel
            {
                List = entities.Select(x => x.ToIDNameAndTemporaryID()).ToList()
            };

            return viewModel;
        }

        public static DocumentListViewModel ToListViewModel(this IList<Document> pageOfEntities, int pageIndex, int pageSize, int totalCount)
        {
            var viewModel = new DocumentListViewModel
            {
                List = pageOfEntities.Select(x => x.ToIDAndName()).ToList(),
                Pager = PagerViewModelFactory.Create(pageIndex, pageSize, totalCount, _maxVisiblePageNumbers)
            };

            return viewModel;
        }

        public static PatchListViewModel ToListViewModel(this IList<Patch> entities)
        {
            var viewModel = new PatchListViewModel
            {
                List = entities.Select(x => x.ToListItemViewModel()).ToList()
            };

            return viewModel;
        }

        public static SampleListViewModel ToListViewModel(this IList<Sample> entities)
        {
            var viewModel = new SampleListViewModel
            {
                List = entities.Select(x => x.ToListItemViewModel()).ToList(),
            };

            return viewModel;
        }
    }
}
