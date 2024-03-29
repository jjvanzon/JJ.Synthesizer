﻿using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_WithCollectionRecalculation 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_WithCollectionRecalculation() => InitializeComponent();

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelRecalculation.Text = ResourceFormatter.CollectionRecalculation;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
            AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
            AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
            AddProperty(labelRecalculation, comboBoxCollectionRecalculation);
            AddProperty(_labelName, _textBoxName);
        }

        // Binding

        public new OperatorPropertiesViewModel_WithCollectionRecalculation ViewModel
        {
            // ReSharper disable once MemberCanBePrivate.Global
            get => (OperatorPropertiesViewModel_WithCollectionRecalculation)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            if (comboBoxCollectionRecalculation.DataSource == null)
            {
                comboBoxCollectionRecalculation.ValueMember = nameof(IDAndName.ID);
                comboBoxCollectionRecalculation.DisplayMember = nameof(IDAndName.Name);
                comboBoxCollectionRecalculation.DataSource = ViewModel.CollectionRecalculationLookup;
            }
            comboBoxCollectionRecalculation.SelectedValue = ViewModel.CollectionRecalculation?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.CollectionRecalculation = (IDAndName)comboBoxCollectionRecalculation.SelectedItem;
        }
    }
}
