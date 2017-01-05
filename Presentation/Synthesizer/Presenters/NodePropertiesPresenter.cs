using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class NodePropertiesPresenter : PresenterBase<NodePropertiesViewModel>
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

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Node entity = _repositories.NodeRepository.Get(userInput.Entity.ID);

            // ToViewModel
            NodePropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public NodePropertiesViewModel Refresh(NodePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Node entity = _repositories.NodeRepository.Get(userInput.Entity.ID);

            // ToViewModel
            NodePropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

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

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Node entity = _repositories.NodeRepository.Get(userInput.Entity.ID);

            // Business
            VoidResult result = _curveManager.SaveNode(entity);

            // ToViewModel
            NodePropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            result.Messages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}
