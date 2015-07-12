using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
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

            IList<AudioFileOutput> sortedEntities = document.AudioFileOutputs.OrderBy(x => x.Name).ToList();

            var viewModel = new AudioFileOutputListViewModel
            {
                List = sortedEntities.ToListItemViewModels(),
                DocumentID = document.ID
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

        public static ChildDocumentListViewModel ToChildDocumentListViewModel(
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
                    RootDocumentID = parentDocumentID,
                    ChildDocumentTypeEnum = childDocumentTypeEnum
                }
            };

            return viewModel;
        }

        public static CurveListViewModel ToListViewModel(this IList<Curve> entities, int rootDocumentID, int? childDocumentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new CurveListViewModel
            {
                List = entities.ToListItemViewModels(rootDocumentID),
                RootDocumentID = rootDocumentID,
                ChildDocumentID = childDocumentID
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

        public static PatchListViewModel ToListViewModel(this IList<Patch> entities, int rootDocumentID, int? childDocumentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new PatchListViewModel
            {
                List = entities.ToListItemViewModels(),
                RootDocumentID = rootDocumentID,
                ChildDocumentID = childDocumentID
            };

            return viewModel;
        }

        public static SampleListViewModel ToListViewModel(this IList<Sample> entities, int rootDocumentID, int? childDocumentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new SampleListViewModel
            {
                List = entities.ToListItemViewModels(),
                RootDocumentID = rootDocumentID,
                ChildDocumentID = childDocumentID
            };

            return viewModel;
        }
    }
}