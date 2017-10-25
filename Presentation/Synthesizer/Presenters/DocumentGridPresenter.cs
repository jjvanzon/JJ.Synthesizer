using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentGridPresenter : PresenterBaseWithoutSave<IList<Document>, DocumentGridViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly AutoPatcher _autoPatcher;

        public DocumentGridPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
            _autoPatcher = new AutoPatcher(_repositories);
        }

        public DocumentGridViewModel Load(DocumentGridViewModel viewModel) => ExecuteAction(viewModel, x => x.Visible = true);

        /// <summary>
        /// Known bug, not easily solvable and also not a large problem: 
        /// A renamed, uncommitted document will not end up in a new place in the list,
        /// because the sorting done by the data store, which is not ware of the new name.
        /// </summary>
        protected override IList<Document> GetEntity(DocumentGridViewModel userInput) => _repositories.DocumentRepository.OrderByName();

        protected override DocumentGridViewModel ToViewModel(IList<Document> entities) => entities.ToGridViewModel();

        public DocumentGridViewModel Play(DocumentGridViewModel userInput, int id)
        {
            return ExecuteAction(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(id);

                    // Business
                    Result<Outlet> result = _autoPatcher.TryAutoPatchFromDocumentRandomly(document, mustIncludeHidden: false);
                    Outlet outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages.AddRange(result.Messages);
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }
    }
}