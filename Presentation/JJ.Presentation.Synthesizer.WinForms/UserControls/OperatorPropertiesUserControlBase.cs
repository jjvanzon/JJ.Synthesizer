using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Framework.Presentation.WinForms.Extensions;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal abstract class OperatorPropertiesUserControlBase<TViewModel> : UserControlBase<TViewModel>
         where TViewModel : OperatorPropertiesViewModelBase
    {
        private const int DEFAULT_TITLE_BAR_HEIGHT = 27;

        private readonly TitleBarUserControl _titleBarUserControl;

        public event EventHandler<Int32EventArgs> CloseRequested;
        public event EventHandler<Int32EventArgs> LoseFocusRequested;

        public OperatorPropertiesUserControlBase()
        {
            Name = GetType().Name;

            Load += Base_Load;
            Leave += Base_Leave;
            Resize += Base_Resize;

            _titleBarUserControl = CreateTitleBarUserControl();
            _titleBarUserControl.CloseClicked += _titleBarUserControl_CloseClicked;
            Controls.Add(_titleBarUserControl);
        }

        ~OperatorPropertiesUserControlBase()
        {
            Load -= Base_Load;
            Leave -= Base_Leave;

            if (_titleBarUserControl != null)
            {
                _titleBarUserControl.CloseClicked -= _titleBarUserControl_CloseClicked;
            }
        }

        private void Base_Load(object sender, EventArgs e)
        {
            this.AutomaticallyAssignTabIndexes();

            SetTitles();
            ApplyStyling();
            PositionControls();
        }

        private TitleBarUserControl CreateTitleBarUserControl()
        {
            var control = new TitleBarUserControl
            {
                Name = nameof(_titleBarUserControl),
                BackColor = SystemColors.Control,
                CloseButtonVisible = true,
                RemoveButtonVisible = false,
                AddButtonVisible = false,
                Margin = new Padding(0, 0, 0, 0),
                Height = DEFAULT_TITLE_BAR_HEIGHT,
                Left = 0,
                Top = 0
            };

            return control;
        }

        // Gui

        public string TitleBarText
        {
            get { return _titleBarUserControl.Text; }
            set { _titleBarUserControl.Text = value; }
        }

        public int TitleBarHeight
        {
            get { return DEFAULT_TITLE_BAR_HEIGHT; }
        }

        protected virtual void SetTitles()
        { }

        protected virtual void ApplyStyling()
        {
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.ButtonFace;
        }

        protected virtual void PositionControls()
        {
            _titleBarUserControl.Width = Width;
        }

        protected virtual void ApplyControlsToViewModel()
        { }

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

        private void _titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void Base_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible) 
            {
                LoseFocus();
            }
        }

        private void Base_Resize(object sender, EventArgs e)
        {
            PositionControls();
        }
    }
}
