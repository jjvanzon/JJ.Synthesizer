using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDetailsPresenter
    {
        private IDocumentRepository _documentRepository;
        private IIDRepository _idRepository;

        public DocumentDetailsViewModel ViewModel { get; private set; }

        public DocumentDetailsPresenter(IDocumentRepository documentRepository, IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _documentRepository = documentRepository;
            _idRepository = idRepository;
        }

        public DocumentDetailsViewModel Create()
        {
            var document = new Document();
            document.ID = _idRepository.GetID();
            _documentRepository.Insert(document);

            ViewModel = document.ToDetailsViewModel();
            ViewModel.IDVisible = false;
            ViewModel.CanDelete = false;
            ViewModel.Visible = true;

            return ViewModel;
        }

        public void Save()
        {
            AssertViewModel();

            Document document = ViewModel.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator_Basic(document);
            if (!validator.IsValid)
            {
                if (ViewModel == null)
                {
                    ViewModel = document.ToDetailsViewModel();
                    ViewModel.IDVisible = ViewModel.IDVisible;
                    ViewModel.CanDelete = ViewModel.CanDelete;
                }

                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                // TODO: Perhaps report success and leave Committing to the MainPresenter.
                _documentRepository.Commit();

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
