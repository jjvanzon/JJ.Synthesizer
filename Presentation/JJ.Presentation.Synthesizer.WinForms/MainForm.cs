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
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Forms;

namespace JJ.Presentation.Synthesizer.WinForms
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
        private DocumentDetailsPresenter _documentDetailsPresenter;
        private DocumentPropertiesPresenter _documentPropertiesPresenter;
        private DocumentTreePresenter _documentTreePresenter;
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
            _documentDetailsPresenter = new DocumentDetailsPresenter(_documentRepository);
            _documentPropertiesPresenter = new DocumentPropertiesPresenter(_documentRepository);
            _documentTreePresenter = new DocumentTreePresenter(_documentRepository);
            _documentConfirmDeletePresenter = new DocumentConfirmDeletePresenter(
                _documentRepository, _curveRepository, _patchRepository, _sampleRepository,
                _audioFileOutputRepository, _documentReferenceRepository, _nodeRepository, _audioFileOutputChannelRepository,
                _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            documentListUserControl.ShowRequested += documentListUserControl_ShowRequested;
            documentListUserControl.CreateRequested += documentListUserControl_CreateRequested;
            documentListUserControl.OpenRequested += documentListUserControl_OpenRequested;
            documentListUserControl.DeleteRequested += documentListUserControl_DeleteRequested;
            documentDetailsUserControl.SaveRequested += documentDetailsUserControl_SaveRequested;
            documentDetailsUserControl.DeleteRequested += documentDetailsUserControl_DeleteRequested;
            documentDetailsUserControl.CloseRequested += documentDetailsUserControl_CloseRequested;
            documentPropertiesUserControl.CloseRequested += documentPropertiesUserControl_CloseRequested;
            documentPropertiesUserControl.LoseFocusRequested += documentPropertiesUserControl_LoseFocusRequested;

            documentTreeUserControl.CloseRequested += documentTreeUserControl_CloseRequested;
            documentTreeUserControl.DocumentPropertiesRequested += documentTreeUserControl_DocumentPropertiesRequested;
            
            SetTitles();

            HideTreePanel();
            HidePropertiesPanel();

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

        private void RefreshDocumentList()
        {
            int pageNumber = 1;
            if (documentDetailsUserControl.ViewModel != null)
            {
                pageNumber = documentListUserControl.ViewModel.Pager.PageNumber;
            }

            DocumentListViewModel viewModel = _documentListPresenter.Show(pageNumber);
            documentListUserControl.ViewModel = viewModel;
        }

        private void HideDocumentList()
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

        private void HideDocumentDetails()
        {
            documentDetailsUserControl.Hide();
        }

        private void CreateDocument()
        {
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();
            ShowDocumentDetails(viewModel);
        }

        private void EditDocument(int id)
        {
            object viewModel = _documentDetailsPresenter.Edit(id);

            var notFoundViewModel = viewModel as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                ShowNotFound(notFoundViewModel);
                ShowDocumentList();
                return;
            }

            var detailsViewModel = viewModel as DocumentDetailsViewModel;
            if (detailsViewModel != null)
            {
                ShowDocumentDetails(detailsViewModel);
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel);
        }

        private void OpenDocument(int id)
        {
            object viewModel = _documentTreePresenter.Show(id);

            var notFoundViewModel = viewModel as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                ShowNotFound(notFoundViewModel);
                ShowDocumentList();
                return;
            }

            var treeViewModel = viewModel as DocumentTreeViewModel;
            if (treeViewModel != null)
            {
                OpenDocument(treeViewModel);
                HideDocumentList();
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel);
        }

        private void OpenDocument(DocumentTreeViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            documentTreeUserControl.ViewModel = viewModel;
            documentTreeUserControl.Show();
            ShowTreePanel();
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
                HideDocumentDetails();
                RefreshDocumentList();
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
                RefreshDocumentList();
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
                RefreshDocumentList();
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
                RefreshDocumentList();
                return;
            }

            var deleteConfirmedViewModel = viewModel2 as DocumentDeleteConfirmedViewModel;
            if (deleteConfirmedViewModel != null)
            {
                HideDocumentDetails();
                MessageBox.Show(CommonMessageFormatter.DeleteConfirmed(PropertyDisplayNames.Document));
                RefreshDocumentList();
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        // Document Tree Actions

        private void HideDocumentTree()
        {
            documentTreeUserControl.Hide();
            HideTreePanel();
        }

        private void RefreshDocumentTree(int id)
        {
            object viewModel = _documentTreePresenter.Show(id);

            var notFoundViewModel = viewModel as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                ShowNotFound(notFoundViewModel);
                return;
            }

            var treeViewModel = viewModel as DocumentTreeViewModel;
            if (treeViewModel != null)
            {
                documentTreeUserControl.ViewModel = treeViewModel;
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel);
        }

        // Document Properties Actions

        private void ShowDocumentProperties(int id)
        {
            object viewModel = _documentPropertiesPresenter.Show(id);

            var notFoundViewModel = viewModel as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                ShowNotFound(notFoundViewModel);
                ShowDocumentList();
                return;
            }

            var propertiesViewModel = viewModel as DocumentPropertiesViewModel;
            if (propertiesViewModel != null)
            {
                ShowDocumentProperties(propertiesViewModel);
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel);
        }

        private void ShowDocumentProperties(DocumentPropertiesViewModel viewModel)
        {
            documentPropertiesUserControl.ViewModel = viewModel;
            documentPropertiesUserControl.Show();
            documentPropertiesUserControl.BringToFront();

            ShowPropertiesPanel();

            // TODO: This 'if' is a major hack.
            if (viewModel.Messages.Count != 0)
            {
                documentPropertiesUserControl.Focus();
            }

            // TODO: ValidationMessages should be shown in a separate panel.
            if (viewModel.Messages.Count > 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, viewModel.Messages.Select(x => x.Text)));
            }
        }

        private void CloseDocumentProperties(DocumentPropertiesViewModel viewModel)
        {
            object viewModel2 = _documentPropertiesPresenter.Close(viewModel);

            var propertiesViewModel = viewModel2 as DocumentPropertiesViewModel;
            if (propertiesViewModel != null)
            {
                ShowDocumentProperties(propertiesViewModel);
                return;
            }

            var noViewModel = viewModel2 as NoViewModel;
            if (noViewModel != null)
            {
                HideDocumentProperties();
                RefreshDocumentList();
                RefreshDocumentTree(viewModel.Document.ID);
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        private void DocumentPropertiesLoseFocus(DocumentPropertiesViewModel viewModel)
        {
            DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.LooseFocus(viewModel);

            RefreshDocumentList();
            RefreshDocumentTree(viewModel.Document.ID);

            ShowDocumentProperties(viewModel2);
        }

        private void HideDocumentProperties()
        {
            documentPropertiesUserControl.Hide();
            HidePropertiesPanel();
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

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
        {
            // TODO: You cannot know which document to open without a MainPresenter and MainViewModel.
            throw new NotImplementedException();
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

        private void documentListUserControl_OpenRequested(object sender, IDEventArgs e)
        {
            OpenDocument(e.ID);
        }

        private void documentListUserControl_DeleteRequested(object sender, IDEventArgs e)
        {
            DeleteDocument(e.ID);
        }

        private void documentListUserControl_CloseRequested(object sender, EventArgs e)
        {
            HideDocumentList();
        }

        // Document Details Events

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
        {
            SaveDocument(documentDetailsUserControl.ViewModel);
        }

        private void documentDetailsUserControl_DeleteRequested(object sender, IDEventArgs e)
        {
            DeleteDocument(e.ID);
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            HideDocumentDetails();
        }

        // Document Tree Events

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e)
        {
            HideDocumentTree();
        }

        private void documentTreeUserControl_DocumentPropertiesRequested(object sender, IDEventArgs e)
        {
            ShowDocumentProperties(e.ID);
        }

        // Document Properties Events

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            CloseDocumentProperties(documentPropertiesUserControl.ViewModel);
        }

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            DocumentPropertiesLoseFocus(documentPropertiesUserControl.ViewModel);
        }

        // Helpers

        private void ShowTreePanel()
        {
            splitContainerTree.Panel1Collapsed = false;
        }

        private void HideTreePanel()
        {
            splitContainerTree.Panel1Collapsed = true;
        }

        private void ShowPropertiesPanel()
        {
            splitContainerProperties.Panel2Collapsed = false;
        }

        private void HidePropertiesPanel()
        {
            splitContainerProperties.Panel2Collapsed = true;
        }
    }
}
