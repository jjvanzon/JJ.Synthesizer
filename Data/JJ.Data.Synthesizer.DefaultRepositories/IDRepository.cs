using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Data;
using System;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class IDRepository : IIDRepository
    {
        // Enforce the constructor, but do not use a field.
        public IDRepository(IContext context)
        { }

        public virtual int GetID()
        {
            throw new NotSupportedException("GetID can only be executed using a specialized repository.");
        }
    }
}
