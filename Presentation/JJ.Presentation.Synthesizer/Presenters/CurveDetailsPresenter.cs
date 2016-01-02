using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation.Resources;
using JJ.Framework.Presentation.Resources;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveDetailsPresenter
    {
        public CurveDetailsViewModel ViewModel { get; set; }

        private CurveRepositories _repositories;
        private CurveManager _curveManager;

        public CurveDetailsPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _curveManager = new CurveManager(_repositories);
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            Curve entity = _repositories.CurveRepository.Get(ViewModel.Entity.ID);

            bool visible = ViewModel.Visible;
            int? selectedNodeID = ViewModel.SelectedNodeID;

            ViewModel = entity.ToDetailsViewModel(_repositories.NodeTypeRepository);

            ViewModel.Visible = true;
            ViewModel.SelectedNodeID = selectedNodeID;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }
        }

        public void LoseFocus()
        {
            Update();
        }

        private void Update()
        {
            AssertViewModel();

            Curve entity = ViewModel.ToEntityWithRelatedEntities(_repositories);

            VoidResult result = _curveManager.Validate(entity);

            ViewModel.Successful = result.Successful;
            ViewModel.ValidationMessages = result.Messages;
        }

        public void SelectNode(int nodeID)
        {
            AssertViewModel();

            ViewModel.SelectedNodeID = nodeID;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}