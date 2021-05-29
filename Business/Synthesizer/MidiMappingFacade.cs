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

        public MidiMappingFacade(MidiMappingRepositories repositories) => _repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));

        public MidiMappingGroup CreateMidiMappingGroupWithDefaults(Document document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            var entity = new MidiMappingGroup { ID = _repositories.IDRepository.GetID() };
            entity.LinkTo(document);
            _repositories.MidiMappingGroupRepository.Insert(entity);

            new MidiMappingGroup_SideEffect_GenerateName(entity).Execute();
            new MidiMappingGroup_SideEffect_AutoCreate_MidiMapping(entity, this).Execute();

            return entity;
        }

        public MidiMapping CreateMidiMappingWithDefaults(MidiMappingGroup midiMappingGroup)
        {
            if (midiMappingGroup == null) throw new ArgumentNullException(nameof(midiMappingGroup));

            var entity = new MidiMapping { ID = _repositories.IDRepository.GetID() };
            _repositories.MidiMappingRepository.Insert(entity);

            entity.LinkTo(midiMappingGroup);

            new MidiMapping_SideEffect_AutoCreateEntityPosition(entity, _repositories.EntityPositionRepository, _repositories.IDRepository).Execute();
            new MidiMapping_SideEffect_SetDefaults(entity, _repositories.DimensionRepository, _repositories.MidiMappingTypeRepository).Execute();

            return entity;
        }

        public VoidResult SaveMidiMappingGroup(MidiMappingGroup entity)
        {
            IValidator validator = new MidiMappingGroupValidator_WithRelatedEntities(entity);

            return validator.ToResult();
        }

        public VoidResult SaveMidiMapping(MidiMapping entity)
        {
            IValidator validator = new MidiMappingValidator(entity);

            return validator.ToResult();
        }

        public void DeleteMidiMappingGroup(int id)
        {
            MidiMappingGroup entity = _repositories.MidiMappingGroupRepository.Get(id);
            DeleteMidiMappingGroup(entity);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void DeleteMidiMappingGroup(MidiMappingGroup entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.DeleteRelatedEntities(_repositories.MidiMappingRepository, _repositories.EntityPositionRepository);
            entity.UnlinkRelatedEntities();

            _repositories.MidiMappingGroupRepository.Delete(entity);
        }

        public void DeleteMidiMapping(int id)
        {
            MidiMapping entity = _repositories.MidiMappingRepository.Get(id);
            DeleteMidiMapping(entity);
        }

        public void DeleteMidiMapping(MidiMapping entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.UnlinkRelatedEntities();

            _repositories.MidiMappingRepository.Delete(entity);

            // Order-Dependence:
            // You need to postpone deleting this 1-to-1 related entity till after deleting the MidiMapping, 
            // or ORM will try to update MidiMapping.EntityPositionID to null and crash.
            if (entity.EntityPosition != null)
            {
                _repositories.EntityPositionRepository.Delete(entity.EntityPosition);
            }
        }
    }
}