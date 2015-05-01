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

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentListUserControl : UserControl
    {
        public event EventHandler CloseRequested;

        private DocumentListViewModel _viewModel;

        public DocumentListUserControl()
        {
            InitializeComponent();

            SetTitles();
        }

        // Persistence

        private IContext _context;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IContext Context
        {
            get { return _context; }
            set 
            {
                if (value == null) throw new NullException(() => value);
                _context = value;
            }
        }

        // Actions

        public void Show(int pageNumber = 1)
        {
            DocumentListPresenter presenter = CreatePresenter();
            _viewModel = presenter.Show(pageNumber);
            ApplyViewModel();

            base.Show();
        }

        private void Add()
        {
            DocumentListPresenter presenter = CreatePresenter();
            _viewModel = presenter.Add();
            ApplyViewModel();
        }

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        // Gui

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

        private void SetTitles()
        {
            labelTitle.Text = PropertyDisplayNames.Documents;
            IDColumn.HeaderText = CommonTitles.ID;
            NameColumn.HeaderText = PropertyDisplayNames.Name;
            toolStripButtonAdd.ToolTipText = CommonTitles.Add;
            toolStripButtonDelete.ToolTipText = CommonTitles.Delete;
            toolStripButtonClose.ToolTipText = CommonTitles.Close;
        }

        // Events

        private void pagerControl_GoToFirstPageClicked(object sender, EventArgs e)
        {
            Show(1);
        }

        private void pagerControl_GoToPreviousPageClicked(object sender, EventArgs e)
        {
            Show(_viewModel.Pager.PageNumber - 1);
        }

        private void pagerControl_PageNumberClicked(object sender, PageNumberEventArgs e)
        {
            Show(e.PageNumber);
        }

        private void pagerControl_GoToNextPageClicked(object sender, EventArgs e)
        {
            Show(_viewModel.Pager.PageNumber + 1);
        }

        private void pagerControl_GoToLastPageClicked(object sender, EventArgs e)
        {
            Show(_viewModel.Pager.PageCount - 1);
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            //Delete(
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Helpers

        private DocumentListPresenter CreatePresenter()
        {
            if (_context == null) throw new Exception("Assign Context first.");

            return new DocumentListPresenter(PersistenceHelper.CreateRepository<IDocumentRepository>(_context));
        }
    }
}
