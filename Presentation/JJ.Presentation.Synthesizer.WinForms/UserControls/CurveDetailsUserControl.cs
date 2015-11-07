using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Framework.Mathematics;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveDetailsUserControl : UserControl
    {
        public event EventHandler CreateNodeRequested;
        public event EventHandler DeleteNodeRequested;
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;
        public event EventHandler<Int32EventArgs> SelectNodeRequested;
        public event EventHandler<MoveEntityEventArgs> MoveNodeRequested;
        public event EventHandler ShowCurvePropertiesRequested;
        public event EventHandler ChangeNodeTypeRequested;
        public event EventHandler<Int32EventArgs> ShowNodePropertiesRequested;

        private CurveDetailsViewModel _viewModel;
        private CurveDetailsViewModelToDiagramConverter _converter;

        public CurveDetailsUserControl()
        {
            _converter = new CurveDetailsViewModelToDiagramConverter(
                SystemInformation.DoubleClickTime,
                SystemInformation.DoubleClickSize.Width);

            _converter.Result.KeyDownGesture.KeyDown += Diagram_KeyDown;
            _converter.Result.SelectNodeGesture.SelectNodeRequested += SelectNodeGesture_NodeSelected;
            _converter.Result.MoveNodeGesture.Moved += MoveNodeGesture_Moved;
            _converter.Result.MoveNodeGesture.Moving += MoveNodeGesture_Moving;
            _converter.Result.ShowCurvePropertiesGesture.ShowCurvePropertiesRequested += ShowCurvePropertiesGesture_ShowCurvePropertiesRequested;
            _converter.Result.ChangeNodeTypeGesture.ChangeNodeTypeRequested += ChangeNodeTypeGesture_ChangeNodeTypeRequested;
            _converter.Result.ShowNodePropertiesGesture.ShowNodePropertiesRequested += ShowNodePropertiesGesture_ShowNodePropertiesRequested;
            _converter.Result.ShowSelectedNodePropertiesGesture.ShowSelectedNodePropertiesRequested += ShowSelectedNodePropertiesGesture_ShowSelectedNodePropertiesRequested;

            InitializeComponent();
            SetTitles();
        }

        private void CurveDetailsUserControl_Load(object sender, EventArgs e)
        {
            diagramControl.Diagram = _converter.Result.Diagram;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CurveDetailsViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModel();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Curve;
        }

        private void ApplyViewModel()
        {
            if (_viewModel == null) return;

            _converter.Execute(_viewModel);

            diagramControl.Refresh();
        }

        // Events

        private void CurveDetailsUserControl_Resize(object sender, EventArgs e)
        {
            ApplyViewModel();
        }

        private void CurveDetailsUserControl_Paint(object sender, PaintEventArgs e)
        {
            ApplyViewModel();
        }

        private void titleBarUserControl_AddClicked(object sender, EventArgs e)
        {
            CreateNode();
        }

        private void titleBarUserControl_RemoveClicked(object sender, EventArgs e)
        {
            DeleteNode();
        }

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void Diagram_KeyDown(object sender, JJ.Framework.Presentation.VectorGraphics.EventArg.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case KeyCodeEnum.Insert:
                    CreateNode();
                    break;

                case KeyCodeEnum.Delete:
                    DeleteNode();
                    break;
            }
        }

        private void SelectNodeGesture_NodeSelected(object sender, ElementEventArgs e)
        {
            if (SelectNodeRequested != null)
            {
                int nodeID = (int)e.Element.Tag;
                SelectNodeRequested(this, new Int32EventArgs(nodeID));
            }
        }

        private void MoveNodeGesture_Moving(object sender, ElementEventArgs e)
        {
            if (MustDoMove(e.Element))
            {
                DoMoveNode(e);
            }
        }

        private bool MustDoMove(Element element)
        {
            // HACK:
            // At the moment, moving a node outside of bounds does not work well,
            // so do not recalculate while draggin then.

            int nodeID = (int)element.Tag;

            // This looks if it is the first or last node.
            // Even when the node is within the diagram bounds
            // moving the last node 'inwards' would change the scaling.
            IList<NodeViewModel> sortedViewModels1 = _viewModel.Entity.Nodes.OrderBy(x => x.Time).ToList();
            IList<NodeViewModel> sortedViewModels2 = _viewModel.Entity.Nodes.OrderBy(x => x.Value).ToList();
            NodeViewModel leftViewModel = sortedViewModels1.First();
            NodeViewModel rightViewModel = sortedViewModels1.Last();
            NodeViewModel bottomViewModel = sortedViewModels2.First();
            NodeViewModel topViewModel = sortedViewModels2.Last();

            bool isAtBounds = nodeID == leftViewModel.ID ||
                              nodeID == rightViewModel.ID ||
                              nodeID == bottomViewModel.ID ||
                              nodeID == topViewModel.ID;

            // This looks it the position is in view. If you are to fast to move the first or last node,
            // it would scale if I do not do a bounds check. I am actually not sure why...
            bool isInRange = Geometry.IsInRectangle(
                element.XInPixels, element.YInPixels,
                0, 0, element.Diagram.WidthInPixels, element.Diagram.HeightInPixels);

            bool mustDoMove = !isAtBounds && isInRange;

            return mustDoMove;
        }

        private void MoveNodeGesture_Moved(object sender, ElementEventArgs e)
        {
            DoMoveNode(e);
        }

        private void DoMoveNode(ElementEventArgs e)
        {
            if (MoveNodeRequested != null)
            {
                int nodeID = (int)e.Element.Tag;

                Rectangle rectangle = (Rectangle)e.Element;

                float x = rectangle.AbsoluteX + rectangle.Width / 2;
                float y = rectangle.AbsoluteY + rectangle.Height / 2;

                MoveNodeRequested(this, new MoveEntityEventArgs(nodeID, x, y));

                ApplyViewModel();
            }
        }

        private void ShowCurvePropertiesGesture_ShowCurvePropertiesRequested(object sender, EventArgs e)
        {
            if (ShowCurvePropertiesRequested != null)
            {
                ShowCurvePropertiesRequested(this, EventArgs.Empty);
            }
        }

        private void ChangeNodeTypeGesture_ChangeNodeTypeRequested(object sender, EventArgs e)
        {
            if (ChangeNodeTypeRequested != null)
            {
                ChangeNodeTypeRequested(this, EventArgs.Empty);
            }
        }

        private void ShowNodePropertiesGesture_ShowNodePropertiesRequested(object sender, NodeIDEventArgs e)
        {
            if (ShowNodePropertiesRequested != null)
            {
                ShowNodePropertiesRequested(this, new Int32EventArgs(e.NodeID));
            }
        }

        private void ShowSelectedNodePropertiesGesture_ShowSelectedNodePropertiesRequested(object sender, EventArgs e)
        {
            if (ShowNodePropertiesRequested != null)
            {
                if (ViewModel == null) throw new NullException(() => ViewModel);

                if (ViewModel.SelectedNodeID.HasValue)
                {
                    int nodeID = ViewModel.SelectedNodeID.Value;
                    ShowNodePropertiesRequested(this, new Int32EventArgs(nodeID));
                }
            }
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void CurveDetailsUserControl_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible)
            {
                LoseFocus();
            }
        }

        // Actions

        private void CreateNode()
        {
            if (CreateNodeRequested != null)
            {
                CreateNodeRequested(this, EventArgs.Empty);
            }
        }

        private void DeleteNode()
        {
            if (DeleteNodeRequested != null)
            {
                DeleteNodeRequested(this, EventArgs.Empty);
            }
        }

        private void LoseFocus()
        {
            if (LoseFocusRequested != null)
            {
                LoseFocusRequested(this, EventArgs.Empty);
            }
        }
    }
}
