﻿//using JJ.Data.Synthesizer;
//using JJ.Framework.Validation;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Presentation.Resources;

//namespace JJ.Business.Synthesizer.Validation.Documents
//{
//    internal class ChildDocument_DocumentValidator : FluentValidator<Document>
//    {
//        public ChildDocument_DocumentValidator(Document obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            Document document = Object;

//            For(() => document.ParentDocument, PropertyDisplayNames.ParentDocument).NotNull();
//            For(() => document.ChildDocuments.Count, PropertyDisplayNames.ChildDocumentCount).Is(0);

//            // TODO: Message prefix, or it will say 'Name is too long'.
//            ExecuteValidator(new NameValidator(Object.GroupName, required: false));

//            // AudioOutput must be null for child documents.
//            For(() => document.AudioOutput, PropertyDisplayNames.AudioOutput).IsNull();

//            // Child Document should have exactly one patch.
//            For(() => document.Patches.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Patches)).Is(1);

//            // Many entities' names are only required if part of a document,
//            // and not required when using Synthesizer as an API,
//            // but Child Document is an exception and should have a name.

//            // Names are required in document, but not when using Synthesizer as an API.
//            // However, child documents are always part of a document.
//            // When using it as an API it would just be loose document.
//            ExecuteValidator(new NameValidator(document.Name));
//        }
//    }
//}
