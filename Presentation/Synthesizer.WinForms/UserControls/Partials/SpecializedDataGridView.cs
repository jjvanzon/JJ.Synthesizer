using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Exceptions;

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
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Margin = new Padding(0);
            RowHeadersVisible = false;
            AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            EnableHeadersVisualStyles = false;
            AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
        }

        public new object DataSource
        {
            get { return base.DataSource; }
            set
            {
                // DataGridView screws up if you do assign a data source that has 0 items.
                var asIList = value as IList;
                if (asIList == null)
                {
                    throw new InvalidTypeException<IList>(() => value);
                }

                // DataGridView screws up if you do not first assign null
                // (possibly only when the data source is the same object, but with the data in it changed).
                base.DataSource = null;

                // DataGridView screws up if you do assign a data source that has 0 items.
                if (asIList.Count != 0)
                {
                    base.DataSource = value;
                }
            }
        }
    }
}
