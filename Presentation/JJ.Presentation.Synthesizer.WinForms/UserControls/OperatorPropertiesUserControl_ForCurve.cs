using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCurve
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForCurve()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelCurve.Text = PropertyDisplayNames.Curve;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelCurve, comboBoxCurve);
            AddProperty(labelStandardDimension, comboBoxStandardDimension);
            AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_ForCurve ViewModel => (OperatorPropertiesViewModel_ForCurve)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            comboBoxCurve.SelectedValue = ViewModel.Curve?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Curve = (IDAndName)comboBoxCurve.SelectedItem;
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
    }
}
