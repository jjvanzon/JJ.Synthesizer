/*
using JJ.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class DocumentRepository : DefaultRepositories.DocumentRepository
    {
        public DocumentRepository(IContext context)
            : base(context)
        { }

        // TODO: Remove outcommented code.
        public override int CountRootDocuments()
        {
            return GetAll().Where(x => x.ParentDocument == null).Count();
        }

        public override IList<Document> GetPageOfRootDocuments(int firstIndex, int count)
        {
            return GetAll().Where(x => x.ParentDocument == null).Skip(firstIndex).Take(count).ToArray();
        }
    }
}
*/