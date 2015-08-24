using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForPatchOutlet : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        /// <summary> virtually not nullable </summary>
        private OperatorPropertiesViewModel_ForPatchOutlet _viewModel;

        public OperatorPropertiesUserControl_ForPatchOutlet()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void OperatorPropertiesUserControl_ForPatchOutlet_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperatorPropertiesViewModel_ForPatchOutlet ViewModel
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
            labelSortOrder.Text = PropertyDisplayNames.SortOrder;

            labelOperatorTypeValue.Text = PropertyDisplayNames.PatchOutlet;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        private void ApplyViewModelToControls()
        {
            textBoxName.Text = _viewModel.Name;
            numericUpDownSortOrder.Value = _viewModel.SortOrder;
        }

        private void ApplyControlsToViewModel()
        {
            _viewModel.Name = textBoxName.Text;
            _viewModel.SortOrder = (int)numericUpDownSortOrder.Value;
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

        private void OperatorPropertiesUserControl_ForPatchOutlet_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                textBoxName.Focus();
                textBoxName.Select(0, 0);
            }
        }

        // This event goes off when I call DocumentPropertiesUserControl.SetFocus after clicking on a DataGridView,
        // but does not go off when I call DocumentPropertiesUserControl.SetFocus after clicking on a TreeView.
        // Thanks, WinForms...
        private void OperatorPropertiesUserControl_ForPatchOutlet_Enter(object sender, EventArgs e)
        {
            textBoxName.Focus();
            textBoxName.Select(0, 0);
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void OperatorPropertiesUserControl_ForPatchOutlet_Leave(object sender, EventArgs e)
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
