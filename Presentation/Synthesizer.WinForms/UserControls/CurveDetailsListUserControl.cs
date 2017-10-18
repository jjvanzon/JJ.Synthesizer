using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveDetailsListUserControl : UserControl
    {
        public CurveDetailsListUserControl() => InitializeComponent();

        public event EventHandler<EventArgs<int>> ChangeSelectedNodeTypeRequested;
        public event EventHandler<EventArgs<int>> CloseRequested;
        public event EventHandler<EventArgs<int>> CreateNodeRequested;
        /// <summary> Will delete the selected node, not the curve. </summary>
        public event EventHandler<EventArgs<int>> DeleteRequested;
        public event EventHandler<EventArgs<int>> LoseFocusRequested;
        public event EventHandler<MoveNodeEventArgs> NodeMoved;
        public event EventHandler<MoveNodeEventArgs> NodeMoving;
        public event EventHandler<NodeEventArgs> SelectNodeRequested;
        public event EventHandler<EventArgs<int>> ShowCurvePropertiesRequested;
        public event EventHandler<EventArgs<int>> ShowNodePropertiesRequested;

        private readonly IList<CurveDetailsUserControl> _userControls = new List<CurveDetailsUserControl>();
        private CurveManager _curveManager;

        /// <see cref="CurveDetailsUserControl.SetCurveManager "/>
        public void SetCurveManager(CurveManager curveManager)
        {
            _curveManager = curveManager ?? throw new ArgumentNullException(nameof(curveManager));
        }

        private IList<CurveDetailsViewModel> _viewModels;
        public IList<CurveDetailsViewModel> ViewModels
        {
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
                userControl.SetCurveManager(_curveManager);
                userControl.ViewModel = viewModel;

                userControl.ChangeSelectedNodeTypeRequested += CurveDetailsUserControl_ChangeSelectedNodeTypeRequested;
                userControl.CloseRequested += CurveDetailsUserControl_CloseRequested;
                userControl.CreateNodeRequested += CurveDetailsUserControl_CreateNodeRequested;
                userControl.RemoveRequested += CurveDetailsUserControl_DeleteRequested;
                userControl.LoseFocusRequested += CurveDetailsUserControl_LoseFocusRequested;
                userControl.NodeMoving += CurveDetailsUserControl_NodeMoving;
                userControl.NodeMoved += CurveDetailsUserControl_NodeMoved;
                userControl.SelectNodeRequested += CurveDetailsUserControl_SelectNodeRequested;
                userControl.ShowCurvePropertiesRequested += CurveDetailsUserControl_ShowCurvePropertiesRequested;
                userControl.ShowNodePropertiesRequested += CurveDetailsUserControl_ShowNodePropertiesRequested;

                Controls.Add(userControl);
                _userControls.Add(userControl);
            }

            // Delete
            for (int i = _userControls.Count - 1; i >= visibleViewModels.Count; i--)
            {
                CurveDetailsUserControl userControl = _userControls[i];

                userControl.ChangeSelectedNodeTypeRequested -= CurveDetailsUserControl_ChangeSelectedNodeTypeRequested;
                userControl.CloseRequested -= CurveDetailsUserControl_CloseRequested;
                userControl.CreateNodeRequested -= CurveDetailsUserControl_CreateNodeRequested;
                userControl.RemoveRequested -= CurveDetailsUserControl_DeleteRequested;
                userControl.LoseFocusRequested -= CurveDetailsUserControl_LoseFocusRequested;
                userControl.NodeMoving -= CurveDetailsUserControl_NodeMoving;
                userControl.NodeMoved -= CurveDetailsUserControl_NodeMoved;
                userControl.SelectNodeRequested -= CurveDetailsUserControl_SelectNodeRequested;
                userControl.ShowCurvePropertiesRequested -= CurveDetailsUserControl_ShowCurvePropertiesRequested;
                userControl.ShowNodePropertiesRequested -= CurveDetailsUserControl_ShowNodePropertiesRequested;

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
        private void CurveDetailsUserControl_ChangeSelectedNodeTypeRequested(object sender, EventArgs<int> e) => ChangeSelectedNodeTypeRequested(sender, e);
        private void CurveDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e) => DeleteRequested(sender, e);
        private void CurveDetailsUserControl_LoseFocusRequested(object sender, EventArgs<int> e) => LoseFocusRequested(sender, e);
        private void CurveDetailsUserControl_NodeMoving(object sender, MoveNodeEventArgs e) => NodeMoving(sender, e);
        private void CurveDetailsUserControl_NodeMoved(object sender, MoveNodeEventArgs e) => NodeMoved(sender, e);
        private void CurveDetailsUserControl_SelectNodeRequested(object sender, NodeEventArgs e) => SelectNodeRequested(sender, e);
        private void CurveDetailsUserControl_ShowCurvePropertiesRequested(object sender, EventArgs<int> e) => ShowCurvePropertiesRequested(sender, e);
        private void CurveDetailsUserControl_ShowNodePropertiesRequested(object sender, EventArgs<int> e) => ShowNodePropertiesRequested(sender, e);
    }
}
