using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurrentPatchItemUserControl : UserControl
    {
        private const int SPACING = 1;

        public event EventHandler<Int32EventArgs> RemoveRequested;

        private CurrentPatchItemViewModel _viewModel;

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
        public CurrentPatchItemViewModel ViewModel
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
                var e2 = new Int32EventArgs(_viewModel.ChildDocumentID);
                RemoveRequested(this, e2);
            }
        }
    }
}
