using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
// ReSharper disable InvertIf

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Document_SideEffect_CreatePatch : ISideEffect
	{
		private readonly Document _entity;
		private readonly RepositoryWrapper _repositories;

		public Document_SideEffect_CreatePatch(Document entity, RepositoryWrapper repositories)
		{
			_entity = entity ?? throw new NullException(() => entity);
			_repositories = repositories ?? throw new NullException(() => repositories);
		}

		public void Execute()
		{
			bool mustCreatePatch = _entity.Patches.Count == 0;
			if (mustCreatePatch)
			{
				var patchManager = new PatchManager(_repositories);
				patchManager.CreatePatch(_entity);
			}
		}
	}
}
