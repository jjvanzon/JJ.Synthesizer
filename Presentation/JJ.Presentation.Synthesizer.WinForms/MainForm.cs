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
        private DocumentDeletePresenter _documentConfirmDeletePresenter;

        private MainViewModel _viewModel;

        private DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();

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
            _documentConfirmDeletePresenter = new DocumentDeletePresenter(_repositoryWrapper);

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

            _documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;

            MessageBoxHelper.NotFoundOK += MessageBoxHelper_NotFoundOK;
            MessageBoxHelper.DocumentDeleteConfirmed += MessageBoxHelper_DocumentDeleteConfirmed;
            MessageBoxHelper.DocumentDeleteCanceled += MessageBoxHelper_DocumentDeleteCanceled;
            MessageBoxHelper.DocumentDeletedOK += MessageBoxHelper_DocumentDeletedOK;

            SetTitles();

            Open();

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
            _viewModel = _presenter.Open();
            ApplyViewModel();
        }

        // Document List Actions

        // Done
        private void DocumentShowList(int pageNumber = 1)
        {
            _viewModel = _presenter.DocumentShowList(_viewModel, pageNumber);
            ApplyViewModel();
        }

        // Done
        private void DocumentListClose()
        {
            _viewModel = _presenter.DocumentCloseList(_viewModel);
            ApplyViewModel();
        }

        // Done
        [Obsolete("Will become obsolete once everything is properly managed through the main presenter.")]
        private void DocumentRefreshList()
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
        private void DocumentHideList()
        {
            documentListUserControl.Hide();
        }

        // Document Details Actions

        // Done
        [Obsolete("Will become obsolete once everything is properly managed through the main presenter.")]
        private void DocumentShowDetails(DocumentDetailsViewModel viewModel)
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
        private void DocumentCreate()
        {
            _viewModel = _presenter.DocumentCreate(_viewModel);
            ApplyViewModel();
        }

        // Done
        private void DocumentOpen(int id)
        {
            _viewModel = _presenter.DocumentOpen(_viewModel, id);
            ApplyViewModel();
        }

        // Done
        private void DocumentSaveDetails(DocumentDetailsViewModel viewModel)
        {
            // TODO: Not sure how much this will still work in a stateless environment.
            _viewModel.DocumentDetails = viewModel;

            _viewModel = _presenter.DocumentSaveDetails(_viewModel);
            ApplyViewModel();
        }

        // Done
        private void DocumentCannotDeleteOK()
        {
            _viewModel = _presenter.DocumentCannotDeleteOK(_viewModel);
            ApplyViewModel();
        }

        // Done
        private void NotFoundOK()
        {
            _viewModel = _presenter.NotFoundOK(_viewModel);
            ApplyViewModel();
        }

        // Done
        private void DocumentDelete(int id)
        {
            _viewModel = _presenter.DocumentDelete(_viewModel, id);
            ApplyViewModel();
        }

        // Done
        private void DocumentConfirmDelete(int id)
        {
            _viewModel = _presenter.DocumentConfirmDelete(_viewModel, id);
            ApplyViewModel();
        }

        // Done
        private void DocumentDeletedOK()
        {
            _viewModel = _presenter.DocumentDeletedOK(_viewModel);
            ApplyViewModel();
        }

        // Done
        private void DocumentCancelDelete()
        {
            _viewModel = _presenter.DocumentCancelDelete(_viewModel);
            ApplyViewModel();
        }

        // Done
        private void DocumentCloseDetails()
        {
            _viewModel = _presenter.DocumentCloseDetails(_viewModel);
            ApplyViewModel();
        }

        // Document Tree Actions

        private void DocumentHideTree()
        {
            documentTreeUserControl.Hide();
            SetTreePanelVisible(false);
        }

        private void DocumentRefreshTree(int id)
        {
            object viewModel = _documentTreePresenter.Show(id);

            var notFoundViewModel = viewModel as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                MessageBoxHelper.ShowNotFound(notFoundViewModel);
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

        private void DocumentShowProperties(int id)
        {
            object viewModel = _documentPropertiesPresenter.Show(id);

            var notFoundViewModel = viewModel as NotFoundViewModel;
            if (notFoundViewModel != null)
            {
                MessageBoxHelper.ShowNotFound(notFoundViewModel);
                DocumentShowList();
                return;
            }

            var propertiesViewModel = viewModel as DocumentPropertiesViewModel;
            if (propertiesViewModel != null)
            {
                DocumentShowProperties(propertiesViewModel);
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel);
        }

        private void DocumentShowProperties(DocumentPropertiesViewModel viewModel)
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

        private void DocumentCloseProperties(DocumentPropertiesViewModel viewModel)
        {
            object viewModel2 = _documentPropertiesPresenter.Close(viewModel);

            var propertiesViewModel = viewModel2 as DocumentPropertiesViewModel;
            if (propertiesViewModel != null)
            {
                DocumentShowProperties(propertiesViewModel);
                return;
            }

            var noViewModel = viewModel2 as NoViewModel;
            if (noViewModel != null)
            {
                DocumentHideProperties();
                DocumentRefreshList();
                DocumentRefreshTree(viewModel.Document.ID);
                return;
            }

            throw new UnexpectedViewModelTypeException(viewModel2);
        }

        private void DocumentPropertiesLoseFocus(DocumentPropertiesViewModel viewModel)
        {
            DocumentPropertiesViewModel viewModel2 = _documentPropertiesPresenter.LooseFocus(viewModel);

            DocumentRefreshList();
            DocumentRefreshTree(viewModel.Document.ID);

            DocumentShowProperties(viewModel2);
        }

        private void DocumentHideProperties()
        {
            documentPropertiesUserControl.Hide();
            SetPropertiesPanelVisible(false);
        }

        // Other Actions

        private void AudioFileOutputListShow()
        {
            var form = new AudioFileOutputListForm();
            form.Context = _context;
            form.Show();
        }

        private void CurveListShow()
        {
            var form = new CurveListForm();
            form.Context = _context;
            form.Show();
        }

        private void PatchListShow()
        {
            var form = new PatchListForm();
            form.Context = _context;
            form.Show();
        }

        private void SampleListShow()
        {
            var form = new SampleListForm();
            form.Context = _context;
            form.Show();
        }

        private void AudioFileOutputDetailsShow()
        {
            var form = new AudioFileOutputDetailsForm();
            form.Context = _context;
            form.Show();
        }

        private void PatchDetailsShow()
        {
            var form = new PatchDetailsForm();
            form.Context = _context;
            form.Show();
        }

        // Events

        // Menu Events

        private void menuUserControl_ShowDocumentListRequested(object sender, EventArgs e)
        {
            DocumentShowList();
        }

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
        {
            // TODO: You cannot know which document to open without a MainPresenter and MainViewModel.
            throw new NotImplementedException();
        }

        // Document List Events

        private void documentListUserControl_ShowRequested(object sender, PageEventArgs e)
        {
            DocumentShowList(e.PageNumber);
        }

        private void documentListUserControl_CreateRequested(object sender, EventArgs e)
        {
            DocumentCreate();
        }

        private void documentListUserControl_OpenRequested(object sender, IDEventArgs e)
        {
            DocumentOpen(e.ID);
        }

        private void documentListUserControl_DeleteRequested(object sender, IDEventArgs e)
        {
            DocumentDelete(e.ID);
        }

        private void documentListUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentListClose();
        }

        // Document Details Events

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
        {
            DocumentSaveDetails(documentDetailsUserControl.ViewModel);
        }

        private void documentDetailsUserControl_DeleteRequested(object sender, IDEventArgs e)
        {
            DocumentDelete(e.ID);
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentCloseDetails();
        }

        // Document Tree Events

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentHideTree();
        }

        private void documentTreeUserControl_DocumentPropertiesRequested(object sender, IDEventArgs e)
        {
            DocumentShowProperties(e.ID);
        }

        // Document Properties Events

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentCloseProperties(documentPropertiesUserControl.ViewModel);
        }

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            DocumentPropertiesLoseFocus(documentPropertiesUserControl.ViewModel);
        }

        // Message Box Events

        private void MessageBoxHelper_NotFoundOK(object sender, EventArgs e)
        {
            NotFoundOK();
        }

        private void MessageBoxHelper_DocumentDeleteCanceled(object sender, EventArgs e)
        {
            DocumentCancelDelete();
        }

        private void MessageBoxHelper_DocumentDeleteConfirmed(object sender, IDEventArgs e)
        {
            DocumentConfirmDelete(e.ID);
        }

        private void MessageBoxHelper_DocumentDeletedOK(object sender, EventArgs e)
        {
            DocumentDeletedOK();
        }

        // DocumentCannotDeleteForm Events

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e)
        {
            DocumentCannotDeleteOK();
        }

        // Helpers

        // TODO: Maybe SetViewModel should simply become ApplyViewModel without a viewModel parameter,
        // just using the _viewModel field and in the MainForm action methods the _viewModel field
        // is simply overwritten without an extra local variable.

        private void ApplyViewModel()
        {
            menuUserControl.Show(_viewModel.Menu);

            documentListUserControl.ViewModel = _viewModel.DocumentList;
            documentListUserControl.Visible = _viewModel.DocumentList.Visible;

            documentDetailsUserControl.ViewModel = _viewModel.DocumentDetails;
            documentDetailsUserControl.Visible = _viewModel.DocumentDetails.Visible;

            documentTreeUserControl.ViewModel = _viewModel.DocumentTree;
            documentTreeUserControl.Visible = _viewModel.DocumentTree.Visible;

            bool treePanelMustBeVisible = _viewModel.DocumentTree.Visible;
            SetTreePanelVisible(treePanelMustBeVisible);

            bool propertiesPanelMustBeVisible = _viewModel.DocumentProperties.Visible;
            SetPropertiesPanelVisible(propertiesPanelMustBeVisible);

            if (_viewModel.NotFound.Visible)
            {
                MessageBoxHelper.ShowNotFound(_viewModel.NotFound);
            }

            if (_viewModel.DocumentDelete.Visible)
            {
                MessageBoxHelper.ShowDocumentConfirmDelete(_viewModel.DocumentDelete);
            }

            if (_viewModel.DocumentDeleted.Visible)
            {
                MessageBoxHelper.ShowDocumentIsDeleted();
            }

            if (_viewModel.DocumentCannotDelete.Visible)
            {
                _documentCannotDeleteForm.ShowDialog(_viewModel.DocumentCannotDelete);
            }

            if (_viewModel.DocumentDetails.Messages.Count != 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, _viewModel.DocumentDetails.Messages.Select(x => x.Text)));
            }

            // TODO: Set other parts of the view.
            // TODO: Make panel visibility dependent on more things.
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
