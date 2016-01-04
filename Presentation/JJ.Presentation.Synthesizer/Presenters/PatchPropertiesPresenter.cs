using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchPropertiesPresenter
    {
        private RepositoryWrapper _repositories;

        private DocumentManager _documentManager;
        private PatchManager _patchManager;

        public PatchPropertiesViewModel ViewModel { get; set; }

        public PatchPropertiesPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

            _documentManager = new DocumentManager(_repositories);
            _patchManager = new PatchManager(new PatchRepositories(_repositories));
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
            ViewModel = entity.ToPatchPropertiesViewModel();
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

            Document childDocument = ViewModel.ToChildDocument(_repositories.DocumentRepository);
            Patch patch = ViewModel.ToPatch(_repositories.PatchRepository);

            VoidResult result = _documentManager.SaveChildDocument(childDocument);
            ViewModel.ValidationMessages = result.Messages;
            ViewModel.Successful = result.Successful;

            if (!result.Successful)
            {
                return;
            }

            _patchManager.Patch = childDocument.Patches[0];
            VoidResult result2 = _patchManager.SavePatch();
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
