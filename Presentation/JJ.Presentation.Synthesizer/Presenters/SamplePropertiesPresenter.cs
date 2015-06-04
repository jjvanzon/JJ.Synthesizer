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
    public class SamplePropertiesPresenter
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

        public SamplePropertiesViewModel LooseFocus(SamplePropertiesViewModel userInput)
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

            IValidator validator = new SampleValidator(entity, new HashSet<object>());
            if (!validator.IsValid)
            {
                ViewModel.Successful = true;
                ViewModel.Messages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                ViewModel.Messages = new Message[0];
                ViewModel.Successful = false;
            }

            return ViewModel;
        }

        private bool MustCreateViewModel(SamplePropertiesViewModel existingViewModel, SamplePropertiesViewModel userInput)
        {
            return existingViewModel == null ||
                   existingViewModel.Sample.Keys.DocumentID != userInput.Sample.Keys.DocumentID ||
                   existingViewModel.Sample.Keys.ListIndex != userInput.Sample.Keys.ListIndex ||
                   existingViewModel.Sample.Keys.ChildDocumentTypeEnum != userInput.Sample.Keys.ChildDocumentTypeEnum ||
                   existingViewModel.Sample.Keys.ChildDocumentListIndex != userInput.Sample.Keys.ChildDocumentListIndex;
        }

        private SamplePropertiesViewModel CreateViewModel(Sample entity, SamplePropertiesViewModel userInput)
        {
            SamplePropertiesViewModel viewModel = entity.ToPropertiesViewModel(
                userInput.Sample.Keys.DocumentID,
                userInput.Sample.Keys.ListIndex,
                userInput.Sample.Keys.ChildDocumentTypeEnum,
                userInput.Sample.Keys.ChildDocumentListIndex,
                _sampleRepositories);

            return viewModel;
        }
    }
}
