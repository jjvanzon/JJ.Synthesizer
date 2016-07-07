using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForNumber 
        : OperatorPropertiesUserControl_ForNumber_NotDesignable
    {
        public OperatorPropertiesUserControl_ForNumber()
        {
            InitializeComponent();

            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelNumber.Text = PropertyDisplayNames.Number;
            labelOperatorTypeValue.Text = PropertyDisplayNames.Number;
        }

        protected override void PositionControls()
        {
            base.PositionControls();

            tableLayoutPanelProperties.Left = 0;
            tableLayoutPanelProperties.Top = TitleBarHeight;
            tableLayoutPanelProperties.Width = Width;
            tableLayoutPanelProperties.Height = Height - TitleBarHeight;
        }

        protected override void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            textBoxName.Text = ViewModel.Name;
            textBoxNumber.Text = ViewModel.Number;
        }

        protected override void ApplyControlsToViewModel()
        {
            if (ViewModel == null) return;

            ViewModel.Name = textBoxName.Text;
            ViewModel.Number = textBoxNumber.Text;
        }
    }

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class OperatorPropertiesUserControl_ForNumber_NotDesignable
        : OperatorPropertiesUserControlBase<OperatorPropertiesViewModel_ForNumber>
    {
        protected override void ApplyControlsToViewModel()
        {
            throw new NotImplementedException();
        }

        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
