using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ChildDocumentValidator : FluentValidator<Document>
    {
        public ChildDocumentValidator(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Document document = Object;

            For(() => document.ParentDocument, PropertyDisplayNames.ParentDocument).NotNull();
            For(() => document.ChildDocuments.Count, PropertyDisplayNames.ChildDocumentCount).Is(0);

            For(() => document.ChildDocumentType, PropertyDisplayNames.ChildDocumentType).NotNull();

            if (document.ChildDocumentType != null)
            {
                For(() => document.GetChildDocumentTypeEnum(), PropertyDisplayNames.ChildDocumentType)
                    .IsEnum<ChildDocumentTypeEnum>();
            }

            // Many entities' names are only required if part of a document,
            // and not required when using Synthesizer as an API,
            // but Child Document is an exception and should have a name.

            // Names are required in document, but not when using Synthesizer as an API.
            // However, child documents are always part of a document.
            // When using it as an API it would just be loose document.
            Execute(new NameValidator(document.Name));
        }
    }
}
