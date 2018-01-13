using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
	internal partial class ButtonBarUserControl : UserControl
	{
		public event EventHandler AddClicked;
		public event EventHandler AddToInstrumentClicked;
		public event EventHandler CloseClicked;
		public event EventHandler NewClicked;
		public event EventHandler ExpandClicked;
		public event EventHandler PlayClicked;
		public event EventHandler RedoClicked;
		public event EventHandler RefreshClicked;
		public event EventHandler DeleteClicked;
		public event EventHandler SaveClicked;
		public event EventHandler UndoClicked;

		public ButtonBarUserControl() => InitializeComponent();

		private void TitleBarUserControl_Load(object sender, EventArgs e)
		{
			buttonAdd.Visible = _addButtonVisible;
			buttonAddToInstrument.Visible = _addToInstrumentButtonVisible;
			buttonClose.Visible = _closeButtonVisible;
			buttonNew.Visible = _newButtonVisible;
			buttonExpand.Visible = _expandButtonVisible;
			buttonPlay.Visible = _playButtonVisible;
			buttonRedo.Visible = _redoButtonVisible;
			buttonRefresh.Visible = _refreshButtonVisible;
			buttonDelete.Visible = _deleteButtonVisible;
			buttonSave.Visible = _saveButtonVisible;
			buttonUndo.Visible = _undoButtonVisible;

			SetTitles();
			PositionControls();
		}

		private void SetTitles()
		{
			toolTip.SetToolTip(buttonAdd, CommonResourceFormatter.Add);
			toolTip.SetToolTip(buttonAddToInstrument, ResourceFormatter.AddToInstrument);
			toolTip.SetToolTip(buttonClose, CommonResourceFormatter.Close);
			toolTip.SetToolTip(buttonNew, CommonResourceFormatter.New);
			toolTip.SetToolTip(buttonExpand, CommonResourceFormatter.Open);
			toolTip.SetToolTip(buttonPlay, ResourceFormatter.Play);
			toolTip.SetToolTip(buttonRedo, CommonResourceFormatter.Redo);
			toolTip.SetToolTip(buttonRefresh, CommonResourceFormatter.Refresh);
			toolTip.SetToolTip(buttonDelete, CommonResourceFormatter.Delete);
			toolTip.SetToolTip(buttonSave, CommonResourceFormatter.Save);
			toolTip.SetToolTip(buttonUndo, CommonResourceFormatter.Undo);
		}

		/// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
		private bool _addButtonVisible;
		[DefaultValue(false)]
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
		private bool _addToInstrumentButtonVisible;
		public bool AddToInstrumentButtonVisible
		{
			get => _addToInstrumentButtonVisible;
			set
			{
				_addToInstrumentButtonVisible = value;
				buttonAddToInstrument.Visible = _addToInstrumentButtonVisible;
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
		private bool _newButtonVisible;
		[DefaultValue(false)]
		public bool NewButtonVisible
		{
			get => _newButtonVisible;
			set
			{
				_newButtonVisible = value;
				buttonNew.Visible = _newButtonVisible;
				PositionControls();
			}
		}

		/// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
		private bool _expandButtonVisible;
		public bool ExpandButtonVisible
		{
			get => _expandButtonVisible;
			set
			{
				_expandButtonVisible = value;
				buttonExpand.Visible = _expandButtonVisible;
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
		private bool _redoButtonVisible;
		[DefaultValue(false)]
		public bool RedoButtonVisible
		{
			get => _redoButtonVisible;
			set
			{
				_redoButtonVisible = value;
				buttonRedo.Visible = _redoButtonVisible;
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
		private bool _deleteButtonVisible;
		public bool DeleteButtonVisible
		{
			get => _deleteButtonVisible;
			set
			{
				_deleteButtonVisible = value;
				buttonDelete.Visible = _deleteButtonVisible;
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

		/// <summary> Keep this field. WinForms will not make Button.Visible immediately take on the value you just assigned! </summary>
		private bool _undoButtonVisible;
		[DefaultValue(false)]
		public bool UndoButtonVisible
		{
			get => _undoButtonVisible;
			set
			{
				_undoButtonVisible = value;
				buttonUndo.Visible = _undoButtonVisible;
				PositionControls();
			}
		}

		// Positioning

		private readonly int _height = StyleHelper.DefaultSpacing + StyleHelper.IconButtonSize + StyleHelper.DefaultSpacing;

		private void PositionControls()
		{
			int visibleButtonCount = GetVisibleButtonCount();

			Width = visibleButtonCount * (StyleHelper.IconButtonSize + StyleHelper.DefaultSpacing);

			Height = _height;

			int x = Width;

			x -= StyleHelper.DefaultSpacing;
			x -= StyleHelper.IconButtonSize;

			var buttonTuplesInReverseOrder = new (Control Control, bool Visible)[]
			{
				(buttonClose, CloseButtonVisible),
				(buttonDelete, DeleteButtonVisible),
				(buttonAdd, AddButtonVisible),
				(buttonNew, NewButtonVisible),
				(buttonExpand, ExpandButtonVisible),
				(buttonRefresh, RefreshButtonVisible),
				(buttonSave, SaveButtonVisible),
				(buttonAddToInstrument, AddToInstrumentButtonVisible),
				(buttonPlay, PlayButtonVisible),
				(buttonRedo, RedoButtonVisible),
				(buttonUndo, UndoButtonVisible)
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

		private void buttonAdd_Click(object sender, EventArgs e) => AddClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonAddToInstrument_Click(object sender, EventArgs e) => AddToInstrumentClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonClose_Click(object sender, EventArgs e) => CloseClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonNew_Click(object sender, EventArgs e) => NewClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonOpen_Click(object sender, EventArgs e) => ExpandClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonPlay_Click(object sender, EventArgs e) => PlayClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonRedo_Click(object sender, EventArgs e) => RedoClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonRefresh_Click(object sender, EventArgs e) => RefreshClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonDelete_Click(object sender, EventArgs e) => DeleteClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonSave_Click(object sender, EventArgs e) => SaveClicked?.Invoke(sender, EventArgs.Empty);
		private void buttonUndo_Click(object sender, EventArgs e) => UndoClicked?.Invoke(sender, EventArgs.Empty);

		// Helpers

		private int GetVisibleButtonCount()
		{
			int count = 0;
			if (_addButtonVisible) count++;
			if (_addToInstrumentButtonVisible) count++;
			if (_closeButtonVisible) count++;
			if (_newButtonVisible) count++;
			if (_expandButtonVisible) count++;
			if (_playButtonVisible) count++;
			if (_redoButtonVisible) count++;
			if (_refreshButtonVisible) count++;
			if (_deleteButtonVisible) count++;
			if (_saveButtonVisible) count++;
			if (_undoButtonVisible) count++;
			return count;
		}
	}
}
