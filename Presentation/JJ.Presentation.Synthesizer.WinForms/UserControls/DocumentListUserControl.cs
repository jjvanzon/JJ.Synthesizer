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

        public event EventHandler CloseRequested;
        public event EventHandler<DocumentDetailsEventArgs> DetailsRequested;
        public event EventHandler<DocumentConfirmDeleteEventArgs> ConfirmDeleteRequested;

        private IContext _context;

        private IDocumentRepository _documentRepository;
        private ICurveRepository _curveRepository;
        private IPatchRepository _patchRepository;
        private ISampleRepository _sampleRepository;
        private IAudioFileOutputRepository _audioFileOutputRepository;
        private IDocumentReferenceRepository _documentReferenceRepository;
        private INodeRepository _nodeRepository;
        private IAudioFileOutputChannelRepository _audioFileOutputChannelRepository;
        private IOperatorRepository _operatorRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private IEntityPositionRepository _entityPositionRepository;

        private DocumentListPresenter _presenter;
        /// <summary> virtually not nullable </summary>
        private DocumentListViewModel _viewModel;

        public DocumentListUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Persistence

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IContext Context
        {
            get { return _context; }
            set 
            {
                if (value == null) throw new NullException(() => value);
                if (_context == value) return;

                _context = value;

                _documentRepository = PersistenceHelper.CreateRepository<IDocumentRepository>(_context);
                _curveRepository = PersistenceHelper.CreateRepository<ICurveRepository>(_context);
                _patchRepository = PersistenceHelper.CreateRepository<IPatchRepository>(_context);
                _sampleRepository = PersistenceHelper.CreateRepository<ISampleRepository>(_context);
                _audioFileOutputRepository = PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(_context);
                _documentReferenceRepository = PersistenceHelper.CreateRepository<IDocumentReferenceRepository>(_context);
                _nodeRepository = PersistenceHelper.CreateRepository<INodeRepository>(_context);
                _audioFileOutputChannelRepository = PersistenceHelper.CreateRepository<IAudioFileOutputChannelRepository>(_context);
                _operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(_context);
                _inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(_context);
                _outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(_context);
                _entityPositionRepository = PersistenceHelper.CreateRepository<IEntityPositionRepository>(_context);

                _presenter = new DocumentListPresenter(_documentRepository);
            }
        }

        private void AssertContext()
        {
            if (_context == null)
            {
                throw new Exception("Assign Context first.");
            }
        }

        // Actions

        public void Show(int pageNumber = 1)
        {
            AssertContext();

            _viewModel = _presenter.Show(pageNumber);

            ApplyViewModel();

            base.Show();
        }

        private void Create()
        {
            DocumentDetailsViewModel viewModel2 = _presenter.Create();

            // TODO: I would almost say you may want something like a controller.
            // Perhaps it should control a giant view model with everything in it...

            if (DetailsRequested != null)
            {
                var e = new DocumentDetailsEventArgs(viewModel2);
                DetailsRequested(this, e);
            }
        }

        private void Delete()
        {
            int id = GetSelectedID();

            object viewModel2 = _presenter.Delete(
                id,
                _curveRepository,
                _patchRepository,
                _sampleRepository,
                _audioFileOutputRepository,
                _documentReferenceRepository,
                _nodeRepository,
                _audioFileOutputChannelRepository,
                _operatorRepository,
                _inletRepository,
                _outletRepository,
                _entityPositionRepository);

            var notFoundViewModel = viewModel2 as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                MessageBox.Show(notFoundViewModel.Message);
                ApplyViewModel();
                return;
            }

            var cannotDeleteViewModel = viewModel2 as DocumentCannotDeleteViewModel;
            if (cannotDeleteViewModel != null)
            {
                var form2 = new DocumentCannotDeleteForm();
                form2.Show(cannotDeleteViewModel);
                return;
            }

            var confirmDeleteViewModel = viewModel2 as DocumentConfirmDeleteViewModel;
            if (confirmDeleteViewModel != null)
            {
                if (ConfirmDeleteRequested != null)
                {
                    var e = new DocumentConfirmDeleteEventArgs(confirmDeleteViewModel);
                    ConfirmDeleteRequested(this, e);
                }

                ApplyViewModel();
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
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
            titleBarUserControl.Text = PropertyDisplayNames.Documents;
            IDColumn.HeaderText = CommonTitles.ID;
            NameColumn.HeaderText = CommonTitles.Name;
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

        // Helpers

        private int GetSelectedID()
        {
            DataGridViewCell cell = dataGridView.CurrentRow.Cells[COLUMN_NAME_ID];
            int id = Convert.ToInt32(cell.Value);
            return id;
        }
    }
}
