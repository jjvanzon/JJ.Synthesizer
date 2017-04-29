using System;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentGridPresenter : GridPresenterBase<DocumentGridViewModel>
    {
        private readonly RepositoryWrapper _repositories;

        public DocumentGridPresenter([NotNull] RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
        }

        protected override DocumentGridViewModel CreateViewModel(DocumentGridViewModel userInput)
        {
            // Known bug, not easily solvable and also not a large problem: 
            // A renamed, uncommitted document will not end up in a new place in the list,
            // because the sorting done by the data store, which is not ware of the new name.

            // GetEntities
            IList<Document> documents = _repositories.DocumentRepository.OrderByName();

            // ToViewModel
            DocumentGridViewModel viewModel = documents.ToGridViewModel();

            return viewModel;
        }

        public DocumentGridViewModel Play(DocumentGridViewModel userInput, int id)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(id);

                    // Business
                    var patchManager = new PatchManager(new PatchRepositories(_repositories));
                    Result<Outlet> result = patchManager.TryAutoPatchFromDocumentRandomly(document);
                    Outlet outlet = result.Data;

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }
    }
}
