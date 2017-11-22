using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
	public interface ICurveRepository : IRepository<Curve, int>
	{
		IList<Curve> GetAll();
	}
}
