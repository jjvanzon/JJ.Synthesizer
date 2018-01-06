using System;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
	public class MidiMappingFacade
	{
		private readonly IMidiMappingRepository _midiMappingRepository;
		private readonly IIDRepository _idRepository;
		private readonly IDimensionRepository _dimensionRepository;
		private readonly IMidiMappingElementRepository _midiMappingElementRepository;

		public MidiMappingFacade(
			IMidiMappingRepository midiMappingRepository,
			IMidiMappingElementRepository midiMappingElementRepository,
			IDimensionRepository dimensionRepository,
			IIDRepository idRepository)
		{
			_midiMappingRepository = midiMappingRepository ?? throw new ArgumentNullException(nameof(midiMappingRepository));
			_idRepository = idRepository ?? throw new ArgumentNullException(nameof(idRepository));
			_dimensionRepository = dimensionRepository ?? throw new ArgumentNullException(nameof(dimensionRepository));
			_midiMappingElementRepository = midiMappingElementRepository ?? throw new ArgumentNullException(nameof(midiMappingElementRepository));
		}

		public MidiMapping CreateMidiMappingWithDefaults(Document document)
		{
			if (document == null) throw new ArgumentNullException(nameof(document));

			var entity = new MidiMapping { ID = _idRepository.GetID() };
			entity.LinkTo(document);
			_midiMappingRepository.Insert(entity);

			new MidiMapping_SideEffect_GenerateName(entity).Execute();
			new MidiMapping_SideEffect_AutoCreate_MidiMapingElement(entity, this).Execute();

			return entity;
		}

		public MidiMappingElement CreateMidiMappingElementWithDefaults(MidiMapping midiMapping)
		{
			if (midiMapping == null) throw new ArgumentNullException(nameof(midiMapping));

			var entity = new MidiMappingElement { ID = _idRepository.GetID() };
			_midiMappingElementRepository.Insert(entity);

			entity.LinkTo(midiMapping);

			new MidiMappingElement_SideEffect_SetDefaults(entity, _dimensionRepository).Execute();

			return entity;
		}

		public VoidResult SaveMidiMapping(MidiMapping entity)
		{
			IValidator validator = new MidiMappingValidator_WithRelatedEntities(entity);

			return validator.ToResult();
		}

		public void DeleteMidiMapping(int id)
		{
			MidiMapping entity = _midiMappingRepository.Get(id);
			DeleteMidiMapping(entity);
		}

		public void DeleteMidiMapping(MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			entity.DeleteRelatedEntities(_midiMappingElementRepository);
			entity.UnlinkRelatedEntities();

			_midiMappingRepository.Delete(entity);
		}

		public void DeleteMidiMappingElement(int id)
		{
			MidiMappingElement entity = _midiMappingElementRepository.Get(id);
			DeleteMidiMappingElement(entity);
		}

		public void DeleteMidiMappingElement(MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			entity.UnlinkRelatedEntities();

			_midiMappingElementRepository.Delete(entity);
		}
	}
}