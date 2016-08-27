using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveGridPresenter : PresenterBase<CurveGridViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        public CurveGridPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

            _documentManager = new DocumentManager(_repositories);
        }

        public CurveGridViewModel Show(CurveGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            CurveGridViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurveGridViewModel Refresh(CurveGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            CurveGridViewModel viewModel = CreateViewModel(userInput);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurveGridViewModel Close(CurveGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            CurveGridViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        // Helpers

        private CurveGridViewModel CreateViewModel(CurveGridViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.DocumentID);

            // Business
            IList<CurveUsedInDto> curveUsedInDtos = document.Curves
                                                            .Select(x => new CurveUsedInDto
                                                            {
                                                                Curve = x,
                                                                UsedIn = _documentManager.GetUsedIn(x)
                                                            })
                                                            .ToArray();
            // ToViewModel
            CurveGridViewModel viewModel = curveUsedInDtos.ToGridViewModel(userInput.DocumentID);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
        }
    }
}