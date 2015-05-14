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
    public class PatchRepository : JJ.Data.Synthesizer.DefaultRepositories.PatchRepository
    {
        private new NHibernateContext _context;

        public PatchRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override IList<Patch> GetByDocumentID(int documentID)
        {
            IList<Patch> entities = _context.Session.QueryOver<Patch>()
                                                    .Where(x => x.Document.ID == documentID)
                                                    .List();
            return entities;
        }
    }
}
