using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCurve
        : OperatorPropertiesUserControl_ForCurve_NotDesignable
    {
        public event EventHandler<Int32EventArgs> CloseRequested;
        public event EventHandler<Int32EventArgs> LoseFocusRequested;

        public OperatorPropertiesUserControl_ForCurve()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void OperatorPropertiesUserControl_ForCurve_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelCurve.Text = PropertyDisplayNames.Curve;
            labelDimension.Text = PropertyDisplayNames.Dimension;
            labelOperatorTypeValue.Text = PropertyDisplayNames.Curve;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            textBoxName.Text = ViewModel.Name;

            if (ViewModel.Curve != null)
            {
                comboBoxCurve.SelectedValue = ViewModel.Curve.ID;
            }
            else
            {
                comboBoxCurve.SelectedValue = 0;
            }

            // Dimension
            if (comboBoxDimension.DataSource == null)
            {
                comboBoxDimension.ValueMember = PropertyNames.ID;
                comboBoxDimension.DisplayMember = PropertyNames.Name;
                comboBoxDimension.DataSource = ViewModel.DimensionLookup;
            }
            if (ViewModel.Dimension != null)
            {
                comboBoxDimension.SelectedValue = ViewModel.Dimension.ID;
            }
            else
            {
                comboBoxDimension.SelectedValue = 0;
            }
        }

        public void SetCurveLookup(IList<IDAndName> curveLookup)
        {
            // Always refill the lookup, so changes to the curve collection are reflected.
            int? selectedID = TryGetSelectedCurveID();
            comboBoxCurve.DataSource = null; // Do this or WinForms will not refresh the list.
            comboBoxCurve.ValueMember = PropertyNames.ID;
            comboBoxCurve.DisplayMember = PropertyNames.Name;
            comboBoxCurve.DataSource = curveLookup;
            if (selectedID != null)
            {
                comboBoxCurve.SelectedValue = selectedID;
            }
        }

        private int? TryGetSelectedCurveID()
        {
            if (comboBoxCurve.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxCurve.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }

        private void ApplyControlsToViewModel()
        {
            if (ViewModel == null) return;

            ViewModel.Name = textBoxName.Text;
            ViewModel.Curve = (IDAndName)comboBoxCurve.SelectedItem;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }

        // Actions

        private void Close()
        {
            if (ViewModel == null) return;
            ApplyControlsToViewModel();
            CloseRequested?.Invoke(this, new Int32EventArgs(ViewModel.ID));
        }

        private void LoseFocus()
        {
            if (ViewModel == null) return;
            ApplyControlsToViewModel();
            LoseFocusRequested?.Invoke(this, new Int32EventArgs(ViewModel.ID));
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void OperatorPropertiesUserControl_ForCurve_Leave(object sender, EventArgs e)
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

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class OperatorPropertiesUserControl_ForCurve_NotDesignable
        : UserControlBase<OperatorPropertiesViewModel_ForCurve>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
