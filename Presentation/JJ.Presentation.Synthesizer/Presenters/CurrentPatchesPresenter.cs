using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurrentPatchesPresenter
    {
        private IDocumentRepository _documentRepository;

        public CurrentPatchesPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public CurrentPatchesViewModel Show(CurrentPatchesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> childDocumentIDs = userInput.List.Select(x => x.ChildDocumentID);
            IList<Document> childDocuments = childDocumentIDs.Select(x => _documentRepository.Get(x)).ToList();

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(childDocuments);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentPatchesViewModel Close(CurrentPatchesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> childDocumentIDs = userInput.List.Select(x => x.ChildDocumentID);
            IList<Document> childDocuments = childDocumentIDs.Select(x => _documentRepository.Get(x)).ToList();

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(childDocuments);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentPatchesViewModel Add(CurrentPatchesViewModel userInput, int childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> childDocumentIDs = userInput.List.Select(x => x.ChildDocumentID);
            IList<Document> childDocuments = childDocumentIDs.Select(x => _documentRepository.Get(x)).ToList();

            // Business
            Document childDocument = _documentRepository.Get(childDocumentID);
            childDocuments.Add(childDocument);

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(childDocuments);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentPatchesViewModel Remove(CurrentPatchesViewModel userInput, int childDocumentID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> childDocumentIDs = userInput.List.Select(x => x.ChildDocumentID);
            IList<Document> childDocuments = childDocumentIDs.Select(x => _documentRepository.Get(x)).ToList();

            // Business
            childDocuments.RemoveFirst(x => x.ID == childDocumentID);

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(childDocuments);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentPatchesViewModel Move(CurrentPatchesViewModel userInput, int childDocumentID, int newPosition)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> childDocumentIDs = userInput.List.Select(x => x.ChildDocumentID);
            IList<Document> childDocuments = childDocumentIDs.Select(x => _documentRepository.Get(x)).ToList();

            // Business
            int currentPosition = childDocuments.IndexOf(x => x.ID == childDocumentID);
            Document childDocument = childDocuments[currentPosition];
            childDocuments.RemoveAt(currentPosition);
            childDocuments.Insert(newPosition, childDocument);

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(childDocuments);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        /// <summary> No new view model is created. Just the child view models are replaced. </summary>
        public CurrentPatchesViewModel Refresh(CurrentPatchesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> childDocumentIDs = userInput.List.Select(x => x.ChildDocumentID);
            IList<Document> childDocuments = childDocumentIDs.Select(x => _documentRepository.Get(x)).ToList();

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(childDocuments);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        private void CopyNonPersistedProperties(CurrentPatchesViewModel sourceViewModel, CurrentPatchesViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}
