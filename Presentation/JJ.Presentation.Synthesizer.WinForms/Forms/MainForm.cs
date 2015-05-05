using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.Svg;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.ViewModel;
using JJ.Framework.Presentation;

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    internal partial class MainForm : Form
    {
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

        private DocumentDetailsPresenter _documentDetailsPresenter;
        private DocumentListPresenter _documentListPresenter;
        private DocumentConfirmDeletePresenter _documentConfirmDeletePresenter;

        public MainForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();

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

            _documentDetailsPresenter = new DocumentDetailsPresenter(_documentRepository);
            _documentListPresenter = new DocumentListPresenter(_documentRepository);
            _documentConfirmDeletePresenter = new DocumentConfirmDeletePresenter(
                _documentRepository, _curveRepository, _patchRepository, _sampleRepository,
                _audioFileOutputRepository, _documentReferenceRepository, _nodeRepository, _audioFileOutputChannelRepository,
                _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            documentListUserControl.ShowRequested += documentListUserControl_ShowRequested;
            documentListUserControl.CreateRequested += documentListUserControl_CreateRequested;
            documentListUserControl.DeleteRequested += documentListUserControl_DeleteRequested;
            documentDetailsUserControl.SaveRequested += documentDetailsUserControl_SaveRequested;
            documentDetailsUserControl.CloseRequested += documentDetailsUserControl_CloseRequested;

            SetTitles();

            ShowDocumentList();

            //ShowAudioFileOutputList();
            //ShowCurveList();
            //ShowPatchList();
            //ShowSampleList();
            //ShowAudioFileOutputDetails();
            //ShowPatchDetails();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            if (_context != null)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }

        private void SetTitles()
        {
            var config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            Text = Titles.ApplicationName + config.General.TitleBarExtraText;
        }

        // Actions

        // General Actions

        private void ShowNotFound(NotFoundViewModel viewModel)
        {
            MessageBox.Show(viewModel.Message);
        }

        // Document List Actions

        private void ShowDocumentList(int pageNumber = 1)
        {
            DocumentListViewModel viewModel = _documentListPresenter.Show(pageNumber);
            documentListUserControl.ViewModel = viewModel;
            documentListUserControl.Show();
        }

        private void CloseDocumentList()
        {
            documentListUserControl.Hide();
        }

        // Document Details Actions

        private void ShowDocumentDetails(DocumentDetailsViewModel viewModel)
        {
            documentDetailsUserControl.ViewModel = viewModel;
            documentDetailsUserControl.Show();
            documentDetailsUserControl.BringToFront();

            // TODO: This is kind of wierd.
            if (viewModel.Messages.Count > 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, viewModel.Messages.Select(x => x.Text)));
            }
        }

        private void CloseDocumentDetails()
        {
            documentDetailsUserControl.Hide();

            ShowDocumentList();
        }

        private void CreateDocument()
        {
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();
            ShowDocumentDetails(viewModel);
        }

        private void SaveDocument(DocumentDetailsViewModel viewModel)
        {
            object viewModel2 = _documentDetailsPresenter.Save(viewModel);

            var detailsViewModel = viewModel2 as DocumentDetailsViewModel;
            if (detailsViewModel != null)
            {
                ShowDocumentDetails(detailsViewModel);
                return;
            }

            var previousViewModel = viewModel2 as PreviousViewModel;
            if (previousViewModel != null)
            {
                CloseDocumentDetails();
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        private void DeleteDocument(int id)
        {
            object viewModel2 = _documentConfirmDeletePresenter.Show(id);

            var notFoundViewModel = viewModel2 as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                ShowNotFound(notFoundViewModel);
                ShowDocumentList();
                return;
            }

            var cannotDeleteViewModel = viewModel2 as DocumentCannotDeleteViewModel;
            if (cannotDeleteViewModel != null)
            {
                ShowDocumentCannotDelete(cannotDeleteViewModel);
                return;
            }

            var confirmDeleteViewModel = viewModel2 as DocumentConfirmDeleteViewModel;
            if (confirmDeleteViewModel != null)
            {
                ShowDocumentConfirmDelete(confirmDeleteViewModel);
                ShowDocumentList();
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        private void ShowDocumentCannotDelete(DocumentCannotDeleteViewModel viewModel)
        {
            var form = new DocumentCannotDeleteForm();
            form.Show(viewModel);
            return;
        }

        private void ShowDocumentConfirmDelete(DocumentConfirmDeleteViewModel viewModel)
        {
            string message = CommonMessageFormatter.ConfirmDeleteObjectWithName(PropertyDisplayNames.Document, viewModel.Object.Name);
            if (MessageBox.Show(message, Titles.ApplicationName, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ConfirmDeleteDocument(viewModel.Object.ID);
            }
        }

        private void ConfirmDeleteDocument(int id)
        {
            object viewModel2 = _documentConfirmDeletePresenter.Confirm(id);

            var notFoundViewModel = viewModel2 as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                MessageBox.Show(notFoundViewModel.Message);
                ShowDocumentList();
                return;
            }

            var deleteConfirmedViewModel = viewModel2 as DocumentDeleteConfirmedViewModel;
            if (deleteConfirmedViewModel != null)
            {
                MessageBox.Show(CommonMessageFormatter.DeleteConfirmed(PropertyDisplayNames.Document));
                ShowDocumentList();
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        // Other Actions

        private void ShowAudioFileOutputList()
        {
            var form = new AudioFileOutputListForm();
            form.Context = _context;
            form.Show();
        }

        private void ShowCurveList()
        {
            var form = new CurveListForm();
            form.Context = _context;
            form.Show();
        }

        private void ShowPatchList()
        {
            var form = new PatchListForm();
            form.Context = _context;
            form.Show();
        }

        private void ShowSampleList()
        {
            var form = new SampleListForm();
            form.Context = _context;
            form.Show();
        }

        private void ShowAudioFileOutputDetails()
        {
            var form = new AudioFileOutputDetailsForm();
            form.Context = _context;
            form.Show();
        }

        private void ShowPatchDetails()
        {
            var form = new PatchDetailsForm();
            form.Context = _context;
            form.Show();
        }

        // Events

        // Menu Events

        private void menuUserControl_ShowDocumentListRequested(object sender, EventArgs e)
        {
            ShowDocumentList();
        }

        // Document List Events

        private void documentListUserControl_ShowRequested(object sender, PageEventArgs e)
        {
            ShowDocumentList(e.PageNumber);
        }

        private void documentListUserControl_CreateRequested(object sender, EventArgs e)
        {
            CreateDocument();
        }

        private void documentListUserControl_DeleteRequested(object sender, DeleteEventArgs e)
        {
            DeleteDocument(e.ID);
        }

        private void documentListUserControl_CloseRequested(object sender, EventArgs e)
        {
            CloseDocumentList();
        }

        // Document Details Events

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
        {
            SaveDocument(documentDetailsUserControl.ViewModel);
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            CloseDocumentDetails();
        }
    }
}
