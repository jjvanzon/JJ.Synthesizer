using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class MidiMapping_SideEffect_GenerateName : ISideEffect
	{
		private readonly MidiMapping _entity;

		public MidiMapping_SideEffect_GenerateName(MidiMapping entity)
		{
			_entity = entity ?? throw new NullException(() => entity);
		}

		public void Execute()
		{
			bool mustExecute = _entity.Document != null;
			if (mustExecute)
			{
				IEnumerable<string> existingNames = _entity.Document.MidiMappings.Select(x => x.Name);

				_entity.Name = SideEffectHelper.GenerateName<MidiMapping>(existingNames);
			}
		}
	}
}