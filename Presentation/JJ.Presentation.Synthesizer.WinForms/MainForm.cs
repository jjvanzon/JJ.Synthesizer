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

            documentGridUserControl.ShowRequested += documentGridUserControl_ShowRequested;
            documentGridUserControl.CloseRequested += documentGridUserControl_CloseRequested;
            documentGridUserControl.CreateRequested += documentGridUserControl_CreateRequested;
            documentGridUserControl.OpenRequested += documentGridUserControl_OpenRequested;
            documentGridUserControl.DeleteRequested += documentGridUserControl_DeleteRequested;
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

            audioFileOutputGridUserControl.CloseRequested += audioFileOutputGridUserControl_CloseRequested;
            audioFileOutputGridUserControl.CreateRequested += audioFileOutputGridUserControl_CreateRequested;
            audioFileOutputGridUserControl.DeleteRequested += audioFileOutputGridUserControl_DeleteRequested;
            audioFileOutputGridUserControl.ShowPropertiesRequested += audioFileOutputGridUserControl_ShowPropertiesRequested;
            audioFileOutputPropertiesUserControl.CloseRequested += audioFileOutputPropertiesUserControl_CloseRequested;
            audioFileOutputPropertiesUserControl.LoseFocusRequested += audioFileOutputPropertiesUserControl_LoseFocusRequested;
            curveGridUserControl.CloseRequested += curveGridUserControl_CloseRequested;
            curveGridUserControl.CreateRequested += curveGridUserControl_CreateRequested;
            curveGridUserControl.DeleteRequested += curveGridUserControl_DeleteRequested;
            instrumentGridUserControl.CloseRequested += instrumentGridUserControl_CloseRequested;
            instrumentGridUserControl.CreateRequested += instrumentGridUserControl_CreateRequested;
            instrumentGridUserControl.DeleteRequested += instrumentGridUserControl_DeleteRequested;
            effectGridUserControl.CloseRequested += effectGridUserControl_CloseRequested;
            effectGridUserControl.CreateRequested += effectGridUserControl_CreateRequested;
            effectGridUserControl.DeleteRequested += effectGridUserControl_DeleteRequested;
            patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
            patchDetailsUserControl.LoseFocusRequested += patchDetailsUserControl_LoseFocusRequested;
            patchDetailsUserControl.DeleteOperatorRequested += patchDetailsUserControl_DeleteOperatorRequested;
            patchDetailsUserControl.AddOperatorRequested += patchDetailsUserControl_AddOperatorRequested;
            patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
            patchDetailsUserControl.ChangeInputOutletRequested += patchDetailsUserControl_ChangeInputOutletRequested;
            patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
            patchDetailsUserControl.SetValueRequested += patchDetailsUserControl_SetValueRequested;
            patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
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
            _presenter.Show();
            ApplyViewModel();
        }

        private void NotFoundOK()
        {
            _presenter.NotFoundOK();
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

        private void DocumentPropertiesShow(int id)
        {
            _presenter.DocumentPropertiesShow(id);
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

        // Curve Actions

        private void CurveListShow(int? childDocumentID)
        {
            _presenter.CurveGridShow(childDocumentID);
            ApplyViewModel();
        }

        private void CurveListClose()
        {
            _presenter.CurveGridClose();
            ApplyViewModel();
        }

        private void CurveCreate(int? childDocumentID)
        {
            _presenter.CurveCreate(childDocumentID);
            ApplyViewModel();
        }

        private void CurveDelete(int curveID)
        {
            _presenter.CurveDelete(curveID);
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

        private void InstrumentDelete(int listIndex)
        {
            _presenter.InstrumentDelete(listIndex);
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

        // Patch Actions

        private void PatchListShow(int? childDocumentID)
        {
            _presenter.PatchGridShow(childDocumentID);
            ApplyViewModel();
        }

        private void PatchListClose()
        {
            _presenter.PatchGridClose();
            ApplyViewModel();
        }

        private void PatchCreate(int? childDocumentID)
        {
            _presenter.PatchCreate(childDocumentID);
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

        private void PatchDetailsAddOperator(int operatorTypeID)
        {
            _presenter.PatchDetailsAddOperator(operatorTypeID);
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

        private void PatchDetailsDeleteOperator()
        {
            _presenter.PatchDetailsDeleteOperator();
            ApplyViewModel();
        }

        private void PatchDetailsSetValue(string value)
        {
            _presenter.PatchDetailsSetValue(value);
            ApplyViewModel();
        }

        private void PatchPlay()
        {
            _presenter.PatchPlay(DEFAULT_DURATION, _sampleFilePath, _outputFilePath);

            ApplyViewModel();

            if (_presenter.ViewModel.Successful)
            {
                SoundPlayer soundPlayer = new SoundPlayer(_outputFilePath);
                soundPlayer.Play();
            }
        }

        // Sample Actions

        private void SampleListShow(int? childDocumentID)
        {
            _presenter.SampleGridShow(childDocumentID);
            ApplyViewModel();
        }

        private void SampleListClose()
        {
            _presenter.SampleGridClose();
            ApplyViewModel();
        }

        private void SampleCreate(int? childDocumentID)
        {
            _presenter.SampleCreate(childDocumentID);
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

        // Curve Events

        private void curveGridUserControl_CreateRequested(object sender, NullableInt32EventArgs e)
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

        // Patch Events

        private void patchGridUserControl_CreateRequested(object sender, NullableInt32EventArgs e)
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

        private void patchDetailsUserControl_SetValueRequested(object sender, SetValueEventArgs e)
        {
            PatchDetailsSetValue(e.Value);
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, SelectOperatorEventArgs e)
        {
            PatchDetailsSelectOperator(e.OperatorID);
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            PatchDetailsChangeInputOutlet(e.InletID, e.InputOutletID);
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
        {
            PatchDetailsMoveOperator(e.OperatorID, e.CenterX, e.CenterY);
        }

        private void patchDetailsUserControl_AddOperatorRequested(object sender, AddOperatorEventArgs e)
        {
            PatchDetailsAddOperator(e.OperatorTypeID);
        }

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs e)
        {
            PatchDetailsDeleteOperator();
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

        private void sampleGridUserControl_CreateRequested(object sender, NullableInt32EventArgs e)
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

        // Helpers

        private void ApplyViewModel()
        {
            SuspendLayout();

            try
            {
                Text = _presenter.ViewModel.WindowTitle + _titleBarExtraText;

                menuUserControl.Show(_presenter.ViewModel.Menu);

                documentGridUserControl.ViewModel = _presenter.ViewModel.DocumentGrid;
                documentGridUserControl.Visible = _presenter.ViewModel.DocumentGrid.Visible;

                documentDetailsUserControl.ViewModel = _presenter.ViewModel.DocumentDetails;
                documentDetailsUserControl.Visible = _presenter.ViewModel.DocumentDetails.Visible;

                documentTreeUserControl.ViewModel = _presenter.ViewModel.Document.DocumentTree;
                documentTreeUserControl.Visible = _presenter.ViewModel.Document.DocumentTree.Visible;

                documentPropertiesUserControl.ViewModel = _presenter.ViewModel.Document.DocumentProperties;
                documentPropertiesUserControl.Visible = _presenter.ViewModel.Document.DocumentProperties.Visible;

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
                if (visiblePatchDetailsViewModel != null)
                {
                    patchDetailsUserControl.ViewModel = visiblePatchDetailsViewModel;
                    patchDetailsVisible = true;
                }
                else
                {
                    visiblePatchDetailsViewModel = Enumerable.Union(_presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.PatchDetailsList),
                                                                    _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.PatchDetailsList))
                                                             .Where(x => x.Visible)
                                                             .SingleOrDefault();
                    if (visiblePatchDetailsViewModel != null)
                    {
                        patchDetailsUserControl.ViewModel = visiblePatchDetailsViewModel;
                        patchDetailsVisible = true;
                    }
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
                if (visibleSamplePropertiesViewModel != null)
                {
                    samplePropertiesUserControl.ViewModel = visibleSamplePropertiesViewModel;
                    samplePropertiesVisible = true;
                }
                else
                {
                    visibleSamplePropertiesViewModel = Enumerable.Union(_presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList),
                                                                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList))
                                                                 .Where(x => x.Visible)
                                                                 .SingleOrDefault();
                    if (visibleSamplePropertiesViewModel != null)
                    {
                        samplePropertiesUserControl.ViewModel = visibleSamplePropertiesViewModel;
                        samplePropertiesVisible = true;
                    }
                }
                samplePropertiesUserControl.Visible = samplePropertiesVisible;

                bool treePanelMustBeVisible = _presenter.ViewModel.Document.DocumentTree.Visible;
                SetTreePanelVisible(treePanelMustBeVisible);

                // TODO: Make panel visibility dependent on more things.
                bool propertiesPanelMustBeVisible = _presenter.ViewModel.Document.DocumentProperties.Visible || 
                                                    audioFileOutputPropertiesVisible ||
                                                    samplePropertiesVisible;

                SetPropertiesPanelVisible(propertiesPanelMustBeVisible);

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
                if (!_presenter.ViewModel.Document.DocumentProperties.Successful)
                {
                    documentPropertiesUserControl.Focus();
                }

                bool mustFocusAudioFileOutputUserControl = _presenter.ViewModel.Document.AudioFileOutputPropertiesList.Any(x => !x.Successful);
                if (mustFocusAudioFileOutputUserControl)
                {
                    audioFileOutputPropertiesUserControl.Focus();
                }

                bool mustFocusSampleUserControl = _presenter.ViewModel.Document.SamplePropertiesList.Any(x => !x.Successful) ||
                                                  _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList).Any(x => !x.Successful) ||
                                                  _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList).Any(x => !x.Successful);
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
