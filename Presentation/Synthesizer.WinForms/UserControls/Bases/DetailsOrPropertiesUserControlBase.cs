using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
    internal class DetailsOrPropertiesUserControlBase : UserControlBase
    {
        private readonly TitleBarUserControl _titleBarUserControl;

        public event EventHandler<EventArgs<int>> AddToInstrumentRequested;
        public event EventHandler<EventArgs<int>> CloseRequested;
        public event EventHandler<EventArgs<int>> LoseFocusRequested;
        public event EventHandler<EventArgs<int>> SaveRequested;
        public event EventHandler<EventArgs<int>> ExpandRequested;
        public event EventHandler<EventArgs<int>> PlayRequested;
        public event EventHandler<EventArgs<int>> RemoveRequested;

        public event EventHandler AddRequested
        {
            add => _titleBarUserControl.AddClicked += value;
            remove => _titleBarUserControl.AddClicked -= value;
        }

        public DetailsOrPropertiesUserControlBase()
        {
            Name = GetType().Name;

            Resize += Base_Resize;
            Leave += Base_Leave;

            _titleBarUserControl = CreateTitleBarUserControl();
            Controls.Add(_titleBarUserControl);
            _titleBarUserControl.AddToInstrumentClicked += _titleBarUserControl_AddToInstrumentClicked;
            _titleBarUserControl.CloseClicked += _titleBarUserControl_CloseClicked;
            _titleBarUserControl.SaveClicked += _titleBarUserControl_SaveClicked;
            _titleBarUserControl.ExpandClicked += _titleBarUserControl_ExpandClicked;
            _titleBarUserControl.PlayClicked += _titleBarUserControl_PlayClicked;
            _titleBarUserControl.RemoveClicked += _titleBarUserControl_RemoveClicked;
        }

        ~DetailsOrPropertiesUserControlBase()
        {
            if (_titleBarUserControl != null)
            {
                _titleBarUserControl.CloseClicked -= _titleBarUserControl_CloseClicked;
                _titleBarUserControl.SaveClicked -= _titleBarUserControl_SaveClicked;
                _titleBarUserControl.ExpandClicked -= _titleBarUserControl_ExpandClicked;
                _titleBarUserControl.PlayClicked -= _titleBarUserControl_PlayClicked;
                _titleBarUserControl.RemoveClicked -= _titleBarUserControl_RemoveClicked;
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

        public string TitleBarText
        {
            get => _titleBarUserControl.Text;
            set => _titleBarUserControl.Text = value;
        }

        public bool AddButtonVisible
        {
            get => _titleBarUserControl.AddButtonVisible;
            set => _titleBarUserControl.AddButtonVisible = value;
        }

        public bool AddToInstrumentButtonVisible
        {
            get => _titleBarUserControl.AddToInstrumentButtonVisible;
            set => _titleBarUserControl.AddToInstrumentButtonVisible = value;
        }

        [DefaultValue(false)]
        public bool ExpandButtonVisible
        {
            get => _titleBarUserControl.ExpandButtonVisible;
            set => _titleBarUserControl.ExpandButtonVisible = value;
        }

        public bool PlayButtonVisible
        {
            get => _titleBarUserControl.PlayButtonVisible;
            set => _titleBarUserControl.PlayButtonVisible = value;
        }

        public bool RemoveButtonVisible
        {
            get => _titleBarUserControl.RemoveButtonVisible;
            set => _titleBarUserControl.RemoveButtonVisible = value;
        }

        public bool RefreshButtonVisible
        {
            get => _titleBarUserControl.RefreshButtonVisible;
            set => _titleBarUserControl.RefreshButtonVisible = value;
        }

        public bool SaveButtonVisible
        {
            get => _titleBarUserControl.SaveButtonVisible;
            set => _titleBarUserControl.SaveButtonVisible = value;
        }

        public bool CloseButtonVisible
        {
            get => _titleBarUserControl.CloseButtonVisible;
            set => _titleBarUserControl.CloseButtonVisible = value;
        }

        public Color TitleBarBackColor
        {
            get => _titleBarUserControl.BackColor;
            set => _titleBarUserControl.BackColor = value;
        }

        public bool TitleLabelVisible
        {
            get => _titleBarUserControl.TitleLabelVisible;
            set => _titleBarUserControl.TitleLabelVisible = value;
        }

        public int TitleBarHeight => _titleBarUserControl.Height;

        protected virtual void PositionControls()
        {
            if (TitleLabelVisible)
            {
                _titleBarUserControl.Width = Width;
            }
            else
            {
                _titleBarUserControl.Width = _titleBarUserControl.ButtonBarWidth;
                _titleBarUserControl.Left = Width - _titleBarUserControl.ButtonBarWidth;
            }
        }

        private void Base_Resize(object sender, EventArgs e) => PositionControls();

        // Binding

        /// <summary> does nothing </summary>
        protected virtual int GetID() => default;

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

        protected void Delete() => RemoveRequested?.Invoke(this, new EventArgs<int>(GetID()));

        // Events


        private void _titleBarUserControl_AddToInstrumentClicked(object sender, EventArgs e) => AddToInstrumentRequested?.Invoke(sender, new EventArgs<int>(GetID()));
        private void _titleBarUserControl_CloseClicked(object sender, EventArgs e) => Close();
        private void _titleBarUserControl_ExpandClicked(object sender, EventArgs e) => ExpandRequested?.Invoke(sender, new EventArgs<int>(GetID()));
        private void _titleBarUserControl_PlayClicked(object sender, EventArgs e) => Play();
        private void _titleBarUserControl_RemoveClicked(object sender, EventArgs e) => Delete();
        private void _titleBarUserControl_SaveClicked(object sender, EventArgs e) => SaveRequested?.Invoke(sender, new EventArgs<int>(GetID()));

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
                Height = StyleHelper.TitleBarHeight,
                Left = 0,
                Top = 0
            };

            return titleBarUserControl;
        }
    }
}
