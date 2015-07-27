using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class DocumentExtensions
    {
        public static IEnumerable<Document> EnumerateSelfAndChildDocuments(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            yield return document;

            foreach (Document childDocument in document.ChildDocuments)
            {
                yield return childDocument;
            }
        }
    }
}
