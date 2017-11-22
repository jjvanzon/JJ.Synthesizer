using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
	public interface IOperatorRepository : IRepository<Operator, int>
	{
		IList<Operator> GetAll();
		IList<Operator> GetManyByUnderlyingPatchID(int underlyingPatchID);
	}
}
