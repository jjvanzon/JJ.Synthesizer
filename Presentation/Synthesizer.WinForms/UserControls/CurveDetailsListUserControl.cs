using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveDetailsListUserControl : UserControl
    {
        public event EventHandler<EventArgs<int>> ChangeSelectedNodeTypeRequested;
        public event EventHandler<EventArgs<int>> CloseRequested;
        public event EventHandler<EventArgs<int>> CreateNodeRequested;
        /// <summary> Parameter is CurveID, not NodeID </summary>
        public event EventHandler<EventArgs<int>> DeleteSelectedNodeRequested;
        public event EventHandler<EventArgs<int>> LoseFocusRequested;
        public event EventHandler<MoveNodeEventArgs> MoveNodeRequested;
        public event EventHandler<NodeEventArgs> SelectNodeRequested;
        public event EventHandler<EventArgs<int>> ShowCurvePropertiesRequested;
        public event EventHandler<EventArgs<int>> ShowNodePropertiesRequested;

        public CurveDetailsListUserControl() => InitializeComponent();

        private readonly IList<CurveDetailsUserControl> _userControls = new List<CurveDetailsUserControl>();

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
                    ViewModel = viewModel,
                    Font = StyleHelper.DefaultFont
                };

                userControl.ChangeSelectedNodeTypeRequested += CurveDetailsUserControl_ChangeSelectedNodeTypeRequested;
                userControl.CloseRequested += CurveDetailsUserControl_CloseRequested;
                userControl.CreateNodeRequested += CurveDetailsUserControl_CreateNodeRequested;
                userControl.DeleteSelectedNodeRequested += CurveDetailsUserControl_DeleteSelectedNodeRequested;
                userControl.LoseFocusRequested += CurveDetailsUserControl_LoseFocusRequested;
                userControl.MoveNodeRequested += CurveDetailsUserControl_MoveNodeRequested;
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
                userControl.DeleteSelectedNodeRequested -= CurveDetailsUserControl_DeleteSelectedNodeRequested;
                userControl.LoseFocusRequested -= CurveDetailsUserControl_LoseFocusRequested;
                userControl.MoveNodeRequested -= CurveDetailsUserControl_MoveNodeRequested;
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

            int totalSpacing = StyleHelper.DefaultSpacing * (controlCount - 1);
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
                y += StyleHelper.DefaultSpacing;
            }
        }

        private void CurveDetailsListUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
        private void CurveDetailsUserControl_CloseRequested(object sender, EventArgs<int> e) => CloseRequested(sender, e);
        private void CurveDetailsUserControl_CreateNodeRequested(object sender, EventArgs<int> e) => CreateNodeRequested(sender, e);
        private void CurveDetailsUserControl_ChangeSelectedNodeTypeRequested(object sender, EventArgs<int> e) => ChangeSelectedNodeTypeRequested(sender, e);
        private void CurveDetailsUserControl_DeleteSelectedNodeRequested(object sender, EventArgs<int> e) => DeleteSelectedNodeRequested(sender, e);
        private void CurveDetailsUserControl_LoseFocusRequested(object sender, EventArgs<int> e) => LoseFocusRequested(sender, e);
        private void CurveDetailsUserControl_MoveNodeRequested(object sender, MoveNodeEventArgs e) => MoveNodeRequested(sender, e);
        private void CurveDetailsUserControl_SelectNodeRequested(object sender, NodeEventArgs e) => SelectNodeRequested(sender, e);
        private void CurveDetailsUserControl_ShowCurvePropertiesRequested(object sender, EventArgs<int> e) => ShowCurvePropertiesRequested(sender, e);
        private void CurveDetailsUserControl_ShowNodePropertiesRequested(object sender, EventArgs<int> e) => ShowNodePropertiesRequested(sender, e);
    }
}
