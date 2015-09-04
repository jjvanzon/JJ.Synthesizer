using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SampleGridPresenter
    {
        private IDocumentRepository _documentRepository;
        private ISampleRepository _sampleRepository;

        public SampleGridViewModel ViewModel { get; set; }

        public SampleGridPresenter(IDocumentRepository documentRepository, ISampleRepository sampleRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _documentRepository = documentRepository;
            _sampleRepository = sampleRepository;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        /// <summary>
        /// Can return SampleGridViewModel or NotFoundViewModel.
        /// </summary>
        public object Refresh()
        {
            AssertViewModel();

            Document document = _documentRepository.TryGet(ViewModel.DocumentID);
            if (document == null)
            {
                ViewModelHelper.CreateDocumentNotFoundViewModel();
            }

            bool visible = ViewModel.Visible;

            ViewModel = document.Samples.ToGridViewModel(ViewModel.DocumentID);

            ViewModel.Visible = visible;

            return ViewModel;
        }

        public void RefreshListItem(int sampleID)
        {
            Sample sample = _sampleRepository.Get(sampleID);

            int listIndex = ViewModel.List.IndexOf(x => x.ID == sampleID);

            SampleListItemViewModel viewModel2 = sample.ToListItemViewModel();
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
