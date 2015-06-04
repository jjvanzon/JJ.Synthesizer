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

        //private AudioFileOutputListForm _audioFileOutputListForm = new AudioFileOutputListForm();
        private CurveListForm _curveListForm = new CurveListForm();
        private PatchListForm _patchListForm = new PatchListForm();
        private SampleListForm _sampleListForm = new SampleListForm();
        private AudioFileOutputPropertiesForm _audioFileOutputPropertiesForm = new AudioFileOutputPropertiesForm();
        private PatchDetailsForm _patchDetailsForm = new PatchDetailsForm();

        private static string _titleBarExtraText;

        static MainForm()
        {
            var config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            _titleBarExtraText = config.General.TitleBarExtraText;
        }

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
            menuUserControl.AudioFileOutputEditRequested += menuUserControl_AudioFileOutputEditRequested;
            menuUserControl.PatchDetailsRequested += menuUserControl_PatchDetailsRequested;

            documentListUserControl.ShowRequested += documentListUserControl_ShowRequested;
            documentListUserControl.CloseRequested += documentListUserControl_CloseRequested;
            documentListUserControl.CreateRequested += documentListUserControl_CreateRequested;
            documentListUserControl.OpenRequested += documentListUserControl_OpenRequested;
            documentListUserControl.DeleteRequested += documentListUserControl_DeleteRequested;
            documentDetailsUserControl.SaveRequested += documentDetailsUserControl_SaveRequested;
            documentDetailsUserControl.DeleteRequested += documentDetailsUserControl_DeleteRequested;
            documentDetailsUserControl.CloseRequested += documentDetailsUserControl_CloseRequested;

            documentTreeUserControl.CloseRequested += documentTreeUserControl_CloseRequested;
            documentTreeUserControl.ExpandNodeRequested += documentTreeUserControl_ExpandNodeRequested;
            documentTreeUserControl.CollapseNodeRequested += documentTreeUserControl_CollapseNodeRequested;
            documentTreeUserControl.DocumentPropertiesRequested += documentTreeUserControl_DocumentPropertiesRequested;
            documentTreeUserControl.ShowInstrumentsRequested += documentTreeUserControl_ShowInstrumentsRequested;
            documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
            documentPropertiesUserControl.CloseRequested += documentPropertiesUserControl_CloseRequested;
            documentPropertiesUserControl.LoseFocusRequested += documentPropertiesUserControl_LoseFocusRequested;
            audioFileOutputListUserControl.CloseRequested += audioFileOutputListUserControl_CloseRequested;
            audioFileOutputListUserControl.CreateRequested += audioFileOutputListUserControl_CreateRequested;
            audioFileOutputListUserControl.DeleteRequested += audioFileOutputListUserControl_DeleteRequested;
            instrumentListUserControl.CloseRequested += instrumentListUserControl_CloseRequested;
            instrumentListUserControl.CreateRequested += instrumentListUserControl_CreateRequested;
            instrumentListUserControl.DeleteRequested += instrumentListUserControl_DeleteRequested;


            _documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;

            _audioFileOutputPropertiesForm.CloseRequested += _audioFileOutputPropertiesForm_CloseRequested;
            _curveListForm.CloseRequested += _curveListForm_CloseRequested;
            _patchListForm.CloseRequested += _patchListForm_CloseRequested;
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
            //ShowAudioFileOutputProperties();
            //ShowPatchDetails();
        }

        private void SetTitles()
        {
            // Does nothing for now, but keep it in there, because I keep refactoring it in and out of the code.
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
                PersistenceHelper.CreateRepository<IEntityPositionRepository>(_context),
                PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(_context),
                PersistenceHelper.CreateRepository<IInterpolationTypeRepository>(_context),
                PersistenceHelper.CreateRepository<INodeTypeRepository>(_context),
                PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(_context),
                PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(_context)
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

        // Actions

        // General Actions

        private void Open()
        {
            _viewModel = _presenter.Show();
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

        // Document Actions

        private void DocumentTreeShow()
        {
            _viewModel = _presenter.DocumentTreeShow(_viewModel);
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

        // Document Tree Actions

        private void DocumentTreeClose()
        {
            _viewModel = _presenter.DocumentTreeClose(_viewModel);
            ApplyViewModel();
        }

        private void DocumentTreeExpandNode(int listIndex)
        {
            _viewModel = _presenter.DocumentTreeExpandNode(_viewModel, listIndex);
            // I can get away not applying view model, because the TreeView control already expanded the node.
            // We just need to remember it in the view model. That's why we have to call the presenter.
            ApplyViewModel();
        }

        private void DocumentTreeCollapseNode(int listIndex)
        {
            _viewModel = _presenter.DocumentTreeCollapseNode(_viewModel, listIndex);
            // I can get away not applying view model, because the TreeView control already expanded the node.
            // We just need to remember it in the view model. That's why we have to call the presenter.
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

        private void DocumentPropertiesLoseFocus()
        {
            _viewModel = _presenter.DocumentPropertiesLoseFocus(_viewModel);
            ApplyViewModel();
        }

        // Other Open Document Actions

        private void AudioFileOutputListShow()
        {
            _viewModel = _presenter.AudioFileOutputListShow(_viewModel);
            ApplyViewModel();
        }

        private void AudioFileOutputDelete(int listIndex)
        {
            _viewModel = _presenter.AudioFileOutputDelete(_viewModel, listIndex);
            ApplyViewModel();
        }

        private void AudioFileOutputCreate()
        {
            _viewModel = _presenter.AudioFileOutputCreate(_viewModel);
            ApplyViewModel();
        }

        private void AudioFileOutputListClose()
        {
            _viewModel = _presenter.AudioFileOutputListClose(_viewModel);
            ApplyViewModel();
        }

        private void InstrumentListShow()
        {
            _viewModel = _presenter.InstrumentListShow(_viewModel);
            ApplyViewModel();
        }

        private void InstrumentListClose()
        {
            _viewModel = _presenter.InstrumentListClose(_viewModel);
            ApplyViewModel();
        }

        private void InstrumentCreate()
        {
            _viewModel = _presenter.InstrumentCreate(_viewModel);
            ApplyViewModel();
        }

        private void InstrumentListDelete(int listIndex)
        {
            _viewModel = _presenter.InstrumentDelete(_viewModel, listIndex);
            ApplyViewModel();
        }

        // Other Actions

        private void CurveListShow(int pageNumber)
        {
            _viewModel = _presenter.CurveListShow(_viewModel, null, null); // TODO: Also use _presenter.CurveListShow for showing CurveLists of ChildDocuments.
            ApplyViewModel();
        }

        private void PatchListShow(int pageNumber)
        {
            _viewModel = _presenter.PatchListShow(_viewModel, null, null); // TODO: Also use _presenter.PatchListShow for showing PatchLists of ChildDocuments.
            ApplyViewModel();
        }

        private void SampleListShow(int pageNumber)
        {
            _viewModel = _presenter.SampleListShow(_viewModel, null, null); // TODO: Also use _presenter.SampleListShow for showing SampleLists of ChildDocuments.
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesEdit(int id)
        {
            _viewModel = _presenter.AudioFileOutputPropertiesEdit(_viewModel, id);
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

        private void AudioFileOutputPropertiesClose()
        {
            _viewModel = _presenter.AudioFileOutputPropertiesClose(_viewModel, listIndex: 0); // TODO: Use the right ListIndex.
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
            DocumentTreeShow();
        }

        private void menuUserControl_ShowAudioFileOutputListRequested(object sender, EventArgs e)
        {
            AudioFileOutputListShow();
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

        private void menuUserControl_AudioFileOutputEditRequested(object sender, EventArgs e)
        {
            int dummyID = 0;
            AudioFileOutputPropertiesEdit(dummyID);
        }

        private void menuUserControl_PatchDetailsRequested(object sender, EventArgs e)
        {
            ConfigurationSection config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            int testPatchID = config.Testing.TestPatchID;

            // TODO: Use the outcommented code line again once patch details becomes part of regular program navigation.
            _patchDetailsForm.Show();
            //PatchDetailsEdit(testPatchID);
        }

        // Document List Events

        private void documentListUserControl_ShowRequested(object sender, Int32EventArgs e)
        {
            DocumentListShow(e.Int32);
        }

        private void documentListUserControl_CreateRequested(object sender, EventArgs e)
        {
            DocumentDetailsCreate();
        }

        private void documentListUserControl_OpenRequested(object sender, Int32EventArgs e)
        {
            DocumentOpen(e.Int32);
        }

        private void documentListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            DocumentDelete(e.Int32);
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

        private void documentDetailsUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            DocumentDelete(e.Int32);
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

        private void documentTreeUserControl_DocumentPropertiesRequested(object sender, Int32EventArgs e)
        {
            DocumentPropertiesShow(e.Int32);
        }

        private void documentTreeUserControl_ExpandNodeRequested(object sender, Int32EventArgs e)
        {
            DocumentTreeExpandNode(e.Int32);
        }

        private void documentTreeUserControl_CollapseNodeRequested(object sender, Int32EventArgs e)
        {
            DocumentTreeCollapseNode(e.Int32);
        }

        private void documentTreeUserControl_ShowInstrumentsRequested(object sender, EventArgs e)
        {
            InstrumentListShow();
        }

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
        {
            AudioFileOutputListShow();
        }

        // Document Properties Events

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentPropertiesClose();
        }

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            DocumentPropertiesLoseFocus();
        }

        // Other Open Document Events

        private void audioFileOutputListUserControl_CreateRequested(object sender, EventArgs e)
        {
            AudioFileOutputCreate();
        }

        private void audioFileOutputListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputDelete(e.Int32);
        }

        private void audioFileOutputListUserControl_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputListClose();
        }

        private void instrumentListUserControl_CreateRequested(object sender, EventArgs e)
        {
            InstrumentCreate();
        }

        private void instrumentListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            InstrumentListDelete(e.Int32);
        }

        private void instrumentListUserControl_CloseRequested(object sender, EventArgs e)
        {
            InstrumentListClose();
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

        private void MessageBoxHelper_DocumentDeleteConfirmed(object sender, Int32EventArgs e)
        {
            DocumentConfirmDelete(e.Int32);
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

        private void _sampleListForm_ShowRequested(object sender, Int32EventArgs e)
        {
            SampleListShow(e.Int32);
        }

        private void _patchListForm_ShowRequested(object sender, Int32EventArgs e)
        {
            PatchListShow(e.Int32);
        }

        private void _curveListForm_ShowRequested(object sender, Int32EventArgs e)
        {
            CurveListShow(e.Int32);
        }

        private void _audioFileOutputPropertiesForm_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputPropertiesClose();
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
            Text = _viewModel.WindowTitle + _titleBarExtraText;

            menuUserControl.Show(_viewModel.Menu);

            documentListUserControl.ViewModel = _viewModel.DocumentList;
            documentListUserControl.Visible = _viewModel.DocumentList.Visible;

            documentDetailsUserControl.ViewModel = _viewModel.DocumentDetails;
            documentDetailsUserControl.Visible = _viewModel.DocumentDetails.Visible;

            documentTreeUserControl.ViewModel = _viewModel.Document.DocumentTree;
            documentTreeUserControl.Visible = _viewModel.Document.DocumentTree.Visible;

            documentPropertiesUserControl.ViewModel = _viewModel.Document.DocumentProperties;
            documentPropertiesUserControl.Visible = _viewModel.Document.DocumentProperties.Visible;

            audioFileOutputListUserControl.ViewModel = _viewModel.Document.AudioFileOutputList;
            audioFileOutputListUserControl.Visible = _viewModel.Document.AudioFileOutputList.Visible;

            instrumentListUserControl.ViewModel = _viewModel.Document.InstrumentList;
            instrumentListUserControl.Visible = _viewModel.Document.InstrumentList.Visible;

            //effectListUserControl.ViewModel = _viewModel.Effects;
            //effectListUserControl.Visible = _viewModel.Effects.Visible;

            _curveListForm.ViewModel = _viewModel.Document.CurveList;
            _curveListForm.Visible = _viewModel.Document.CurveList.Visible;

            _patchListForm.ViewModel = _viewModel.Document.PatchList;
            _patchListForm.Visible = _viewModel.Document.PatchList.Visible;

            _sampleListForm.ViewModel = _viewModel.Document.SampleList;
            _sampleListForm.Visible = _viewModel.Document.SampleList.Visible;

            _audioFileOutputPropertiesForm.ViewModel = _viewModel.TemporaryAudioFileOutputProperties;
            _audioFileOutputPropertiesForm.Visible = _viewModel.TemporaryAudioFileOutputProperties.Visible;

            _patchDetailsForm.ViewModel = _viewModel.TemporaryPatchDetails;
            _patchDetailsForm.Visible = _viewModel.TemporaryPatchDetails.Visible;

            bool treePanelMustBeVisible = _viewModel.Document.DocumentTree.Visible;
            SetTreePanelVisible(treePanelMustBeVisible);

            bool propertiesPanelMustBeVisible = _viewModel.Document.DocumentProperties.Visible;
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

            if (_viewModel.Document.DocumentProperties.Messages.Count > 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, _viewModel.Document.DocumentProperties.Messages.Select(x => x.Text)));
            }

            // TODO: This 'if' is a major hack.
            if (_viewModel.Document.DocumentProperties.Messages.Count != 0)
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
