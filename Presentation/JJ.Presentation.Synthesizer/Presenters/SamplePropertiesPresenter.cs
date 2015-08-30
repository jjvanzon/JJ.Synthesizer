using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SamplePropertiesPresenter
    {
        private SampleRepositories _sampleRepositories;

        public SamplePropertiesViewModel ViewModel { get; set; }

        public SamplePropertiesPresenter(SampleRepositories samplesRepositories)
        {
            if (samplesRepositories == null) throw new NullException(() => samplesRepositories);

            _sampleRepositories = samplesRepositories;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            if (ViewModel.Successful)
            {
                RefreshDuration();

                ViewModel.Visible = false;
            }
        }

        public void LoseFocus()
        {
            AssertViewModel();

            Update();

            if (ViewModel.Successful)
            {
                RefreshDuration();
            }
        }

        private void Update()
        {
            // TODO: Consider letting ToEntity return SampleInfo, because it also updates the sample's Bytes.
            Sample entity = ViewModel.ToEntity(_sampleRepositories);

            IValidator validator = new SampleValidator_InDocument(entity);
            if (!validator.IsValid)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                ViewModel.ValidationMessages = new List<Message>();
                ViewModel.Successful = true;
            }
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }

        private void RefreshDuration()
        {
            Sample sample = _sampleRepositories.SampleRepository.Get(ViewModel.Entity.ID);
            ViewModel.Entity.Duration = sample.GetDuration(ViewModel.Entity.Bytes);
        }
    }
}
