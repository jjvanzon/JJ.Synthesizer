using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveDetailsPresenter
    {
        private CurveRepositories _repositories;
        private CurveManager _curveManager;

        public CurveDetailsPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _curveManager = new CurveManager(_repositories);
        }

        public CurveDetailsViewModel Show(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.ID);

            // ToViewModel
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel(_repositories.NodeTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurveDetailsViewModel Refresh(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.ID);

            // ToViewModel
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel(_repositories.NodeTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurveDetailsViewModel Close(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            CurveDetailsViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public CurveDetailsViewModel LoseFocus(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            CurveDetailsViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private CurveDetailsViewModel Update(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.ID);

            // Business
            VoidResult result = _curveManager.Validate(entity);

            // ToViewModel
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel(_repositories.NodeTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        public CurveDetailsViewModel SelectNode(CurveDetailsViewModel userInput, int nodeID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.ID);

            // ToViewModel
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel(_repositories.NodeTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.SelectedNodeID = nodeID;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        // Helpers

        private void CopyNonPersistedProperties(CurveDetailsViewModel sourceViewModel, CurveDetailsViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;

            destViewModel.SelectedNodeID = sourceViewModel.SelectedNodeID;
        }
    }
}