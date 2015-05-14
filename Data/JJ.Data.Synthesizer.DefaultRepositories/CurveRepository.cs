using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class CurveRepository : RepositoryBase<Curve, int>, ICurveRepository
    {
        public CurveRepository(IContext context)
            : base(context)
        { }

        public virtual IList<Curve> GetManyByDocumentID(int documentID)
        {
            return _context.Query<Curve>()
                           .Where(x => x.Document.ID == documentID)
                           .ToArray();
        }
    }
}
