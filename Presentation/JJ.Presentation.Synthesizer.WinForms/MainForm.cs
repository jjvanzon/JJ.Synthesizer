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
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Forms;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private IContext _context;

        private RepositoryWrapper _repositoryWrapper;

        private MainPresenter _presenter;
        private DocumentListPresenter _documentListPresenter;
        private DocumentDetailsPresenter _documentDetailsPresenter;
        private DocumentPropertiesPresenter _documentPropertiesPresenter;
        private DocumentTreePresenter _documentTreePresenter;
        private DocumentConfirmDeletePresenter _documentConfirmDeletePresenter;

        private MainViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();
            _repositoryWrapper = CreateRepositoryWrapper();

            _presenter = new MainPresenter(_repositoryWrapper);

            _documentListPresenter = new DocumentListPresenter(_repositoryWrapper.DocumentRepository);
            _documentDetailsPresenter = new DocumentDetailsPresenter(_repositoryWrapper.DocumentRepository);
            _documentPropertiesPresenter = new DocumentPropertiesPresenter(_repositoryWrapper.DocumentRepository);
            _documentTreePresenter = new DocumentTreePresenter(_repositoryWrapper.DocumentRepository);
            _documentConfirmDeletePresenter = new DocumentConfirmDeletePresenter(_repositoryWrapper);

            documentListUserControl.ShowRequested += documentListUserControl_ShowRequested;
            documentListUserControl.CloseRequested += documentListUserControl_CloseRequested;
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

            //HideTreePanel();
            //HidePropertiesPanel();

            Open();

            //ShowDocumentList();

            //ShowAudioFileOutputList();
            //ShowCurveList();
            //ShowPatchList();
            //ShowSampleList();
            //ShowAudioFileOutputDetails();
            //ShowPatchDetails();
        }

        private RepositoryWrapper CreateRepositoryWrapper()
        {
            var repositoryWrapper = new RepositoryWrapper
            (
                PersistenceHelper.CreateRepository<IDocumentRepository>(_context),
                PersistenceHelper.CreateRepository<ICurveRepository>(_context),
                PersistenceHelper.CreateRepository<IPatchRepository>(_context),
                PersistenceHelper.CreateRepository<ISampleRepository>(_context),
                PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(_context),
                PersistenceHelper.CreateRepository<IDocumentReferenceRepository>(_context),
                PersistenceHelper.CreateRepository<INodeRepository>(_context),
                PersistenceHelper.CreateRepository<IAudioFileOutputChannelRepository>(_context),
                PersistenceHelper.CreateRepository<IOperatorRepository>(_context),
                PersistenceHelper.CreateRepository<IInletRepository>(_context),
                PersistenceHelper.CreateRepository<IOutletRepository>(_context),
                PersistenceHelper.CreateRepository<IEntityPositionRepository>(_context)
            );

            return repositoryWrapper;
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

        private void Open()
        {
            MainViewModel viewModel = _presenter.Open();
            SetViewModel(viewModel);
        }

        // Document List Actions

        // Done
        private void ShowDocumentList(int pageNumber = 1)
        {
            MainViewModel viewModel2 = _presenter.ShowDocumentList(_viewModel, pageNumber);
            SetViewModel(viewModel2);
        }

        // Done
        private void CloseDocumentList()
        {
            MainViewModel viewModel2 = _presenter.CloseDocumentList(_viewModel);
            SetViewModel(viewModel2);
        }

        // Done
        [Obsolete("Will become obsolete once everything is properly managed through the main presenter.")]
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

        // Done
        [Obsolete("Will become obsolete once everything is properly managed through the main presenter.")]
        private void HideDocumentList()
        {
            documentListUserControl.Hide();
        }

        // Document Details Actions

        // Done
        [Obsolete("Will become obsolete once everything is properly managed through the main presenter.")]
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

        // Done
        [Obsolete("Will become obsolete once everything is properly managed through the main presenter.")]
        private void HideDocumentDetails()
        {
            documentDetailsUserControl.Hide();
        }

        // Done
        private void CreateDocument()
        {
            MainViewModel viewModel2 = _presenter.CreateDocument(_viewModel);
            SetViewModel(viewModel2);
        }

        // Done
        private void OpenDocument(int id)
        {
            MainViewModel viewModel2 = _presenter.OpenDocument(_viewModel, id);
            SetViewModel(viewModel2);
        }

        // Done
        private void SaveDocumentDetails(DocumentDetailsViewModel viewModel)
        {
            // TODO: Not sure how much this will still work in a stateless environment.
            _viewModel.DocumentDetails = viewModel;

            MainViewModel viewModel2 = _presenter.SaveDocumentDetails(_viewModel);
            SetViewModel(viewModel2);
        }

        // Done
        private void DocumentCannotDeleteOK()
        {
            MainViewModel viewModel2 = _presenter.DocumentCannotDeleteOK(_viewModel);
            SetViewModel(viewModel2);
        }

        // Done
        private void NotFoundOK()
        {
            MainViewModel viewModel2 = _presenter.NotFoundOK(_viewModel);
            SetViewModel(viewModel2);
        }

        // Done
        private void DeleteDocument(int id)
        {
            MainViewModel viewModel2 = _presenter.DeleteDocument(_viewModel, id);
            SetViewModel(viewModel2);
        }

        // Done
        private void ConfirmDeleteDocument(int id)
        {
            MainViewModel viewModel2 = _presenter.ConfirmDeleteDocument(_viewModel, id);
            SetViewModel(viewModel2);
        }

        // Done
        private void DocumentDeleteConfirmedOK()
        {
            MainViewModel viewModel2 = _presenter.DocumentDeleteConfirmedOK(_viewModel);
            SetViewModel(viewModel2);
        }

        // Done
        private void CancelConfirmDeleteDocument()
        {
            MainViewModel viewModel2 = _presenter.CancelConfirmDeleteDocument(_viewModel);
            SetViewModel(viewModel2);
        }

        // Done
        private void CloseDocumentDetails()
        {
            MainViewModel viewModel2 = _presenter.CloseDocumentDetails(_viewModel);
            SetViewModel(viewModel2);
            
            //HideDocumentDetails();
        }

        // Document Tree Actions

        private void HideDocumentTree()
        {
            documentTreeUserControl.Hide();
            SetTreePanelVisible(false);
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

            SetPropertiesPanelVisible(true);

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
            SetPropertiesPanelVisible(false);
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
            CloseDocumentList();
        }

        // Document Details Events

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
        {
            SaveDocumentDetails(documentDetailsUserControl.ViewModel);
        }

        private void documentDetailsUserControl_DeleteRequested(object sender, IDEventArgs e)
        {
            DeleteDocument(e.ID);
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            CloseDocumentDetails();
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

        // TODO: Maybe SetViewModel should simply become ApplyViewModel without a viewModel parameter,
        // just using the _viewModel field and in the MainForm action methods the _viewModel field
        // is simply overwritten without an extra local variable.

        private void SetViewModel(MainViewModel viewModel)
        {
            _viewModel = viewModel;

            menuUserControl.Show(viewModel.Menu);

            documentListUserControl.ViewModel = viewModel.DocumentList;
            documentListUserControl.Visible = viewModel.DocumentList.Visible;

            documentDetailsUserControl.ViewModel = viewModel.DocumentDetails;
            documentDetailsUserControl.Visible = viewModel.DocumentDetails.Visible;

            documentTreeUserControl.ViewModel = viewModel.DocumentTree;
            documentTreeUserControl.Visible = viewModel.DocumentTree.Visible;

            bool treePanelMustBeVisible = viewModel.DocumentTree.Visible;
            SetTreePanelVisible(treePanelMustBeVisible);

            bool propertiesPanelMustBeVisible = viewModel.DocumentProperties.Visible;
            SetPropertiesPanelVisible(propertiesPanelMustBeVisible);

            if (viewModel.NotFound.Visible)
            {
                ShowNotFound(viewModel.NotFound);
            }

            if (viewModel.DocumentCannotDelete.Visible)
            {
                var form = new DocumentCannotDeleteForm();
                form.ShowDialog(viewModel.DocumentCannotDelete);
                DocumentCannotDeleteOK();
            }

            if (viewModel.DocumentConfirmDelete.Visible)
            {
                ShowDocumentConfirmDelete(viewModel.DocumentConfirmDelete);
            }

            if (viewModel.DocumentDeleteConfirmed.Visible)
            {
                MessageBox.Show(CommonMessageFormatter.ObjectIsDeleted(PropertyDisplayNames.Document));
                DocumentDeleteConfirmedOK();
            }

            // TODO: Set other parts of the view.
            // TODO: Make panel visibility dependent on more things.
        }

        private void ShowDocumentConfirmDelete(DocumentConfirmDeleteViewModel viewModel)
        {
            string message = CommonMessageFormatter.AreYouSureYouWishToDeleteWithName(PropertyDisplayNames.Document, viewModel.Document.Name);

            DialogResult dialogResult = MessageBox.Show(message, Titles.ApplicationName, MessageBoxButtons.YesNo);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    ConfirmDeleteDocument(viewModel.Document.ID);
                    break;

                case DialogResult.No:
                    CancelConfirmDeleteDocument();
                    break;

                default:
                    throw new ValueNotSupportedException(dialogResult);
            }
        }

        /// <summary>
        /// Eventually ShowNotFound will only be called in SetMainViewModel,
        /// after refactoring so that everything uses MainPresenter.
        /// Then you might not need a separate ShowNotFound method anymore.
        /// </summary>
        private void ShowNotFound(NotFoundViewModel viewModel)
        {
            MessageBox.Show(viewModel.Message);
            NotFoundOK();
        }

        private void SetTreePanelVisible(bool visible)
        {
            splitContainerTree.Panel1Collapsed = !visible;
        }

        private void SetPropertiesPanelVisible(bool visible)
        {
            splitContainerProperties.Panel2Collapsed = !visible;
        }
    }
}
