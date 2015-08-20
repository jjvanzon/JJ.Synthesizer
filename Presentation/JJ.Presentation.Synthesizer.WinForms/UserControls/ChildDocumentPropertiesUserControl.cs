using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Framework.Data;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class ChildDocumentPropertiesUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        /// <summary> virtually not nullable </summary>
        private ChildDocumentPropertiesViewModel _viewModel;

        public ChildDocumentPropertiesUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ChildDocumentPropertiesViewModel ViewModel
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
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.ChildDocument);
            labelName.Text = CommonTitles.Name;
            labelChildDocumentType.Text = PropertyDisplayNames.ChildDocumentType;

            var labels = new Label[] 
            {
                labelName,
                labelChildDocumentType,
            };

            foreach (Label label in labels)
            {
                toolTip.SetToolTip(label, label.Text);
            }
        }

        private void ApplyViewModelToControls()
        {
            textBoxName.Text = _viewModel.Name;

            if (comboBoxChildDocumentType.DataSource == null)
            {
                comboBoxChildDocumentType.DataSource = _viewModel.ChildDocumentTypeLookup;
                comboBoxChildDocumentType.ValueMember = PropertyNames.ID;
                comboBoxChildDocumentType.DisplayMember = PropertyNames.Name;
            }
            comboBoxChildDocumentType.SelectedValue = _viewModel.ChildDocumentType.ID;
        }

        private void ApplyControlsToViewModel()
        {
            _viewModel.Name = textBoxName.Text;
            _viewModel.ChildDocumentType.ID = (int)comboBoxChildDocumentType.SelectedValue;
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

        private void ChildDocumentPropertiesUserControl_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                textBoxName.Focus();
            }
        }

        // This event goes off when I call DocumentPropertiesUserControl.SetFocus after clicking on a DataGridView,
        // but does not go off when I call DocumentPropertiesUserControl.SetFocus after clicking on a TreeView.
        // Thanks, WinForms...
        private void ChildDocumentPropertiesUserControl_Enter(object sender, EventArgs e)
        {
            textBoxName.Focus();
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void ChildDocumentPropertiesUserControl_Leave(object sender, EventArgs e)
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
