using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class CurrentInstrumentItemUserControl : UserControl
    {
        private const int SPACING = 1;

        public event EventHandler<EventArgs<int>> RemoveRequested;

        public CurrentInstrumentItemUserControl() => InitializeComponent();

        private void CurrentInstrumentItemUserControl_Load(object sender, EventArgs e)
        {
            SetTitles();
            PositionControls();
        }

        private IDAndName _viewModel;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDAndName ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                ApplyViewModel();
            }
        }


        private void SetTitles()
        {
            toolTip.SetToolTip(buttonRemove, CommonResourceFormatter.Remove);
        }

        private void ApplyViewModel()
        {
            labelName.Text = _viewModel.Name;
            PositionControls();
        }

        private void PositionControls()
        {
            int x = SPACING;
            int y = SPACING;

            labelName.Left = x;
            labelName.Top = y;

            x += labelName.Width;

            buttonRemove.Left = x;
            buttonRemove.Top = y;

            x += buttonRemove.Width + SPACING + SPACING;
            y += buttonRemove.Height + SPACING + SPACING;

            Width = x;
            Height = y;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var e2 = new EventArgs<int>(_viewModel.ID);
            RemoveRequested?.Invoke(this, e2);
        }
    }
}
