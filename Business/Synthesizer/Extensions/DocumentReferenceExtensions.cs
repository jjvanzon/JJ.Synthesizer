using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

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
    }
}
