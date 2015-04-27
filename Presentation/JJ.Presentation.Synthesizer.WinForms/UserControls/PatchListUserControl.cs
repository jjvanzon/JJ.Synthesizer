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

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    public partial class PatchListUserControl : UserControl
    {
        private PatchListViewModel _viewModel;

        public PatchListUserControl()
        {
            InitializeComponent();
        }

        // Persistence

        private IContext _context;
        private PatchListPresenter _presenter;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IContext Context
        {
            get { return _context; }
            set 
            {
                if (value == null) throw new NullException(() => value);
                _context = value;
                _presenter = CreatePresenter(_context);
            }
        }

        private PatchListPresenter CreatePresenter(IContext context)
        {
            return new PatchListPresenter(PersistenceHelper.CreateRepository<IPatchRepository>(context));
        }

        private void EnsurePresenter()
        {
            if (_presenter == null)
            {
                throw new Exception("Assign Context first.");
            }
        }

        // Actions

        public void Show(int pageNumber = 1)
        {
            EnsurePresenter();

            _viewModel = _presenter.Show(pageNumber);

            ApplyViewModel();
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
    }
}
