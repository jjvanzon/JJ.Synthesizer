using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class MidiMappingGroup_SideEffect_GenerateName : ISideEffect
    {
        private readonly MidiMappingGroup _entity;

        public MidiMappingGroup_SideEffect_GenerateName(MidiMappingGroup entity) => _entity = entity ?? throw new NullException(() => entity);

        public void Execute()
        {
            bool mustExecute = _entity.Document != null;
            if (mustExecute)
            {
                IEnumerable<string> existingNames = _entity.Document.MidiMappingGroups.Select(x => x.Name);

                _entity.Name = SideEffectHelper.GenerateName<MidiMappingGroup>(existingNames);
            }
        }
    }
}