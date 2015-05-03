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

        private IContext _context;
        private IDocumentRepository _documentRepository;
        private DocumentDetailsPresenter _presenter;

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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IContext Context
        {
            get { return _context; }
            set 
            {
                if (value == null) throw new NullException(() => value);
                if (_context == value) return;

                _context = value;
                _documentRepository = PersistenceHelper.CreateRepository<IDocumentRepository>(_context);
                _presenter = new DocumentDetailsPresenter(_documentRepository);
            }
        }

        private void AssertContext()
        {
            if (_context == null)
            {
                throw new Exception("Assign Context first.");
            }
        }

        // Actions

        public void Show(DocumentDetailsViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            AssertContext();

            _viewModel = viewModel;
            ApplyViewModelToControls();

            base.Show();
        }

        public void Create()
        {
            AssertContext();
            _viewModel = _presenter.Create();
            ApplyViewModelToControls();
        }

        private void Save()
        {
            AssertContext();

            ApplyControlsToViewModel();

            object viewModel = _presenter.Save(_viewModel);

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

            // TODO: This is weird. Because you would not know at what points (how many times) this method is called,
            // yet every time we show a message box.
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
    }
}
