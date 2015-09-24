using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ScaleGridPresenter
    {
        private IDocumentRepository _documentRepository;

        public ScaleGridViewModel ViewModel { get; set; }

        public ScaleGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        /// <summary> Can return ScaleGridViewModel or NotFoundViewModel. </summary>
        public object Refresh()
        {
            AssertViewModel();

            Document document = _documentRepository.TryGet(ViewModel.DocumentID);
            if (document == null)
            {
                ViewModelHelper.CreateDocumentNotFoundViewModel();
            }

            bool visible = ViewModel.Visible;

            ViewModel = document.Scales.ToGridViewModel(ViewModel.DocumentID);

            ViewModel.Visible = visible;

            return ViewModel;
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
