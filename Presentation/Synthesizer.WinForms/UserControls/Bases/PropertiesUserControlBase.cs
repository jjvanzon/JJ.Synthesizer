using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
	internal class PropertiesUserControlBase : DetailsOrPropertiesUserControlBase
	{
		private const float ROW_HEIGHT = 32F;
		private const int VERY_SMALL_LENGTH = 10;
		private const int NAME_COLUMN = 0;
		private const int VALUE_COLUMN = 1;

		private readonly TableLayoutPanel _tableLayoutPanel;

		// ReSharper disable once MemberCanBeProtected.Global
		public PropertiesUserControlBase()
		{
			_tableLayoutPanel = CreateTableLayoutPanel();
			Controls.Add(_tableLayoutPanel);
		}

		protected override void OnLoad(EventArgs e)
		{
			AddProperties();

			// After AddProperties,
			// Always add an empty row at the end, to fill up the space.
			// We could have done this in the AddProperties,
			// But then there is too much change the base does not
			// get executed at the end of the override.
			_tableLayoutPanel.RowCount++;

			base.OnLoad(e);
		}

		// Gui

		protected override void ApplyStyling()
		{
			base.ApplyStyling();

			for (int i = 0; i < _tableLayoutPanel.RowCount - 1; i++)
			{
				_tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
			}

			// LastRow always is empty space at the end.
			_tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

			StyleHelper.SetPropertyLabelColumnSize(_tableLayoutPanel);
		}

		protected override void PositionControls()
		{
			base.PositionControls();

			_tableLayoutPanel.Width =
				Width - StyleHelper.DefaultSpacing; // Arbitrarily take off spacing, because otherwise it is stuck at the left side.

			_tableLayoutPanel.Height = Height - StyleHelper.TitleBarHeight;
		}

		/// <summary> does nothing </summary>
		protected virtual void AddProperties() { }

		/// <summary> Call from AddProperties. </summary>
		/// <param name="nameControl">nameControl and valueControl cannot both be null.</param>
		/// <param name="valueControl">nameControl and valueControl cannot both be null.</param>
		protected void AddProperty(Control nameControl, Control valueControl)
		{
			if (nameControl == null && valueControl == null)
			{
				throw new Exception($"{nameof(nameControl)} and {nameof(valueControl)} cannot both be null.");
			}

			int rowIndex = _tableLayoutPanel.RowCount++;

			if (nameControl != null)
			{
				_tableLayoutPanel.Controls.Add(nameControl, NAME_COLUMN, rowIndex);
				nameControl.Dock = DockStyle.Fill;
				Controls.Remove(nameControl);
			}

			// ReSharper disable once InvertIf
			if (valueControl != null)
			{
				_tableLayoutPanel.Controls.Add(valueControl, VALUE_COLUMN, rowIndex);
				valueControl.Dock = DockStyle.Fill;
				Controls.Remove(valueControl);
			}
		}

		/// <summary> Call from AddProperties. Adds spacing in between properties. </summary>
		protected void AddSpacing() => AddProperty(new Label(), null);

		// Create Controls

		private TableLayoutPanel CreateTableLayoutPanel()
		{
			var tableLayoutPanel = new TableLayoutPanel
			{
				ColumnCount = 2,
				Name = nameof(_tableLayoutPanel),
				Left = 0,
				Top = StyleHelper.TitleBarHeight,
				Size = new Size(VERY_SMALL_LENGTH, VERY_SMALL_LENGTH)
			};

			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, VERY_SMALL_LENGTH));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

			return tableLayoutPanel;
		}
	}
}