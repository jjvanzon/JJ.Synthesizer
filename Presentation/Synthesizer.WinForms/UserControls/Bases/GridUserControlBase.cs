using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
    internal class GridUserControlBase : UserControlBase
    {
        public event EventHandler CreateRequested;
        public event EventHandler<EventArgs<int>> DeleteRequested;
        public event EventHandler CloseRequested;
        public event EventHandler<EventArgs<int>> ShowDetailsRequested;

        private readonly TitleBarUserControl _titleBarUserControl;
        private readonly SpecializedDataGridView _specializedDataGridView;
        private readonly TableLayoutPanel _tableLayoutPanel;

        public GridUserControlBase()
        {
            _tableLayoutPanel = CreateTableLayoutPanel();
            Controls.Add(_tableLayoutPanel);

            _titleBarUserControl = CreateTitleBarUserControl();
            _tableLayoutPanel.Controls.Add(_titleBarUserControl, 0, 0);

            _specializedDataGridView = CreateSpecializedDataGridView();
            _tableLayoutPanel.Controls.Add(_specializedDataGridView, 0, 1);

            AutoScaleMode = AutoScaleMode.None;

            SetTitles();
            // ReSharper disable once VirtualMemberCallInConstructor
            AddColumns();
        }

        // Create Controls

        private TableLayoutPanel CreateTableLayoutPanel()
        {
            var tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 2,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
            };

            tableLayoutPanel.Name = nameof(tableLayoutPanel);
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanel.RowStyles.Add(new RowStyle());

            return tableLayoutPanel;
        }

        private TitleBarUserControl CreateTitleBarUserControl()
        {
            var titleBarUserControl = new TitleBarUserControl
            {
                AddButtonVisible = true,
                CloseButtonVisible = true,
                RemoveButtonVisible = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Name = nameof(_titleBarUserControl)
            };

            titleBarUserControl.CloseClicked += _titleBarUserControl_CloseClicked;
            titleBarUserControl.RemoveClicked += _titleBarUserControl_RemoveClicked;
            titleBarUserControl.AddClicked += _titleBarUserControl_AddClicked;

            return titleBarUserControl;
        }

        private SpecializedDataGridView CreateSpecializedDataGridView()
        {
            var specializedDataGridView = new SpecializedDataGridView
            {
                Name = nameof(_specializedDataGridView),
                Dock = DockStyle.Fill,
                ColumnHeadersVisible = ColumnHeadersVisible,
                Visible = true
            };

            specializedDataGridView.DoubleClick += _specializedDataGridView_DoubleClick;
            specializedDataGridView.KeyDown += _specializedDataGridView_KeyDown;

            return specializedDataGridView;
        }

        // Gui

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        protected virtual string IDPropertyName { get; }
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        protected virtual string Title { get; }
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        protected virtual bool ColumnHeadersVisible { get; }
        protected virtual void AddColumns() { }

        protected void AddColumn(string dataPropertyName, string title, int widthInPixels = 120, bool visible = true, bool autoSize = false)
        {
            if (string.IsNullOrEmpty(dataPropertyName)) throw new NullOrEmptyException(() => dataPropertyName);
            if (string.IsNullOrEmpty(title)) throw new NullOrEmptyException(() => title);

            var dataGridViewColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataPropertyName,
                HeaderText = title,
                Name = dataPropertyName + "Column",
                ReadOnly = true,
                Visible = visible
            };

            if (autoSize)
            {
                dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            _specializedDataGridView.Columns.Add(dataGridViewColumn);
        }

        private void SetTitles()
        {
            _titleBarUserControl.Text = Title;
        }

        // Binding

        protected virtual object GetDataSource() => null;

        protected override void ApplyViewModelToControls()
        {
            _specializedDataGridView.DataSource = GetDataSource();
        }

        // Events

        private void _titleBarUserControl_AddClicked(object sender, EventArgs e)
        {
            Create();
        }

        private void _titleBarUserControl_RemoveClicked(object sender, EventArgs e)
        {
            Delete();
        }

        private void _titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void _specializedDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    Delete();
                    break;

                case Keys.Enter:
                    ShowDetails();
                    break;
            }
        }

        private void _specializedDataGridView_DoubleClick(object sender, EventArgs e)
        {
            ShowDetails();
        }

        // Actions

        private void Create()
        {
            if (ViewModel == null) return;

            CreateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Delete()
        {
            if (ViewModel == null) return;

            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                DeleteRequested?.Invoke(this, new EventArgs<int>(id.Value));
            }
        }

        private void Close()
        {
            if (ViewModel == null) return;

            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ShowDetails()
        {
            if (ViewModel == null) return;

            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                ShowDetailsRequested?.Invoke(this, new EventArgs<int>(id.Value));
            }
        }

        // Helpers

        private int? TryGetSelectedID()
        {
            if (_specializedDataGridView.CurrentRow == null)
            {
                return null;
            }

            string idColumnName = $"{IDPropertyName}Column";

            DataGridViewCell cell = _specializedDataGridView.CurrentRow.Cells[idColumnName];
            int id = (int)cell.Value;
            return id;
        }
    }
}
