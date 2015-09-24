using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;
using System.Linq;

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

        public static ChildDocumentGridViewModel ToChildDocumentGridViewModel(this Document rootDocument, int childDocumentTypeID)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            IList<Document> childDocuments = rootDocument.ChildDocuments
                                                         .Where(x => x.ChildDocumentType != null && 
                                                                     x.ChildDocumentType.ID == childDocumentTypeID)
                                                         .ToList();

            ChildDocumentGridViewModel viewModel = childDocuments.ToChildDocumentGridViewModel(rootDocument.ID, childDocumentTypeID);

            return viewModel;
        }

        private static ChildDocumentGridViewModel ToChildDocumentGridViewModel(
            this IList<Document> entities,
            int rootDocumentID,
            int childDocumentTypeID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ChildDocumentGridViewModel
            {
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList(),
                RootDocumentID = rootDocumentID,
                ChildDocumentTypeID = childDocumentTypeID
            };

            return viewModel;
        }

        public static CurveGridViewModel ToGridViewModel(this IList<Curve> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new CurveGridViewModel
            {
                DocumentID = documentID,
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList()
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

        public static PatchGridViewModel ToGridViewModel(this IList<Patch> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new PatchGridViewModel
            {
                DocumentID = documentID,
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList()
            };

            return viewModel;
        }

        public static SampleGridViewModel ToGridViewModel(this IList<Sample> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new SampleGridViewModel
            {
                DocumentID = documentID,
                List = entities.ToListItemViewModels()
            };

            return viewModel;
        }

        public static ScaleGridViewModel ToGridViewModel(this IList<Scale> entities, int documentID)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModel = new ScaleGridViewModel
            {
                DocumentID = documentID,
                List = entities.OrderBy(x => x.Name)
                               .Select(x => x.ToIDAndName())
                               .ToList()
            };

            return viewModel;
        }
    }
}