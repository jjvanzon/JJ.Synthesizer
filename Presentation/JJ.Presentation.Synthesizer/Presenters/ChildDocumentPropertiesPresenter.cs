using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ChildDocumentPropertiesPresenter
    {
        private RepositoryWrapper _repositories;
        private DocumentManager _documentManager;

        public PatchPropertiesViewModel ViewModel { get; set; }

        public ChildDocumentPropertiesPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

            _documentManager = new DocumentManager(_repositories);
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            Document entity = _repositories.DocumentRepository.Get(ViewModel.ChildDocumentID);
            bool visible = ViewModel.Visible;
            ViewModel = entity.ToChildDocumentPropertiesViewModel(_repositories.ChildDocumentTypeRepository);
            ViewModel.Visible = visible;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }
        }

        public void LoseFocus()
        {
            AssertViewModel();

            Update();
        }

        private void Update()
        {
            AssertViewModel();

            Document entity = ViewModel.ToEntity(_repositories.DocumentRepository, _repositories.ChildDocumentTypeRepository);

            VoidResult result = _documentManager.SaveChildDocument(entity);

            ViewModel.ValidationMessages = result.Messages;
            ViewModel.Successful = result.Successful;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
