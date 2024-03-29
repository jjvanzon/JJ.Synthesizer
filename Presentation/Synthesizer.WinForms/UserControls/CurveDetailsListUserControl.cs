﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer;
using JJ.Framework.Common;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveDetailsListUserControl : UserControl
    {
        public CurveDetailsListUserControl() => InitializeComponent();

        public event EventHandler<EventArgs<int>> ChangeInterpolationOfSelectedNodeRequested;
        public event EventHandler<EventArgs<int>> CloseRequested;
        public event EventHandler<EventArgs<int>> CreateNodeRequested;
        public event EventHandler<EventArgs<int>> DeleteSelectedNodeRequested;
        public event EventHandler<EventArgs<int>> ExpandCurveRequested;
        public event EventHandler<EventArgs<int>> LoseFocusRequested;
        public event EventHandler<MoveNodeEventArgs> NodeMoved;
        public event EventHandler<MoveNodeEventArgs> NodeMoving;
        public event EventHandler<EventArgs<int>> SelectCurveRequested;
        public event EventHandler<NodeEventArgs> SelectNodeRequested;
        public event EventHandler<NodeEventArgs> ExpandNodeRequested;

        private readonly IList<CurveDetailsUserControl> _userControls = new List<CurveDetailsUserControl>();
        private CurveFacade _curveFacade;

        /// <see cref="CurveDetailsUserControl.SetCurveFacade"/>
        public void SetCurveFacade(CurveFacade curveFacade) => _curveFacade = curveFacade ?? throw new ArgumentNullException(nameof(curveFacade));

        private IList<CurveDetailsViewModel> _viewModels;
        public IList<CurveDetailsViewModel> ViewModels
        {
            // ReSharper disable once UnusedMember.Global
            get => _viewModels;
            set
            {
                _viewModels = value;
                ApplyViewModelsToControls();
            }
        }

        private void ApplyViewModelsToControls()
        {
            if (_viewModels == null) return;

            IList<CurveDetailsViewModel> visibleViewModels = _viewModels.Where(x => x.Visible).ToArray();

            // Update
            int count = Math.Min(visibleViewModels.Count, _userControls.Count);
            for (int i = 0; i < count; i++)
            {
                CurveDetailsViewModel viewModel = visibleViewModels[i];
                CurveDetailsUserControl userControl = _userControls[i];
                userControl.ViewModel = viewModel;
            }

            // Create
            for (int i = _userControls.Count; i < visibleViewModels.Count; i++)
            {
                CurveDetailsViewModel viewModel = visibleViewModels[i];
                var userControl = new CurveDetailsUserControl
                {
                    Name = $"{nameof(CurveDetailsUserControl)}{i}",
                    Font = StyleHelper.DefaultFont
                };
                userControl.SetCurveFacade(_curveFacade);
                userControl.ViewModel = viewModel;

                userControl.ChangeInterpolationOfSelectedNodeRequested += CurveDetailsUserControl_ChangeInterpolationOfSelectedNodeRequested;
                userControl.CloseRequested += CurveDetailsUserControl_CloseRequested;
                userControl.CreateNodeRequested += CurveDetailsUserControl_CreateNodeRequested;
                userControl.ExpandCurveRequested += CurveDetailsUserControl_ExpandCurveRequested;
                userControl.LoseFocusRequested += CurveDetailsUserControl_LoseFocusRequested;
                userControl.NodeMoving += CurveDetailsUserControl_NodeMoving;
                userControl.NodeMoved += CurveDetailsUserControl_NodeMoved;
                userControl.DeleteRequested += CurveDetailsUserControl_DeleteRequested;
                userControl.SelectCurveRequested += CurveDetailsUserControl_SelectCurveRequested;
                userControl.SelectNodeRequested += CurveDetailsUserControl_SelectNodeRequested;
                userControl.ExpandNodeRequested += CurveDetailsUserControl_ExpandNodeRequested;

                Controls.Add(userControl);
                _userControls.Add(userControl);
            }

            // Delete
            for (int i = _userControls.Count - 1; i >= visibleViewModels.Count; i--)
            {
                CurveDetailsUserControl userControl = _userControls[i];

                userControl.ChangeInterpolationOfSelectedNodeRequested -= CurveDetailsUserControl_ChangeInterpolationOfSelectedNodeRequested;
                userControl.CloseRequested -= CurveDetailsUserControl_CloseRequested;
                userControl.CreateNodeRequested -= CurveDetailsUserControl_CreateNodeRequested;
                userControl.ExpandCurveRequested -= CurveDetailsUserControl_ExpandCurveRequested;
                userControl.LoseFocusRequested -= CurveDetailsUserControl_LoseFocusRequested;
                userControl.NodeMoving -= CurveDetailsUserControl_NodeMoving;
                userControl.NodeMoved -= CurveDetailsUserControl_NodeMoved;
                userControl.DeleteRequested -= CurveDetailsUserControl_DeleteRequested;
                userControl.SelectNodeRequested -= CurveDetailsUserControl_SelectNodeRequested;
                userControl.ExpandNodeRequested -= CurveDetailsUserControl_ExpandNodeRequested;

                Controls.Remove(userControl);
                _userControls.RemoveAt(i);
            }

            PositionControls();
        }

        private void PositionControls()
        {
            int controlCount = _userControls.Count;
            if (controlCount == 0)
            {
                return;
            }

            // SplitterWidth used on purpose instead of DefaultSpacing.
            // Even though it is not a splitter, it looks better if it is the same width as a splitter.
            int totalSpacing = StyleHelper.SplitterWidth * (controlCount - 1);
            int controlHeight = (Height - totalSpacing) / controlCount;
            if (controlHeight <= 0) controlHeight = 1;

            const int x = 0;
            int y = 0;

            // ReSharper disable once MoreSpecificForeachVariableTypeAvailable
            foreach (Control control in _userControls)
            {
                control.Left = x;
                control.Top = y;
                control.Width = Width;
                control.Height = controlHeight;

                y += controlHeight;

                // SplitterWidth used on purpose instead of DefaultSpacing.
                // Even though it is not a splitter, it looks better if it is the same width as a splitter.
                y += StyleHelper.SplitterWidth;
            }
        }

        private void CurveDetailsListUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
        private void CurveDetailsUserControl_CloseRequested(object sender, EventArgs<int> e) => CloseRequested(sender, e);
        private void CurveDetailsUserControl_CreateNodeRequested(object sender, EventArgs<int> e) => CreateNodeRequested(sender, e);
        private void CurveDetailsUserControl_ChangeInterpolationOfSelectedNodeRequested(object sender, EventArgs<int> e) => ChangeInterpolationOfSelectedNodeRequested(sender, e);
        private void CurveDetailsUserControl_ExpandCurveRequested(object sender, EventArgs<int> e) => ExpandCurveRequested(sender, e);
        private void CurveDetailsUserControl_LoseFocusRequested(object sender, EventArgs<int> e) => LoseFocusRequested(sender, e);
        private void CurveDetailsUserControl_NodeMoving(object sender, MoveNodeEventArgs e) => NodeMoving(sender, e);
        private void CurveDetailsUserControl_NodeMoved(object sender, MoveNodeEventArgs e) => NodeMoved(sender, e);
        private void CurveDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e) => DeleteSelectedNodeRequested(sender, e);
        private void CurveDetailsUserControl_SelectCurveRequested(object sender, EventArgs<int> e) => SelectCurveRequested(sender, e);
        private void CurveDetailsUserControl_SelectNodeRequested(object sender, NodeEventArgs e) => SelectNodeRequested(sender, e);
        private void CurveDetailsUserControl_ExpandNodeRequested(object sender, NodeEventArgs e) => ExpandNodeRequested(sender, e);
    }
}
