using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Business.CanonicalModel;
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

            IValidator validator = new CurveValidator(entity);

            ViewModel.Successful = validator.IsValid;
            ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
        }

        public void SelectNode(int nodeID)
        {
            AssertViewModel();

            ViewModel.SelectedNodeID = nodeID;
        }

        public void CreateNode()
        {
            AssertViewModel();

            // ToEntity
            Curve curve = ViewModel.ToEntityWithRelatedEntities(_repositories);
            Node afterNode = null;
            if (ViewModel.SelectedNodeID.HasValue)
            {
                afterNode = _repositories.NodeRepository.Get(ViewModel.SelectedNodeID.Value);
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

        public void DeleteNode()
        {
            AssertViewModel();

            if (!ViewModel.SelectedNodeID.HasValue)
            {
                ViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedNodeID,
                    Text = PresentationMessages.SelectANodeFirst
                });
                return;
            }

            if (ViewModel.Entity.Nodes.Count <= 2)
            {
                ViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PropertyNames.Nodes,
                    // TODO: If you would just have done the ToEntity-Business-ToViewModel roundtrip, the validator would have taken care of it.
                    Text = ValidationMessageFormatter.Min(CommonTitleFormatter.EntityCount(PropertyDisplayNames.Nodes), 2)
                });
                return;
            }

            ViewModel.Entity.Nodes.RemoveFirst(x => x.ID == ViewModel.SelectedNodeID);

            ViewModel.SelectedNodeID = null;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}