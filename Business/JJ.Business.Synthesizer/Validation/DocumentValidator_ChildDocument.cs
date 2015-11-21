using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentValidator_ChildDocument : FluentValidator<Document>
    {
        public DocumentValidator_ChildDocument(Document obj)
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

            // Child Document should have exactly one patch, that is the same as its MainPatch.
            For(() => document.MainPatch, PropertyDisplayNames.MainPatch).NotNull();
            For(() => document.Patches.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Patches)).Is(1);

            if (document.Patches.Count == 1)
            {
                if (document.MainPatch != document.Patches[0])
                {
                    ValidationMessages.Add(PropertyNames.MainPatch, Messages.MainPatchShouldBeEqualToTheSinglePatchInThePatchCollection);
                }
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
