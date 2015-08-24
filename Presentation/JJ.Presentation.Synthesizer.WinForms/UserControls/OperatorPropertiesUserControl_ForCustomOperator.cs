using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCustomOperator : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        /// <summary> virtually not nullable </summary>
        private OperatorPropertiesViewModel_ForCustomOperator _viewModel;

        public OperatorPropertiesUserControl_ForCustomOperator()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void OperatorPropertiesUserControl_ForCustomOperator_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperatorPropertiesViewModel_ForCustomOperator ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _viewModel = value;
                ApplyViewModelToControls();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = PropertyDisplayNames.OperatorType + ":";
            labelUnderlyingDocument.Text = PropertyDisplayNames.UnderlyingDocument;

            var labels = new Label[]
            {
                labelName,
                labelOperatorTypeTitle,
                labelUnderlyingDocument
            };

            foreach (Label label in labels)
            {
                toolTip.SetToolTip(label, label.Text);
            }

            labelOperatorTypeValue.Text = PropertyDisplayNames.CustomOperator;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        private void ApplyViewModelToControls()
        {
            textBoxName.Text = _viewModel.Name;

            if (_viewModel.UnderlyingDocument != null)
            {
                comboBoxUnderlyingDocument.SelectedValue = _viewModel.UnderlyingDocument.ID;
            }
            else
            {
                comboBoxUnderlyingDocument.SelectedValue = 0;
            }
        }

        public void SetUnderlyingDocumentLookup(IList<IDAndName> underlyingDocumentLookup)
        {
            // Always refill the MainPatch lookup, so changes to the patch collection are reflected.
            comboBoxUnderlyingDocument.DataSource = null; // Do this or WinForms will not refresh the list.
            comboBoxUnderlyingDocument.DataSource = underlyingDocumentLookup;
            comboBoxUnderlyingDocument.ValueMember = PropertyNames.ID;
            comboBoxUnderlyingDocument.DisplayMember = PropertyNames.Name;
        }

        private void ApplyControlsToViewModel()
        {
            _viewModel.Name = textBoxName.Text;
            _viewModel.UnderlyingDocument = (IDAndName)comboBoxUnderlyingDocument.SelectedItem;
        }

        // Actions

        private void Close()
        {
            if (CloseRequested != null)
            {
                ApplyControlsToViewModel();
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void LoseFocus()
        {
            if (LoseFocusRequested != null)
            {
                ApplyControlsToViewModel();
                LoseFocusRequested(this, EventArgs.Empty);
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void OperatorPropertiesUserControl_ForCustomOperator_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                textBoxName.Focus();
                textBoxName.Select(0, 0);
            }
        }

        // This event goes off when I call OperatorPropertiesUserControl_ForCustomOperator.SetFocus after clicking on a DataGridView,
        // but does not go off when I call OperatorPropertiesUserControl_ForCustomOperator.SetFocus after clicking on a TreeView.
        // Thanks, WinForms...
        private void OperatorPropertiesUserControl_ForCustomOperator_Enter(object sender, EventArgs e)
        {
            textBoxName.Focus();
            textBoxName.Select(0, 0);
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void OperatorPropertiesUserControl_ForCustomOperator_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible) 
            {
                LoseFocus();
            }
        }
    }
}
