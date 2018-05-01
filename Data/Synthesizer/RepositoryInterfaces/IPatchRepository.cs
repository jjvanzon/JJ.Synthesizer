using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
	public interface IPatchRepository : IRepository<Patch, int>
	{
		Patch GetByName(string name);
		// ReSharper disable once UnusedMemberInSuper.Global
		Patch TryGetByName(string name);
	}
}
