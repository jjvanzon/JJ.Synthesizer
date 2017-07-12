using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_DoesNotReferenceItself : VersatileValidator
    {
        public DocumentValidator_DoesNotReferenceItself([NotNull] Document document) 
        {
            if (document == null) throw new NullException(() => document);

            bool referencesItself = document.LowerDocumentReferences.Any(x => x.LowerDocument?.ID == document.ID);
            if (referencesItself)
            {
                Messages.Add(ResourceFormatter.DocumentCannotReferenceItself);
            }
        }
    }
}
