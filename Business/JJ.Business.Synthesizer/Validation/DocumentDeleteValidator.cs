using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentDeleteValidator : FluentValidator<Document>
    {
        public DocumentDeleteValidator(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Document document = Object;

            if (document == null) throw new NullException(() => document);

            foreach (DocumentReference dependentDocument in document.DependentDocuments)
            {
                string message = MessageFormatter.DocumentIsDependentOnDocument(dependentDocument.DependentDocument.Name, dependentDocument.DependentOnDocument.Name);

                ValidationMessages.Add(PropertyNames.DocumentReference, message);
            }
        }
    }
}
