using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
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
                // DataGridView screws up if you do not first assign null
                // (possibly only when the data source is the same object, but with the data in it changed).
                base.DataSource = null;
                base.DataSource = value;
            }
        }
    }
}
