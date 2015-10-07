using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Framework.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveDetailsUserControl : UserControl
    {
        public event EventHandler CreateNodeRequested;
        public event EventHandler DeleteNodeRequested;
        public event EventHandler CloseRequested;
        public event EventHandler<Int32EventArgs> SelectNodeRequested;

        private CurveDetailsViewModel _viewModel;
        private CurveDetailsViewModelToDiagramConverter _converter;
        private NodeViewModelsToDiagramConverterResult _converterResult;
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
            _converter = new CurveDetailsViewModelToDiagramConverter(_mustShowInvisibleElements);

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
            }

            _converterResult = _converter.Execute(_viewModel, _converterResult);

            _converterResult.KeyDownGesture.KeyDown += Diagram_KeyDown;
            _converterResult.SelectNodeGesture.SelectNodeRequested += Diagram_NodeSelected;

            diagramControl.Diagram = _converterResult.Diagram;

            // TODO: Now I can no longer test why not re-assigning the Diagram property
            // resulted in the show of the Curvedetails not properly scaling the diagram,
            // and resize also does not scale the diagram in time, when it is a mamimize or restore.
            // If that was even the cause.
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
            Close();
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
            int nodeID = (int)e.Element.Tag;
            SelectNode(nodeID);
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

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void SelectNode(int nodeID)
        {
            if (SelectNodeRequested != null)
            {
                SelectNodeRequested(this, new Int32EventArgs(nodeID));
            }
        }
    }
}
