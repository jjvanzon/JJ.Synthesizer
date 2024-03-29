﻿using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_UniqueAlias : VersatileValidator
    {
        public DocumentReferenceValidator_UniqueAlias(DocumentReference obj)
        {
            if (obj == null) throw new NullException(() => obj);

            bool isUnique = ValidationHelper.DocumentReferenceAliasIsUnique(obj);

            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                Messages.AddNotUniqueMessageSingular(ResourceFormatter.Alias, obj.Alias);
            }
        }
    }
}
