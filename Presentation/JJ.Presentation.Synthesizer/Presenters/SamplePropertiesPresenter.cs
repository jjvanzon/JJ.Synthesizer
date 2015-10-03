using System.Collections.Generic;
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
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SamplePropertiesPresenter
    {
        private SampleRepositories _repositories;

        public SamplePropertiesViewModel ViewModel { get; set; }

        public SamplePropertiesPresenter(SampleRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            Sample entity = _repositories.SampleRepository.Get(ViewModel.Entity.ID);
            bool visible = ViewModel.Visible;
            ViewModel = entity.ToPropertiesViewModel(_repositories);
            ViewModel.Visible = visible;
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
            Sample entity = ViewModel.ToEntity(_repositories);

            IValidator validator = new SampleValidator_InDocument(entity);

            ViewModel.Successful = validator.IsValid;
            ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }

        private void RefreshDuration()
        {
            Sample sample = _repositories.SampleRepository.Get(ViewModel.Entity.ID);
            ViewModel.Entity.Duration = sample.GetDuration(ViewModel.Entity.Bytes);
        }
    }
}
