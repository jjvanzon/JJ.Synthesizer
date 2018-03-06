using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.TypeChecking;

// ReSharper disable RedundantBaseQualifier

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
	internal class SpecializedDataGridView : DataGridView
	{
		public SpecializedDataGridView()
		{
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeRows = false;
			AutoGenerateColumns = false;
			BackgroundColor = SystemColors.Window;
			BorderStyle = BorderStyle.None;
			ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			Margin = new Padding(0);
			RowHeadersVisible = false;
			AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
			EnableHeadersVisualStyles = false;
			AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}

		public new object DataSource
		{
			get => base.DataSource;
			set
			{
				// DataGridView screws up if you do assign a data source that has 0 items.
				if (!(value is IList asIList))
				{
					throw new IsNotTypeException<IList>(() => value);
				}

				int? rowIndex = base.CurrentCell?.RowIndex;
				int? columnIndex = base.CurrentCell?.ColumnIndex;

				// DataGridView screws up if you do not first assign null
				// (possibly only when the data source is the same object, but with the data in it changed).
				base.DataSource = null;

				// DataGridView screws up if you do assign a data source that has 0 items.
				if (asIList.Count != 0)
				{
					base.DataSource = value;
				}

				if (rowIndex.HasValue)
				{
					SetSelectedCell(rowIndex.Value, columnIndex.Value);
				}
			}
		}

		private void SetSelectedCell(int rowIndex, int columnIndex)
		{
			if (base.RowCount == 0 || base.ColumnCount == 0)
			{
				return;
			}

			if (rowIndex > base.RowCount - 1)
			{
				rowIndex = base.RowCount - 1;
			}

			if (columnIndex > base.ColumnCount - 1)
			{
				columnIndex = base.ColumnCount - 1;
			}

			base.ClearSelection();

			base.SetSelectedCellCore(columnIndex, rowIndex, true);
			base.CurrentCell = base.Rows[rowIndex].Cells[columnIndex];
		}
	}
}
