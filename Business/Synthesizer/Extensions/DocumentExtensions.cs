using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class DocumentExtensions
    {
        public static IList<Patch> GetPatchesAndLowerDocumentPatches(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<Patch> patches = document.LowerDocumentReferences
                                           .SelectMany(x => x.LowerDocument.Patches)
                                           .Union(document.Patches)
                                           .ToArray();
            return patches;
        }

        public static IList<Patch> GetPatchesAndHigherDocumentPatches(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<Patch> patches = document.HigherDocumentReferences
                                           .SelectMany(x => x.HigherDocument.Patches)
                                           .Union(document.Patches)
                                           .ToArray();
            return patches;
        }
    }
}
