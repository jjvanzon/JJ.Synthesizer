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

        // TODO: Remove outcommented code.
        //public void DeleteNode()
        //{
        //    AssertViewModel();

        //    if (!ViewModel.SelectedNodeID.HasValue)
        //    {
        //        ViewModel.ValidationMessages.Add(new Message
        //        {
        //            PropertyKey = PresentationPropertyNames.SelectedNodeID,
        //            Text = PresentationMessages.SelectANodeFirst
        //        });
        //        return;
        //    }

        //    if (ViewModel.Entity.Nodes.Count <= 2)
        //    {
        //        ViewModel.ValidationMessages.Add(new Message
        //        {
        //            PropertyKey = PropertyNames.Nodes,
        //            // TODO: If you would just have done the ToEntity-Business-ToViewModel roundtrip, the validator would have taken care of it.
        //            Text = ValidationMessageFormatter.Min(CommonTitleFormatter.EntityCount(PropertyDisplayNames.Nodes), 2)
        //        });
        //        return;
        //    }

        //    ViewModel.Entity.Nodes.RemoveFirst(x => x.ID == ViewModel.SelectedNodeID);

        //    ViewModel.SelectedNodeID = null;
        //}

        //public void MoveNode(int nodeID, double time, double value)
        //{
        //    AssertViewModel();

        //    NodeViewModel nodeViewModel = ViewModel.Entity.Nodes.Where(x => x.ID == nodeID).Single();
        //    nodeViewModel.Time = time;
        //    nodeViewModel.Value = value;
        //}

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}