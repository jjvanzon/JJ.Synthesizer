using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class InletRepository : RepositoryBase<Inlet, int>, IInletRepository
	{
		public InletRepository(IContext context)
			: base(context)
		{ }
	}
}
