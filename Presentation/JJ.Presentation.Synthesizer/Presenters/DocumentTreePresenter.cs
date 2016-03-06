using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Linq;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentTreePresenter : PresenterBase<DocumentTreeViewModel>
    {
        private IDocumentRepository _documentRepository;

        public DocumentTreePresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public DocumentTreeViewModel Show(DocumentTreeViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.ID);

            // ToViewModel
            DocumentTreeViewModel viewModel = document.ToTreeViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentTreeViewModel Refresh(DocumentTreeViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.ID);

            // ToViewModel
            DocumentTreeViewModel viewModel = document.ToTreeViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentTreeViewModel ExpandNode(DocumentTreeViewModel userInput, int childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.ID);

            // ToViewModel
            DocumentTreeViewModel viewModel = document.ToTreeViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // 'Business'
            PatchTreeNodeViewModel nodeViewModel = 
                EnumeratePatchTreeNodeViewModels(viewModel)
                .Where(x => x.ChildDocumentID == childDocumentID)
                .SingleOrDefault();

            if (nodeViewModel == null)
            {
                throw new Exception(String.Format("childDocumentID '{0}' does not match with any PatchTreeNodeViewModel.", childDocumentID));
            }

            nodeViewModel.IsExpanded = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentTreeViewModel CollapseNode(DocumentTreeViewModel userInput, int childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.ID);

            // ToViewModel
            DocumentTreeViewModel viewModel = document.ToTreeViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // 'Business'
            PatchTreeNodeViewModel nodeViewModel =
                EnumeratePatchTreeNodeViewModels(viewModel)
                .Where(x => x.ChildDocumentID == childDocumentID)
                .SingleOrDefault();

            if (nodeViewModel == null)
            {
                throw new Exception(String.Format("childDocumentID '{0}' does not match with any PatchTreeNodeViewModel.", childDocumentID));
            }

            nodeViewModel.IsExpanded = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentTreeViewModel Close(DocumentTreeViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.ID);

            // ToViewModel
            DocumentTreeViewModel viewModel = document.ToTreeViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        // Helpers

        /// <summary>
        /// Copies the Visible en IsExpanded properties.
        /// It matches source and dest nodes by the ChildDocumentID,
        /// so it is important to keep those unique and (relatively) constant.
        /// </summary>
        protected override void CopyNonPersistedProperties(DocumentTreeViewModel sourceViewModel, DocumentTreeViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            var join = from sourceInstrumentViewModel in EnumeratePatchTreeNodeViewModels(sourceViewModel)
                       join destInstrumentViewModel in EnumeratePatchTreeNodeViewModels(destViewModel)
                       on sourceInstrumentViewModel.ChildDocumentID equals destInstrumentViewModel.ChildDocumentID
                       select new { sourceInstrumentViewModel, destInstrumentViewModel };

            foreach (var tuple in join)
            {
                tuple.destInstrumentViewModel.IsExpanded = tuple.sourceInstrumentViewModel.IsExpanded;
            }
        }

        private IEnumerable<PatchTreeNodeViewModel> EnumeratePatchTreeNodeViewModels(DocumentTreeViewModel documentTreeViewModel)
        {
            return Enumerable.Union(
                documentTreeViewModel.PatchesNode.PatchNodes,
                documentTreeViewModel.PatchesNode.PatchGroupNodes.SelectMany(x => x.Patches));
        }
    }
}
