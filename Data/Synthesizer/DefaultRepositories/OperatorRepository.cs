using JJ.Framework.Data;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class OperatorRepository : RepositoryBase<Operator, int>, IOperatorRepository
	{
		// ReSharper disable once MemberCanBeProtected.Global
		public OperatorRepository(IContext context)
			: base(context)
		{ }

		public virtual IList<Operator> GetAll() => _context.Query<Operator>().ToArray();

		public virtual IList<Operator> GetManyByUnderlyingPatchID(int underlyingPatchID) => _context.Query<Operator>()
		                                                                                            .Where(x => x.UnderlyingPatch.ID == underlyingPatchID)
		                                                                                            .ToArray();
	}
}
