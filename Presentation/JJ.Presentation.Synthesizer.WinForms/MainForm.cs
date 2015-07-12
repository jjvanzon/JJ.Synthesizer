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
using System.Media;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private const double DEFAULT_DURATION = 10;

        private IContext _context;
        private RepositoryWrapper _repositoryWrapper;
        private MainPresenter _presenter;
        private MainViewModel _viewModel;

        private DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();
        private static string _titleBarExtraText;
        private static string _sampleFilePath;
        private static string _outputFilePath;

        static MainForm()
        {
            var config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            _titleBarExtraText = config.General.TitleBarExtraText;
            _sampleFilePath = config.FilePaths.SampleFilePath;
            _outputFilePath = config.FilePaths.OutputFilePath;
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
            menuUserControl.DocumentSaveRequested += menuUserControl_DocumentSaveRequested;

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
            patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
            patchDetailsUserControl.LoseFocusRequested += patchDetailsUserControl_LoseFocusRequested;
            patchDetailsUserControl.DeleteOperatorRequested += patchDetailsUserControl_DeleteOperatorRequested;
            patchDetailsUserControl.AddOperatorRequested += patchDetailsUserControl_AddOperatorRequested;
            patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
            patchDetailsUserControl.ChangeInputOutletRequested += patchDetailsUserControl_ChangeInputOutletRequested;
            patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
            patchDetailsUserControl.SetValueRequested += patchDetailsUserControl_SetValueRequested;
            patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
            patchListUserControl.CloseRequested += patchListUserControl_CloseRequested;
            patchListUserControl.CreateRequested += patchListUserControl_CreateRequested;
            patchListUserControl.DeleteRequested += patchListUserControl_DeleteRequested;
            patchListUserControl.ShowDetailsRequested += patchListUserControl_ShowDetailsRequested;
            sampleListUserControl.CloseRequested += sampleListUserControl_CloseRequested;
            sampleListUserControl.CreateRequested += sampleListUserControl_CreateRequested;
            sampleListUserControl.DeleteRequested += sampleListUserControl_DeleteRequested;
            sampleListUserControl.ShowPropertiesRequested += sampleListUserControl_ShowPropertiesRequested;
            samplePropertiesUserControl.CloseRequested += samplePropertiesUserControl_CloseRequested;
            samplePropertiesUserControl.LoseFocusRequested += samplePropertiesUserControl_LoseFocusRequested;

            _documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;

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

        private void DocumentSave()
        {
            _viewModel = _presenter.DocumentSave(_viewModel);
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

        private void CurveListShow(int? childDocumentID)
        {
            _viewModel = _presenter.CurveListShow(_viewModel, childDocumentID);
            ApplyViewModel();
        }

        private void CurveListClose()
        {
            _viewModel = _presenter.CurveListClose(_viewModel);
            ApplyViewModel();
        }

        private void CurveCreate(int? childDocumentID)
        {
            _viewModel = _presenter.CurveCreate(_viewModel, childDocumentID);
            ApplyViewModel();
        }

        private void CurveDelete(int curveID)
        {
            _viewModel = _presenter.CurveDelete(_viewModel, curveID);
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

        private void PatchListShow(int? childDocumentID)
        {
            _viewModel = _presenter.PatchListShow(_viewModel, childDocumentID);
            ApplyViewModel();
        }

        private void PatchListClose()
        {
            _viewModel = _presenter.PatchListClose(_viewModel);
            ApplyViewModel();
        }

        private void PatchCreate(int? childDocumentID)
        {
            _viewModel = _presenter.PatchCreate(_viewModel, childDocumentID);
            ApplyViewModel();
        }

        private void PatchDelete(int patchID)
        {
            _viewModel = _presenter.PatchDelete(_viewModel, patchID);
            ApplyViewModel();
        }

        private void PatchDetailsShow(int patchID)
        {
            _viewModel = _presenter.PatchDetailsShow(_viewModel, patchID);
            ApplyViewModel();
        }

        private void PatchDetailsClose(int patchID)
        {
            _viewModel = _presenter.PatchDetailsClose(_viewModel, patchID);
            ApplyViewModel();
        }

        private void PatchDetailsLoseFocus(int patchID)
        {
            _viewModel = _presenter.PatchDetailsLoseFocus(_viewModel, patchID);
            ApplyViewModel();
        }

        private void PatchDetailsAddOperator(int patchID, int operatorTypeID)
        {
            _viewModel = _presenter.PatchDetailsAddOperator(_viewModel, patchID, operatorTypeID);
            ApplyViewModel();
        }

        private void PatchDetailsMoveOperator(int patchID, int operatorID, float centerX, float centerY)
        {
            _viewModel = _presenter.PatchDetailsMoveOperator(_viewModel, patchID, operatorID, centerX, centerY);
            ApplyViewModel();
        }

        private void PatchDetailsChangeInputOutlet(int patchID, int inletID, int inputOutletID)
        {
            _viewModel = _presenter.PatchDetailsChangeInputOutlet(_viewModel, patchID, inletID, inputOutletID);
            ApplyViewModel();
        }

        private void PatchDetailsSelectOperator(int patchID, int operatorID)
        {
            _viewModel = _presenter.PatchDetailsSelectOperator(_viewModel, patchID, operatorID);
            ApplyViewModel();
        }

        private void PatchDetailsDeleteOperator(int patchID)
        {
            _viewModel = _presenter.PatchDetailsDeleteOperator(_viewModel, patchID);
            ApplyViewModel();
        }

        private void PatchDetailsSetValue(int patchID, string value)
        {
            _viewModel = _presenter.PatchDetailsSetValue(_viewModel, patchID, value);
            ApplyViewModel();
        }

        private void PatchPlay(int patchID)
        {
            _viewModel = _presenter.PatchPlay(_viewModel, patchID, DEFAULT_DURATION, _sampleFilePath, _outputFilePath);

            ApplyViewModel();

            if (_viewModel.Successful)
            {
                SoundPlayer soundPlayer = new SoundPlayer(_outputFilePath);
                soundPlayer.Play();
            }
        }

        // Sample Actions

        private void SampleListShow(int? childDocumentID)
        {
            _viewModel = _presenter.SampleListShow(_viewModel, childDocumentID);
            ApplyViewModel();
        }

        private void SampleListClose()
        {
            _viewModel = _presenter.SampleListClose(_viewModel);
            ApplyViewModel();
        }

        private void SampleCreate(int? childDocumentID)
        {
            _viewModel = _presenter.SampleCreate(_viewModel, childDocumentID);
            ApplyViewModel();
        }

        private void SampleDelete(int sampleID)
        {
            _viewModel = _presenter.SampleDelete(_viewModel, sampleID);
            ApplyViewModel();
        }

        private void SamplePropertiesShow(int sampleID)
        {
            _viewModel = _presenter.SamplePropertiesShow(_viewModel, sampleID);
            ApplyViewModel();
        }

        private void SamplePropertiesClose(int sampleID)
        {
            _viewModel = _presenter.SamplePropertiesClose(_viewModel, sampleID);
            ApplyViewModel();
        }

        private void SamplePropertiesLoseFocus(int sampleID)
        {
            _viewModel = _presenter.SamplePropertiesLoseFocus(_viewModel, sampleID);
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

        private void menuUserControl_DocumentSaveRequested(object sender, EventArgs e)
        {
            DocumentSave();
        }

        // Document List Events

        private void documentListUserControl_ShowRequested(object sender, Int32EventArgs e)
        {
            DocumentListShow(e.Value);
        }

        private void documentListUserControl_CreateRequested(object sender, EventArgs e)
        {
            DocumentDetailsCreate();
        }

        private void documentListUserControl_OpenRequested(object sender, Int32EventArgs e)
        {
            DocumentOpen(e.Value);
        }

        private void documentListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            DocumentDelete(e.Value);
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

        private void documentTreeUserControl_DocumentPropertiesRequested(object sender, Int32EventArgs e)
        {
            DocumentPropertiesShow(e.Value);
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

        private void documentTreeUserControl_ShowSamplesRequested(object sender, NullableInt32EventArgs e)
        {
            SampleListShow(e.Value);
        }

        private void documentTreeUserControl_ShowCurvesRequested(object sender, NullableInt32EventArgs e)
        {
            CurveListShow(e.Value);
        }

        private void documentTreeUserControl_ShowPatchesRequested(object sender, NullableInt32EventArgs e)
        {
            PatchListShow(e.Value);
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
            AudioFileOutputDelete(e.Value);
        }

        private void audioFileOutputListUserControl_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputListClose();
        }

        private void audioFileOutputListUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputPropertiesShow(e.Value);
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputPropertiesClose(e.Value);
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputPropertiesLoseFocus(e.Value);
        }

        // Curve Events

        private void curveListUserControl_CreateRequested(object sender, NullableInt32EventArgs e)
        {
            CurveCreate(e.Value);
        }

        private void curveListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            CurveDelete(e.Value);
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
            InstrumentDelete(e.Value);
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
            EffectDelete(e.Value);
        }

        private void effectListUserControl_CloseRequested(object sender, EventArgs e)
        {
            EffectListClose();
        }

        // Patch Events

        private void patchListUserControl_CreateRequested(object sender, NullableInt32EventArgs e)
        {
            PatchCreate(e.Value);
        }

        private void patchListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            PatchDelete(e.Value);
        }

        private void patchListUserControl_CloseRequested(object sender, EventArgs e)
        {
            PatchListClose();
        }

        private void patchListUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            PatchDetailsShow(e.Value);
        }

        private void patchDetailsUserControl_PlayRequested(object sender, Int32EventArgs e)
        {
            PatchPlay(e.Value);
        }

        private void patchDetailsUserControl_SetValueRequested(object sender, SetValueEventArgs e)
        {
            PatchDetailsSetValue(e.PatchID, e.Value);
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, SelectOperatorEventArgs e)
        {
            PatchDetailsSelectOperator(e.PatchID, e.OperatorID);
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            PatchDetailsChangeInputOutlet(e.PatchID, e.InletID, e.InputOutletID);
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
        {
            PatchDetailsMoveOperator(e.PatchID, e.OperatorID, e.CenterX, e.CenterY);
        }

        private void patchDetailsUserControl_AddOperatorRequested(object sender, AddOperatorEventArgs e)
        {
            PatchDetailsAddOperator(e.PatchID, e.OperatorTypeID);
        }

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, Int32EventArgs e)
        {
            PatchDetailsDeleteOperator(e.Value);
        }

        private void patchDetailsUserControl_LoseFocusRequested(object sender, Int32EventArgs e)
        {
            PatchDetailsLoseFocus(e.Value);
        }

        private void patchDetailsUserControl_CloseRequested(object sender, Int32EventArgs e)
        {
            PatchDetailsClose(e.Value);
        }

        // Sample Events

        private void sampleListUserControl_CreateRequested(object sender, NullableInt32EventArgs e)
        {
            SampleCreate(e.Value);
        }

        private void sampleListUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            SampleDelete(e.Value);
        }

        private void sampleListUserControl_CloseRequested(object sender, EventArgs e)
        {
            SampleListClose();
        }

        private void sampleListUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            SamplePropertiesShow(e.Value);
        }

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, Int32EventArgs e)
        {
            SamplePropertiesLoseFocus(e.Value);
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, Int32EventArgs e)
        {
            SamplePropertiesClose(e.Value);
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

                // PatchDetailsViewModel
                bool patchDetailsVisible = false;
                PatchDetailsViewModel visiblePatchDetailsViewModel = _viewModel.Document.PatchDetailsList.Where(x => x.Visible).SingleOrDefault();
                if (visiblePatchDetailsViewModel != null)
                {
                    patchDetailsUserControl.ViewModel = visiblePatchDetailsViewModel;
                    patchDetailsVisible = true;
                }
                else
                {
                    visiblePatchDetailsViewModel = Enumerable.Union(_viewModel.Document.InstrumentDocumentList.SelectMany(x => x.PatchDetailsList),
                                                                    _viewModel.Document.EffectDocumentList.SelectMany(x => x.PatchDetailsList))
                                                             .Where(x => x.Visible)
                                                             .SingleOrDefault();
                    if (visiblePatchDetailsViewModel != null)
                    {
                        patchDetailsUserControl.ViewModel = visiblePatchDetailsViewModel;
                        patchDetailsVisible = true;
                    }
                }
                patchDetailsUserControl.Visible = patchDetailsVisible;

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
                    // TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
                    MessageBox.Show(String.Join(Environment.NewLine, _viewModel.ValidationMessages.Select(x => x.Text)));

                    // Clear them so the next time the message box is not shown (message box is a temporary solution).
                    _viewModel.ValidationMessages.Clear();
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
