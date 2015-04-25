using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class InterpolationTypeRepository : RepositoryBase<InterpolationType, int>, IInterpolationTypeRepository
    {
        public InterpolationTypeRepository(IContext context)
            : base(context)
        { }
    }
}
