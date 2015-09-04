using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
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

            Execute(new NameValidator(document.Name), ValidationHelper.GetMessagePrefix(document));

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
