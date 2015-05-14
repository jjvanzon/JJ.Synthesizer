using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IDocumentRepository : IRepository<Document, int>
    {
        IList<Document> GetPageOfRootDocuments(int pageIndex, int pageSize);
        int CountRootDocuments();

        IList<Document> GetInstruments(int documentID);
        IList<Document> GetEffects(int documentID);
    }
}
