using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.VectorGraphics;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Enums;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveDetailsUserControl : UserControl
    {
        public event EventHandler CreateNodeRequested;
        public event EventHandler DeleteNodeRequested;
        public event EventHandler CloseRequested;

        private CurveDetailsViewModel _viewModel;
        private NodeViewModelsToDiagramConverter _converter;
        private static bool _mustShowInvisibleElements = GetMustShowInvisibleElements();

        private KeyDownGesture _keyDownGesture;

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
            _converter = new NodeViewModelsToDiagramConverter(_mustShowInvisibleElements);

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

            if (diagramControl.Diagram == null) // TODO: This is dirty.
            {
                diagramControl.Diagram = new Diagram();
                _keyDownGesture = new KeyDownGesture();
                _keyDownGesture.KeyDown += Diagram_KeyDown;
                diagramControl.Diagram.Gestures.Add(_keyDownGesture);
            }

            _converter.Execute(_viewModel.Entity.Nodes, diagramControl.Diagram);

            diagramControl.Refresh();
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
    }
}
