using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDetailsPresenter
    {
        private RepositoryWrapper _repositories;
        private DocumentManager _documentManager;

        public DocumentDetailsViewModel ViewModel { get; private set; }

        public DocumentDetailsPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _documentManager = new DocumentManager(repositories);
        }

        public DocumentDetailsViewModel Create()
        {
            var document = new Document();
            document.ID = _repositories.IDRepository.GetID();
            _repositories.DocumentRepository.Insert(document);

            ViewModel = document.ToDetailsViewModel();
            ViewModel.IDVisible = false;
            ViewModel.CanDelete = false;
            ViewModel.Visible = true;

            return ViewModel;
        }

        public void Save()
        {
            AssertViewModel();

            Document document = ViewModel.ToEntity(_repositories.DocumentRepository);

            VoidResult result = _documentManager.ValidateNonRecursive(document);
            if (!result.Successful)
            {
                ViewModel.ValidationMessages = result.Messages;
            }
            else
            {
                // TODO: Perhaps report success and leave Committing to the MainPresenter.
                _repositories.DocumentRepository.Commit();

                if (ViewModel == null)
                {
                    ViewModel = ViewModelHelper.CreateEmptyDocumentDetailsViewModel();
                }

                ViewModel.Visible = false;
            }
        }

        public void Close()
        {
            AssertViewModel();

            ViewModel.Visible = false;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
