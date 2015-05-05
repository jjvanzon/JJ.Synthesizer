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
    internal partial class DocumentListUserControl : UserControl
    {
        private const string COLUMN_NAME_ID = "IDColumn";

        public event EventHandler<PageEventArgs> ShowRequested;
        public event EventHandler CreateRequested;
        public event EventHandler CloseRequested;
        public event EventHandler<DeleteEventArgs> DeleteRequested;

        /// <summary> virtually not nullable </summary>
        private DocumentListViewModel _viewModel;

        public DocumentListUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DocumentListViewModel ViewModel
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
            titleBarUserControl.Text = PropertyDisplayNames.Documents;
            IDColumn.HeaderText = CommonTitles.ID;
            NameColumn.HeaderText = CommonTitles.Name;
        }

        private void ApplyViewModel()
        {
            if (_viewModel == null)
            {
                pagerControl.PagerViewModel = null;
                dataGridView.DataSource = null;
            }

            pagerControl.PagerViewModel = _viewModel.Pager;

            dataGridView.DataSource = _viewModel.List;
        }

        // Events

        // TODO: It would probably look better if you add a method for each action and raise the event there.

        private void pagerControl_GoToFirstPageClicked(object sender, EventArgs e)
        {
            if (ShowRequested != null)
            {
                int pageNumber = 1;
                ShowRequested(this, new PageEventArgs(pageNumber));
            }
        }

        private void pagerControl_GoToPreviousPageClicked(object sender, EventArgs e)
        {
            if (ShowRequested != null)
            {
                int pageNumber = _viewModel.Pager.PageNumber - 1;
                ShowRequested(this, new PageEventArgs(pageNumber));
            }
        }

        private void pagerControl_PageNumberClicked(object sender, PageNumberEventArgs e)
        {
            if (ShowRequested != null)
            {
                int pageNumber = e.PageNumber;
                ShowRequested(this, new PageEventArgs(pageNumber));
            }
        }

        private void pagerControl_GoToNextPageClicked(object sender, EventArgs e)
        {
            if (ShowRequested != null)
            {
                int pageNumber = _viewModel.Pager.PageNumber + 1;
                ShowRequested(this, new PageEventArgs(pageNumber));
            }
        }

        private void pagerControl_GoToLastPageClicked(object sender, EventArgs e)
        {
            if (ShowRequested != null)
            {
                int pageNumber = _viewModel.Pager.PageCount - 1;
                ShowRequested(this, new PageEventArgs(pageNumber));
            }
        }

        private void titleBarUserControl_AddClicked(object sender, EventArgs e)
        {
            if (CreateRequested != null)
            {
                CreateRequested(this, EventArgs.Empty);
            }
        }

        private void titleBarUserControl_RemoveClicked(object sender, EventArgs e)
        {
            if (DeleteRequested != null)
            {
                int id = GetSelectedID();
                DeleteRequested(this, new DeleteEventArgs(id));
            }
        }

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        // Helpers

        private int GetSelectedID()
        {
            DataGridViewCell cell = dataGridView.CurrentRow.Cells[COLUMN_NAME_ID];
            int id = Convert.ToInt32(cell.Value);
            return id;
        }
    }
}
