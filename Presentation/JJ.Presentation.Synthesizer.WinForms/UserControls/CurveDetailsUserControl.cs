using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Configuration;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using JJ.Framework.Reflection.Exceptions;

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
        private CurveDetailsViewModelToDiagramConverterResult _converterResult;
        private static bool _mustShowInvisibleElements = GetMustShowInvisibleElements();

        private static bool GetMustShowInvisibleElements()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                var config = CustomConfigurationManager.GetSection<ConfigurationSection>();
                return config.Testing.MustShowInvisibleElements;
            }
            else
            {
                return false;
            }
        }

        public CurveDetailsUserControl()
        {
            _converter = new CurveDetailsViewModelToDiagramConverter(
                SystemInformation.DoubleClickTime,
                SystemInformation.DoubleClickSize.Width,
                _mustShowInvisibleElements);

            InitializeComponent();
            SetTitles();
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

            if (_converterResult != null)
            {
                _converterResult.KeyDownGesture.KeyDown -= Diagram_KeyDown;
                _converterResult.SelectNodeGesture.SelectNodeRequested -= Diagram_NodeSelected;
                _converterResult.MoveNodeGesture.Moved -= MoveNodeGesture_Moved;
                _converterResult.ShowCurvePropertiesGesture.ShowCurvePropertiesRequested -= ShowCurvePropertiesGesture_ShowCurvePropertiesRequested;
                _converterResult.ChangeNodeTypeGesture.ChangeNodeTypeRequested -= ChangeNodeTypeGesture_ChangeNodeTypeRequested;
                _converterResult.ShowNodePropertiesGesture.ShowNodePropertiesRequested -= ShowNodePropertiesGesture_ShowNodePropertiesRequested;
                _converterResult.ShowSelectedNodePropertiesGesture.ShowSelectedNodePropertiesRequested -= ShowSelectedNodePropertiesGesture_ShowSelectedNodePropertiesRequested;
            }

            _converterResult = _converter.Execute(_viewModel, _converterResult);

            _converterResult.KeyDownGesture.KeyDown += Diagram_KeyDown;
            _converterResult.SelectNodeGesture.SelectNodeRequested += Diagram_NodeSelected;
            _converterResult.MoveNodeGesture.Moved += MoveNodeGesture_Moved;
            _converterResult.ShowCurvePropertiesGesture.ShowCurvePropertiesRequested += ShowCurvePropertiesGesture_ShowCurvePropertiesRequested;
            _converterResult.ChangeNodeTypeGesture.ChangeNodeTypeRequested += ChangeNodeTypeGesture_ChangeNodeTypeRequested;
            _converterResult.ShowNodePropertiesGesture.ShowNodePropertiesRequested += ShowNodePropertiesGesture_ShowNodePropertiesRequested;
            _converterResult.ShowSelectedNodePropertiesGesture.ShowSelectedNodePropertiesRequested += ShowSelectedNodePropertiesGesture_ShowSelectedNodePropertiesRequested;

            diagramControl.Diagram = _converterResult.Diagram;

            // TODO: Now I can no longer test why not re-assigning the Diagram property
            // resulted in the show of the Curvedetails not properly scaling the diagram,
            // and resize also does not scale the diagram in time, when it is a mamimize or restore.
            // If that was even the cause.

            // Temporarily enable again. It might just be the cause of the missing refreshes.
            // Refresh made ChangeNodeType gesture go off twice???
            //diagramControl.Refresh();
        }

        // Events

        private void CurveDetailsUserControl_Resize(object sender, EventArgs e)
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

        private void Diagram_NodeSelected(object sender, ElementEventArgs e)
        {
            if (SelectNodeRequested != null)
            {
                int nodeID = (int)e.Element.Tag;
                SelectNodeRequested(this, new Int32EventArgs(nodeID));
            }
        }

        private void MoveNodeGesture_Moved(object sender, ElementEventArgs e)
        {
            if (MoveNodeRequested != null)
            {
                int nodeID = (int)e.Element.Tag;

                Rectangle rectangle = (Rectangle)e.Element;

                float x = rectangle.AbsoluteX + rectangle.Width / 2;
                float y = rectangle.AbsoluteY + rectangle.Height / 2;

                MoveNodeRequested(this, new MoveEntityEventArgs(nodeID, x, y));
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
