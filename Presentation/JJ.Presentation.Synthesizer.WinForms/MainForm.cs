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
using ConfigurationSection = JJ.Presentation.Synthesizer.WinForms.Configuration.ConfigurationSection;
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
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private IContext _context;

        private RepositoryWrapper _repositoryWrapper;

        private MainPresenter _presenter;

        private MainViewModel _viewModel;

        private DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();
        //private AudioFileOutputPropertiesForm _audioFileOutputPropertiesForm = new AudioFileOutputPropertiesForm();
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
            _repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(_context);
            _presenter = new MainPresenter(_repositoryWrapper);

            menuUserControl.ShowDocumentListRequested += menuUserControl_ShowDocumentListRequested;
            menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
            menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
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
            documentTreeUserControl.ShowEffectsRequested += documentTreeUserControl_ShowEffectsRequested;
            documentTreeUserControl.ShowSamplesRequested += documentTreeUserControl_ShowSamplesRequested;
            documentTreeUserControl.ShowCurvesRequested += documentTreeUserControl_ShowCurvesRequested;
            documentTreeUserControl.ShowPatchesRequested += documentTreeUserControl_ShowPatchesRequested;
            documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
            documentPropertiesUserControl.CloseRequested += documentPropertiesUserControl_CloseRequested;
            documentPropertiesUserControl.LoseFocusRequested += documentPropertiesUserControl_LoseFocusRequested;

            audioFileOutputListUserControl.CloseRequested += audioFileOutputListUserControl_CloseRequested;
            audioFileOutputListUserControl.CreateRequested += audioFileOutputListUserControl_CreateRequested;
            audioFileOutputListUserControl.DeleteRequested += audioFileOutputListUserControl_DeleteRequested;
            audioFileOutputListUserControl.ShowPropertiesRequested += audioFileOutputListUserControl_ShowPropertiesRequested;
            audioFileOutputPropertiesUserControl.CloseRequested += audioFileOutputPropertiesUserControl_CloseRequested;
            audioFileOutputPropertiesUserControl.LoseFocusRequested += audioFileOutputPropertiesUserControl_LoseFocusRequested;
            curveListUserControl.CloseRequested += curveListUserControl_CloseRequested;
            curveListUserControl.CreateRequested += curveListUserControl_CreateRequested;
            curveListUserControl.DeleteRequested += curveListUserControl_DeleteRequested;
            instrumentListUserControl.CloseRequested += instrumentListUserControl_CloseRequested;
            instrumentListUserControl.CreateRequested += instrumentListUserControl_CreateRequested;
            instrumentListUserControl.DeleteRequested += instrumentListUserControl_DeleteRequested;
            effectListUserControl.CloseRequested += effectListUserControl_CloseRequested;
            effectListUserControl.CreateRequested += effectListUserControl_CreateRequested;
            effectListUserControl.DeleteRequested += effectListUserControl_DeleteRequested;
            patchListUserControl.CloseRequested += patchListUserControl_CloseRequested;
            patchListUserControl.CreateRequested += patchListUserControl_CreateRequested;
            patchListUserControl.DeleteRequested += patchListUserControl_DeleteRequested;
            sampleListUserControl.CloseRequested += sampleListUserControl_CloseRequested;
            sampleListUserControl.CreateRequested += sampleListUserControl_CreateRequested;
            sampleListUserControl.DeleteRequested += sampleListUserControl_DeleteRequested;
            sampleListUserControl.ShowPropertiesRequested += sampleListUserControl_ShowPropertiesRequested;
            samplePropertiesUserControl.CloseRequested += samplePropertiesUserControl_CloseRequested;
            samplePropertiesUserControl.LoseFocusRequested += samplePropertiesUserControl_LoseFocusRequested;

            _documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;

            _patchDetailsForm.CloseRequested += _patchDetailsForm_CloseRequested;

            _patchDetailsForm.Context = _context;

            MessageBoxHelper.NotFoundOK += MessageBoxHelper_NotFoundOK;
            MessageBoxHelper.DocumentDeleteConfirmed += MessageBoxHelper_DocumentDeleteConfirmed;
            MessageBoxHelper.DocumentDeleteCanceled += MessageBoxHelper_DocumentDeleteCanceled;
            MessageBoxHelper.DocumentDeletedOK += MessageBoxHelper_DocumentDeletedOK;
            MessageBoxHelper.PopupMessagesOK += MessageBoxHelper_PopupMessagesOK;

            ApplyStyling();

            Open();
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

        private void NotFoundOK()
        {
            _viewModel = _presenter.NotFoundOK(_viewModel);
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

        private void DocumentCannotDeleteOK()
        {
            _viewModel = _presenter.DocumentCannotDeleteOK(_viewModel);
            ApplyViewModel();
        }

        // Document Actions

        private void DocumentOpen(int id)
        {
            _viewModel = _presenter.DocumentOpen(_viewModel, id);
            ApplyViewModel();
        }

        private void DocumentClose()
        {
            _viewModel = _presenter.DocumentClose(_viewModel);
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

        private void PopupMessagesOK()
        {
            _viewModel = _presenter.PopupMessagesOK(_viewModel);
            ApplyViewModel();
        }

        private void DocumentCancelDelete()
        {
            _viewModel = _presenter.DocumentCancelDelete(_viewModel);
            ApplyViewModel();
        }

        // Document Tree Actions

        private void DocumentTreeShow()
        {
            _viewModel = _presenter.DocumentTreeShow(_viewModel);
            ApplyViewModel();
        }

        private void DocumentTreeClose()
        {
            _viewModel = _presenter.DocumentTreeClose(_viewModel);
            ApplyViewModel();
        }

        private void DocumentTreeExpandNode(int nodeIndex)
        {
            _viewModel = _presenter.DocumentTreeExpandNode(_viewModel, nodeIndex);
            ApplyViewModel();
        }

        private void DocumentTreeCollapseNode(int nodeIndex)
        {
            _viewModel = _presenter.DocumentTreeCollapseNode(_viewModel, nodeIndex);
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

        // AudioFileOutput Actions

        private void AudioFileOutputListShow()
        {
            _viewModel = _presenter.AudioFileOutputListShow(_viewModel);
            ApplyViewModel();
        }

        private void AudioFileOutputListClose()
        {
            _viewModel = _presenter.AudioFileOutputListClose(_viewModel);
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

        private void AudioFileOutputPropertiesShow(int listIndex)
        {
            _viewModel = _presenter.AudioFileOutputPropertiesShow(_viewModel, listIndex);
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesClose(int listIndex)
        {
            _viewModel = _presenter.AudioFileOutputPropertiesClose(_viewModel, listIndex);
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesLoseFocus(int listIndex)
        {
            _viewModel = _presenter.AudioFileOutputPropertiesLoseFocus(_viewModel, listIndex);
            ApplyViewModel();
        }

        // Curve Actions

        private void CurveListShow(ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.CurveListShow(_viewModel, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void CurveListClose()
        {
            _viewModel = _presenter.CurveListClose(_viewModel);
            ApplyViewModel();
        }

        private void CurveCreate(ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.CurveCreate(_viewModel, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void CurveDelete(int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.CurveDelete(_viewModel, listIndex, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        // Instrument Actions

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

        private void InstrumentDelete(int listIndex)
        {
            _viewModel = _presenter.InstrumentDelete(_viewModel, listIndex);
            ApplyViewModel();
        }

        // Effect Actions

        private void EffectListShow()
        {
            _viewModel = _presenter.EffectListShow(_viewModel);
            ApplyViewModel();
        }

        private void EffectListClose()
        {
            _viewModel = _presenter.EffectListClose(_viewModel);
            ApplyViewModel();
        }

        private void EffectCreate()
        {
            _viewModel = _presenter.EffectCreate(_viewModel);
            ApplyViewModel();
        }

        private void EffectDelete(int listIndex)
        {
            _viewModel = _presenter.EffectDelete(_viewModel, listIndex);
            ApplyViewModel();
        }

        // Patch Actions

        private void PatchListShow(ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.PatchListShow(_viewModel, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void PatchListClose()
        {
            _viewModel = _presenter.PatchListClose(_viewModel);
            ApplyViewModel();
        }

        private void PatchCreate(ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.PatchCreate(_viewModel, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void PatchDelete(int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.PatchDelete(_viewModel, listIndex, childDocumentTypeEnum, childDocumentListIndex);
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

        // Sample Actions

        private void SampleListShow(ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.SampleListShow(_viewModel, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void SampleListClose()
        {
            _viewModel = _presenter.SampleListClose(_viewModel);
            ApplyViewModel();
        }

        private void SampleCreate(ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.SampleCreate(_viewModel, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void SampleDelete(int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.SampleDelete(_viewModel, listIndex, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void SamplePropertiesShow(int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.SamplePropertiesShow(_viewModel, listIndex, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void SamplePropertiesClose(int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.SamplePropertiesClose(_viewModel, listIndex, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        private void SamplePropertiesLoseFocus(int listIndex, ChildDocumentTypeEnum? childDocumentTypeEnum, int? childDocumentListIndex)
        {
            _viewModel = _presenter.SamplePropertiesLoseFocus(_viewModel, listIndex, childDocumentTypeEnum, childDocumentListIndex);
            ApplyViewModel();
        }

        // Menu Events

        private void menuUserControl_ShowDocumentListRequested(object sender, EventArgs e)
        {
            DocumentListShow();
        }

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
        {
            DocumentTreeShow();
        }

        private void menuUserControl_DocumentCloseRequested(object sender, EventArgs e)
        {
            DocumentClose();
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

        private void documentTreeUserControl_ShowEffectsRequested(object sender, EventArgs e)
        {
            EffectListShow();
        }

        private void documentTreeUserControl_ShowSamplesRequested(object sender, ChildDocumentEventArgs e)
        {
            SampleListShow(e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void documentTreeUserControl_ShowCurvesRequested(object sender, ChildDocumentEventArgs e)
        {
            CurveListShow(e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void documentTreeUserControl_ShowPatchesRequested(object sender, ChildDocumentEventArgs e)
        {
            PatchListShow(e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
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

        // AudioFileOutput Events

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

        private void audioFileOutputListUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputPropertiesShow(e.Int32);
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputPropertiesClose(e.Int32);
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputPropertiesLoseFocus(e.Int32);
        }

        // Curve Events

        private void curveListUserControl_CreateRequested(object sender, ChildDocumentEventArgs e)
        {
            CurveCreate(e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void curveListUserControl_DeleteRequested(object sender, ChildDocumentSubListItemEventArgs e)
        {
            CurveDelete(e.ListIndex, e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void curveListUserControl_CloseRequested(object sender, EventArgs e)
        {
            CurveListClose();
        }

        // Instrument Events

        private void instrumentListUserControl_CreateRequested(object sender, EventArgs e)
        {
            InstrumentCreate();
        }

        private void instrumentListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            InstrumentDelete(e.Int32);
        }

        private void instrumentListUserControl_CloseRequested(object sender, EventArgs e)
        {
            InstrumentListClose();
        }

        // Effect Events

        private void effectListUserControl_CreateRequested(object sender, EventArgs e)
        {
            EffectCreate();
        }

        private void effectListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            EffectDelete(e.Int32);
        }

        private void effectListUserControl_CloseRequested(object sender, EventArgs e)
        {
            EffectListClose();
        }

        // Sample Events

        private void sampleListUserControl_CreateRequested(object sender, ChildDocumentEventArgs e)
        {
            SampleCreate(e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void sampleListUserControl_DeleteRequested(object sender, ChildDocumentSubListItemEventArgs e)
        {
            SampleDelete(e.ListIndex, e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void sampleListUserControl_CloseRequested(object sender, EventArgs e)
        {
            SampleListClose();
        }

        private void sampleListUserControl_ShowPropertiesRequested(object sender, ChildDocumentSubListItemEventArgs e)
        {
            SamplePropertiesShow(e.ListIndex, e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, ChildDocumentSubListItemEventArgs e)
        {
            SamplePropertiesLoseFocus(e.ListIndex, e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, ChildDocumentSubListItemEventArgs e)
        {
            SamplePropertiesClose(e.ListIndex, e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        // Patch Events

        private void patchListUserControl_CreateRequested(object sender, ChildDocumentEventArgs e)
        {
            PatchCreate(e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void patchListUserControl_DeleteRequested(object sender, ChildDocumentSubListItemEventArgs e)
        {
            PatchDelete(e.ListIndex, e.ChildDocumentTypeEnum, e.ChildDocumentListIndex);
        }

        private void patchListUserControl_CloseRequested(object sender, EventArgs e)
        {
            PatchListClose();
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

        private void MessageBoxHelper_PopupMessagesOK(object sender, EventArgs e)
        {
            PopupMessagesOK();
        }

        // DocumentCannotDeleteForm Events

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e)
        {
            DocumentCannotDeleteOK();
        }

        // Temporary Form Events

        private void _patchDetailsForm_CloseRequested(object sender, EventArgs e)
        {
            PatchDetailsClose();
        }

        // Helpers

        private void ApplyViewModel()
        {
            SuspendLayout();

            try
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

                // AudioFileOutputPropertiesViewModel
                bool audioFileOutputPropertiesVisible = false;
                AudioFileOutputPropertiesViewModel visibleAudioFileOutputPropertiesViewModel =
                    _viewModel.Document.AudioFileOutputPropertiesList.Where(x => x.Visible).SingleOrDefault();
                if (visibleAudioFileOutputPropertiesViewModel != null)
                {
                    audioFileOutputPropertiesUserControl.ViewModel = visibleAudioFileOutputPropertiesViewModel;
                    audioFileOutputPropertiesVisible = true;
                }
                audioFileOutputPropertiesUserControl.Visible = audioFileOutputPropertiesVisible;

                // CurveListViewModel
                bool curveListVisible = false;
                if (_viewModel.Document.CurveList.Visible)
                {
                    curveListUserControl.ViewModel = _viewModel.Document.CurveList;
                    curveListVisible = true;
                }
                else
                {
                    CurveListViewModel visibleCurveListViewModel = Enumerable.Union(_viewModel.Document.InstrumentDocumentList.Select(x => x.CurveList),
                                                                                    _viewModel.Document.EffectDocumentList.Select(x => x.CurveList))
                                                                             .Where(x => x.Visible)
                                                                             .SingleOrDefault();
                    if (visibleCurveListViewModel != null)
                    {
                        curveListUserControl.ViewModel = visibleCurveListViewModel;
                        curveListVisible = true;
                    }
                }
                curveListUserControl.Visible = curveListVisible;

                instrumentListUserControl.ViewModel = _viewModel.Document.InstrumentList;
                instrumentListUserControl.Visible = _viewModel.Document.InstrumentList.Visible;

                effectListUserControl.ViewModel = _viewModel.Document.EffectList;
                effectListUserControl.Visible = _viewModel.Document.EffectList.Visible;

                // PatchListViewModel
                bool patchListVisible = false;
                if (_viewModel.Document.PatchList.Visible)
                {
                    patchListUserControl.ViewModel = _viewModel.Document.PatchList;
                    patchListVisible = true;
                }
                else
                {
                    PatchListViewModel visiblePatchListViewModel = Enumerable.Union(_viewModel.Document.InstrumentDocumentList.Select(x => x.PatchList),
                                                                                    _viewModel.Document.EffectDocumentList.Select(x => x.PatchList))
                                                                             .Where(x => x.Visible)
                                                                             .SingleOrDefault();
                    if (visiblePatchListViewModel != null)
                    {
                        patchListUserControl.ViewModel = visiblePatchListViewModel;
                        patchListVisible = true;
                    }
                }
                patchListUserControl.Visible = patchListVisible;

                // SampleListViewModel
                bool sampleListVisible = false;
                if (_viewModel.Document.SampleList.Visible)
                {
                    sampleListUserControl.ViewModel = _viewModel.Document.SampleList;
                    sampleListVisible = true;
                }
                else
                {
                    SampleListViewModel visibleSampleListViewModel = Enumerable.Union(_viewModel.Document.InstrumentDocumentList.Select(x => x.SampleList),
                                                                                      _viewModel.Document.EffectDocumentList.Select(x => x.SampleList))
                                                                               .Where(x => x.Visible)
                                                                               .SingleOrDefault();
                    if (visibleSampleListViewModel != null)
                    {
                        sampleListUserControl.ViewModel = visibleSampleListViewModel;
                        sampleListVisible = true;
                    }
                }
                sampleListUserControl.Visible = sampleListVisible;

                // SamplePropertiesViewModel
                bool samplePropertiesVisible = false;
                SamplePropertiesViewModel visibleSamplePropertiesViewModel =
                    _viewModel.Document.SamplePropertiesList.Where(x => x.Visible).SingleOrDefault();
                if (visibleSamplePropertiesViewModel != null)
                {
                    samplePropertiesUserControl.ViewModel = visibleSamplePropertiesViewModel;
                    samplePropertiesVisible = true;
                }
                else
                {
                    visibleSamplePropertiesViewModel = Enumerable.Union(_viewModel.Document.InstrumentDocumentList.SelectMany(x => x.SamplePropertiesList),
                                                                        _viewModel.Document.EffectDocumentList.SelectMany(x => x.SamplePropertiesList))
                                                                 .Where(x => x.Visible)
                                                                 .SingleOrDefault();
                    if (visibleSamplePropertiesViewModel != null)
                    {
                        samplePropertiesUserControl.ViewModel = visibleSamplePropertiesViewModel;
                        samplePropertiesVisible = true;
                    }
                }
                samplePropertiesUserControl.Visible = samplePropertiesVisible;

                _patchDetailsForm.ViewModel = _viewModel.TemporaryPatchDetails;
                _patchDetailsForm.Visible = _viewModel.TemporaryPatchDetails.Visible;

                bool treePanelMustBeVisible = _viewModel.Document.DocumentTree.Visible;
                SetTreePanelVisible(treePanelMustBeVisible);

                // TODO: Make panel visibility dependent on more things.
                bool propertiesPanelMustBeVisible = _viewModel.Document.DocumentProperties.Visible || 
                                                    audioFileOutputPropertiesVisible ||
                                                    samplePropertiesVisible;

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

                if (_viewModel.ValidationMessages.Count != 0)
                {
                    MessageBox.Show(String.Join(Environment.NewLine, _viewModel.ValidationMessages.Select(x => x.Text)));
                }

                if (_viewModel.PopupMessages.Count != 0)
                {
                    MessageBoxHelper.ShowPopupMessages(_viewModel.PopupMessages);
                }

                // Focus control if not valid.
                if (!_viewModel.Document.DocumentProperties.Successful)
                {
                    documentPropertiesUserControl.Focus();
                }

                bool mustFocusAudioFileOutputUserControl = _viewModel.Document.AudioFileOutputPropertiesList.Any(x => !x.Successful);
                if (mustFocusAudioFileOutputUserControl)
                {
                    audioFileOutputPropertiesUserControl.Focus();
                }

                bool mustFocusSampleUserControl = _viewModel.Document.SamplePropertiesList.Any(x => !x.Successful) ||
                                                  _viewModel.Document.InstrumentDocumentList.SelectMany(x => x.SamplePropertiesList).Any(x => !x.Successful) ||
                                                  _viewModel.Document.EffectDocumentList.SelectMany(x => x.SamplePropertiesList).Any(x => !x.Successful);
                if (mustFocusSampleUserControl)
                {
                    samplePropertiesUserControl.Focus();
                }
            }
            finally
            {
                ResumeLayout();
            }
        }

        private void SetTreePanelVisible(bool visible)
        {
            splitContainerTree.Panel1Collapsed = !visible;
        }

        private void SetPropertiesPanelVisible(bool visible)
        {
            splitContainerProperties.Panel2Collapsed = !visible;
        }

        private void ApplyStyling()
        {
            splitContainerProperties.SplitterWidth = WinFormsThemeHelper.DefaultSpacing;
            splitContainerTree.SplitterWidth = WinFormsThemeHelper.DefaultSpacing;
        }
    }
}
