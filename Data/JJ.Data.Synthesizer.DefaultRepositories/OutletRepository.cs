using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OutletRepository : RepositoryBase<Outlet, int>, IOutletRepository
    {
        public OutletRepository(IContext context)
            : base(context)
        { }
    }
}
