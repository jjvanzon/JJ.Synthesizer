using System;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveGridPresenter : GridPresenterBase<CurveGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentManager _documentManager;
        private readonly CurveManager _curveManager;

        public CurveGridPresenter(CurveManager curveManager, RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentRepository = repositories.DocumentRepository;
            _documentManager = new DocumentManager(repositories);
            _curveManager = curveManager ?? throw new ArgumentNullException(nameof(curveManager));
        }

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

        public CurveGridViewModel Delete(CurveGridViewModel userInput, int id)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // Business
                    IResult result = _curveManager.DeleteWithRelatedEntities(id);

                    // Non-Persisted
                    viewModel.ValidationMessages.AddRange(result.Messages);

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }
    }
}
