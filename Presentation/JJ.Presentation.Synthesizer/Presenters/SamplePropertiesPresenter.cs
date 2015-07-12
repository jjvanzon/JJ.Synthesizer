using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Helpers;

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

        public SamplePropertiesViewModel Show(SamplePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                Sample entity = userInput.ToEntity(_sampleRepositories);

                ViewModel = CreateViewModel(entity, userInput);
            }

            ViewModel.Visible = true;

            return ViewModel;
        }

        public SamplePropertiesViewModel Close(SamplePropertiesViewModel userInput)
        {
            ViewModel = Update(userInput);

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }

            return ViewModel;
        }

        public SamplePropertiesViewModel LoseFocus(SamplePropertiesViewModel userInput)
        {
            ViewModel = Update(userInput);

            return ViewModel;
        }

        private SamplePropertiesViewModel Update(SamplePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            Sample entity = userInput.ToEntity(_sampleRepositories);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                ViewModel = CreateViewModel(entity, userInput);
            }

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

            return ViewModel;
        }

        public void Clear()
        {
            ViewModel = null;
        }

        private bool MustCreateViewModel(SamplePropertiesViewModel existingViewModel, SamplePropertiesViewModel userInput)
        {
            return existingViewModel == null ||
                   existingViewModel.Entity.ID != userInput.Entity.ID;
        }

        private SamplePropertiesViewModel CreateViewModel(Sample entity, SamplePropertiesViewModel userInput)
        {
            SamplePropertiesViewModel viewModel = entity.ToPropertiesViewModel(_sampleRepositories);

            return viewModel;
        }
    }
}
