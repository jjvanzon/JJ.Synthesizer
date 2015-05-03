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

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputDetailsUserControl : UserControl
    {
        private IContext _context;
        private AudioFileOutputDetailsPresenter _presenter;
        private AudioFileOutputDetailsViewModel _viewModel;

        public AudioFileOutputDetailsUserControl()
        {
            InitializeComponent();

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
                _presenter = new AudioFileOutputDetailsPresenter(
                    PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(_context),
                    PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(_context),
                    PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(_context),
                    PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(_context));
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

        public void Show(int id)
        {
            AssertContext();
            _viewModel = _presenter.Edit(id);
            ApplyViewModel();
        }

        // ApplyViewModel

        private void ApplyViewModel()
        {
            if (_viewModel == null)
            {
            }
        }
    }
}
