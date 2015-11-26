using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;
using JJ.Framework.Common;

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

            HashSet<string> existingNames = _childDocument.ParentDocument.EnumerateSelfAndParentAndTheirChildren()
                                                                         .SelectMany(x => x.ChildDocuments)
                                                                         .Select(x => x.Name)
                                                                         .ToHashSet();
            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.Patch, number++);
                nameExists = existingNames.Contains(suggestedName);
            }
            while (nameExists);

            _childDocument.Name = suggestedName;
        }
    }
}
