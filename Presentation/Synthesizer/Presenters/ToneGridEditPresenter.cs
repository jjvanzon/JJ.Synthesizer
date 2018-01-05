using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class ToneGridEditPresenter : EntityPresenterWithSaveBase<Scale, ToneGridEditViewModel>
	{
		private readonly IScaleRepository _scaleRepository;
		private readonly ScaleFacade _scaleFacade;

		public ToneGridEditPresenter(IScaleRepository scaleRepository, ScaleFacade scaleFacade)
		{
			_scaleRepository = scaleRepository ?? throw new ArgumentNullException(nameof(scaleRepository));
			_scaleFacade = scaleFacade ?? throw new ArgumentNullException(nameof(scaleFacade));
		}

		protected override Scale GetEntity(ToneGridEditViewModel userInput) => _scaleRepository.Get(userInput.ScaleID);

		protected override ToneGridEditViewModel ToViewModel(Scale entity) => entity.ToToneGridEditViewModel();

		protected override IResult SaveWithUserInput(Scale scale, ToneGridEditViewModel userInput)
		{
			// ViewModel Validator
			IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
			if (!viewModelValidator.IsValid)
			{
				return viewModelValidator.ToResult();
			}

			// Business
			VoidResult result = _scaleFacade.Save(scale);
			return result;
		}

		public ToneGridEditViewModel CreateTone(ToneGridEditViewModel userInput)
		{
			Tone tone = null;

			return ExecuteAction(
				userInput,
				scale =>
				{
					// ViewModelValidator
					IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
					if (!viewModelValidator.IsValid)
					{
						return viewModelValidator.ToResult();
					}

					// Business
					tone = _scaleFacade.CreateTone(scale);
					return ResultHelper.Successful;
				},
				viewModel =>
				{
					// ToVieWModel
					ToneViewModel toneViewModel = tone.ToViewModel();
					viewModel.Tones.Add(toneViewModel);
					// Do not sort grid, so that the new item appears at the bottom.
					viewModel.CreatedToneID = tone.ID;
				});
		}

		public ToneGridEditViewModel DeleteTone(ToneGridEditViewModel userInput, int toneID)
		{
			return ExecuteAction(
				userInput,
				scale =>
				{
					// ViewModelValidator
					IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
					if (!viewModelValidator.IsValid)
					{
						return viewModelValidator.ToResult();
					}

					// Business
					_scaleFacade.DeleteTone(toneID);
					return ResultHelper.Successful;
				});
		}

		// Refreshing upon edit is required to update the Frequency values.
		public ToneGridEditViewModel Edit(ToneGridEditViewModel userInput) => Refresh(userInput);
	}
}
