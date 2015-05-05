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

            _documentListPresenter = new DocumentListPresenter(_documentRepository);
            _documentConfirmDeletePresenter = new DocumentConfirmDeletePresenter(
                _documentRepository, _curveRepository, _patchRepository, _sampleRepository,
                _audioFileOutputRepository, _documentReferenceRepository, _nodeRepository, _audioFileOutputChannelRepository,
                _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            documentListUserControl.ShowRequested += documentListUserControl_ShowRequested;
            documentListUserControl.CreateRequested += documentListUserControl_CreateRequested;
            documentListUserControl.DeleteRequested += documentListUserControl_DeleteRequested;

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

        private void ShowNotFound(NotFoundViewModel viewModel)
        {
            MessageBox.Show(viewModel.Message);
        }

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

        private void ShowDocumentDetails(DocumentDetailsViewModel viewModel)
        {
            documentDetailsUserControl1.Context = _context;
            documentDetailsUserControl1.Show(viewModel);
            documentDetailsUserControl1.BringToFront();
        }

        private void CloseDocumentDetails()
        {
            documentDetailsUserControl1.Hide();

            ShowDocumentList();
        }

        private void ShowDocumentCannotDelete(DocumentCannotDeleteViewModel viewModel)
        {
            var form = new DocumentCannotDeleteForm();
            form.Show(viewModel);
            return;
        }

        /// <summary>
        /// This should not be this complex. It should just be boilerplate code.
        /// </summary>
        private void ShowDocumentConfirmDelete(DocumentConfirmDeleteViewModel viewModel)
        {
            string message = CommonMessageFormatter.ConfirmDeleteObjectWithName(PropertyDisplayNames.Document, viewModel.Object.Name);
            if (MessageBox.Show(message, Titles.ApplicationName, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {   
                object viewModel2 = _documentConfirmDeletePresenter.Confirm(viewModel.Object.ID);

                var notFoundViewModel = viewModel2 as NotFoundViewModel;
                if (notFoundViewModel != null)
                {
                    MessageBox.Show(notFoundViewModel.Message);
                    // TODO: Why does the presenter layer not tell us what action to undertake?
                    //CloseConfirmDelete();
                    ShowDocumentList();
                    return;
                }

                var deleteConfirmedViewModel = viewModel2 as DocumentDeleteConfirmedViewModel;
                if (deleteConfirmedViewModel != null)
                {
                    MessageBox.Show(CommonMessageFormatter.DeleteConfirmed(PropertyDisplayNames.Document));
                    // TODO: Why does the presenter layer not tell us what action to undertake?
                    //CloseConfirmDelete();
                    ShowDocumentList();
                    return;
                }

                throw new UnexpectedViewModelTypeException(viewModel2);
            }
        }

        private void ShowAudioFileOutputList()
        {
            var form = new AudioFileOutputListForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowCurveList()
        {
            var form = new CurveListForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowPatchList()
        {
            var form = new PatchListForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowSampleList()
        {
            var form = new SampleListForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowAudioFileOutputDetails()
        {
            var form = new AudioFileOutputDetailsForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowPatchDetails()
        {
            var form = new PatchDetailsForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        // Events

        private void menuUserControl_ShowDocumentListRequested(object sender, EventArgs e)
        {
            ShowDocumentList();
        }

        private void documentListUserControl_ShowRequested(object sender, PageEventArgs e)
        {
            ShowDocumentList(e.PageNumber);
        }

        private void documentListUserControl_CreateRequested(object sender, EventArgs e)
        {
            DocumentDetailsViewModel viewModel = _documentListPresenter.Create();
            ShowDocumentDetails(viewModel);
        }

        private void documentListUserControl_DeleteRequested(object sender, DeleteEventArgs e)
        {
            object viewModel2 = _documentListPresenter.Delete(
                e.ID,
                _curveRepository, _patchRepository, _sampleRepository, _audioFileOutputRepository,
                _documentReferenceRepository, _nodeRepository, _audioFileOutputChannelRepository, _operatorRepository,
                _inletRepository, _outletRepository, _entityPositionRepository);

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

        private void documentListUserControl_CloseRequested(object sender, EventArgs e)
        {
            CloseDocumentList();
        }

        private void documentDetailsUserControl1_CloseRequested(object sender, EventArgs e)
        {
            CloseDocumentDetails();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // For debugging
        }
    }
}
