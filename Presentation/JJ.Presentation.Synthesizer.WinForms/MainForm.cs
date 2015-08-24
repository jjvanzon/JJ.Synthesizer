using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.WinForms.Forms;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using ConfigurationSection = JJ.Presentation.Synthesizer.WinForms.Configuration.ConfigurationSection;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private IContext _context;
        private RepositoryWrapper _repositoryWrapper;
        private MainPresenter _presenter;

        private DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();
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

            audioFileOutputGridUserControl.CloseRequested += audioFileOutputGridUserControl_CloseRequested;
            audioFileOutputGridUserControl.CreateRequested += audioFileOutputGridUserControl_CreateRequested;
            audioFileOutputGridUserControl.DeleteRequested += audioFileOutputGridUserControl_DeleteRequested;
            audioFileOutputGridUserControl.ShowPropertiesRequested += audioFileOutputGridUserControl_ShowPropertiesRequested;
            audioFileOutputPropertiesUserControl.CloseRequested += audioFileOutputPropertiesUserControl_CloseRequested;
            audioFileOutputPropertiesUserControl.LoseFocusRequested += audioFileOutputPropertiesUserControl_LoseFocusRequested;
            childDocumentPropertiesUserControl.CloseRequested += childDocumentPropertiesUserControl_CloseRequested;
            childDocumentPropertiesUserControl.LoseFocusRequested += childDocumentPropertiesUserControl_LoseFocusRequested;
            curveGridUserControl.CloseRequested += curveGridUserControl_CloseRequested;
            curveGridUserControl.CreateRequested += curveGridUserControl_CreateRequested;
            curveGridUserControl.DeleteRequested += curveGridUserControl_DeleteRequested;
            documentDetailsUserControl.SaveRequested += documentDetailsUserControl_SaveRequested;
            documentDetailsUserControl.DeleteRequested += documentDetailsUserControl_DeleteRequested;
            documentDetailsUserControl.CloseRequested += documentDetailsUserControl_CloseRequested;
            documentGridUserControl.ShowRequested += documentGridUserControl_ShowRequested;
            documentGridUserControl.CloseRequested += documentGridUserControl_CloseRequested;
            documentGridUserControl.CreateRequested += documentGridUserControl_CreateRequested;
            documentGridUserControl.OpenRequested += documentGridUserControl_OpenRequested;
            documentGridUserControl.DeleteRequested += documentGridUserControl_DeleteRequested;
            documentPropertiesUserControl.CloseRequested += documentPropertiesUserControl_CloseRequested;
            documentPropertiesUserControl.LoseFocusRequested += documentPropertiesUserControl_LoseFocusRequested;
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
            documentTreeUserControl.ShowChildDocumentPropertiesRequested += documentTreeUserControl_ShowChildDocumentPropertiesRequested;
            effectGridUserControl.CloseRequested += effectGridUserControl_CloseRequested;
            effectGridUserControl.CreateRequested += effectGridUserControl_CreateRequested;
            effectGridUserControl.DeleteRequested += effectGridUserControl_DeleteRequested;
            effectGridUserControl.ShowPropertiesRequested += effectGridUserControl_ShowPropertiesRequested;
            instrumentGridUserControl.CloseRequested += instrumentGridUserControl_CloseRequested;
            instrumentGridUserControl.CreateRequested += instrumentGridUserControl_CreateRequested;
            instrumentGridUserControl.DeleteRequested += instrumentGridUserControl_DeleteRequested;
            instrumentGridUserControl.ShowPropertiesRequested += instrumentGridUserControl_ShowPropertiesRequested;
            menuUserControl.ShowDocumentListRequested += menuUserControl_ShowDocumentListRequested;
            menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
            menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
            menuUserControl.DocumentSaveRequested += menuUserControl_DocumentSaveRequested;
            operatorPropertiesUserControl.CloseRequested += operatorPropertiesUserControl_CloseRequested;
            operatorPropertiesUserControl.LoseFocusRequested += operatorPropertiesUserControl_LoseFocusRequested;
            operatorPropertiesUserControl_ForCustomOperator.CloseRequested += operatorPropertiesUserControl_ForCustomOperator_CloseRequested;
            operatorPropertiesUserControl_ForCustomOperator.LoseFocusRequested += operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchInlet.CloseRequested += operatorPropertiesUserControl_ForPatchInlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchInlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchOutlet.CloseRequested += operatorPropertiesUserControl_ForPatchOutlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchOutlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForValue.CloseRequested += operatorPropertiesUserControl_ForValue_CloseRequested;
            operatorPropertiesUserControl_ForValue.LoseFocusRequested += operatorPropertiesUserControl_ForValue_LoseFocusRequested;
            patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
            patchDetailsUserControl.LoseFocusRequested += patchDetailsUserControl_LoseFocusRequested;
            patchDetailsUserControl.DeleteOperatorRequested += patchDetailsUserControl_DeleteOperatorRequested;
            patchDetailsUserControl.CreateOperatorRequested += patchDetailsUserControl_CreateOperatorRequested;
            patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
            patchDetailsUserControl.ChangeInputOutletRequested += patchDetailsUserControl_ChangeInputOutletRequested;
            patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
            patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
            patchDetailsUserControl.OperatorPropertiesRequested += patchDetailsUserControl_OperatorPropertiesRequested;
            patchGridUserControl.CloseRequested += patchGridUserControl_CloseRequested;
            patchGridUserControl.CreateRequested += patchGridUserControl_CreateRequested;
            patchGridUserControl.DeleteRequested += patchGridUserControl_DeleteRequested;
            patchGridUserControl.ShowDetailsRequested += patchGridUserControl_ShowDetailsRequested;
            sampleGridUserControl.CloseRequested += sampleGridUserControl_CloseRequested;
            sampleGridUserControl.CreateRequested += sampleGridUserControl_CreateRequested;
            sampleGridUserControl.DeleteRequested += sampleGridUserControl_DeleteRequested;
            sampleGridUserControl.ShowPropertiesRequested += sampleGridUserControl_ShowPropertiesRequested;
            samplePropertiesUserControl.CloseRequested += samplePropertiesUserControl_CloseRequested;
            samplePropertiesUserControl.LoseFocusRequested += samplePropertiesUserControl_LoseFocusRequested;

            _documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;

            MessageBoxHelper.NotFoundOK += MessageBoxHelper_NotFoundOK;
            MessageBoxHelper.DocumentDeleteConfirmed += MessageBoxHelper_DocumentDeleteConfirmed;
            MessageBoxHelper.DocumentDeleteCanceled += MessageBoxHelper_DocumentDeleteCanceled;
            MessageBoxHelper.DocumentDeletedOK += MessageBoxHelper_DocumentDeletedOK;
            MessageBoxHelper.PopupMessagesOK += MessageBoxHelper_PopupMessagesOK;

            SetTitles();
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

        // Events


        // AudioFileOutput Events

        private void audioFileOutputGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            AudioFileOutputCreate();
        }

        private void audioFileOutputGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputDelete(e.Value);
        }

        private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputListClose();
        }

        private void audioFileOutputGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputPropertiesShow(e.Value);
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputPropertiesClose();
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            AudioFileOutputPropertiesLoseFocus();
        }

        // Child Document Events

        private void childDocumentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            ChildDocumentPropertiesLoseFocus();
        }

        private void childDocumentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            ChildDocumentPropertiesClose();
        }

        // Curve Events

        private void curveGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            CurveCreate(e.Value);
        }

        private void curveGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            CurveDelete(e.Value);
        }

        private void curveGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            CurveListClose();
        }

        // Document List Events

        private void documentGridUserControl_ShowRequested(object sender, Int32EventArgs e)
        {
            DocumentListShow(e.Value);
        }

        private void documentGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            DocumentDetailsCreate();
        }

        private void documentGridUserControl_OpenRequested(object sender, Int32EventArgs e)
        {
            DocumentOpen(e.Value);
        }

        private void documentGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            DocumentDelete(e.Value);
        }

        private void documentGridUserControl_CloseRequested(object sender, EventArgs e)
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
            DocumentDelete(e.Value);
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

        private void documentTreeUserControl_DocumentPropertiesRequested(object sender, EventArgs e)
        {
            DocumentPropertiesShow();
        }

        private void documentTreeUserControl_ExpandNodeRequested(object sender, Int32EventArgs e)
        {
            DocumentTreeExpandNode(e.Value);
        }

        private void documentTreeUserControl_CollapseNodeRequested(object sender, Int32EventArgs e)
        {
            DocumentTreeCollapseNode(e.Value);
        }

        private void documentTreeUserControl_ShowInstrumentsRequested(object sender, EventArgs e)
        {
            InstrumentListShow();
        }

        private void documentTreeUserControl_ShowEffectsRequested(object sender, EventArgs e)
        {
            EffectListShow();
        }

        private void documentTreeUserControl_ShowSamplesRequested(object sender, Int32EventArgs e)
        {
            SampleListShow(e.Value);
        }

        private void documentTreeUserControl_ShowCurvesRequested(object sender, Int32EventArgs e)
        {
            CurveListShow(e.Value);
        }

        private void documentTreeUserControl_ShowPatchesRequested(object sender, Int32EventArgs e)
        {
            PatchListShow(e.Value);
        }

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
        {
            AudioFileOutputListShow();
        }

        private void documentTreeUserControl_ShowChildDocumentPropertiesRequested(object sender, Int32EventArgs e)
        {
            ChildDocumentPropertiesShow(e.Value);
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

        // Effect Events

        private void effectGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            EffectCreate();
        }

        private void effectGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            EffectDelete(e.Value);
        }

        private void effectGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            EffectListClose();
        }

        private void effectGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            ChildDocumentPropertiesShow(e.Value);
        }

        // Instrument Events

        private void instrumentGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            InstrumentCreate();
        }

        private void instrumentGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            InstrumentDelete(e.Value);
        }

        private void instrumentGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            InstrumentListClose();
        }

        private void instrumentGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            ChildDocumentPropertiesShow(e.Value);
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

        private void menuUserControl_DocumentSaveRequested(object sender, EventArgs e)
        {
            DocumentSave();
        }

        // Operator Events

        private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus();
        }

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose();
        }

        private void operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForCustomOperator();
        }

        private void operatorPropertiesUserControl_ForCustomOperator_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForCustomOperator();
        }

        private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForPatchInlet();
        }

        private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForPatchInlet();
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForPatchOutlet();
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForPatchOutlet();
        }

        private void operatorPropertiesUserControl_ForValue_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForValue();
        }

        private void operatorPropertiesUserControl_ForValue_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForValue();
        }

        // Patch Events

        private void patchGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            PatchCreate(e.Value);
        }

        private void patchGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            PatchDelete(e.Value);
        }

        private void patchGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            PatchListClose();
        }

        private void patchGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            PatchDetailsShow(e.Value);
        }

        private void patchDetailsUserControl_PlayRequested(object sender, EventArgs e)
        {
            PatchPlay();
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, Int32EventArgs e)
        {
            PatchDetailsSelectOperator(e.Value);
        }

        private void patchDetailsUserControl_OperatorPropertiesRequested(object sender, Int32EventArgs e)
        {
            OperatorPropertiesShow(e.Value);
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            PatchDetailsChangeInputOutlet(e.InletID, e.InputOutletID);
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
        {
            PatchDetailsMoveOperator(e.OperatorID, e.CenterX, e.CenterY);
        }

        private void patchDetailsUserControl_CreateOperatorRequested(object sender, CreateOperatorEventArgs e)
        {
            OperatorCreate(e.OperatorTypeID);
        }

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs e)
        {
            OperatorDelete();
        }

        private void patchDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            PatchDetailsLoseFocus();
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            PatchDetailsClose();
        }

        // Sample Events

        private void sampleGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            SampleCreate(e.Value);
        }

        private void sampleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            SampleDelete(e.Value);
        }

        private void sampleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            SampleListClose();
        }

        private void sampleGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            SamplePropertiesShow(e.Value);
        }

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            SamplePropertiesLoseFocus();
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            SamplePropertiesClose();
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
            DocumentConfirmDelete(e.Value);
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

        // Actions

        // General Actions

        private void Open()
        {
            _presenter.Show();
            ApplyViewModel();
        }

        private void NotFoundOK()
        {
            _presenter.NotFoundOK();
            ApplyViewModel();
        }

        // AudioFileOutput Actions

        private void AudioFileOutputListShow()
        {
            _presenter.AudioFileOutputGridShow();
            ApplyViewModel();
        }

        private void AudioFileOutputListClose()
        {
            _presenter.AudioFileOutputGridClose();
            ApplyViewModel();
        }

        private void AudioFileOutputDelete(int id)
        {
            _presenter.AudioFileOutputDelete(id);
            ApplyViewModel();
        }

        private void AudioFileOutputCreate()
        {
            _presenter.AudioFileOutputCreate();
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesShow(int id)
        {
            _presenter.AudioFileOutputPropertiesShow(id);
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesClose()
        {
            _presenter.AudioFileOutputPropertiesClose();
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesLoseFocus()
        {
            _presenter.AudioFileOutputPropertiesLoseFocus();
            ApplyViewModel();
        }

        // ChildDocument Actions

        private void ChildDocumentPropertiesShow(int childDocumentID)
        {
            _presenter.ChildDocumentPropertiesShow(childDocumentID);
            ApplyViewModel();
        }

        private void ChildDocumentPropertiesClose()
        {
            _presenter.ChildDocumentPropertiesClose();
            ApplyViewModel();
        }

        private void ChildDocumentPropertiesLoseFocus()
        {
            _presenter.ChildDocumentPropertiesLoseFocus();
            ApplyViewModel();
        }

        // Curve Actions

        private void CurveListShow(int documentID)
        {
            _presenter.CurveGridShow(documentID);
            ApplyViewModel();
        }

        private void CurveListClose()
        {
            _presenter.CurveGridClose();
            ApplyViewModel();
        }

        private void CurveCreate(int documentID)
        {
            _presenter.CurveCreate(documentID);
            ApplyViewModel();
        }

        private void CurveDelete(int curveID)
        {
            _presenter.CurveDelete(curveID);
            ApplyViewModel();
        }

        // Document List Actions

        private void DocumentListShow(int pageNumber = 1)
        {
            _presenter.DocumentGridShow(pageNumber);
            ApplyViewModel();
        }

        private void DocumentListClose()
        {
            _presenter.DocumentGridClose();
            ApplyViewModel();
        }

        // Document Details Actions

        private void DocumentDetailsCreate()
        {
            _presenter.DocumentDetailsCreate();
            ApplyViewModel();
        }

        private void DocumentDetailsSave(DocumentDetailsViewModel viewModel)
        {
            // TODO: Not sure how much this will still work in a stateless environment.
            _presenter.ViewModel.DocumentDetails = viewModel;

            _presenter.DocumentDetailsSave();
            ApplyViewModel();
        }

        private void DocumentDetailsClose()
        {
            _presenter.DocumentDetailsClose();
            ApplyViewModel();
        }

        private void DocumentCannotDeleteOK()
        {
            _presenter.DocumentCannotDeleteOK();
            ApplyViewModel();
        }

        // Document Actions

        private void DocumentOpen(int id)
        {
            _presenter.DocumentOpen(id);
            ApplyViewModel();
        }

        private void DocumentClose()
        {
            _presenter.DocumentClose();
            ApplyViewModel();
        }

        private void DocumentDelete(int id)
        {
            _presenter.DocumentDelete(id);
            ApplyViewModel();
        }

        private void DocumentConfirmDelete(int id)
        {
            _presenter.DocumentConfirmDelete(id);
            ApplyViewModel();
        }

        private void DocumentDeletedOK()
        {
            _presenter.DocumentDeletedOK();
            ApplyViewModel();
        }

        private void PopupMessagesOK()
        {
            _presenter.PopupMessagesOK();
            ApplyViewModel();
        }

        private void DocumentCancelDelete()
        {
            _presenter.DocumentCancelDelete();
            ApplyViewModel();
        }

        private void DocumentSave()
        {
            _presenter.DocumentSave();
            ApplyViewModel();
        }

        // Document Tree Actions

        private void DocumentTreeShow()
        {
            _presenter.DocumentTreeShow();
            ApplyViewModel();
        }

        private void DocumentTreeClose()
        {
            _presenter.DocumentTreeClose();
            ApplyViewModel();
        }

        private void DocumentTreeExpandNode(int nodeIndex)
        {
            _presenter.DocumentTreeExpandNode(nodeIndex);
            ApplyViewModel();
        }

        private void DocumentTreeCollapseNode(int nodeIndex)
        {
            _presenter.DocumentTreeCollapseNode(nodeIndex);
            ApplyViewModel();
        }

        // Document Properties Actions

        private void DocumentPropertiesShow()
        {
            _presenter.DocumentPropertiesShow();
            ApplyViewModel();
        }

        private void DocumentPropertiesClose()
        {
            _presenter.DocumentPropertiesClose();
            ApplyViewModel();
        }

        private void DocumentPropertiesLoseFocus()
        {
            _presenter.DocumentPropertiesLoseFocus();
            ApplyViewModel();
        }

        // Effect Actions

        private void EffectListShow()
        {
            _presenter.EffectGridShow();
            ApplyViewModel();
        }

        private void EffectListClose()
        {
            _presenter.EffectGridClose();
            ApplyViewModel();
        }

        private void EffectCreate()
        {
            _presenter.EffectCreate();
            ApplyViewModel();
        }

        private void EffectDelete(int listIndex)
        {
            _presenter.EffectDelete(listIndex);
            ApplyViewModel();
        }

        // Instrument Actions

        private void InstrumentListShow()
        {
            _presenter.InstrumentGridShow();
            ApplyViewModel();
        }

        private void InstrumentListClose()
        {
            _presenter.InstrumentGridClose();
            ApplyViewModel();
        }

        private void InstrumentCreate()
        {
            _presenter.InstrumentCreate();
            ApplyViewModel();
        }

        private void InstrumentDelete(int id)
        {
            _presenter.InstrumentDelete(id);
            ApplyViewModel();
        }

        // Operator Actions

        private void OperatorPropertiesShow(int operatorID)
        {
            _presenter.OperatorPropertiesShow(operatorID);
            ApplyViewModel();
        }

        private void OperatorPropertiesClose()
        {
            _presenter.OperatorPropertiesClose();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus()
        {
            _presenter.OperatorPropertiesLoseFocus();
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForCustomOperator()
        {
            _presenter.OperatorPropertiesClose_ForCustomOperator();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForCustomOperator()
        {
            _presenter.OperatorPropertiesLoseFocus_ForCustomOperator();
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForPatchInlet()
        {
            _presenter.OperatorPropertiesClose_ForPatchInlet();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForPatchInlet()
        {
            _presenter.OperatorPropertiesLoseFocus_ForPatchInlet();
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForPatchOutlet()
        {
            _presenter.OperatorPropertiesClose_ForPatchOutlet();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForPatchOutlet()
        {
            _presenter.OperatorPropertiesLoseFocus_ForPatchOutlet();
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForValue()
        {
            _presenter.OperatorPropertiesClose_ForValue();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForValue()
        {
            _presenter.OperatorPropertiesLoseFocus_ForValue();
            ApplyViewModel();
        }

        private void OperatorCreate(int operatorTypeID)
        {
            _presenter.OperatorCreate(operatorTypeID);
            ApplyViewModel();
        }

        private void OperatorDelete()
        {
            _presenter.OperatorDelete();
            ApplyViewModel();
        }

        // Patch Actions

        private void PatchListShow(int documentID)
        {
            _presenter.PatchGridShow(documentID);
            ApplyViewModel();
        }

        private void PatchListClose()
        {
            _presenter.PatchGridClose();
            ApplyViewModel();
        }

        private void PatchCreate(int documentID)
        {
            _presenter.PatchCreate(documentID);
            ApplyViewModel();
        }

        private void PatchDelete(int patchID)
        {
            _presenter.PatchDelete(patchID);
            ApplyViewModel();
        }

        private void PatchDetailsShow(int patchID)
        {
            _presenter.PatchDetailsShow(patchID);
            ApplyViewModel();
        }

        private void PatchDetailsClose()
        {
            _presenter.PatchDetailsClose();
            ApplyViewModel();
        }

        private void PatchDetailsLoseFocus()
        {
            _presenter.PatchDetailsLoseFocus();
            ApplyViewModel();
        }

        private void PatchDetailsMoveOperator(int operatorID, float centerX, float centerY)
        {
            _presenter.PatchDetailsMoveOperator(operatorID, centerX, centerY);
            ApplyViewModel();
        }

        private void PatchDetailsChangeInputOutlet(int inletID, int inputOutletID)
        {
            _presenter.PatchDetailsChangeInputOutlet(inletID, inputOutletID);
            ApplyViewModel();
        }

        private void PatchDetailsSelectOperator(int operatorID)
        {
            _presenter.PatchDetailsSelectOperator(operatorID);
            ApplyViewModel();
        }

        private void PatchPlay()
        {
            string outputFilePath = _presenter.PatchPlay();

            ApplyViewModel();

            if (_presenter.ViewModel.Successful)
            {
                SoundPlayer soundPlayer = new SoundPlayer(outputFilePath);
                soundPlayer.Play();
            }
        }

        // Sample Actions

        private void SampleListShow(int documentID)
        {
            _presenter.SampleGridShow(documentID);
            ApplyViewModel();
        }

        private void SampleListClose()
        {
            _presenter.SampleGridClose();
            ApplyViewModel();
        }

        private void SampleCreate(int documentID)
        {
            _presenter.SampleCreate(documentID);
            ApplyViewModel();
        }

        private void SampleDelete(int sampleID)
        {
            _presenter.SampleDelete(sampleID);
            ApplyViewModel();
        }

        private void SamplePropertiesShow(int sampleID)
        {
            _presenter.SamplePropertiesShow(sampleID);
            ApplyViewModel();
        }

        private void SamplePropertiesClose()
        {
            _presenter.SamplePropertiesClose();
            ApplyViewModel();
        }

        private void SamplePropertiesLoseFocus()
        {
            _presenter.SamplePropertiesLoseFocus();
            ApplyViewModel();
        }

        // Helpers

        private void ApplyViewModel()
        {
            SuspendLayout();

            try
            {
                Text = _presenter.ViewModel.WindowTitle + _titleBarExtraText;

                menuUserControl.Show(_presenter.ViewModel.Menu);

                audioFileOutputGridUserControl.ViewModel = _presenter.ViewModel.Document.AudioFileOutputGrid;
                audioFileOutputGridUserControl.Visible = _presenter.ViewModel.Document.AudioFileOutputGrid.Visible;

                // AudioFileOutputPropertiesViewModel
                bool audioFileOutputPropertiesVisible = false;
                AudioFileOutputPropertiesViewModel visibleAudioFileOutputPropertiesViewModel =
                    _presenter.ViewModel.Document.AudioFileOutputPropertiesList.Where(x => x.Visible).SingleOrDefault();
                if (visibleAudioFileOutputPropertiesViewModel != null)
                {
                    audioFileOutputPropertiesUserControl.ViewModel = visibleAudioFileOutputPropertiesViewModel;
                    audioFileOutputPropertiesVisible = true;
                }
                audioFileOutputPropertiesUserControl.Visible = audioFileOutputPropertiesVisible;

                // ChildDocumentPropertiesViewModel
                bool childDocumentPropertiesVisible = false;
                ChildDocumentPropertiesViewModel visibleChildDocumentPropertiesViewModel =
                    _presenter.ViewModel.Document.ChildDocumentPropertiesList.Where(x => x.Visible).SingleOrDefault();
                if (visibleChildDocumentPropertiesViewModel != null)
                {
                    childDocumentPropertiesUserControl.ViewModel = visibleChildDocumentPropertiesViewModel;
                    childDocumentPropertiesVisible = true;
                }
                childDocumentPropertiesUserControl.Visible = childDocumentPropertiesVisible;

                // CurveGridViewModel
                bool curveGridVisible = false;
                if (_presenter.ViewModel.Document.CurveGrid.Visible)
                {
                    curveGridUserControl.ViewModel = _presenter.ViewModel.Document.CurveGrid;
                    curveGridVisible = true;
                }
                else
                {
                    CurveGridViewModel visibleCurveGridViewModel = Enumerable.Union(_presenter.ViewModel.Document.ChildDocumentList.Select(x => x.CurveGrid),
                                                                                    _presenter.ViewModel.Document.ChildDocumentList.Select(x => x.CurveGrid))
                                                                             .Where(x => x.Visible)
                                                                             .SingleOrDefault();
                    if (visibleCurveGridViewModel != null)
                    {
                        curveGridUserControl.ViewModel = visibleCurveGridViewModel;
                        curveGridVisible = true;
                    }
                }
                curveGridUserControl.Visible = curveGridVisible;

                instrumentGridUserControl.ViewModel = _presenter.ViewModel.Document.InstrumentGrid;
                instrumentGridUserControl.Visible = _presenter.ViewModel.Document.InstrumentGrid.Visible;

                effectGridUserControl.ViewModel = _presenter.ViewModel.Document.EffectGrid;
                effectGridUserControl.Visible = _presenter.ViewModel.Document.EffectGrid.Visible;

                // Document ViewModels
                documentGridUserControl.ViewModel = _presenter.ViewModel.DocumentGrid;
                documentGridUserControl.Visible = _presenter.ViewModel.DocumentGrid.Visible;

                documentDetailsUserControl.ViewModel = _presenter.ViewModel.DocumentDetails;
                documentDetailsUserControl.Visible = _presenter.ViewModel.DocumentDetails.Visible;

                documentTreeUserControl.ViewModel = _presenter.ViewModel.Document.DocumentTree;
                documentTreeUserControl.Visible = _presenter.ViewModel.Document.DocumentTree.Visible;

                documentPropertiesUserControl.ViewModel = _presenter.ViewModel.Document.DocumentProperties;
                documentPropertiesUserControl.Visible = _presenter.ViewModel.Document.DocumentProperties.Visible;

                // OperatorPropertiesViewModel
                bool operatorPropertiesVisible = false;
                OperatorPropertiesViewModel visibleOperatorPropertiesViewModel =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList))
                    .Where(x => x.Visible).SingleOrDefault();
                if (visibleOperatorPropertiesViewModel != null)
                {
                    operatorPropertiesUserControl.ViewModel = visibleOperatorPropertiesViewModel;
                    operatorPropertiesVisible = true;
                }
                operatorPropertiesUserControl.Visible = operatorPropertiesVisible;

                bool operatorPropertiesVisible_ForCustomOperator = false;
                OperatorPropertiesViewModel_ForCustomOperator visibleOperatorPropertiesViewModel_ForCustomOperator =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList_ForCustomOperators,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators))
                    .Where(x => x.Visible).SingleOrDefault();
                if (visibleOperatorPropertiesViewModel_ForCustomOperator != null)
                {
                    operatorPropertiesUserControl_ForCustomOperator.SetUnderlyingDocumentLookup(_presenter.ViewModel.Document.UnderlyingDocumentLookup);
                    operatorPropertiesUserControl_ForCustomOperator.ViewModel = visibleOperatorPropertiesViewModel_ForCustomOperator;
                    operatorPropertiesVisible_ForCustomOperator = true;
                }
                operatorPropertiesUserControl_ForCustomOperator.Visible = operatorPropertiesVisible_ForCustomOperator;

                bool operatorPropertiesVisible_ForPatchInlet = false;
                OperatorPropertiesViewModel_ForPatchInlet visibleOperatorPropertiesViewModel_ForPatchInlet =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList_ForPatchInlets,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchInlets))
                    .Where(x => x.Visible).SingleOrDefault();
                if (visibleOperatorPropertiesViewModel_ForPatchInlet != null)
                {
                    operatorPropertiesUserControl_ForPatchInlet.ViewModel = visibleOperatorPropertiesViewModel_ForPatchInlet;
                    operatorPropertiesVisible_ForPatchInlet = true;
                }
                operatorPropertiesUserControl_ForPatchInlet.Visible = operatorPropertiesVisible_ForPatchInlet;

                bool operatorPropertiesVisible_ForPatchOutlet = false;
                OperatorPropertiesViewModel_ForPatchOutlet visibleOperatorPropertiesViewModel_ForPatchOutlet =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList_ForPatchOutlets,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchOutlets))
                    .Where(x => x.Visible).SingleOrDefault();
                if (visibleOperatorPropertiesViewModel_ForPatchOutlet != null)
                {
                    operatorPropertiesUserControl_ForPatchOutlet.ViewModel = visibleOperatorPropertiesViewModel_ForPatchOutlet;
                    operatorPropertiesVisible_ForPatchOutlet = true;
                }
                operatorPropertiesUserControl_ForPatchOutlet.Visible = operatorPropertiesVisible_ForPatchOutlet;

                bool operatorPropertiesVisible_ForValue = false;
                OperatorPropertiesViewModel_ForValue visibleOperatorPropertiesViewModel_ForValue =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList_ForValues,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForValues))
                    .Where(x => x.Visible).SingleOrDefault();
                if (visibleOperatorPropertiesViewModel_ForValue != null)
                {
                    operatorPropertiesUserControl_ForValue.ViewModel = visibleOperatorPropertiesViewModel_ForValue;
                    operatorPropertiesVisible_ForValue = true;
                }
                operatorPropertiesUserControl_ForValue.Visible = operatorPropertiesVisible_ForValue;

                // PatchGridViewModel
                bool patchGridVisible = false;
                if (_presenter.ViewModel.Document.PatchGrid.Visible)
                {
                    patchGridUserControl.ViewModel = _presenter.ViewModel.Document.PatchGrid;
                    patchGridVisible = true;
                }
                else
                {
                    PatchGridViewModel visiblePatchGridViewModel = Enumerable.Union(_presenter.ViewModel.Document.ChildDocumentList.Select(x => x.PatchGrid),
                                                                                    _presenter.ViewModel.Document.ChildDocumentList.Select(x => x.PatchGrid))
                                                                             .Where(x => x.Visible)
                                                                             .SingleOrDefault();
                    if (visiblePatchGridViewModel != null)
                    {
                        patchGridUserControl.ViewModel = visiblePatchGridViewModel;
                        patchGridVisible = true;
                    }
                }
                patchGridUserControl.Visible = patchGridVisible;

                // PatchDetailsViewModel
                bool patchDetailsVisible = false;
                PatchDetailsViewModel visiblePatchDetailsViewModel = _presenter.ViewModel.Document.PatchDetailsList.Where(x => x.Visible).SingleOrDefault();
                if (visiblePatchDetailsViewModel == null)
                {
                    visiblePatchDetailsViewModel = Enumerable.Union(_presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.PatchDetailsList),
                                                                    _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.PatchDetailsList))
                                                             .Where(x => x.Visible)
                                                             .SingleOrDefault();
                }
                if (visiblePatchDetailsViewModel != null)
                {
                    patchDetailsUserControl.ViewModel = visiblePatchDetailsViewModel;
                    patchDetailsVisible = true;
                }
                patchDetailsUserControl.Visible = patchDetailsVisible;

                // SampleGridViewModel
                bool sampleGridVisible = false;
                if (_presenter.ViewModel.Document.SampleGrid.Visible)
                {
                    sampleGridUserControl.ViewModel = _presenter.ViewModel.Document.SampleGrid;
                    sampleGridVisible = true;
                }
                else
                {
                    SampleGridViewModel visibleSampleGridViewModel = Enumerable.Union(_presenter.ViewModel.Document.ChildDocumentList.Select(x => x.SampleGrid),
                                                                                      _presenter.ViewModel.Document.ChildDocumentList.Select(x => x.SampleGrid))
                                                                               .Where(x => x.Visible)
                                                                               .SingleOrDefault();
                    if (visibleSampleGridViewModel != null)
                    {
                        sampleGridUserControl.ViewModel = visibleSampleGridViewModel;
                        sampleGridVisible = true;
                    }
                }
                sampleGridUserControl.Visible = sampleGridVisible;

                // SamplePropertiesViewModel
                bool samplePropertiesVisible = false;
                SamplePropertiesViewModel visibleSamplePropertiesViewModel =
                    _presenter.ViewModel.Document.SamplePropertiesList.Where(x => x.Visible).SingleOrDefault();
                if (visibleSamplePropertiesViewModel == null)
                {
                    visibleSamplePropertiesViewModel = Enumerable.Union(_presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList),
                                                                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList))
                                                                 .Where(x => x.Visible)
                                                                 .SingleOrDefault();
                }
                if (visibleSamplePropertiesViewModel != null)
                {
                    samplePropertiesUserControl.ViewModel = visibleSamplePropertiesViewModel;
                    samplePropertiesVisible = true;
                }
                samplePropertiesUserControl.Visible = samplePropertiesVisible;

                bool treePanelMustBeVisible = _presenter.ViewModel.Document.DocumentTree.Visible;
                SetTreePanelVisible(treePanelMustBeVisible);

                bool propertiesPanelMustBeVisible = _presenter.ViewModel.Document.DocumentProperties.Visible ||
                                                    audioFileOutputPropertiesVisible ||
                                                    childDocumentPropertiesVisible ||
                                                    operatorPropertiesVisible ||
                                                    operatorPropertiesVisible_ForCustomOperator ||
                                                    operatorPropertiesVisible_ForPatchInlet ||
                                                    operatorPropertiesVisible_ForPatchOutlet ||
                                                    operatorPropertiesVisible_ForValue ||
                                                    samplePropertiesVisible;

                SetPropertiesPanelVisible(propertiesPanelMustBeVisible);
            }
            finally
            {
                ResumeLayout();
            }

            if (_presenter.ViewModel.NotFound.Visible)
            {
                MessageBoxHelper.ShowNotFound(_presenter.ViewModel.NotFound);
            }

            if (_presenter.ViewModel.DocumentDelete.Visible)
            {
                MessageBoxHelper.ShowDocumentConfirmDelete(_presenter.ViewModel.DocumentDelete);
            }

            if (_presenter.ViewModel.DocumentDeleted.Visible)
            {
                MessageBoxHelper.ShowDocumentIsDeleted();
            }

            if (_presenter.ViewModel.DocumentCannotDelete.Visible)
            {
                _documentCannotDeleteForm.ShowDialog(_presenter.ViewModel.DocumentCannotDelete);
            }

            if (_presenter.ViewModel.ValidationMessages.Count != 0)
            {
                // TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
                MessageBox.Show(String.Join(Environment.NewLine, _presenter.ViewModel.ValidationMessages.Select(x => x.Text)));

                // Clear them so the next time the message box is not shown (message box is a temporary solution).
                _presenter.ViewModel.ValidationMessages.Clear();
            }

            if (_presenter.ViewModel.PopupMessages.Count != 0)
            {
                MessageBoxHelper.ShowPopupMessages(_presenter.ViewModel.PopupMessages);
            }

            // Focus control if not valid.
            bool mustFocusAudioFileOutputPropertiesUserControl = audioFileOutputPropertiesUserControl.Visible &&
                                                                !audioFileOutputPropertiesUserControl.ViewModel.Successful;
            if (mustFocusAudioFileOutputPropertiesUserControl)
            {
                audioFileOutputPropertiesUserControl.Focus();
            }

            bool mustFocusChildDocumentPropertiesUserControl = childDocumentPropertiesUserControl.Visible &&
                                                              !childDocumentPropertiesUserControl.ViewModel.Successful;
            if (mustFocusChildDocumentPropertiesUserControl)
            {
                childDocumentPropertiesUserControl.Focus();
            }

            bool mustFocusDocumentPropertiesUserControl = documentPropertiesUserControl.Visible &&
                                                         !documentPropertiesUserControl.ViewModel.Successful;
            if (mustFocusDocumentPropertiesUserControl)
            {
                documentPropertiesUserControl.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl = operatorPropertiesUserControl.Visible &&
                                                         !operatorPropertiesUserControl.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl)
            {
                operatorPropertiesUserControl.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForCustomOperator = operatorPropertiesUserControl_ForCustomOperator.Visible &&
                                                                           !operatorPropertiesUserControl_ForCustomOperator.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForCustomOperator)
            {
                operatorPropertiesUserControl_ForCustomOperator.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForPatchInlet = operatorPropertiesUserControl_ForPatchInlet.Visible &&
                                                                       !operatorPropertiesUserControl_ForPatchInlet.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForPatchInlet)
            {
                operatorPropertiesUserControl_ForPatchInlet.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForPatchOutlet = operatorPropertiesUserControl_ForPatchOutlet.Visible &&
                                                                        !operatorPropertiesUserControl_ForPatchOutlet.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForPatchOutlet)
            {
                operatorPropertiesUserControl_ForPatchOutlet.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForValue = operatorPropertiesUserControl_ForValue.Visible &&
                                                                  !operatorPropertiesUserControl_ForValue.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForValue)
            {
                operatorPropertiesUserControl_ForValue.Focus();
            }

            bool mustFocusSamplePropertiesUserControl = samplePropertiesUserControl.Visible &&
                                                       !samplePropertiesUserControl.ViewModel.Successful;
            if (mustFocusSamplePropertiesUserControl)
            {
                samplePropertiesUserControl.Focus();
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

        private void SetTitles()
        {
            instrumentGridUserControl.Title = PropertyDisplayNames.Instruments;
            effectGridUserControl.Title = PropertyDisplayNames.Effects;
        }

        private void ApplyStyling()
        {
            splitContainerProperties.SplitterWidth = StyleHelper.DefaultSpacing;
            splitContainerTree.SplitterWidth = StyleHelper.DefaultSpacing;
        }
    }
}
