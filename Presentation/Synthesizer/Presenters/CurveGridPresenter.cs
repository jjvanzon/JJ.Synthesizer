using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveGridPresenter : GridPresenterBase<CurveGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentManager _documentManager;

        public CurveGridPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentRepository = repositories.DocumentRepository;
            _documentManager = new DocumentManager(repositories);
        }

        // Helpers

        protected override CurveGridViewModel CreateViewModel(CurveGridViewModel userInput)
        {
            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // Business
            IList<UsedInDto<Curve>> dtos = _documentManager.GetUsedIn(document.Curves);

            // ToViewModel
            CurveGridViewModel viewModel = dtos.ToGridViewModel(userInput.DocumentID);

            return viewModel;
        }
    }
}
