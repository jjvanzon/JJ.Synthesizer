//using System;
//using System.Linq;
//using JetBrains.Annotations;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Data.Synthesizer.RepositoryInterfaces;
//using JJ.Framework.Collections;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer
//{
//    public class SystemDocumentManager
//    {
//        public const string SYSTEM_DOCUMENT_NAME = "System";

//        private readonly IDocumentRepository _documentRepository;

//        public SystemDocumentManager(IDocumentRepository documentRepository)
//        {
//            _documentRepository = documentRepository;
//        }

//        public bool IsSystemDocument([NotNull] Document document)
//        {
//            if (document == null) throw new NullException(() => document);

//            bool isSystemDocument = string.Equals(document.Name, SYSTEM_DOCUMENT_NAME);
//            return isSystemDocument;
//        }

//        public Document GetSystemDocument()
//        {
//            // TODO: System document should be cached.
//            Document document = _documentRepository.GetByNameComplete(SYSTEM_DOCUMENT_NAME);
//            return document;
//        }

//        public Patch GetPatch(OperatorTypeEnum operatorTypeEnum)
//        {
//            Document document = GetSystemDocument();
//            string patchName = operatorTypeEnum.ToString();
//            Patch patch = document.Patches.Where(x => string.Equals(x.Name, patchName)).SingleWithClearException(new { name = patchName });

//            return patch;

//        }
//    }
//}
