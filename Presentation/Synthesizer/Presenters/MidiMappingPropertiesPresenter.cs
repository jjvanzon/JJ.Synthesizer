using System;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class MidiMappingPropertiesPresenter
		: EntityPresenterWithSaveBase<MidiMapping, MidiMappingPropertiesViewModel>
	{
		private readonly MidiMappingRepositories _repositories;
		private readonly MidiMappingFacade _midiMappingFacade;

		public MidiMappingPropertiesPresenter(MidiMappingRepositories repositories, MidiMappingFacade midiMappingFacade)
		{
			_repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
			_midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
		}

		protected override MidiMapping GetEntity(MidiMappingPropertiesViewModel userInput)
		{
			return userInput.ToEntity(_repositories);
		}

		protected override MidiMappingPropertiesViewModel ToViewModel(MidiMapping entity)
		{
			return entity.ToPropertiesViewModel();
		}

		protected override IResult Save(MidiMapping entity, MidiMappingPropertiesViewModel userInput)
		{
			IValidator validator = new MidiMappingPropertiesViewModel_Validator(userInput);
			if (!validator.IsValid)
			{
				return validator.ToResult();
			}

			return _midiMappingFacade.SaveMidiMapping(entity);
		}

		public MidiMappingPropertiesViewModel Delete(MidiMappingPropertiesViewModel userInput)
		{
			return ExecuteAction(userInput, entity => _midiMappingFacade.DeleteMidiMapping(entity));
		}

		public override void CopyNonPersistedProperties(MidiMappingPropertiesViewModel sourceViewModel, MidiMappingPropertiesViewModel destViewModel)
		{
			base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

			destViewModel.FromDimensionValue = sourceViewModel.FromDimensionValue;
			destViewModel.TillDimensionValue = sourceViewModel.TillDimensionValue;
			destViewModel.MinDimensionValue = sourceViewModel.MinDimensionValue;
			destViewModel.MaxDimensionValue = sourceViewModel.MaxDimensionValue;
			destViewModel.FromPosition = sourceViewModel.FromPosition;
			destViewModel.TillPosition = sourceViewModel.TillPosition;
		}
	}
}