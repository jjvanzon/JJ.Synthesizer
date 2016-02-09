using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class NodePropertiesPresenter
    {
        private readonly CurveRepositories _repositories;
        private readonly CurveManager _curveManager;

        public NodePropertiesPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _curveManager = new CurveManager(_repositories);
        }

        public NodePropertiesViewModel Show(NodePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Node entity = _repositories.NodeRepository.Get(userInput.Entity.ID);

            // ToViewModel
            NodePropertiesViewModel viewModel = entity.ToPropertiesViewModel(_repositories.NodeTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            return viewModel;
        }

        public NodePropertiesViewModel Refresh(NodePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Node entity = _repositories.NodeRepository.Get(userInput.Entity.ID);

            // ToViewModel
            NodePropertiesViewModel viewModel = entity.ToPropertiesViewModel(_repositories.NodeTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
        }

        public NodePropertiesViewModel Close(NodePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            NodePropertiesViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public NodePropertiesViewModel LoseFocus(NodePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            NodePropertiesViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private NodePropertiesViewModel Update(NodePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Node entity = _repositories.NodeRepository.Get(userInput.Entity.ID);

            // TODO: Low priority: I doubt it is wise to validate without parent,
            // because it is incorrect data.
            VoidResult result = _curveManager.ValidateNodeWithoutParent(entity);

            // ToViewModel
            NodePropertiesViewModel viewModel = entity.ToPropertiesViewModel(_repositories.NodeTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        // Helpers

        private void CopyNonPersistedProperties(NodePropertiesViewModel sourceViewModel, NodePropertiesViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}
