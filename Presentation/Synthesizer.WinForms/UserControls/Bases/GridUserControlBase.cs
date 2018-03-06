using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
	internal class GridUserControlBase : UserControlBase
	{
		private const int DEFAULT_COLUMN_WIDTH_IN_PIXELS = 120;

		// Events

		public event EventHandler AddRequested;
		public event EventHandler<EventArgs<int>> DeleteRequested;
		public event EventHandler CloseRequested;
		public event EventHandler<EventArgs<int>> OpenItemExternallyRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;
		public event EventHandler<EventArgs<int>> ShowItemRequested;

		public new event KeyEventHandler KeyDown
		{
			add => _specializedDataGridView.KeyDown += value;
			remove => _specializedDataGridView.KeyDown -= value;
		}

		public event DataGridViewCellEventHandler CellClick
		{
			add => _specializedDataGridView.CellClick += value;
			remove => _specializedDataGridView.CellClick -= value;
		}

		// Fields

		private readonly TitleBarUserControl _titleBarUserControl;
		private readonly SpecializedDataGridView _specializedDataGridView;
		private int _columnCounter = 1;

		// Construction

		// ReSharper disable once MemberCanBeProtected.Global
		public GridUserControlBase()
		{
			TableLayoutPanel tableLayoutPanel = CreateTableLayoutPanel();
			Controls.Add(tableLayoutPanel);

			_titleBarUserControl = CreateTitleBarUserControl();
			tableLayoutPanel.Controls.Add(_titleBarUserControl, 0, 0);

			_specializedDataGridView = CreateSpecializedDataGridView();
			tableLayoutPanel.Controls.Add(_specializedDataGridView, 0, 1);

			AutoScaleMode = AutoScaleMode.None;

			// ReSharper disable once VirtualMemberCallInConstructor
			AddColumns();
		}

		private TableLayoutPanel CreateTableLayoutPanel()
		{
			var tableLayoutPanel = new TableLayoutPanel
			{
				ColumnCount = 1,
				RowCount = 2,
				Dock = DockStyle.Fill,
				Margin = new Padding(0),
				Padding = new Padding(0)
			};

			tableLayoutPanel.Name = nameof(tableLayoutPanel);
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, StyleHelper.TitleBarHeight));
			tableLayoutPanel.RowStyles.Add(new RowStyle());

			return tableLayoutPanel;
		}

		private TitleBarUserControl CreateTitleBarUserControl()
		{
			var titleBarUserControl = new TitleBarUserControl
			{
				AddButtonVisible = true,
				CloseButtonVisible = true,
				DeleteButtonVisible = true,
				SaveButtonVisible = false,
				PlayButtonVisible = false,
				Dock = DockStyle.Fill,
				Margin = new Padding(0),
				Name = nameof(_titleBarUserControl),
			};

			titleBarUserControl.AddClicked += _titleBarUserControl_AddClicked;
			titleBarUserControl.CloseClicked += _titleBarUserControl_CloseClicked;
			titleBarUserControl.ExpandClicked += _titleBarUserControl_OpenClicked;
			titleBarUserControl.PlayClicked += _titleBarUserControl_PlayClicked;
			titleBarUserControl.DeleteClicked += TitleBarUserControl_DeleteClicked;

			return titleBarUserControl;
		}

		private SpecializedDataGridView CreateSpecializedDataGridView()
		{
			var specializedDataGridView = new SpecializedDataGridView
			{
				Name = nameof(_specializedDataGridView),
				Dock = DockStyle.Fill,
				Visible = true
			};

			specializedDataGridView.DoubleClick += _specializedDataGridView_DoubleClick;
			specializedDataGridView.KeyDown += _specializedDataGridView_KeyDown;

			return specializedDataGridView;
		}

		// Gui

		protected string Title
		{
			get => _titleBarUserControl.Text;
			set => _titleBarUserControl.Text = value;
		}

		protected bool ColumnTitlesVisible
		{
			get => _specializedDataGridView.ColumnHeadersVisible;
			set => _specializedDataGridView.ColumnHeadersVisible = value;
		}

		[DefaultValue(true)]
		protected bool AddButtonVisible
		{
			get => _titleBarUserControl.AddButtonVisible;
			set => _titleBarUserControl.AddButtonVisible = value;
		}

		[DefaultValue(true)]
		protected bool CloseButtonVisible
		{
			get => _titleBarUserControl.CloseButtonVisible;
			set => _titleBarUserControl.CloseButtonVisible = value;
		}

		protected bool OpenItemExternallyButtonVisible
		{
			get => _titleBarUserControl.ExpandButtonVisible;
			set => _titleBarUserControl.ExpandButtonVisible = value;
		}

		protected bool PlayButtonVisible
		{
			get => _titleBarUserControl.PlayButtonVisible;
			set => _titleBarUserControl.PlayButtonVisible = value;
		}

		[DefaultValue(true)]
		protected bool DeleteButtonVisible
		{
			get => _titleBarUserControl.DeleteButtonVisible;
			set => _titleBarUserControl.DeleteButtonVisible = value;
		}

		protected bool FullRowSelect
		{
			get => _specializedDataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect;
			set => _specializedDataGridView.SelectionMode = value ? DataGridViewSelectionMode.FullRowSelect : DataGridViewSelectionMode.CellSelect;
		}

		/// <summary> base does nothing </summary>
		protected virtual void AddColumns() { }

		protected DataGridViewTextBoxColumn AddHiddenColumn(string dataPropertyName)
		{
			DataGridViewTextBoxColumn dataGridViewColumn = CreateTextBoxColumn(dataPropertyName);
			dataGridViewColumn.Visible = false;

			return dataGridViewColumn;
		}

		protected DataGridViewTextBoxColumn AddAutoSizeColumn(string dataPropertyName, string title)
		{
			if (string.IsNullOrWhiteSpace(title)) throw new NullOrEmptyException(() => title);

			DataGridViewTextBoxColumn dataGridViewColumn = CreateTextBoxColumn(dataPropertyName);
			dataGridViewColumn.HeaderText = title;
			dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			return dataGridViewColumn;
		}

		protected DataGridViewTextBoxColumn AddColumnWithWidth(string dataPropertyName, string title, int widthInPixels)
		{
			DataGridViewTextBoxColumn dataGridViewColumn = CreateTextBoxColumn(dataPropertyName);
			dataGridViewColumn.HeaderText = title;
			dataGridViewColumn.Width = widthInPixels;

			return dataGridViewColumn;
		}

		protected DataGridViewImageColumn AddImageColumn(Image image)
		{
			var dataGridViewColumn = new DataGridViewImageColumn
			{
				Image = image,
				Name = $"{nameof(image)}Column{_columnCounter++}",
				HeaderText = "",
				Visible = true,
				Resizable = DataGridViewTriState.False,
				Width = image.Width + StyleHelper.DefaultSpacing + StyleHelper.DefaultSpacing,
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Alignment = DataGridViewContentAlignment.MiddleCenter,
					BackColor = SystemColors.Window
				}
			};

			_specializedDataGridView.Columns.Add(dataGridViewColumn);

			return dataGridViewColumn;
		}

		protected void AddColumn(string dataPropertyName, string title)
		{
			DataGridViewColumn dataGridViewColumn = CreateTextBoxColumn(dataPropertyName);
			dataGridViewColumn.HeaderText = title;
			dataGridViewColumn.Width = DEFAULT_COLUMN_WIDTH_IN_PIXELS;
		}

		private DataGridViewTextBoxColumn CreateTextBoxColumn(string dataPropertyName)
		{
			if (string.IsNullOrEmpty(dataPropertyName)) throw new NullOrEmptyException(() => dataPropertyName);

			var dataGridViewColumn = new DataGridViewTextBoxColumn
			{
				DataPropertyName = dataPropertyName,
				Name = dataPropertyName + "Column",
				ReadOnly = true,
				Visible = true
			};

			_specializedDataGridView.Columns.Add(dataGridViewColumn);

			return dataGridViewColumn;
		}

		// Binding

		protected virtual object GetDataSource() => null;
		protected string IDPropertyName { get; set; }
		protected override void ApplyViewModelToControls() => _specializedDataGridView.DataSource = GetDataSource();

		// Event Handlers

		private void _titleBarUserControl_AddClicked(object sender, EventArgs e) => Add();
		private void _titleBarUserControl_CloseClicked(object sender, EventArgs e) => Close();
		private void _titleBarUserControl_OpenClicked(object sender, EventArgs e) => OpenItemExternally();
		private void _titleBarUserControl_PlayClicked(object sender, EventArgs e) => Play();
		private void TitleBarUserControl_DeleteClicked(object sender, EventArgs e) => Delete();
		private void _specializedDataGridView_DoubleClick(object sender, EventArgs e) => ShowItem();

		private void _specializedDataGridView_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;

			switch (e.KeyCode)
			{
				case Keys.Delete:
					Delete();
					break;

				case Keys.Enter:
					ShowItem();
					break;

				default:
					e.Handled = false;
					break;
			}
		}

		// Actions

		private void Add()
		{
			if (ViewModel == null) return;

			AddRequested?.Invoke(this, EventArgs.Empty);
		}

		private void Close()
		{
			if (ViewModel == null) return;

			CloseRequested?.Invoke(this, EventArgs.Empty);
		}

		protected void OpenItemExternally()
		{
			if (ViewModel == null) return;

			int? id = TryGetSelectedID();
			if (id.HasValue)
			{
				OpenItemExternallyRequested?.Invoke(this, new EventArgs<int>(id.Value));
			}
		}

		protected void Play()
		{
			if (ViewModel == null) return;

			int? id = TryGetSelectedID();
			if (id.HasValue)
			{
				PlayRequested?.Invoke(this, new EventArgs<int>(id.Value));
			}
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

		protected void ShowItem()
		{
			if (ViewModel == null) return;

			int? id = TryGetSelectedID();
			if (id.HasValue)
			{
				ShowItemRequested?.Invoke(this, new EventArgs<int>(id.Value));
			}
		}

		// Helpers

		protected int? TryGetColumnIndex() => _specializedDataGridView.CurrentCell?.ColumnIndex;

		protected int? TryGetSelectedID()
		{
			if (_specializedDataGridView.CurrentRow == null)
			{
				return null;
			}

			if (string.IsNullOrEmpty(IDPropertyName))
			{
				throw new NullException(() => IDPropertyName);
			}

			string idColumnName = $"{IDPropertyName}Column";

			DataGridViewCell cell = _specializedDataGridView.CurrentRow.Cells[idColumnName];
			int id = (int)cell.Value;
			return id;
		}
	}
}
