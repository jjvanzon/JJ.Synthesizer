using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class DocumentReferenceExtensions
    {
        public static string GetAliasOrName(this DocumentReference lowerDocumentReference)
        {
            if (lowerDocumentReference == null) throw new NullException(() => lowerDocumentReference);

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!string.IsNullOrWhiteSpace(lowerDocumentReference.Alias))
            {
                return lowerDocumentReference.Alias;
            }

            return lowerDocumentReference.LowerDocument?.Name;
        }

        public static IList<string> GetFilledInAliases(this IEnumerable<DocumentReference> documentReferences)
        {
            if (documentReferences == null) throw new NullException(() => documentReferences);

            IList<string> aliases = documentReferences.Select(x => x.Alias).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            return aliases;
        }
    }
}
