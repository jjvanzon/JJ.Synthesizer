using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class SampleRepository : RepositoryBase<Sample, int>, ISampleRepository
    {
        public SampleRepository(IContext context)
            : base(context)
        { }

        public virtual void SetBinary(int id, byte[] bytes)
        {
            throw new NotSupportedException("Binary can only be accessed using a specialized repository.");
        }

        public virtual byte[] GetBinary(int id)
        {
            throw new NotSupportedException("Binary can only be accessed using a specialized repository.");
        }

        public virtual IList<Sample> GetManyByDocumentID(int documentID)
        {
            IList<Sample> entities = _context.Query<Sample>()
                                             .Where(x => x.Document.ID == documentID)
                                             .ToArray();
            return entities;
        }
    }
}
