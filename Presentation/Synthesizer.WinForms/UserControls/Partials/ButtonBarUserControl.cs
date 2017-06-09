using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class ButtonBarUserControl : UserControl
    {
        public event EventHandler AddClicked;
        public event EventHandler CloseClicked;
        public event EventHandler OpenClicked;
        public event EventHandler PlayClicked;
        public event EventHandler RefreshClicked;
        public event EventHandler RemoveClicked;
        public event EventHandler SaveClicked;

        public ButtonBarUserControl() => InitializeComponent();

        private void TitleBarUserControl_Load(object sender, EventArgs e)
        {
            buttonPlay.Visible = _playButtonVisible;
            buttonSave.Visible = _saveButtonVisible;
            buttonRefresh.Visible = _refreshButtonVisible;
            buttonOpen.Visible = _openButtonVisible;
            buttonAdd.Visible = _addButtonVisible;
            buttonRemove.Visible = _removeButtonVisible;
            buttonClose.Visible = _closeButtonVisible;

            PositionControls();
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _addButtonVisible;
        public bool AddButtonVisible
        {
            get => _addButtonVisible;
            set
            {
                _addButtonVisible = value;
                buttonAdd.Visible = _addButtonVisible;
                PositionControls();
            }
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _closeButtonVisible = true;
        public bool CloseButtonVisible
        {
            get => _closeButtonVisible;
            set
            {
                _closeButtonVisible = value;
                buttonClose.Visible = _closeButtonVisible;
                PositionControls();
            }
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _openButtonVisible;
        public bool OpenButtonVisible
        {
            get => _openButtonVisible;
            set
            {
                _openButtonVisible = value;
                buttonOpen.Visible = _openButtonVisible;
                PositionControls();
            }
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _playButtonVisible;
        public bool PlayButtonVisible
        {
            get => _playButtonVisible;
            set
            {
                _playButtonVisible = value;
                buttonPlay.Visible = _playButtonVisible;
                PositionControls();
            }
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _refreshButtonVisible;
        public bool RefreshButtonVisible
        {
            get => _refreshButtonVisible;
            set
            {
                _refreshButtonVisible = value;
                buttonRefresh.Visible = _refreshButtonVisible;
                PositionControls();
            }
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _removeButtonVisible;
        public bool RemoveButtonVisible
        {
            get => _removeButtonVisible;
            set
            {
                _removeButtonVisible = value;
                buttonRemove.Visible = _removeButtonVisible;
                PositionControls();
            }
        }

        /// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
        private bool _saveButtonVisible;
        public bool SaveButtonVisible
        {
            get => _saveButtonVisible;
            set
            {
                _saveButtonVisible = value;
                buttonSave.Visible = _saveButtonVisible;
                PositionControls();
            }
        }

        // Positioning

        private readonly int _height = StyleHelper.DefaultSpacing + StyleHelper.IconButtonSize + StyleHelper.DefaultSpacing;

        private void PositionControls()
        {
            Width = GetVisibleButtonCount() * StyleHelper.IconButtonSize +
                    (GetVisibleButtonCount() - 1) * StyleHelper.DefaultSpacing;

            Height = _height;

            int x = Width;

            x -= StyleHelper.IconButtonSize;

            var buttonTuplesInReverseOrder = new (Control Control, bool Visible)[]
            {
                (buttonClose, CloseButtonVisible),
                (buttonRemove, RemoveButtonVisible),
                (buttonAdd, AddButtonVisible),
                (buttonOpen, OpenButtonVisible),
                (buttonRefresh, RefreshButtonVisible),
                (buttonSave, SaveButtonVisible),
                (buttonPlay, PlayButtonVisible)
            };

            foreach ((Control Control, bool Visible) buttonTuple in buttonTuplesInReverseOrder)
            {
                if (buttonTuple.Visible)
                {
                    buttonTuple.Control.Location = new Point(x, StyleHelper.DefaultSpacing);
                    buttonTuple.Control.Size = new Size(StyleHelper.IconButtonSize, StyleHelper.IconButtonSize);
                    x -= StyleHelper.DefaultSpacing;
                    x -= StyleHelper.IconButtonSize;
                }
            }
        }

        private void TitleBarUserControl_Resize(object sender, EventArgs e) => PositionControls();

        // Events

        private void buttonPlay_Click(object sender, EventArgs e) => PlayClicked?.Invoke(sender, EventArgs.Empty);
        private void buttonSave_Click(object sender, EventArgs e) => SaveClicked?.Invoke(sender, EventArgs.Empty);
        private void buttonRefresh_Click(object sender, EventArgs e) => RefreshClicked?.Invoke(sender, EventArgs.Empty);
        private void buttonOpen_Click(object sender, EventArgs e) => OpenClicked?.Invoke(sender, EventArgs.Empty);
        private void buttonAdd_Click(object sender, EventArgs e) => AddClicked?.Invoke(sender, EventArgs.Empty);
        private void buttonRemove_Click(object sender, EventArgs e) => RemoveClicked?.Invoke(sender, EventArgs.Empty);
        private void buttonClose_Click(object sender, EventArgs e) => CloseClicked?.Invoke(sender, EventArgs.Empty);

        // Helpers

        private int GetVisibleButtonCount()
        {
            int count = 0;
            if (_addButtonVisible) count++;
            if (_closeButtonVisible) count++;
            if (_openButtonVisible) count++;
            if (_playButtonVisible) count++;
            if (_refreshButtonVisible) count++;
            if (_removeButtonVisible) count++;
            if (_saveButtonVisible) count++;
            return count;
        }
    }
}
