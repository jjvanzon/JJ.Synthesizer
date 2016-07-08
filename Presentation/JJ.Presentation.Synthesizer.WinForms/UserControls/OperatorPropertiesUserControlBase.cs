using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Reflection.Exceptions;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal abstract class OperatorPropertiesUserControlBase<TViewModel> : UserControlBase<TViewModel>
         where TViewModel : OperatorPropertiesViewModelBase
    {
        private const int TITLE_BAR_HEIGHT = 27;
        private const float ROW_HEIGHT = 32F;
        private const int NAME_COLUMN = 0;
        private const int VALUE_COLUMN = 1;
        private const int VERY_SMALL_LENGTH = 10;

        private readonly TitleBarUserControl _titleBarUserControl;
        private readonly TableLayoutPanel _tableLayoutPanel;

        public event EventHandler<Int32EventArgs> CloseRequested;
        public event EventHandler<Int32EventArgs> LoseFocusRequested;

        public OperatorPropertiesUserControlBase()
        {
            Name = GetType().Name;

            Load += Base_Load;
            Leave += Base_Leave;
            Resize += Base_Resize;

            _titleBarUserControl = CreateTitleBarUserControl();
            Controls.Add(_titleBarUserControl);
            _titleBarUserControl.CloseClicked += _titleBarUserControl_CloseClicked;

            _tableLayoutPanel = CreateTableLayoutPanel();
            Controls.Add(_tableLayoutPanel);
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
            SetTitles();
            AddProperties();

            // Always add an empty row at the end, to fill up the space.
            _tableLayoutPanel.RowCount++;

            ApplyStyling();
            PositionControls();

            this.AutomaticallyAssignTabIndexes();
        }

        // Gui

        /// <summary> does nothing </summary>
        protected virtual void SetTitles()
        { }

        private void ApplyStyling()
        {
            BackColor = SystemColors.ButtonFace;

            for (int i = 0; i < _tableLayoutPanel.RowCount - 1; i++)
            {
                _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            }
            // LastRow always is empty space at the end.
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize)); 

            StyleHelper.SetPropertyLabelColumnSize(_tableLayoutPanel);
        }

        private void PositionControls()
        {
            _titleBarUserControl.Width = Width;
            _tableLayoutPanel.Width = Width - StyleHelper.DefaultSpacing; // Arbitraily take off spacing, because otherwise it is stuck at the left side.
            _tableLayoutPanel.Height = Height - TitleBarHeight;
        }

        public string TitleBarText
        {
            get { return _titleBarUserControl.Text; }
            set { _titleBarUserControl.Text = value; }
        }

        public int TitleBarHeight
        {
            get { return TITLE_BAR_HEIGHT; }
        }

        /// <summary> does nothing </summary>
        protected virtual void AddProperties()
        { }

        protected void AddProperty(Control nameControl, Control valueControl)
        {
            if (nameControl == null) throw new NullException(() => nameControl);
            if (valueControl == null) throw new NullException(() => valueControl);

            int rowIndex = _tableLayoutPanel.RowCount++;

            _tableLayoutPanel.Controls.Add(nameControl, NAME_COLUMN, rowIndex);
            nameControl.Dock = DockStyle.Fill;
            Controls.Remove(nameControl);

            _tableLayoutPanel.Controls.Add(valueControl, VALUE_COLUMN, rowIndex);
            valueControl.Dock = DockStyle.Fill;
            Controls.Remove(valueControl);
        }

        // Binding
        /// <summary> does nothing </summary>
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

        private TableLayoutPanel CreateTableLayoutPanel()
        {
            var tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                Name = nameof(_tableLayoutPanel),
                Left = 0,
                Top = TITLE_BAR_HEIGHT,
                Size = new Size(VERY_SMALL_LENGTH, VERY_SMALL_LENGTH)
            };

            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, VERY_SMALL_LENGTH));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            return tableLayoutPanel;
        }
    }
}
