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
	internal class MidiMappingElementPropertiesPresenter
		: EntityPresenterWithSaveBase<MidiMappingElement, MidiMappingElementPropertiesViewModel>
	{
		private readonly MidiMappingRepositories _repositories;
		private readonly MidiMappingFacade _midiMappingFacade;

		public MidiMappingElementPropertiesPresenter(MidiMappingRepositories repositories, MidiMappingFacade midiMappingFacade)
		{
			_repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
			_midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
		}

		protected override MidiMappingElement GetEntity(MidiMappingElementPropertiesViewModel userInput)
		{
			return userInput.ToEntity(_repositories);
		}

		protected override MidiMappingElementPropertiesViewModel ToViewModel(MidiMappingElement entity)
		{
			return entity.ToPropertiesViewModel();
		}

		protected override IResult Save(MidiMappingElement entity, MidiMappingElementPropertiesViewModel userInput)
		{
			IValidator validator = new MidiMappingElementPropertiesViewModel_Validator(userInput);
			if (!validator.IsValid)
			{
				return validator.ToResult();
			}

			return _midiMappingFacade.SaveMidiMappingElement(entity);
		}

		public MidiMappingElementPropertiesViewModel Delete(MidiMappingElementPropertiesViewModel userInput)
		{
			return ExecuteAction(userInput, entity => _midiMappingFacade.DeleteMidiMappingElement(entity));
		}

		public override void CopyNonPersistedProperties(MidiMappingElementPropertiesViewModel sourceViewModel, MidiMappingElementPropertiesViewModel destViewModel)
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