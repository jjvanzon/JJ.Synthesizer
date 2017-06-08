//using System;
//using JetBrains.Annotations;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Data.Synthesizer.RepositoryInterfaces;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Presentation.Resources;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.Documents
//{
//    internal class DocumentValidator_UniqueName : VersatileValidator<Document>
//    {
//        private readonly IDocumentRepository _documentRepository;

//        /// <summary>
//        /// NOTE:
//        /// Do not always execute this validator everywhere,
//        /// because then validating a document becomes inefficient.
//        /// Extensive document validation will include validating that the Document names are unique already
//        /// and it will do so in a more efficient way.
//        /// </summary>
//        public DocumentValidator_UniqueName(Document obj, [NotNull] IDocumentRepository documentRepository)
//            : base(obj, postponeExecute: true)
//        {
//            _documentRepository = documentRepository ?? throw new NullException(() => documentRepository);

//            Execute();
//        }

//        protected sealed override void Execute()
//        {
//            bool isUnique = ValidationHelper.DocumentNameIsUnique(Obj, _documentRepository);
//            // ReSharper disable once InvertIf
//            if (!isUnique)
//            {
//                ValidationMessages.AddNotUniqueMessageSingular(nameof(Obj.Name), CommonResourceFormatter.Name, Obj.Name);
//            }
//        }
//    }
//}
