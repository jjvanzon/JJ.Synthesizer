//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Business;
//using JJ.Framework.Reflection.Exceptions;
//using System;
//using System.Linq;
//using System.Collections.Generic;

//namespace JJ.Business.Synthesizer.SideEffects
//{
//    internal class Document_SideEffect_GenerateChildDocumentName : ISideEffect
//    {
//        private readonly Document _childDocument;

//        public Document_SideEffect_GenerateChildDocumentName(Document childDocument)
//        {
//            if (childDocument == null) throw new NullException(() => childDocument);
//            if (childDocument.ParentDocument == null) throw new NullException(() => childDocument.ParentDocument);

//            _childDocument = childDocument;
//        }

//        public void Execute()
//        {
//            IEnumerable<string> existingNames = _childDocument.ParentDocument.EnumerateSelfAndParentAndTheirChildren()
//                                                                             .SelectMany(x => x.ChildDocuments)
//                                                                             .Select(x => x.Name);

//            // Type argument 'Patch' is not a mistake. Names should be like 'Patch 1'.
//            _childDocument.Name = SideEffectHelper.GenerateName<Patch>(existingNames);
//        }
//    }
//}
