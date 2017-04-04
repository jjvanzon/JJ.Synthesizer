using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SampleGridPresenter : GridPresenterBase<SampleGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentManager _documentManager;

        public SampleGridPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentRepository = repositories.DocumentRepository;
            _documentManager = new DocumentManager(repositories);
        }

        protected override SampleGridViewModel CreateViewModel(SampleGridViewModel userInput)
        {
            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // Business
            IList<UsedInDto<Sample>> dtos = _documentManager.GetUsedIn(document.Samples);

            // ToViewModel
            SampleGridViewModel viewModel = dtos.ToGridViewModel(document.ID);

            return viewModel;
        }
    }
}
