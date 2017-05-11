using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
    internal class DetailsOrPropertiesUserControlBase : UserControlBase
    {
        private const int TITLE_BAR_HEIGHT = 27;

        private readonly TitleBarUserControl _titleBarUserControl;

        public event EventHandler<EventArgs<int>> CloseRequested;
        public event EventHandler<EventArgs<int>> LoseFocusRequested;
        public event EventHandler<EventArgs<int>> SaveRequested;
        public event EventHandler<EventArgs<int>> OpenExternallyRequested;
        public event EventHandler<EventArgs<int>> PlayRequested;

        public event EventHandler AddRequested
        {
            add => _titleBarUserControl.AddClicked += value;
            remove => _titleBarUserControl.AddClicked -= value;
        }

        public event EventHandler RemoveRequested
        {
            add => _titleBarUserControl.RemoveClicked += value;
            remove => _titleBarUserControl.RemoveClicked -= value;
        }

        public DetailsOrPropertiesUserControlBase()
        {
            Name = GetType().Name;

            Resize += Base_Resize;
            Leave += Base_Leave;

            _titleBarUserControl = CreateTitleBarUserControl();
            Controls.Add(_titleBarUserControl);
            _titleBarUserControl.CloseClicked += _titleBarUserControl_CloseClicked;
            _titleBarUserControl.SaveClicked += _titleBarUserControl_SaveClicked;
            _titleBarUserControl.OpenClicked += _titleBarUserControl_OpenClicked;
            _titleBarUserControl.PlayClicked += _titleBarUserControl_PlayClicked;
        }

        ~DetailsOrPropertiesUserControlBase()
        {
            if (_titleBarUserControl != null)
            {
                _titleBarUserControl.CloseClicked -= _titleBarUserControl_CloseClicked;
                _titleBarUserControl.SaveClicked -= _titleBarUserControl_SaveClicked;
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

        protected virtual void ApplyStyling() => BackColor = SystemColors.ButtonFace;

        /// <summary> does nothing </summary>
        protected virtual void SetTitles()
        { }

        public int TitleBarHeight => TITLE_BAR_HEIGHT;

        public string TitleBarText
        {
            get => _titleBarUserControl.Text;
            set => _titleBarUserControl.Text = value;
        }

        public bool PlayButtonVisible
        {
            get => _titleBarUserControl.PlayButtonVisible;
            set => _titleBarUserControl.PlayButtonVisible = value;
        }

        public bool SaveButtonVisible
        {
            get => _titleBarUserControl.SaveButtonVisible;
            set => _titleBarUserControl.SaveButtonVisible = value;
        }

        public bool RefreshButtonVisible
        {
            get => _titleBarUserControl.RefreshButtonVisible;
            set => _titleBarUserControl.RefreshButtonVisible = value;
        }

        public bool OpenExternallyButtonVisible
        {
            get => _titleBarUserControl.OpenButtonVisible;
            set => _titleBarUserControl.OpenButtonVisible = value;
        }

        public bool AddButtonVisible
        {
            get => _titleBarUserControl.AddButtonVisible;
            set => _titleBarUserControl.AddButtonVisible = value;
        }

        public bool RemoveButtonVisible
        {
            get => _titleBarUserControl.RemoveButtonVisible;
            set => _titleBarUserControl.RemoveButtonVisible = value;
        }

        public bool CloseButtonVisible
        {
            get => _titleBarUserControl.CloseButtonVisible;
            set => _titleBarUserControl.CloseButtonVisible = value;
        }

        protected virtual void PositionControls() => _titleBarUserControl.Width = Width;

        private void Base_Resize(object sender, EventArgs e) => PositionControls();

        // Binding

        /// <summary> does nothing </summary>
        protected virtual int GetID() => default(int);

        /// <summary> does nothing </summary>
        protected virtual void ApplyControlsToViewModel()
        { }

        // Actions

        private void Close()
        {
            if (ViewModel == null) return;

            ApplyControlsToViewModel();

            CloseRequested?.Invoke(this, new EventArgs<int>(GetID()));
        }

        private void LoseFocus()
        {
            if (ViewModel == null) return;

            ApplyControlsToViewModel();

            LoseFocusRequested?.Invoke(this, new EventArgs<int>(GetID()));
        }

        private void Play()
        {
            if (ViewModel == null) return;

            ApplyControlsToViewModel();

            PlayRequested?.Invoke(this, new EventArgs<int>(GetID()));
        }

        // Events

        private void _titleBarUserControl_CloseClicked(object sender, EventArgs e) => Close();
        private void _titleBarUserControl_SaveClicked(object sender, EventArgs e) => SaveRequested?.Invoke(sender, new EventArgs<int>(GetID()));
        private void _titleBarUserControl_PlayClicked(object sender, EventArgs e) => Play();
        private void _titleBarUserControl_OpenClicked(object sender, EventArgs e) => OpenExternallyRequested?.Invoke(sender, new EventArgs<int>(GetID()));

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
                PlayButtonVisible = false,
                SaveButtonVisible = false,
                Margin = new Padding(0, 0, 0, 0),
                Height = TITLE_BAR_HEIGHT,
                Left = 0,
                Top = 0
            };

            return titleBarUserControl;
        }
    }
}
