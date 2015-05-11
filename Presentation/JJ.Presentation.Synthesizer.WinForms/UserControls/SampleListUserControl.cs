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
        public event EventHandler<PageEventArgs> ShowRequested;

        /// <summary> virtually not nullable </summary>
        private SampleListViewModel _viewModel;

        public SampleListUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Persistence

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
            labelTitle.Text = PropertyDisplayNames.Samples;
            IDColumn.HeaderText = CommonTitles.ID;
            NameColumn.HeaderText = CommonTitles.Name;
            AudioFileFormatColumn.HeaderText = PropertyDisplayNames.AudioFileOutput;
            SampleDataTypeColumn.HeaderText = PropertyDisplayNames.SampleDataType;
            SpeakerSetupColumn.HeaderText = PropertyDisplayNames.SpeakerSetup;
            SamplingRateColumn.HeaderText = PropertyDisplayNames.SamplingRate;
            IsActiveColumn.HeaderText = PropertyDisplayNames.IsActive;
        }

        private void ApplyViewModel()
        {
            if (_viewModel == null)
            {
                pagerControl.PagerViewModel = null;
                dataGridView.DataSource = null;
            }

            pagerControl.PagerViewModel = _viewModel.Pager;

            dataGridView.DataSource = _viewModel.List;
        }

        // Actions

        public void Show(int pageNumber)
        {
            if (ShowRequested != null)
            {
                ShowRequested(this, new PageEventArgs(pageNumber));
            }
        }

        // Events

        private void pagerControl_GoToFirstPageClicked(object sender, EventArgs e)
        {
            Show(1);
        }

        private void pagerControl_GoToPreviousPageClicked(object sender, EventArgs e)
        {
            Show(_viewModel.Pager.PageNumber - 1);
        }

        private void pagerControl_PageNumberClicked(object sender, PageNumberEventArgs e)
        {
            Show(e.PageNumber);
        }

        private void pagerControl_GoToNextPageClicked(object sender, EventArgs e)
        {
            Show(_viewModel.Pager.PageNumber + 1);
        }

        private void pagerControl_GoToLastPageClicked(object sender, EventArgs e)
        {
            Show(_viewModel.Pager.PageCount - 1);
        }
    }
}
