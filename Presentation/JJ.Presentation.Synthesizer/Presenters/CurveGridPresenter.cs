using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveGridPresenter
    {
        private IDocumentRepository _documentRepository;
        private ICurveRepository _curveRepository;

        public CurveGridViewModel ViewModel { get; set; }

        public CurveGridPresenter(IDocumentRepository documentRepository, ICurveRepository curveRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            _documentRepository = documentRepository;
            _curveRepository = curveRepository;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        /// <summary> Can return CurveGridViewModel or NotFoundViewModel. </summary>
        public object Refresh()
        {
            AssertViewModel();

            Document document = _documentRepository.TryGet(ViewModel.DocumentID);
            if (document == null)
            {
                ViewModelHelper.CreateDocumentNotFoundViewModel();
            }

            bool visible = ViewModel.Visible;

            ViewModel = document.Curves.ToGridViewModel(ViewModel.DocumentID);

            ViewModel.Visible = visible;

            return ViewModel;
        }

        public void RefreshListItem(int curveID)
        {
            Curve curve = _curveRepository.Get(curveID);

            int listIndex = ViewModel.List.IndexOf(x => x.ID == curveID);

            IDAndName viewModel2 = curve.ToIDAndName();
            ViewModel.List[listIndex] = viewModel2;

            ViewModel.List = ViewModel.List.OrderBy(x => x.Name).ToList();
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
