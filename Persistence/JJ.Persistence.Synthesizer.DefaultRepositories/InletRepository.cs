using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    public class InletRepository : RepositoryBase<Inlet>, IInletRepository
    {
        public InletRepository(IContext context)
            : base(context)
        { }
    }
}
