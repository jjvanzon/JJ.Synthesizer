using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentValidator_Delete : FluentValidator<Document>
    {
        public DocumentValidator_Delete(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Document document = Object;

            foreach (DocumentReference dependentDocument in document.DependentDocuments)
            {
                string message = MessageFormatter.DocumentIsDependentOnDocument(dependentDocument.DependentDocument.Name, dependentDocument.DependentOnDocument.Name);

                ValidationMessages.Add(PropertyNames.DocumentReference, message);
            }
        }
    }
}
