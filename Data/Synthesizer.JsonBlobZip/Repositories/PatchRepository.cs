using System;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.JsonBlobZip.Repositories
{
    [UsedImplicitly]
    public class PatchRepository : DefaultRepositories.PatchRepository
    {
        private new readonly SynthesizerJsonBlobZipContext _context;

        public PatchRepository(IContext context) : base(context) => _context = (SynthesizerJsonBlobZipContext)context;

        public override Patch TryGetByName(string name)
        {
            Patch entity = _context.RootObject
                                   .Patches
                                   .SingleOrDefaultWithClearException(
                                       x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase),
                                       () => name);
            return entity;
        }
    }
}