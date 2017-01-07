using System.Collections.Generic;
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
            AddProperty(_labelOperatorTypeTitle, _labelOperatorTypeValue);
            AddProperty(labelCurve, comboBoxCurve);
            AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
            AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
            AddProperty(_labelName, _textBoxName);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForCurve ViewModel
        {
            get { return (OperatorPropertiesViewModel_ForCurve)base.ViewModel; }
            set { base.ViewModel = value; }
        }

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
