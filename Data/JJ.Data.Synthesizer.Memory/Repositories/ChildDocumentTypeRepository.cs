using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class ChildDocumentTypeRepository : JJ.Data.Synthesizer.DefaultRepositories.ChildDocumentTypeRepository
    {
        public ChildDocumentTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Instrument");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Effect");
       }
    }
}