using System;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
	public class MidiMappingFacade
	{
		private readonly MidiMappingRepositories _repositories;

		public MidiMappingFacade(MidiMappingRepositories repositories)
		{
			_repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
		}

		public MidiMapping CreateMidiMappingWithDefaults(Document document)
		{
			if (document == null) throw new ArgumentNullException(nameof(document));

			var entity = new MidiMapping { ID = _repositories.IDRepository.GetID() };
			entity.LinkTo(document);
			_repositories.MidiMappingRepository.Insert(entity);

			new MidiMapping_SideEffect_GenerateName(entity).Execute();
			new MidiMapping_SideEffect_AutoCreate_MidiMapingElement(entity, this).Execute();

			return entity;
		}

		public MidiMappingElement CreateMidiMappingElementWithDefaults(MidiMapping midiMapping)
		{
			if (midiMapping == null) throw new ArgumentNullException(nameof(midiMapping));

			var entity = new MidiMappingElement { ID = _repositories.IDRepository.GetID() };
			_repositories.MidiMappingElementRepository.Insert(entity);

			entity.LinkTo(midiMapping);

			new MidiMappingElement_SideEffect_SetDefaults(entity, _repositories.DimensionRepository).Execute();

			return entity;
		}

		public VoidResult SaveMidiMapping(MidiMapping entity)
		{
			IValidator validator = new MidiMappingValidator_WithRelatedEntities(entity);

			return validator.ToResult();
		}

		public VoidResult SaveMidiMappingElement(MidiMappingElement entity)
		{
			IValidator validator = new MidiMappingElementValidator(entity);

			return validator.ToResult();
		}

		public void DeleteMidiMapping(int id)
		{
			MidiMapping entity = _repositories.MidiMappingRepository.Get(id);
			DeleteMidiMapping(entity);
		}

		public void DeleteMidiMapping(MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			entity.DeleteRelatedEntities(_repositories.MidiMappingElementRepository);
			entity.UnlinkRelatedEntities();

			_repositories.MidiMappingRepository.Delete(entity);
		}

		public void DeleteMidiMappingElement(int id)
		{
			MidiMappingElement entity = _repositories.MidiMappingElementRepository.Get(id);
			DeleteMidiMappingElement(entity);
		}

		public void DeleteMidiMappingElement(MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			entity.UnlinkRelatedEntities();

			_repositories.MidiMappingElementRepository.Delete(entity);
		}
	}
}