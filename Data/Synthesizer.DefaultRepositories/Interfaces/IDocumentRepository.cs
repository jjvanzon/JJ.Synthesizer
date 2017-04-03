using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IDocumentRepository : IRepository<Document, int>
    {
        IList<Document> OrderByName();

        /// <summary>
        /// Gets the Document entity including all its descendant entities in one blow,
        /// so not through lazy loading, so in a more efficient manner.
        /// </summary>
        Document TryGetComplete(int id);

        /// <summary>
        /// Gets the Document entity including all its descendant entities in one blow,
        /// so not through lazy loading, so in a more efficient manner.
        /// </summary>
        Document GetComplete(int id);
    }
}
