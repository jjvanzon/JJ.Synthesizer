using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
	public class CurveRepository : DefaultRepositories.CurveRepository
	{
		private new readonly NHibernateContext _context;

		public CurveRepository(IContext context) 
			: base(context)
		{
			_context = (NHibernateContext)context;
		}

		public override IList<Curve> GetAll()
		{
			return _context.Session.QueryOver<Curve>().List();
		}
	}
}
