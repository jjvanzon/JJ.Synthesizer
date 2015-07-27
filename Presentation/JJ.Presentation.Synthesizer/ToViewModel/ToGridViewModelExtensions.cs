using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
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
    internal static class ToGridViewModelExtensions
    {
        private static int _maxVisiblePageNumbers;

        static ToGridViewModelExtensions()
        {
            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _maxVisiblePageNumbers = config.MaxVisiblePageNumbers;
        }

        public static AudioFileOutputGridViewModel ToAudioFileOutputGridViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<AudioFileOutput> sortedEntities = document.AudioFileOutputs.OrderBy(x => x.Name).ToList();

            var viewModel = new AudioFileOutputGridViewModel
            {
                List = sortedEntities.ToListItemViewModels(),
                DocumentID = document.ID
            };

            return viewModel;
        }

        public static DocumentGridViewModel ToGridViewModel(this IList<Document> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new DocumentGridViewModel
            {
                List = entities.Select(x => x.ToIDAndName()).ToList(),
                Pager = ViewModelHelper.CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        public static ChildDocumentGridViewModel ToChildDocumentGridViewModel(this Document rootDocument, ChildDocumentTypeEnum childDocumentTypeEnum)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            IList<Document> childDocuments = rootDocument.ChildDocuments
                                                         .Where(x => x.GetChildDocumentTypeEnum() == childDocumentTypeEnum)
                                                         .ToArray();

            ChildDocumentGridViewModel viewModel = childDocuments.ToChildDocumentGridViewModel(rootDocument.ID, childDocumentTypeEnum);

            return viewModel;
        }

        private static ChildDocumentGridViewModel ToChildDocumentGridViewModel(
            this IList<Document> entities,
            int rootDocumentID,
            ChildDocumentTypeEnum childDocumentTypeEnum)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ChildDocumentGridViewModel
            {
                List = entities.ToChildDocumentListItemsViewModel(),
                Keys = new ChildDocumentGridKeysViewModel
                {
                    RootDocumentID = rootDocumentID,
                    ChildDocumentTypeEnum = childDocumentTypeEnum
                }
            };

            return viewModel;
        }

        public static CurveGridViewModel ToGridViewModel(this IList<Curve> entities, int rootDocumentID, int? childDocumentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new CurveGridViewModel
            {
                List = entities.ToListItemViewModels(rootDocumentID),
                RootDocumentID = rootDocumentID,
                ChildDocumentID = childDocumentID
            };

            return viewModel;
        }

        public static DocumentGridViewModel ToGridViewModel(this IList<Document> pageOfEntities, int pageIndex, int pageSize, int totalCount)
        {
            if (pageOfEntities == null) throw new NullException(() => pageOfEntities);

            var viewModel = new DocumentGridViewModel
            {
                List = pageOfEntities.Select(x => x.ToIDAndName()).ToList(),
                Pager = PagerViewModelFactory.Create(pageIndex, pageSize, totalCount, _maxVisiblePageNumbers)
            };

            return viewModel;
        }

        public static PatchGridViewModel ToGridViewModel(this IList<Patch> entities, int rootDocumentID, int? childDocumentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new PatchGridViewModel
            {
                List = entities.ToListItemViewModels(),
                RootDocumentID = rootDocumentID,
                ChildDocumentID = childDocumentID
            };

            return viewModel;
        }

        public static SampleGridViewModel ToGridViewModel(this IList<Sample> entities, int documentID, int? childDocumentTypeID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new SampleGridViewModel
            {
                List = entities.ToListItemViewModels(),
                DocumentID = documentID,
                ChildDocumentTypeID = childDocumentTypeID
            };

            return viewModel;
        }
    }
}