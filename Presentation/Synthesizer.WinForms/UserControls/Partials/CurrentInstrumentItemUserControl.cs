using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class CurrentInstrumentItemUserControl : UserControl
    {
        private const int SPACING = 1;

        public event EventHandler<EventArgs<int>> MoveBackwardRequested;
        public event EventHandler<EventArgs<int>> MoveForwardRequested;
        public event EventHandler<EventArgs<int>> RemoveRequested;

        public CurrentInstrumentItemUserControl() => InitializeComponent();

        private void CurrentInstrumentItemUserControl_Load(object sender, EventArgs e)
        {
            buttonMoveBackward.Visible = _moveBackwardButtonVisible;
            buttonMoveForward.Visible = _moveForwardButtonVisible;
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
            toolTip.SetToolTip(buttonMoveBackward, CommonResourceFormatter.Move);
            toolTip.SetToolTip(buttonMoveForward, CommonResourceFormatter.Move);
            toolTip.SetToolTip(buttonRemove, CommonResourceFormatter.Remove);
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _moveBackwardButtonVisible = true;
        [DefaultValue(true)]
        public bool MoveBackwardButtonVisible
        {
            get => _moveBackwardButtonVisible;
            set
            {
                _moveBackwardButtonVisible = value;
                buttonMoveBackward.Visible = _moveBackwardButtonVisible;
                PositionControls();
            }
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _moveForwardButtonVisible = true;
        [DefaultValue(true)]
        public bool MoveForwardButtonVisible
        {
            get => _moveForwardButtonVisible;
            set
            {
                _moveForwardButtonVisible = value;
                buttonMoveForward.Visible = _moveForwardButtonVisible;
                PositionControls();
            }
        }

        private void ApplyViewModel()
        {
            labelName.Text = _viewModel.Name;
            PositionControls();
        }

        private void PositionControls()
        {
            int buttonWidth = buttonMoveBackward.Width;

            int x = SPACING;
            int y = SPACING;

            labelName.Location = new Point(x, y);
            //x += labelName.Width + SPACING;
            x += labelName.Width;

            buttonMoveBackward.Location = new Point(x, y);
            x += buttonWidth + SPACING;

            buttonMoveForward.Location = new Point(x, y);
            x += buttonWidth + SPACING;

            buttonRemove.Location = new Point(x, y);
            x += buttonWidth + SPACING;

            x += SPACING;

            Width = x;
        }

        private void buttonMoveBackward_Click(object sender, EventArgs e) => MoveBackwardRequested(this, new EventArgs<int>(_viewModel.ID));
        private void buttonMoveForward_Click(object sender, EventArgs e) => MoveForwardRequested(this, new EventArgs<int>(_viewModel.ID));
        private void buttonRemove_Click(object sender, EventArgs e) => RemoveRequested(this, new EventArgs<int>(_viewModel.ID));
    }
}
