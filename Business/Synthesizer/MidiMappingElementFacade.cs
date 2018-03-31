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
	public class MidiMappingElementFacade
	{
		private readonly MidiMappingElementRepositories _repositories;

		public MidiMappingElementFacade(MidiMappingElementRepositories repositories)
		{
			_repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
		}

		public MidiMappingGroup CreateMidiMappingGroupWithDefaults(Document document)
		{
			if (document == null) throw new ArgumentNullException(nameof(document));

			var entity = new MidiMappingGroup { ID = _repositories.IDRepository.GetID() };
			entity.LinkTo(document);
			_repositories.MidiMappingGroupRepository.Insert(entity);

			new MidiMappingGroup_SideEffect_GenerateName(entity).Execute();
			new MidiMappingGroup_SideEffect_AutoCreate_MidiMapingElement(entity, this).Execute();

			return entity;
		}

		public MidiMappingElement CreateMidiMappingElementWithDefaults(MidiMappingGroup midiMappingGroup)
		{
			if (midiMappingGroup == null) throw new ArgumentNullException(nameof(midiMappingGroup));

			var entity = new MidiMappingElement { ID = _repositories.IDRepository.GetID() };
			_repositories.MidiMappingElementRepository.Insert(entity);

			entity.LinkTo(midiMappingGroup);

			new MidiMappingElement_SideEffect_AutoCreateEntityPosition(entity, _repositories.EntityPositionRepository, _repositories.IDRepository).Execute();
			new MidiMappingElement_SideEffect_SetDefaults(entity, _repositories.DimensionRepository).Execute();

			return entity;
		}

		public VoidResult SaveMidiMappingGroup(MidiMappingGroup entity)
		{
			IValidator validator = new MidiMappingGroupValidator_WithRelatedEntities(entity);

			return validator.ToResult();
		}

		public VoidResult SaveMidiMappingElement(MidiMappingElement entity)
		{
			IValidator validator = new MidiMappingElementValidator(entity);

			return validator.ToResult();
		}

		public void DeleteMidiMappingGroup(int id)
		{
			MidiMappingGroup entity = _repositories.MidiMappingGroupRepository.Get(id);
			DeleteMidiMappingGroup(entity);
		}

		public void DeleteMidiMappingGroup(MidiMappingGroup entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			entity.DeleteRelatedEntities(_repositories.MidiMappingElementRepository, _repositories.EntityPositionRepository);
			entity.UnlinkRelatedEntities();

			_repositories.MidiMappingGroupRepository.Delete(entity);
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

			// Order-Dependence:
			// You need to postpone deleting this 1-to-1 related entity till after deleting the MidiMappingElement, 
			// or ORM will try to update MidiMappingElement.EntityPositionID to null and crash.
			if (entity.EntityPosition != null)
			{
				_repositories.EntityPositionRepository.Delete(entity.EntityPosition);
			}
		}
	}
}