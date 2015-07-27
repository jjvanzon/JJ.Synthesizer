using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class DocumentValidator_Basic : FluentValidator<Document>
    {
        public DocumentValidator_Basic(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Document document = Object;

            string messagePrefix = PropertyDisplayNames.Document + ": ";

            Execute(new NameValidator(document.Name), messagePrefix);

            if ((document.ParentDocument == null) != (document.ChildDocumentType == null))
            {
                ValidationMessages.Add(PropertyNames.ChildDocument, Messages.ParentDocumentAndChildDocumentTypeShouldBothBeNullOrBothFilledIn);
            }

            if (document.ChildDocumentType != null)
            {
                For(() => document.ChildDocumentType.ID, PropertyDisplayNames.ChildDocumentType)
                    .IsEnumValue<ChildDocumentTypeEnum>();
            }
        }
    }
}
