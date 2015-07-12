using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
