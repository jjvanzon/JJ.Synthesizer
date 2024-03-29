﻿using System;
using System.Windows.Forms;
using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.Common;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class ToneGridEditUserControl : UserControlBase
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler<EventArgs<int>> SetInstrumentScaleRequested;
        public event EventHandler<EventArgs<int>> CreateToneRequested;
        public event EventHandler<ScaleAndToneEventArgs> DeleteToneRequested;
        public event EventHandler<ScaleAndToneEventArgs> PlayToneRequested;
        public event EventHandler<EventArgs<int>> CloseRequested;
        public event EventHandler<EventArgs<int>> LoseFocusRequested;
        public event EventHandler<EventArgs<int>> Edited;

        public ToneGridEditUserControl()
        {
            InitializeComponent();
            SetTitles();

            specializedDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = ResourceFormatter.Tones;
            OctaveColumn.HeaderText = ResourceFormatter.Octave;
            FrequencyColumn.HeaderText = ResourceFormatter.Frequency;
            ToneNumberColumn.HeaderText = ResourceFormatter.Number;
            OrdinalColumn.HeaderText = ResourceFormatter.Ordinal;
        }

        // Binding

        public new ToneGridEditViewModel ViewModel
        {
            // ReSharper disable once MemberCanBePrivate.Global
            get => (ToneGridEditViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            ValueColumn.HeaderText = ViewModel.ValueTitle;
            FrequencyColumn.Visible = ViewModel.FrequencyVisible;

            specializedDataGridView.DataSource = ViewModel.Tones;
        }

        // Actions

        private void CreateTone() => CreateToneRequested.Invoke(this, new EventArgs<int>(ViewModel.ScaleID));

        private void DeleteTone()
        {
            if (ViewModel == null) return;

            int? toneID = TryGetSelectedID();
            if (toneID.HasValue)
            {
                DeleteToneRequested.Invoke(this, new ScaleAndToneEventArgs(ViewModel.ScaleID, toneID.Value));
            }
        }

        private void Close()
        {
            if (ViewModel == null) return;

            specializedDataGridView.EndEdit();

            CloseRequested.Invoke(this, new EventArgs<int>(ViewModel.ScaleID));
        }

        private void LoseFocus()
        {
            if (ViewModel == null) return;

            specializedDataGridView.EndEdit();

            LoseFocusRequested.Invoke(this, new EventArgs<int>(ViewModel.ScaleID));
        }

        // Events

        private void titleBarUserControl_AddClicked(object sender, EventArgs e) => CreateTone();

        private void titleBarUserControl_AddToInstrumentClicked(object sender, EventArgs e) => SetInstrumentScaleRequested(sender, new EventArgs<int>(ViewModel.ScaleID));

        private void titleBarUserControl_DeleteClicked(object sender, EventArgs e) => DeleteTone();

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e) => Close();

        private void specializedDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) => BeginInvoke(new Action(() => Edited.Invoke(this, new EventArgs<int>(ViewModel.ScaleID))));

        private void specializedDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ViewModel == null) return;

            if (e.RowIndex == -1)
            {
                return;
            }

            // ReSharper disable once InvertIf
            if (e.ColumnIndex == PlayColumn.Index)
            {
                int toneID = (int)specializedDataGridView.Rows[e.RowIndex].Cells[IDColumn.Name].Value;
                PlayToneRequested.Invoke(this, new ScaleAndToneEventArgs(ViewModel.ScaleID, toneID));
            }
        }

        private void specializedDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteTone();
                    e.Handled = true;
                    break;

                case Keys.Insert:
                    CreateTone();
                    e.Handled = true;
                    break;

                case Keys.Space:
                case Keys.Enter:
                    bool isPlayColumn = specializedDataGridView.CurrentCell?.ColumnIndex == PlayColumn.Index;
                    if (isPlayColumn)
                    {
                        int? toneID = TryGetSelectedID();
                        if (toneID.HasValue)
                        {
                            PlayToneRequested.Invoke(this, new ScaleAndToneEventArgs(ViewModel.ScaleID, toneID.Value));
                            e.Handled = true;
                        }
                    }
                    break;
            }
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void ToneGridEditUserControl_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible)
            {
                LoseFocus();
            }
        }

        // Helpers

        private int? TryGetSelectedID()
        {
            if (specializedDataGridView.CurrentRow == null)
            {
                return null;
            }

            DataGridViewCell cell = specializedDataGridView.CurrentRow.Cells[ID_COLUMN_NAME];
            int id = (int)cell.Value;
            return id;
        }
    }
}
