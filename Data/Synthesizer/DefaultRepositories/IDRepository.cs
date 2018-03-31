using System;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;
// ReSharper disable MemberCanBeProtected.Global

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class IDRepository : IIDRepository
	{
		// Enforce the constructor, but do not use a field.
		// ReSharper disable once UnusedParameter.Local
		public IDRepository(IContext context)
		{ }

		public virtual int GetID() => throw new NotSupportedException("GetID can only be executed using a specialized repository.");
	}
}
