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
    public partial class AudioFileOutputDetailsUserControl : UserControl
    {
        private AudioFileOutputDetailsViewModel _viewModel;

        public AudioFileOutputDetailsUserControl()
        {
            InitializeComponent();

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

        public void Show(int id)
        {
            AudioFileOutputDetailsPresenter presenter = CreatePresenter();
            _viewModel = presenter.Edit(id);

            ApplyViewModel();
        }

        // ApplyViewModel

        private void ApplyViewModel()
        {
            if (_viewModel == null)
            {
            }
        }

        // Helpers

        private AudioFileOutputDetailsPresenter CreatePresenter()
        {
            if (_context == null) throw new Exception("Assign Context first.");

            return new AudioFileOutputDetailsPresenter(
                PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(_context),
                PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(_context),
                PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(_context),
                PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(_context));
        }

        private void labelSamplingRate_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AudioFileOutputDetailsUserControl_Load(object sender, EventArgs e)
        {

        }

        private void labelIDValue_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownSamplingRate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxAudioFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSampleDataType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSpeakerSetup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownStartTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownDuration_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
