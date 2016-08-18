using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Canonical;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurvePropertiesPresenter : PresenterBase<CurvePropertiesViewModel>
    {
        private CurveRepositories _repositories;
        private CurveManager _curveManager;

        public CurvePropertiesPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _curveManager = new CurveManager(_repositories);
        }

        public CurvePropertiesViewModel Show(CurvePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.ID);

            // ToViewModel
            CurvePropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurvePropertiesViewModel Refresh(CurvePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.ID);

            // ToViewModel
            CurvePropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurvePropertiesViewModel Close(CurvePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            CurvePropertiesViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public CurvePropertiesViewModel LoseFocus(CurvePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            CurvePropertiesViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private CurvePropertiesViewModel Update(CurvePropertiesViewModel userInput)
        {
            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.ID);

            // Business
            VoidResult result = _curveManager.SaveCurveWithRelatedEntities(entity);

            // ToViewModel
            CurvePropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}
