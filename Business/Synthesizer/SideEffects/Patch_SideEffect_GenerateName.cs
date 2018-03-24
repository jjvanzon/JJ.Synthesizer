using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Patch_SideEffect_GenerateName : ISideEffect
	{
		private readonly Patch _entity;

		public Patch_SideEffect_GenerateName(Patch entity)
		{
			_entity = entity ?? throw new NullException(() => entity);
		}

		public void Execute()
		{
			bool mustExecute = _entity.Document != null;
			// ReSharper disable once InvertIf
			if (mustExecute)
			{
				IEnumerable<string> existingNames = _entity.Document.Patches.Select(x => x.Name);

				_entity.Name = SideEffectHelper.GenerateName<Patch>(existingNames);
			}
		}
	}
}