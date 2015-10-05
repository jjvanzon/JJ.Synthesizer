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
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Business.Synthesizer.Api;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    public partial class CurveDetailsUserControl : UserControl
    {
        private CurveDetailsViewModel _viewModel;
        private NodeViewModelsToDiagramConverter _converter;

        public CurveDetailsUserControl()
        {
            _converter = new NodeViewModelsToDiagramConverter();

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

            // TODO: This is dirty
            diagramControl.Diagram = diagramControl.Diagram ?? new Diagram();

            _converter.Execute(_viewModel.Entity.Nodes, diagramControl.Diagram);
        }

        private void CurveDetailsUserControl_Resize(object sender, EventArgs e)
        {
            ApplyViewModel();
        }
    }
}
