using System.Linq;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_DoesNotReferenceItself : VersatileValidator
    {
        public DocumentValidator_DoesNotReferenceItself(Document document) 
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
