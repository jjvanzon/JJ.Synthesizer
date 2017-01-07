using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class CurrentPatchItemUserControl : UserControl
    {
        private const int SPACING = 1;

        public event EventHandler<EventArgs<int>> RemoveRequested;

        private IDAndName _viewModel;

        public CurrentPatchItemUserControl()
        {
            InitializeComponent();
        }

        private void CurrentPatchItemUserControl_Load(object sender, EventArgs e)
        {
            PositionControls();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDAndName ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModel();
            }
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
            if (RemoveRequested != null)
            {
                var e2 = new EventArgs<int>(_viewModel.ID);
                RemoveRequested(this, e2);
            }
        }
    }
}
