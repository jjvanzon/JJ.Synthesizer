using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal class DetailsOrPropertiesUserControlBase : UserControlBase
    {
        private const int TITLE_BAR_HEIGHT = 27;

        private readonly TitleBarUserControl _titleBarUserControl;

        public event EventHandler<Int32EventArgs> CloseRequested;
        public event EventHandler<Int32EventArgs> LoseFocusRequested;

        public DetailsOrPropertiesUserControlBase()
        {
            Name = GetType().Name;

            Resize += Base_Resize;
            Leave += Base_Leave;

            _titleBarUserControl = CreateTitleBarUserControl();
            Controls.Add(_titleBarUserControl);
            _titleBarUserControl.CloseClicked += _titleBarUserControl_CloseClicked;
        }

        ~DetailsOrPropertiesUserControlBase()
        {
            if (_titleBarUserControl != null)
            {
                _titleBarUserControl.CloseClicked -= _titleBarUserControl_CloseClicked;
            }
        }

        /// <summary> Executes SetTiltes, ApplyStyling, PositionControls and AutomaticallyAssignTabIndexes. </summary>
        protected override void OnLoad(EventArgs e)
        {
            SetTitles();
            ApplyStyling();
            PositionControls();

            this.AutomaticallyAssignTabIndexes();

            base.OnLoad(e);
        }

        // Gui

        protected virtual void ApplyStyling()
        {
            BackColor = SystemColors.ButtonFace;
        }

        /// <summary> does nothing </summary>
        protected virtual void SetTitles()
        { }

        public int TitleBarHeight
        {
            get { return TITLE_BAR_HEIGHT; }
        }

        public string TitleBarText
        {
            get { return _titleBarUserControl.Text; }
            set { _titleBarUserControl.Text = value; }
        }

        protected virtual void PositionControls()
        {
            _titleBarUserControl.Width = Width;
        }

        private void Base_Resize(object sender, EventArgs e)
        {
            PositionControls();
        }

        // Binding

        /// <summary> does nothing </summary>
        protected virtual int GetID()
        {
            return default(int);
        }

        /// <summary> does nothing </summary>
        protected virtual void ApplyControlsToViewModel()
        { }

        // Actions

        private void Close()
        {
            if (ViewModel == null) return;

            ApplyControlsToViewModel();

            CloseRequested?.Invoke(this, new Int32EventArgs(GetID()));
        }

        private void LoseFocus()
        {
            if (ViewModel == null) return;

            ApplyControlsToViewModel();

            LoseFocusRequested?.Invoke(this, new Int32EventArgs(GetID()));
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

        // Create Controls

        private TitleBarUserControl CreateTitleBarUserControl()
        {
            var titleBarUserControl = new TitleBarUserControl
            {
                Name = nameof(_titleBarUserControl),
                BackColor = SystemColors.Control,
                CloseButtonVisible = true,
                RemoveButtonVisible = false,
                AddButtonVisible = false,
                Margin = new Padding(0, 0, 0, 0),
                Height = TITLE_BAR_HEIGHT,
                Left = 0,
                Top = 0
            };

            return titleBarUserControl;
        }
    }
}
