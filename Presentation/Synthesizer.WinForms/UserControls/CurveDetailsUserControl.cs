using System;
using System.Windows.Forms;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveDetailsUserControl : DetailsOrPropertiesUserControlBase
    {
        public event EventHandler<EventArgs<int>> CreateNodeRequested;
        /// <summary> Parameter is CurveID, not NodeID </summary>
        public event EventHandler<EventArgs<int>> DeleteSelectedNodeRequested;
        public event EventHandler<NodeEventArgs> SelectNodeRequested;
        public event EventHandler<MoveNodeEventArgs> MoveNodeRequested;
        public event EventHandler<EventArgs<int>> ShowCurvePropertiesRequested;
        public event EventHandler<EventArgs<int>> ChangeSelectedNodeTypeRequested;
        public event EventHandler<EventArgs<int>> ShowNodePropertiesRequested;

        private readonly CurveDetailsViewModelToDiagramConverter _converter;

        public CurveDetailsUserControl()
        {
            _converter = new CurveDetailsViewModelToDiagramConverter(
                SystemInformation.DoubleClickTime,
                SystemInformation.DoubleClickSize.Width);

            _converter.Result.KeyDownGesture.KeyDown += Diagram_KeyDown;
            _converter.Result.SelectNodeGesture.SelectNodeRequested += SelectNodeGesture_NodeSelected;
            _converter.Result.MoveNodeGesture.Moving += MoveNodeGesture_Moving;
            _converter.Result.ShowCurvePropertiesGesture.ShowCurvePropertiesRequested += ShowCurvePropertiesGesture_ShowCurvePropertiesRequested;
            _converter.Result.ChangeNodeTypeGesture.ChangeNodeTypeRequested += ChangeNodeTypeGesture_ChangeNodeTypeRequested;
            _converter.Result.ShowNodePropertiesMouseGesture.ShowNodePropertiesRequested += ShowNodePropertiesMouseGesture_ShowNodePropertiesRequested;
            _converter.Result.ShowNodePropertiesKeyboardGesture.ShowNodePropertiesRequested += ShowNodePropertiesKeyboardGesture_ShowNodePropertiesRequested;
            _converter.Result.NodeToolTipGesture.ToolTipTextRequested += NodeToolTipGesture_ToolTipTextRequested;

            InitializeComponent();
        }

        private void CurveDetailsUserControl_Load(object sender, EventArgs e)
        {
            diagramControl.Diagram = _converter.Result.Diagram;
        }

        // Gui

        protected override void SetTitles()
        {
            TitleBarText = CommonTitlesFormatter.ObjectDetails(PropertyDisplayNames.Curve);
        }

        protected override void PositionControls()
        {
            base.PositionControls();

            diagramControl.Left = 0;
            diagramControl.Top = TitleBarHeight;
            diagramControl.Width = Width;
            diagramControl.Height = Height - TitleBarHeight;
        }

        // Binding

        public new CurveDetailsViewModel ViewModel
        {
            get { return (CurveDetailsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override int GetID()
        {
            return ViewModel.CurveID;
        }

        protected override void ApplyViewModelToControls()
        {
            _converter.Execute(ViewModel);

            diagramControl.Refresh();
        }

        // Events

        private void CurveDetailsUserControl_Resize(object sender, EventArgs e)
        {
            if (ViewModel != null)
            {
                ApplyViewModelToControls();
            }
        }

        private void CurveDetailsUserControl_Paint(object sender, PaintEventArgs e)
        {
            if (ViewModel != null)
            {
                ApplyViewModelToControls();
            }
        }

        private void CurveDetailsUserControl_AddClicked(object sender, EventArgs e)
        {
            CreateNode();

        }

        private void CurveDetailsUserControl_RemoveClicked(object sender, EventArgs e)
        {
            DeleteSelectedNode();
        }

        private void Diagram_KeyDown(object sender, JJ.Framework.Presentation.VectorGraphics.EventArg.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case KeyCodeEnum.Insert:
                    CreateNode();
                    break;

                case KeyCodeEnum.Delete:
                    DeleteSelectedNode();
                    break;
            }
        }

        private void SelectNodeGesture_NodeSelected(object sender, ElementEventArgs e)
        {
            if (ViewModel == null) return;

            int nodeID = (int)e.Element.Tag;

            SelectNodeRequested?.Invoke(this, new NodeEventArgs(ViewModel.CurveID, nodeID));

            _converter.Result.ShowNodePropertiesKeyboardGesture.SelectedNodeID = nodeID;
        }

        private void MoveNodeGesture_Moving(object sender, ElementEventArgs e)
        {
            if (ViewModel == null) return;
            if (MoveNodeRequested == null) return;

            int nodeID = (int)e.Element.Tag;

            var rectangle = (Rectangle)e.Element;

            float x = rectangle.Position.AbsoluteX + rectangle.Position.Width / 2;
            float y = rectangle.Position.AbsoluteY + rectangle.Position.Height / 2;

            MoveNodeRequested(this, new MoveNodeEventArgs(ViewModel.CurveID, nodeID, x, y));

            ApplyViewModelToControls();

            // TODO: This kind of seems to belong in the ApplyViewModelToControls().
            // Refresh ToolTip Text
            NodeViewModel nodeViewModel = ViewModel.Nodes[nodeID];
            _converter.Result.NodeToolTipGesture.SetToolTipText(nodeViewModel.Caption);
        }

        private void ShowCurvePropertiesGesture_ShowCurvePropertiesRequested(object sender, EventArgs e)
        {
            ShowCurvePropertiesRequested?.Invoke(this, new EventArgs<int>(ViewModel.CurveID));
        }

        private void ChangeNodeTypeGesture_ChangeNodeTypeRequested(object sender, EventArgs e)
        {
            if (ViewModel == null) return;
            ChangeSelectedNodeTypeRequested?.Invoke(this, new EventArgs<int>(ViewModel.CurveID));
        }

        private void ShowNodePropertiesMouseGesture_ShowNodePropertiesRequested(object sender, IDEventArgs e)
        {
            ShowNodePropertiesRequested?.Invoke(this, new EventArgs<int>(e.ID));
        }

        private void ShowNodePropertiesKeyboardGesture_ShowNodePropertiesRequested(object sender, IDEventArgs e)
        {
            ShowNodePropertiesRequested?.Invoke(this, new EventArgs<int>(e.ID));
        }

        // TODO: This logic should be in the presenter.
        private void NodeToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            if (ViewModel == null) return;

            int nodeID = VectorGraphicsTagHelper.GetNodeID(e.Element.Tag);

            NodeViewModel nodeViewModel = ViewModel.Nodes[nodeID];

            e.ToolTipText = nodeViewModel.Caption;
        }

        private void DeleteSelectedNode()
        {
            if (ViewModel == null) return;
            DeleteSelectedNodeRequested?.Invoke(this, new EventArgs<int>(ViewModel.CurveID));
        }

        private void CreateNode()
        {
            if (ViewModel == null) return;
            CreateNodeRequested?.Invoke(this, new EventArgs<int>(ViewModel.CurveID));
        }
    }
}
