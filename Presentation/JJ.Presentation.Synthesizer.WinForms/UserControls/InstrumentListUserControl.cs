using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Framework.Data;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class InstrumentListUserControl : UserControl
    {
        private const string TEMPORARY_ID_COLUMN_NAME = "TemporaryIDColumn";

        public event EventHandler CreateRequested;
        public event EventHandler<TemporaryIDEventArgs> DeleteRequested;
        public event EventHandler CloseRequested;

        /// <summary> virtually not nullable </summary>
        private InstrumentListViewModel _viewModel;

        public InstrumentListUserControl()
        {
            InitializeComponent();
            SetTitles();

            dataGridView.AutoGenerateColumns = false;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public InstrumentListViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _viewModel = value;
                ApplyViewModel();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Instruments;
        }

        private void ApplyViewModel()
        {
            dataGridView.DataSource = _viewModel.List;
        }

        // Actions

        private void Create()
        {
            if (CreateRequested != null)
            {
                CreateRequested(this, EventArgs.Empty);
            }
        }

        private void Delete()
        {
            if (DeleteRequested != null)
            {
                Guid? id = TryGetSelectedTemporaryID();
                if (id.HasValue)
                {
                    DeleteRequested(this, new TemporaryIDEventArgs(id.Value));
                }
            }
        }

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void titleBarUserControl_AddClicked(object sender, EventArgs e)
        {
            Create();
        }

        private void titleBarUserControl_RemoveClicked(object sender, EventArgs e)
        {
            Delete();
        }

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    Delete();
                    break;
            }
        }

        // Helpers

        private Guid? TryGetSelectedTemporaryID()
        {
            if (dataGridView.CurrentRow != null)
            {
                DataGridViewCell cell = dataGridView.CurrentRow.Cells[TEMPORARY_ID_COLUMN_NAME];
                Guid temporaryID = Guid.Parse(Convert.ToString(cell.Value));
                return temporaryID;
            }

            return null;
        }
    }
}
