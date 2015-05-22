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
    internal partial class EffectListUserControl : UserControl
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler CreateRequested;
        public event EventHandler<IDEventArgs> DeleteRequested;
        public event EventHandler CloseRequested;

        /// <summary> virtually not nullable </summary>
        private ChildDocumentListViewModel _viewModel;

        public EffectListUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ChildDocumentListViewModel ViewModel
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
            titleBarUserControl.Text = PropertyDisplayNames.Effects;
            IDColumn.HeaderText = CommonTitles.ID;
            NameColumn.HeaderText = CommonTitles.Name;
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
                int? id = TryGetSelectedID();
                if (id.HasValue)
                {
                    DeleteRequested(this, new IDEventArgs(id.Value));
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

        private int? TryGetSelectedID()
        {
            if (dataGridView.CurrentRow != null)
            {
                DataGridViewCell cell = dataGridView.CurrentRow.Cells[ID_COLUMN_NAME];
                int id = Convert.ToInt32(cell.Value);
                return id;
            }

            return null;
        }
    }
}
