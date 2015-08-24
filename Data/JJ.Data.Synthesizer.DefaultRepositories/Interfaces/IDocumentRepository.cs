using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IDocumentRepository : IRepository<Document, int>
    {
        IList<Document> GetPageOfRootDocumentsOrderedByName(int firstIndex, int pageSize);
        int CountRootDocuments();
    }
}
