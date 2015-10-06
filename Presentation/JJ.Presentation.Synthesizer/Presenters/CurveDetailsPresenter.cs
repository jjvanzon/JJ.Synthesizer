using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Api;
using System;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Managers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Linq;

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

            // TODO: Remove outcommente code.
            //Curve mockCurve = CurveApi.Create(10.0, 0, 0.8, 1.0, null, 0.8, null, null, 0.2, null, null, 0.0);
            //ViewModel.Entity = mockCurve.ToViewModelWithRelatedEntities();

            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            Curve entity = _repositories.CurveRepository.Get(ViewModel.Entity.ID);

            bool visible = ViewModel.Visible;
            ViewModel = entity.ToDetailsViewModel(_repositories.NodeTypeRepository);
            ViewModel.Visible = true;
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

            IValidator validator = new CurveValidator(entity);

            ViewModel.Successful = validator.IsValid;
            ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
        }

        public void CreateNode()
        {
            AssertViewModel();

            // ToEntity
            Curve curve = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Node afterNode = null;
            if (ViewModel.SelectedNode != null)
            {
                afterNode = _repositories.NodeRepository.Get(ViewModel.SelectedNode.ID);
            }
            else
            {
                // Insert after last node if none selected.
                afterNode = curve.Nodes.OrderBy(x => x.Time).Last();
            }

            // Business
            Node node = _curveManager.CreateNode(curve, afterNode);

            // ToViewModel
            NodeViewModel nodeViewModel = node.ToViewModel();
            ViewModel.Entity.Nodes.Add(nodeViewModel);
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
   }
}