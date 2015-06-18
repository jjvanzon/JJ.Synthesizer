using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Framework.Data;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class SampleListUserControl : UserControl
    {
        private const string LIST_INDEX_COLUMN_NAME = "ListIndexColumn";

        public event EventHandler<ChildDocumentEventArgs> CreateRequested;
        public event EventHandler<ChildDocumentSubListItemEventArgs> DeleteRequested;
        public event EventHandler CloseRequested;
        public event EventHandler<ChildDocumentSubListItemEventArgs> ShowPropertiesRequested;

        /// <summary> virtually not nullable </summary>
        private SampleListViewModel _viewModel;

        public SampleListUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SampleListViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _viewModel = value;
                ApplyViewModel();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Samples;
            NameColumn.HeaderText = CommonTitles.Name;
            AudioFileFormatColumn.HeaderText = PropertyDisplayNames.AudioFileFormat;
            SampleDataTypeColumn.HeaderText = PropertyDisplayNames.SampleDataType;
            SpeakerSetupColumn.HeaderText = PropertyDisplayNames.SpeakerSetup;
            SamplingRateColumn.HeaderText = PropertyDisplayNames.SamplingRate;
            IsActiveColumn.HeaderText = PropertyDisplayNames.IsActive;
        }

        private void ApplyViewModel()
        {
            specializedDataGridView.DataSource = _viewModel.List.Select(x => new
            {
                x.Keys.ListIndex,
                x.Name,
                IsActive = x.IsActive ? CommonTitles.Yes : CommonTitles.No,
                x.SamplingRate,
                x.SampleDataType,
                x.SpeakerSetup,
                x.AudioFileFormat,
            }).ToArray();
        }

        // Actions

        private void Create()
        {
            if (CreateRequested != null)
            {
                var e = new ChildDocumentEventArgs(ViewModel.Keys.ChildDocumentTypeEnum, ViewModel.Keys.ChildDocumentListIndex);
                CreateRequested(this, e);
            }
        }

        private void Delete()
        {
            if (DeleteRequested != null)
            {
                int? listIndex = TryGetSelectedListIndex();
                if (listIndex.HasValue)
                {
                    var e = new ChildDocumentSubListItemEventArgs(listIndex.Value, ViewModel.Keys.ChildDocumentTypeEnum, ViewModel.Keys.ChildDocumentListIndex);
                    DeleteRequested(this, e);
                }
            }
        }

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void ShowProperties()
        {
            if (ShowPropertiesRequested != null)
            {
                int? listIndex = TryGetSelectedListIndex();
                if (listIndex.HasValue)
                {
                    var e = new ChildDocumentSubListItemEventArgs(listIndex.Value, ViewModel.Keys.ChildDocumentTypeEnum, ViewModel.Keys.ChildDocumentListIndex);
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
            }
        }

        private void specializedDataGridView_DoubleClick(object sender, EventArgs e)
        {
            ShowProperties();
        }

        // Helpers

        private int? TryGetSelectedListIndex()
        {
            if (specializedDataGridView.CurrentRow != null)
            {
                DataGridViewCell cell = specializedDataGridView.CurrentRow.Cells[LIST_INDEX_COLUMN_NAME];
                int listIndex = Convert.ToInt32(cell.Value);
                return listIndex;
            }

            return null;
        }
    }
}
