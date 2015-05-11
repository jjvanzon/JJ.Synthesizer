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

        private AudioFileOutputListForm _audioFileOutputListForm = new AudioFileOutputListForm();
        private CurveListForm _curveListForm = new CurveListForm();
        private PatchListForm _patchListForm = new PatchListForm();
        private SampleListForm _sampleListForm = new SampleListForm();
        private AudioFileOutputDetailsForm _audioFileOutputDetailsForm = new AudioFileOutputDetailsForm();
        private PatchDetailsForm _patchDetailsForm = new PatchDetailsForm();

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

            menuUserControl.ShowDocumentListRequested += menuUserControl_ShowDocumentListRequested;
            menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
            menuUserControl.ShowAudioFileOutputListRequested += menuUserControl_ShowAudioFileOutputListRequested;
            menuUserControl.CurveListRequested += menuUserControl_CurveListRequested;
            menuUserControl.PatchListRequested += menuUserControl_PatchListRequested;
            menuUserControl.SampleListRequested += menuUserControl_SampleListRequested;
            menuUserControl.AudioFileOutputDetailsRequested += menuUserControl_AudioFileOutputDetailsRequested;
            menuUserControl.PatchDetailsRequested += menuUserControl_PatchDetailsRequested;

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

            _audioFileOutputListForm.ShowRequested += _audioFileOutputListForm_ShowRequested;
            _audioFileOutputListForm.CloseRequested += _audioFileOutputListForm_CloseRequested;
            _audioFileOutputDetailsForm.CloseRequested += _audioFileOutputDetailsForm_CloseRequested;
            _curveListForm.ShowRequested += _curveListForm_ShowRequested;
            _curveListForm.CloseRequested += _curveListForm_CloseRequested;
            _patchListForm.ShowRequested += _patchListForm_ShowRequested;
            _patchListForm.CloseRequested += _patchListForm_CloseRequested;
            _sampleListForm.ShowRequested += _sampleListForm_ShowRequested;
            _sampleListForm.CloseRequested += _sampleListForm_CloseRequested;
            _patchDetailsForm.CloseRequested += _patchDetailsForm_CloseRequested;

            _patchDetailsForm.Context = _context;

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

        private void DocumentListShow(int pageNumber = 1)
        {
            _viewModel = _presenter.DocumentListShow(_viewModel, pageNumber);
            ApplyViewModel();
        }

        private void DocumentListClose()
        {
            _viewModel = _presenter.DocumentListClose(_viewModel);
            ApplyViewModel();
        }

        // Document Details Actions

        private void DocumentDetailsCreate()
        {
            _viewModel = _presenter.DocumentDetailsCreate(_viewModel);
            ApplyViewModel();
        }

        private void DocumentDetailsSave(DocumentDetailsViewModel viewModel)
        {
            // TODO: Not sure how much this will still work in a stateless environment.
            _viewModel.DocumentDetails = viewModel;

            _viewModel = _presenter.DocumentDetailsSave(_viewModel);
            ApplyViewModel();
        }

        private void DocumentDetailsClose()
        {
            _viewModel = _presenter.DocumentDetailsClose(_viewModel);
            ApplyViewModel();
        }

        private void DocumentOpen(int id)
        {
            _viewModel = _presenter.DocumentOpen(_viewModel, id);
            ApplyViewModel();
        }

        private void DocumentCannotDeleteOK()
        {
            _viewModel = _presenter.DocumentCannotDeleteOK(_viewModel);
            ApplyViewModel();
        }

        private void NotFoundOK()
        {
            _viewModel = _presenter.NotFoundOK(_viewModel);
            ApplyViewModel();
        }

        private void DocumentDelete(int id)
        {
            _viewModel = _presenter.DocumentDelete(_viewModel, id);
            ApplyViewModel();
        }

        private void DocumentConfirmDelete(int id)
        {
            _viewModel = _presenter.DocumentConfirmDelete(_viewModel, id);
            ApplyViewModel();
        }

        private void DocumentDeletedOK()
        {
            _viewModel = _presenter.DocumentDeletedOK(_viewModel);
            ApplyViewModel();
        }

        private void DocumentCancelDelete()
        {
            _viewModel = _presenter.DocumentCancelDelete(_viewModel);
            ApplyViewModel();
        }

        // Document Properties Actions

        private void DocumentPropertiesShow(int id)
        {
            _viewModel = _presenter.DocumentPropertiesShow(_viewModel, id);
            ApplyViewModel();
        }

        private void DocumentPropertiesClose()
        {
            _viewModel = _presenter.DocumentPropertiesClose(_viewModel);
            ApplyViewModel();
        }

        private void DocumentPropertiesLoseFocus(DocumentPropertiesViewModel viewModel)
        {
            _viewModel = _presenter.DocumentPropertiesLoseFocus(_viewModel);
            ApplyViewModel();
        }

        // Document Tree Actions

        private void DocumentTreeClose()
        {
            _viewModel = _presenter.DocumentTreeClose(_viewModel);
            ApplyViewModel();
        }

        // Other Actions

        private void AudioFileOutputListShow(int pageNumber)
        {
            _viewModel = _presenter.AudioFileOutputListShow(_viewModel, pageNumber);
            ApplyViewModel();
        }

        private void CurveListShow(int pageNumber)
        {
            _viewModel = _presenter.CurveListShow(_viewModel, pageNumber);
            ApplyViewModel();
        }

        private void PatchListShow(int pageNumber)
        {
            _viewModel = _presenter.PatchListShow(_viewModel, pageNumber);
            ApplyViewModel();
        }

        private void SampleListShow(int pageNumber)
        {
            _viewModel = _presenter.SampleListShow(_viewModel, pageNumber);
            ApplyViewModel();
        }

        private void AudioFileOutputDetailsShow(int id)
        {
            _viewModel = _presenter.AudioFileOutputDetailsShow(_viewModel, id);
            ApplyViewModel();
        }

        private void PatchDetailsShow()
        {
            // Changing this one to the new structure is postponed,
            // because it is complicated, it works now and I only want to change it once it becomes
            // part of the program navigation.
            _patchDetailsForm.Context = _context;
            _patchDetailsForm.Show();
        }

        private void AudioFileOutputDetailsClose()
        {
            _viewModel = _presenter.AudioFileOutputDetailsClose(_viewModel);
            ApplyViewModel();
        }

        private void AudioFileOutputListClose()
        {
            _viewModel = _presenter.AudioFileOutputListClose(_viewModel);
            ApplyViewModel();
        }

        private void CurveListClose()
        {
            _viewModel = _presenter.CurveListClose(_viewModel);
            ApplyViewModel();
        }

        private void PatchListClose()
        {
            _viewModel = _presenter.PatchListClose(_viewModel);
            ApplyViewModel();
        }

        private void SampleListClose()
        {
            _viewModel = _presenter.SampleListClose(_viewModel);
            ApplyViewModel();
        }

        private void PatchDetailsEdit(int id)
        {
            _viewModel = _presenter.PatchDetailsEdit(_viewModel, id);
            ApplyViewModel();
        }

        private void PatchDetailsClose()
        {
            _viewModel = _presenter.PatchDetailsClose(_viewModel);
            ApplyViewModel();
        }

        // Events

        // Menu Events

        private void menuUserControl_ShowDocumentListRequested(object sender, EventArgs e)
        {
            DocumentListShow();
        }

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
        {
            // TODO: You cannot know which document to open without a MainPresenter and MainViewModel.
            throw new NotImplementedException();
        }

        private void menuUserControl_ShowAudioFileOutputListRequested(object sender, EventArgs e)
        {
            AudioFileOutputListShow(1);
        }

        private void menuUserControl_CurveListRequested(object sender, EventArgs e)
        {
            CurveListShow(1);
        }

        private void menuUserControl_PatchListRequested(object sender, EventArgs e)
        {
            PatchListShow(1);
        }

        private void menuUserControl_SampleListRequested(object sender, EventArgs e)
        {
            SampleListShow(1);
        }

        private void menuUserControl_AudioFileOutputDetailsRequested(object sender, EventArgs e)
        {
            int dummyID = 0;
            AudioFileOutputDetailsShow(dummyID);
        }

        private void menuUserControl_PatchDetailsRequested(object sender, EventArgs e)
        {
            ConfigurationSection config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            int testPatchID = config.Testing.TestPatchID;

            PatchDetailsEdit(testPatchID);
        }

        // Document List Events

        private void documentListUserControl_ShowRequested(object sender, PageEventArgs e)
        {
            DocumentListShow(e.PageNumber);
        }

        private void documentListUserControl_CreateRequested(object sender, EventArgs e)
        {
            DocumentDetailsCreate();
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
            DocumentDetailsSave(documentDetailsUserControl.ViewModel);
        }

        private void documentDetailsUserControl_DeleteRequested(object sender, IDEventArgs e)
        {
            DocumentDelete(e.ID);
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentDetailsClose();
        }

        // Document Tree Events

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentTreeClose();
        }

        private void documentTreeUserControl_DocumentPropertiesRequested(object sender, IDEventArgs e)
        {
            DocumentPropertiesShow(e.ID);
        }

        // Document Properties Events

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentPropertiesClose();
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

        // Temporary (List) Form Events

        private void _audioFileOutputListForm_ShowRequested(object sender, PageEventArgs e)
        {
            AudioFileOutputListShow(e.PageNumber);
        }

        private void _sampleListForm_ShowRequested(object sender, PageEventArgs e)
        {
            SampleListShow(e.PageNumber);
        }

        private void _patchListForm_ShowRequested(object sender, PageEventArgs e)
        {
            PatchListShow(e.PageNumber);
        }

        private void _curveListForm_ShowRequested(object sender, PageEventArgs e)
        {
            CurveListShow(e.PageNumber);
        }

        private void _audioFileOutputDetailsForm_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputDetailsClose();
        }

        private void _audioFileOutputListForm_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputListClose();
        }

        private void _curveListForm_CloseRequested(object sender, EventArgs e)
        {
            CurveListClose();
        }

        private void _patchListForm_CloseRequested(object sender, EventArgs e)
        {
            PatchListClose();
        }

        private void _sampleListForm_CloseRequested(object sender, EventArgs e)
        {
            SampleListClose();
        }

        private void _patchDetailsForm_CloseRequested(object sender, EventArgs e)
        {
            PatchDetailsClose();
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

            documentPropertiesUserControl.ViewModel = _viewModel.DocumentProperties;
            documentPropertiesUserControl.Visible = _viewModel.DocumentProperties.Visible;

            _audioFileOutputListForm.ViewModel = _viewModel.AudioFileOutputList;
            _audioFileOutputListForm.Visible = _viewModel.AudioFileOutputList.Visible;

            _curveListForm.ViewModel = _viewModel.CurveList;
            _curveListForm.Visible = _viewModel.CurveList.Visible;

            _patchListForm.ViewModel = _viewModel.PatchList;
            _patchListForm.Visible = _viewModel.PatchList.Visible;

            _sampleListForm.ViewModel = _viewModel.SampleList;
            _sampleListForm.Visible = _viewModel.SampleList.Visible;

            _audioFileOutputDetailsForm.ViewModel = _viewModel.AudioFileOutputDetails;
            _audioFileOutputDetailsForm.Visible = _viewModel.AudioFileOutputDetails.Visible;

            _patchDetailsForm.ViewModel = _viewModel.PatchDetails;
            _patchDetailsForm.Visible = _viewModel.PatchDetails.Visible;

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

            if (_viewModel.DocumentProperties.Messages.Count > 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, _viewModel.DocumentProperties.Messages.Select(x => x.Text)));
            }

            // TODO: This 'if' is a major hack.
            if (_viewModel.DocumentProperties.Messages.Count != 0)
            {
                documentPropertiesUserControl.Focus();
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
