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
        public event EventHandler CloseRequested;

        /// <summary>
        /// virtually not nullable
        /// </summary>
        private DocumentDetailsViewModel _viewModel;

        public DocumentDetailsUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        // Persistence

        private IContext _context;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IContext Context
        {
            get { return _context; }
            set 
            {
                if (value == null) throw new NullException(() => value);
                _context = value;
            }
        }

        // Actions

        public void Show(DocumentDetailsViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            _viewModel = viewModel;
            ApplyViewModelToControls();

            base.Show();
        }

        public void Create()
        {
            DocumentDetailsPresenter presenter = CreatePresenter();
            _viewModel = presenter.Create();
            ApplyViewModelToControls();
        }

        private void Save()
        {
            ApplyControlsToViewModel();

            DocumentDetailsPresenter presenter = CreatePresenter();
            object viewModel = presenter.Save(_viewModel);

            var detailsViewModel = viewModel as DocumentDetailsViewModel;
            if (detailsViewModel != null)
            {
                _viewModel = detailsViewModel;
                ApplyViewModelToControls();
                return;
            }

            var previousViewModel = viewModel as PreviousViewModel;
            if (previousViewModel != null)
            {
                Close();
            }
        }

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
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
            if (_viewModel == null)
            {
                // TODO: This of some better fallback.
                return;
            }

            labelIDValue.Text = _viewModel.Document.ID.ToString();
            textBoxName.Text = _viewModel.Document.Name;

            // TODO: This is weird.

            if (_viewModel.Messages.Count > 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, _viewModel.Messages.Select(x => x.Text)));
            }
        }

        private void ApplyControlsToViewModel()
        {
            _viewModel.Document.Name = textBoxName.Text;
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

        // Helpers

        private DocumentDetailsPresenter CreatePresenter()
        {
            if (_context == null) throw new Exception("Assign Context first.");

            return new DocumentDetailsPresenter(PersistenceHelper.CreateRepository<IDocumentRepository>(_context));
        }
    }
}
