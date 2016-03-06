using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal class SpecializedDataGridView : DataGridView
    {
        public SpecializedDataGridView()
        {
            base.AllowUserToAddRows = false;
            base.AllowUserToDeleteRows = false;
            base.AllowUserToResizeRows = false;
            base.AutoGenerateColumns = false;
            base.BackgroundColor = SystemColors.Control;
            base.BorderStyle = BorderStyle.None;
            base.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            base.Margin = new Padding(0);
        }

        public new object DataSource
        {
            get { return base.DataSource; }
            set
            {
                // DataGridView screws up if you do assign a data source that has 0 items.
                IList ilist = value as IList;
                if (ilist == null)
                {
                    throw new Exception("value must be IList.");
                }

                // DataGridView screws up if you do not first assign null
                // (possibly only when the data source is the same object, but with the data in it changed).
                base.DataSource = null;

                // DataGridView screws up if you do assign a data source that has 0 items.
                if (ilist.Count != 0)
                {
                    base.DataSource = value;
                }
            }
        }
    }
}
