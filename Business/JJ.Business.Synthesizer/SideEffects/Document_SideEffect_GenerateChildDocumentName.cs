using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Document_SideEffect_GenerateChildDocumentName : ISideEffect
    {
        private Document _childDocument;

        public Document_SideEffect_GenerateChildDocumentName(Document childDocument)
        {
            if (childDocument == null) throw new NullException(() => childDocument);
            _childDocument = childDocument;
        }

        public void Execute()
        {
            if (_childDocument.ParentDocument == null) throw new NullException(() => _childDocument.ParentDocument);

            Document parentDocument = _childDocument.ParentDocument;

            int number = 1;
            string suggestedName;
            bool nameExists;

            ChildDocumentTypeEnum childDocumentTypeEnum = _childDocument.GetChildDocumentTypeEnum();
            string childDocumentTypeName = _childDocument.ChildDocumentType.Name;
            string childDocumentTypeDisplayName = ResourceHelper.GetPropertyDisplayName(childDocumentTypeName);

            do
            {
                suggestedName = String.Format("{0} {1}", childDocumentTypeDisplayName, number++);
                nameExists = parentDocument.ChildDocuments.Where(x => x.GetChildDocumentTypeEnum() == childDocumentTypeEnum &&
                                                                      String.Equals(x.Name, suggestedName))
                                                          .Any();
            }
            while (nameExists);

            _childDocument.Name = suggestedName;
        }
    }
}
