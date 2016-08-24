using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class SampleGridUserControl : UserControlBase
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler<Int32EventArgs> CreateRequested;
        public event EventHandler<DocumentAndChildEntityEventArgs> DeleteRequested;
        public event EventHandler<Int32EventArgs> CloseRequested;
        public event EventHandler<Int32EventArgs> ShowPropertiesRequested;

        public SampleGridUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Samples;
            NameColumn.HeaderText = CommonTitles.Name;
            SampleDataTypeColumn.HeaderText = PropertyDisplayNames.SampleDataType;
            SpeakerSetupColumn.HeaderText = PropertyDisplayNames.SpeakerSetup;
            SamplingRateColumn.HeaderText = PropertyDisplayNames.SamplingRate;
            IsActiveColumn.HeaderText = PropertyDisplayNames.IsActive;
        }

        // Binding

        private new SampleGridViewModel ViewModel => (SampleGridViewModel)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            specializedDataGridView.DataSource = ViewModel.List.Select(x => new
            {
                x.ID,
                x.Name,
                IsActive = x.IsActive ? CommonTitles.Yes : CommonTitles.No,
                x.SamplingRate,
                x.SampleDataType,
                x.SpeakerSetup,
            }).ToArray();
        }

        // Actions

        private void Create()
        {
            CreateRequested?.Invoke(this, new Int32EventArgs(ViewModel.DocumentID));
        }

        private void Delete()
        {
            if (ViewModel == null) return;

            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                DeleteRequested?.Invoke(this, new DocumentAndChildEntityEventArgs(ViewModel.DocumentID, id.Value));
            }
        }

        private void Close()
        {
            if (ViewModel == null) return;
            CloseRequested?.Invoke(this, new Int32EventArgs(ViewModel.DocumentID));
        }

        private void ShowProperties()
        {
            if (ShowPropertiesRequested != null)
            {
                int? id = TryGetSelectedID();
                if (id.HasValue)
                {
                    var e = new Int32EventArgs(id.Value);
                    ShowPropertiesRequested(this, e);
                }
            }
        }

        // Events

        private void titleBarUserControl_AddClicked(object sender, EventArgs e)
        {
            Create();
        }

        private void titleBarUserControl_RemoveClicked(object sender, EventArgs e)
        {
            Delete();
        }

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void specializedDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    Delete();
                    break;

                case Keys.Enter:
                    ShowProperties();
                    break;
            }
        }

        private void specializedDataGridView_DoubleClick(object sender, EventArgs e)
        {
            ShowProperties();
        }

        // Helpers

        private int? TryGetSelectedID()
        {
            if (specializedDataGridView.CurrentRow != null)
            {
                DataGridViewCell cell = specializedDataGridView.CurrentRow.Cells[ID_COLUMN_NAME];
                int id = (int)cell.Value;
                return id;
            }

            return null;
        }
    }
}
