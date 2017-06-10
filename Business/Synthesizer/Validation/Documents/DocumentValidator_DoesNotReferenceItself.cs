using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_DoesNotReferenceItself : VersatileValidator<Document>
    {
        public DocumentValidator_DoesNotReferenceItself([NotNull] Document document) 
            : base(document)
        { 
            bool referencesItself = document.LowerDocumentReferences.Any(x => x.LowerDocument?.ID == document.ID);
            if (referencesItself)
            {
                ValidationMessages.Add(nameof(DocumentReference), ResourceFormatter.DocumentCannotReferenceItself);
            }
        }
    }
}
