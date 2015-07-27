using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Names;
using JJ.Framework.Validation.Resources;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    public class ChildDocumentValidator : FluentValidator<Document>
    {
        public ChildDocumentValidator(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Document document = Object;

            if (document.ParentDocument == null ||
                document.ParentDocument == null)
            {
                // TODO: I do not like the message this produces.
                // I would like a custom message that is more clear about what's going on.
                ValidationMessages.Add(PropertyNames.ParentDocument, ValidationMessageFormatter.IsNull(PropertyDisplayNames.ParentDocument));
            }

            Execute(new NameValidator(document.Name));
        }
    }
}
