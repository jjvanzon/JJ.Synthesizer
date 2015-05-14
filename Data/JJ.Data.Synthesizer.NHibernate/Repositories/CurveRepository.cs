using JJ.Data.Synthesizer.NHibernate.Helpers;
using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Framework.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class CurveRepository : JJ.Data.Synthesizer.DefaultRepositories.CurveRepository
    {
        private new NHibernateContext _context;

        public CurveRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override IList<Curve> GetManyByDocumentID(int documentID)
        {
            IList<Curve> entities = _context.Session.QueryOver<Curve>()
                                                    .Where(x => x.Document.ID == documentID)
                                                    .List();
            return entities;
        }
    }
}
