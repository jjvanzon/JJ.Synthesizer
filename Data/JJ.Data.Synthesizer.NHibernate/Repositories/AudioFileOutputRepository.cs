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
    public class AudioFileOutputRepository : JJ.Data.Synthesizer.DefaultRepositories.AudioFileOutputRepository
    {
        private new NHibernateContext _context;

        public AudioFileOutputRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override IList<AudioFileOutput> GetManyByDocumentID(int documentID)
        {
            return _context.Session.QueryOver<AudioFileOutput>()
                                   .Where(x => x.Document.ID == documentID)
                                   .List();
        }
    }
}
