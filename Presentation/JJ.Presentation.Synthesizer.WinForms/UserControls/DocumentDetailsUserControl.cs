using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Framework.Data;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentDetailsUserControl : UserControl
    {
        public event EventHandler SaveRequested;
        public event EventHandler CloseRequested;

        /// <summary> virtually not nullable </summary>
        private DocumentDetailsViewModel _viewModel;

        public DocumentDetailsUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DocumentDetailsViewModel ViewModel
        {
            get { return _viewModel; }
            set 
            {
                if (value == null) throw new NullException(() => value);
                _viewModel = value;
                ApplyViewModelToControls();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl1.Text = PropertyDisplayNames.Document;
            labelIDTitle.Text = CommonTitles.ID;
            labelName.Text = CommonTitles.Name;
            buttonSave.Text = CommonTitles.Save;
        }

        private void ApplyViewModelToControls()
        {
            labelIDValue.Text = _viewModel.Document.ID.ToString();
            textBoxName.Text = _viewModel.Document.Name;
        }

        private void ApplyControlsToViewModel()
        {
            _viewModel.Document.Name = textBoxName.Text;
        }

        // Actions

        private void Save()
        {
            if (SaveRequested != null)
            {
                ApplyControlsToViewModel();
                SaveRequested(this, EventArgs.Empty);
            }
        }

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        // Events

        private void titleBarUserControl1_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
