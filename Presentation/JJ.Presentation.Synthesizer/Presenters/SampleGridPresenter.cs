using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SampleGridPresenter : PresenterBase<SampleGridViewModel>
    {
        private IDocumentRepository _documentRepository;
        private ISampleRepository _sampleRepository;

        public SampleGridPresenter(IDocumentRepository documentRepository, ISampleRepository sampleRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _documentRepository = documentRepository;
            _sampleRepository = sampleRepository;
        }

        public SampleGridViewModel Show(SampleGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            SampleGridViewModel viewModel = document.Samples.ToGridViewModel(document.ID);

            // Non-Persisted
            viewModel.Visible = true;

            return viewModel;
        }

        public SampleGridViewModel Refresh(SampleGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);
            
            // ToViewModel
            SampleGridViewModel viewModel = document.Samples.ToGridViewModel(document.ID);

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            return viewModel;
        }

        public SampleGridViewModel Close(SampleGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            SampleGridViewModel viewModel = document.Samples.ToGridViewModel(document.ID);

            // Non-Persisted
            viewModel.Visible = false;

            return viewModel;
        }
    }
}
