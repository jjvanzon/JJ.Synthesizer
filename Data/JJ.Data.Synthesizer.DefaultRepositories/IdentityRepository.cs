using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class IdentityRepository : IIdentityRepository
    {
        // Enforce the constructor, but do not use a field.
        public IdentityRepository(IContext context)
        { }

        public virtual int GenerateID()
        {
            throw new NotSupportedException("GetID can only be executed using a specialized repository.");
        }
    }
}
